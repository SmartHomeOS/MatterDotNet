// MatterDotNet Copyright (C) 2025 
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MatterDotNet.Clusters;
using MatterDotNet.Clusters.CHIP;
using MatterDotNet.Clusters.General;
using MatterDotNet.DCL;
using MatterDotNet.Messages.Certificates;
using MatterDotNet.OperationalDiscovery;
using MatterDotNet.PKI;
using MatterDotNet.Protocol.Connection;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Security;
using MatterDotNet.Util;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MatterDotNet.Entities
{
    public class Controller
    {
        Fabric fabric;
        Dictionary<ulong, Node> nodes = new Dictionary<ulong, Node>();

        /// <summary>
        /// Create a controller from an existing fabric
        /// </summary>
        /// <param name="fabric"></param>
        public Controller(Fabric fabric)
        {
            this.fabric = fabric;
        }
        
        /// <summary>
        /// Create a controller with a new fabric
        /// </summary>
        /// <param name="fabricName"></param>
        public Controller(string fabricName = "MatterDotNot") : this((ulong)Random.Shared.NextInt64(), fabricName) { }

        /// <summary>
        /// Create a controller with a new fabric
        /// </summary>
        /// <param name="fabricId"></param>
        /// <param name="fabricName"></param>
        public Controller(ulong fabricId, string fabricName = "MatterDotNot")
        {
            this.fabric = new Fabric(fabricName, fabricId, RandomNumberGenerator.GetBytes(16));
            this.fabric.CreateCommissioner();
        }

        /// <summary>
        /// Load a controller from a fabric and key file
        /// </summary>
        /// <param name="fabricPath"></param>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        public static Controller Load(string fabricPath, string keyPath)
        {
            return new Controller(Fabric.Import(fabricPath, keyPath));
        }

        /// <summary>
        /// Save the controller to a fabric and key file
        /// </summary>
        /// <param name="fabricPath"></param>
        /// <param name="keyPath"></param>
        public void Save(string fabricPath, string keyPath)
        {
            fabric.Export(fabricPath, keyPath);
        }

        /// <summary>
        /// Start the controller and enumerate all nodes
        /// </summary>
        /// <returns></returns>
        public async Task EnumerateFabric()
        {
            foreach (var noc in fabric.GetNodes())
            {
                Node? node = await Node.Enumerate(noc, fabric);
                if (node != null)
                    nodes.Add(noc.NodeID!.Value, node);
            }
        }

        /// <summary>
        /// Commission a node into the fabric using a commissioning payload
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="targetSSID"></param>
        /// <param name="verification"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="CryptographicException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<CommissioningState> StartCommissioning(CommissioningPayload payload, string? targetSSID = null, VerificationLevel verification = VerificationLevel.CertifiedDevicesOnly)
        {
            SessionContext? unsecureSession = null;
            SecureSession? paseSecureSession = null;

            // Discover the Node
            ODNode? commissionableNode = null;
            if (payload.Capabilities == CommissioningPayload.DiscoveryCapabilities.UNKNOWN)
            {
                commissionableNode = await BTDiscoveryService.Find(payload);
                if (commissionableNode == null)
                    commissionableNode = await IPDiscoveryService.Shared.Find(payload);
                
            }
            if ((payload.Capabilities | CommissioningPayload.DiscoveryCapabilities.BLE) != 0)
                commissionableNode = await BTDiscoveryService.Find(payload);
            if (commissionableNode == null && (payload.Capabilities | CommissioningPayload.DiscoveryCapabilities.IP) != 0)
                commissionableNode = await IPDiscoveryService.Shared.Find(payload);
            if (commissionableNode == null)
                return new CommissioningState();

            try
            {
                // Establish PASE session
                if (commissionableNode.IP6Address != null)
                    unsecureSession = SessionManager.GetUnsecureSession(new IPEndPoint(commissionableNode.IP6Address, commissionableNode.Port), true);
                else if (commissionableNode.IP4Address != null)
                    unsecureSession = SessionManager.GetUnsecureSession(new IPEndPoint(commissionableNode.IP4Address, commissionableNode.Port), true);
                else if (commissionableNode.BTAddress != null)
                    unsecureSession = SessionManager.GetUnsecureSession(new BLEEndPoint(commissionableNode.BTAddress), true);
                else
                    throw new NotSupportedException("Failed to discover the Node's connection info");
                PASE pase = new PASE(unsecureSession);
                paseSecureSession = await pase.EstablishSecureSession(payload.Passcode);
                if (paseSecureSession == null)
                    throw new IOException("PASE pairing failed");
                
                // Get Basic Commissioning Info
                GeneralCommissioning commissioning = new GeneralCommissioning(0);
                GeneralCommissioning.BasicCommissioningInfo basicInfo = await commissioning.GetBasicCommissioningInfo(paseSecureSession);
                ushort expiration = Math.Min(Math.Max((ushort)180, basicInfo.FailSafeExpiryLengthSeconds), basicInfo.MaxCumulativeFailsafeSeconds);
                
                // Arm Fail Safe
                GeneralCommissioning.ArmFailSafeResponse? failSafe = await commissioning.ArmFailSafe(paseSecureSession, expiration, 42);

                // Discover Root Clusters
                EndPoint root = new EndPoint(0);
                await root.EnumerateClusters(paseSecureSession);

                // Get Network Setup
                List<string> connected = new List<string>();
                FabricInterface SupportedComms = FabricInterface.None;
                if (root.HasCluster<NetworkCommissioning>())
                {
                    NetworkCommissioning.Feature features = await root.GetCluster<NetworkCommissioning>().GetSupportedFeatures(paseSecureSession);
                    if ((features & NetworkCommissioning.Feature.WiFiNetworkInterface) != 0)
                        SupportedComms = FabricInterface.WiFi | FabricInterface.IP;
                    if ((features & NetworkCommissioning.Feature.ThreadNetworkInterface) != 0)
                        SupportedComms = FabricInterface.Thread | FabricInterface.IP;
                    if ((features & NetworkCommissioning.Feature.EthernetNetworkInterface) != 0)
                        SupportedComms = FabricInterface.Ethernet | FabricInterface.IP;
                }
                else
                {
                    SupportedComms = FabricInterface.IP;
                    connected.Add("Default");
                }

                // Set regulatory information
                if ((SupportedComms & FabricInterface.WiFi) != 0 || (SupportedComms & FabricInterface.Thread) != 0)
                    await commissioning.SetRegulatoryConfig(paseSecureSession, GeneralCommissioning.RegulatoryLocationType.IndoorOutdoor, RegionInfo.CurrentRegion.TwoLetterISORegionName, 42);

                // Configure Date/Time
                try
                {
                    if (root.HasCluster<TimeSynchronization>())
                    {
                        TimeSynchronization timeSync = root.GetCluster<TimeSynchronization>();
                        bool success = await timeSync.SetUTCTime(paseSecureSession, DateTime.UtcNow, TimeSynchronization.Granularity.MillisecondsGranularity, TimeSynchronization.TimeSource.NonMatterNTP);
                        if (!success)
                            Console.WriteLine("Failed to set UTC Time");
                        if (await timeSync.Supports(paseSecureSession, TimeSynchronization.Feature.TimeZone))
                        {
                            var rules = TimeZoneInfo.Local.GetAdjustmentRules();
                            List<TimeSynchronization.TimeZone> zones = new List<TimeSynchronization.TimeZone>();
                            foreach (var rule in rules)
                            {
                                if (rule.DateEnd > DateTime.Now)
                                {
                                    zones.Add(new TimeSynchronization.TimeZone()
                                    {
                                        Offset = (int)(rule.BaseUtcOffsetDelta + TimeZoneInfo.Local.BaseUtcOffset).TotalSeconds,
                                        ValidAt = TimeUtil.Max(rule.DateStart, TimeUtil.EPOCH),
                                        Name = TimeZoneInfo.Local.DisplayName.Truncate(64)
                                    });
                                }
                            }
                            TimeSynchronization.SetTimeZoneResponse? tzResp = await timeSync.SetTimeZone(paseSecureSession, zones.ToArray());
                            if (tzResp.HasValue && tzResp.Value.DSTOffsetRequired)
                            {
                                //TODO - await timeSync.SetDSTOffset()
                            }
                        }
                    }
                }
                catch (Exception) {
                    Console.WriteLine("Failed to update time sync cluster");
                }

                // Validate Device Attestation Certificate (DAC)
                OperationalCredentials operationalCredentials = new OperationalCredentials(0);
                OperationalCredentials.CertificateChainResponse? dacResp = await operationalCredentials.CertificateChainRequest(paseSecureSession, OperationalCredentials.CertificateChainType.DACCertificate);
                OperationalCredentials.CertificateChainResponse? paiResp = await operationalCredentials.CertificateChainRequest(paseSecureSession, OperationalCredentials.CertificateChainType.PAICertificate);
                
                byte[] nonce = RandomNumberGenerator.GetBytes(32);
                OperationalCredentials.AttestationResponse? resp = await operationalCredentials.AttestationRequest(paseSecureSession, nonce);

                OperationalCertificate dacMatter = new OperationalCertificate(dacResp.Value.Certificate);
                if (verification != VerificationLevel.AnyDevice && !dacMatter.VerifyChain(paiResp.Value.Certificate, new DCLClient(), verification))
                {
                    dacMatter.Export("Failed.crt");
                    new OperationalCertificate(paiResp.Value.Certificate).Export("PAI.crt");
                    throw new CryptographicException("Node has an invalid certificate chain");
                }

                // Validate device has private key
                byte[] attestation_tbs = SpanUtil.Combine(resp.Value.AttestationElements, pase.GetAttestationChallenge());
                if (!dacMatter.VerifyData(attestation_tbs, resp.Value.AttestationSignature))
                    throw new CryptographicException("Node attestation was not signed");
                TLVReader reader = new TLVReader(resp.Value.AttestationElements);
                AttestationElements elements = new AttestationElements(reader);
                if (!elements.Attestation_nonce.SequenceEqual(nonce))
                    throw new CryptographicException("Node attempted to change attestation nonce");

                // Request CSR from node
                nonce = RandomNumberGenerator.GetBytes(32);
                OperationalCredentials.CSRResponse? csr = await operationalCredentials.CSRRequest(paseSecureSession, nonce, false);
                NocsrElements nocsr = new NocsrElements(csr.Value.NOCSRElements);
                CertificateRequest certReq = CertificateRequest.LoadSigningRequest(nocsr.Csr, HashAlgorithmName.SHA256);

                // Validate CSR came from device
                byte[] csr_tbs = new byte[csr.Value.NOCSRElements.Length + pase.GetAttestationChallenge().Length];
                Array.Copy(csr.Value.NOCSRElements, csr_tbs, csr.Value.NOCSRElements.Length);
                Array.Copy(pase.GetAttestationChallenge(), 0, csr_tbs, csr.Value.NOCSRElements.Length, pase.GetAttestationChallenge().Length);
                if (!dacMatter.VerifyData(csr_tbs, csr.Value.AttestationSignature))
                    throw new CryptographicException("Node signing request was not signed");
                if (!nocsr.CSRNonce.SequenceEqual(nonce))
                    throw new CryptographicException("Node attempted to change CSR nonce");

                // Load fabric root CA
                bool certAdded = await operationalCredentials.AddTrustedRootCertificate(paseSecureSession, fabric.GetMatterCertBytes());
                if (!certAdded)
                    throw new CryptographicException("Node did not accept root certificate");

                // Issue NOC
                OperationalCertificate nodeCert = fabric.Sign(certReq);
                OperationalCredentials.NOCResponse? nocAdded = await operationalCredentials.AddNOC(paseSecureSession, nodeCert.GetMatterCertBytes(), null, fabric.EpochKey, fabric.Commissioner!.NodeID!.Value, 0xFFF1);
                if (nocAdded.Value.StatusCode != OperationalCredentials.NodeOperationalCertStatus.OK)
                    throw new IOException($"Failed to add new Network Operational Certificate: Error ({nocAdded.Value.StatusCode}): {nocAdded.Value.DebugText}");
                
                // Set Fabric Label
                OperationalCredentials.NOCResponse? nocUpdated = await operationalCredentials.UpdateFabricLabel(paseSecureSession, (!string.IsNullOrEmpty(fabric.CommonName) ? fabric.CommonName : "MatterDotNet"));
                if (nocAdded.Value.StatusCode != OperationalCredentials.NodeOperationalCertStatus.OK)
                    Console.WriteLine("Failed to update fabric label");

                Node? node = Node.CreateTemp(nodeCert, fabric, commissionableNode, root);

                NetworkCommissioning.ThreadInterfaceScanResult[] threadNetworks = Array.Empty<NetworkCommissioning.ThreadInterfaceScanResult>();
                NetworkCommissioning.WiFiInterfaceScanResult[] wifiNetworks = Array.Empty<NetworkCommissioning.WiFiInterfaceScanResult>();

                if ((SupportedComms & FabricInterface.WiFi) != 0 || (SupportedComms & FabricInterface.Thread) != 0)
                {
                    NetworkCommissioning.NetworkInfo[] networks = await root.GetCluster<NetworkCommissioning>().GetNetworks(paseSecureSession);
                    foreach (NetworkCommissioning.NetworkInfo info in networks)
                    {
                        if (info.Connected)
                            connected.Add(Encoding.UTF8.GetString(info.NetworkID));
                    }

                    byte[]? ssid = null;
                    if (targetSSID != null)
                        ssid = Encoding.UTF8.GetBytes(targetSSID);
                    var results = await root.GetCluster<NetworkCommissioning>().ScanNetworks(paseSecureSession, ssid, 42);
                    if (results.HasValue)
                    {
                        if (results.Value.ThreadScanResults != null)
                            threadNetworks = results.Value.ThreadScanResults;
                        if (results.Value.WiFiScanResults != null)
                            wifiNetworks = results.Value.WiFiScanResults;
                    }
                }

                return new CommissioningState(node, paseSecureSession, SupportedComms, wifiNetworks, threadNetworks, connected.ToArray());
            }
            finally
            {
                if (unsecureSession != null)
                    unsecureSession.Dispose();
            }
        }

        /// <summary>
        /// Connect the device to the selected WiFi network and then complete commissioning
        /// </summary>
        /// <param name="info"></param>
        /// <param name="selectedNetwork"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task CompleteCommissioning(CommissioningState info, NetworkCommissioning.WiFiInterfaceScanResult selectedNetwork, string password)
        {
            await CompleteCommissioning(info, selectedNetwork, Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Connect the device to the selected WiFi network and then complete commissioning
        /// </summary>
        /// <param name="info"></param>
        /// <param name="selectedNetwork"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task CompleteCommissioning(CommissioningState info, NetworkCommissioning.WiFiInterfaceScanResult selectedNetwork, byte[] password)
        {
            ArgumentNullException.ThrowIfNull(info, nameof(info));
            ArgumentNullException.ThrowIfNull(selectedNetwork, nameof(selectedNetwork));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            if (!info.CommissioningStarted)
                throw new ArgumentException("Commissioning was unable to find the device. Completion is not possible.");
            try
            {
                if ((info.SupportedInterfaces & FabricInterface.WiFi) == 0)
                    throw new NotSupportedException("The device does not support WiFi");

                NetworkCommissioning network = info.Node!.Root.GetCluster<NetworkCommissioning>();
                var result = await network.AddOrUpdateWiFiNetwork(info.PASE!, selectedNetwork.SSID, password, 42, null, null, null);
                if (!result.HasValue)
                    throw new IOException("Failed to configure network. Unknown Error");
                else if (result.Value.NetworkingStatus != NetworkCommissioning.NetworkCommissioningStatus.Success && result.Value.NetworkingStatus != NetworkCommissioning.NetworkCommissioningStatus.DuplicateNetworkID)
                    throw new IOException("Failed to configure network. Error: " + result.Value.NetworkingStatus + " (" + result.Value.DebugText + ")");

                var connect = await network.ConnectNetwork(info.PASE!, selectedNetwork.SSID, 42);
                if (!connect.HasValue)
                    throw new IOException("Failed to connect to network. Unknown Error");
                else if (connect.Value.NetworkingStatus != NetworkCommissioning.NetworkCommissioningStatus.Success)
                    throw new IOException("Failed to connect to network. Error: " + result.Value.NetworkingStatus + " (" + result.Value.DebugText + ")");
                else
                    info.Upgrade(Encoding.UTF8.GetString(selectedNetwork.SSID));
                Console.WriteLine("Connected to " + selectedNetwork.SSID);
            }
            catch
            {
                info.PASE?.Dispose();
                throw;
            }

            await CompleteCommissioning(info);
        }

        /// <summary>
        /// Connect the device to the selected Thread network and then complete commissioning
        /// </summary>
        /// <param name="info"></param>
        /// <param name="selectedNetwork"></param>
        /// <param name="operationalDataSet"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task CompleteCommissioning(CommissioningState info, NetworkCommissioning.ThreadInterfaceScanResult selectedNetwork, byte[] operationalDataSet)
        {
            ArgumentNullException.ThrowIfNull(info, nameof(info));
            ArgumentNullException.ThrowIfNull(selectedNetwork, nameof(selectedNetwork));
            ArgumentNullException.ThrowIfNull(operationalDataSet, nameof(operationalDataSet));

            if (!info.CommissioningStarted)
                throw new ArgumentException("Commissioning was unable to find the device. Completion is not possible.");
            try
            {
                if ((info.SupportedInterfaces & FabricInterface.Thread) == 0)
                    throw new NotSupportedException("The device does not support Thread");

                NetworkCommissioning network = info.Node!.Root.GetCluster<NetworkCommissioning>();
                var result = await network.AddOrUpdateThreadNetwork(info.PASE!, operationalDataSet, 42);
                if (!result.HasValue)
                    throw new IOException("Failed to configure network. Unknown Error");
                else if (result.Value.NetworkingStatus != NetworkCommissioning.NetworkCommissioningStatus.Success && result.Value.NetworkingStatus != NetworkCommissioning.NetworkCommissioningStatus.DuplicateNetworkID)
                    throw new IOException("Failed to configure network. Error: " + result.Value.NetworkingStatus + " (" + result.Value.DebugText + ")");

                var connect = await network.ConnectNetwork(info.PASE!, Encoding.UTF8.GetBytes(selectedNetwork.NetworkName!), 42);
                if (!connect.HasValue)
                    throw new IOException("Failed to connect to network. Unknown Error");
                else if (connect.Value.NetworkingStatus != NetworkCommissioning.NetworkCommissioningStatus.Success)
                    throw new IOException("Failed to connect to network. Error: " + result.Value.NetworkingStatus + " (" + result.Value.DebugText + ")");
                else
                    info.Upgrade(selectedNetwork.NetworkName!);
                Console.WriteLine("Connected to " + selectedNetwork.NetworkName);
            }
            catch
            {
                info.PASE?.Dispose();
                throw;
            }

            await CompleteCommissioning(info);
        }

        /// <summary>
        /// Complete comissioning for a device that is already on the operational network
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task CompleteCommissioning(CommissioningState info)
        {
            if (!info.CommissioningStarted)
                throw new ArgumentException("Commissioning was unable to find the device. Completion is not possible.");
            try
            {
                if (info.ConnectedNetworks.Length == 0)
                    throw new NotSupportedException("The device is not connected to any networks. WiFi or Thread network info is required.");

                // Perform Operational Discovery for the node now on the correct network
                ODNode? discoveredNode = await IPDiscoveryService.Shared.Find(info.Node!.OperationalInstanceName, true);
                if (discoveredNode == null || (discoveredNode.IP6Address == null && discoveredNode.IP4Address == null))
                    throw new InvalidOperationException("The device could not be found on the operational network");

                // Establish CASE session
                SessionContext? unsecureSession = null;
                SecureSession? caseSecureSession = null;
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        IPAddress address;
                        if (discoveredNode.IP4Address == null)
                            address = discoveredNode.IP6Address!;
                        else if (discoveredNode.IP6Address == null)
                            address = discoveredNode.IP4Address;
                        else
                            address = ((i % 2) == 0) ? discoveredNode.IP6Address : discoveredNode.IP4Address;
                        unsecureSession = SessionManager.GetUnsecureSession(new IPEndPoint(address!, discoveredNode.Port), true);
                        caseSecureSession = await info.Node!.GetCASESession(unsecureSession);
                        break;
                    }
                    catch (IOException) {
                        Console.WriteLine("Unable to connect to device. Retry attempt: " + (i + 1));
                        if (i == 5)
                            throw new IOException("Device is not reachable on the new network");
                        //Service is likely still starting
                        await Task.Delay(750 * i);
                    }
                }

                try
                {
                    // Done
                    GeneralCommissioning.CommissioningCompleteResponse? complete = await info.Node.Root.GetCluster<GeneralCommissioning>().CommissioningComplete(caseSecureSession!);
                    if (complete.Value.ErrorCode != GeneralCommissioning.CommissioningError.OK)
                        throw new InvalidOperationException(complete.Value.ErrorCode + ": " + complete.Value.DebugText);
                    nodes.Add(info.Node.ID, info.Node);
                    Console.WriteLine("Commissioning Complete!");

                    await Node.Populate(caseSecureSession!, info.Node);
                }
                finally
                {
                    caseSecureSession?.Dispose();
                }
            }
            finally
            {
                info.PASE?.Dispose();
            }
        }
        
        /// <summary>
        /// Remove a node from the fabric
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public async Task<bool> RemoveNode(Node node)
        {
            SecureSession session = await node.GetCASESession();
            OperationalCredentials ops = node.Root.GetCluster<OperationalCredentials>();
            byte index = await ops.GetCurrentFabricIndex(session);
            var response = await ops.RemoveFabric(session, index);
            bool success = response.Value.StatusCode == OperationalCredentials.NodeOperationalCertStatus.OK;
            if (success)
                nodes.Remove(node.ID);
            return success;
        }

        /// <summary>
        /// The collection of nodes in this fabric
        /// </summary>
        public IReadOnlyCollection<Node> Nodes { get { return nodes.Values; } }

        /// <summary>
        /// Get a node by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Node? GetNode(ulong id)
        {
            if (nodes.TryGetValue(id, out Node? node))
                return node;
            return null;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Controller {fabric.FabricID:X16}:");
            foreach (var node in nodes.Values)
                sb.AppendLine("- " + node.ToString());
            return sb.ToString();
        }
    }
}
