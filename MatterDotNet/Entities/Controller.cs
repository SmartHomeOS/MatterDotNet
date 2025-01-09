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

using MatterDotNet.Clusters.Utility;
using MatterDotNet.DCL;
using MatterDotNet.Messages.Certificates;
using MatterDotNet.OperationalDiscovery;
using MatterDotNet.PKI;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Security;
using MatterDotNet.Util;
using System.Globalization;
using System.Net;
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
        /// <param name="fabricId"></param>
        /// <param name="fabricName"></param>
        public Controller(uint fabricId, string fabricName = "MatterDotNot")
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
        public async Task Start()
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
        /// <param name="verification"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="CryptographicException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Node?> Commission(PayloadParser payload, VerificationLevel verification = VerificationLevel.CertifiedDevicesOnly)
        {
            SessionContext? unsecureSession = null;
            SecureSession? paseSecureSession = null;
            SecureSession? caseSecureSession = null;
            if ((payload.Capabilities & PayloadParser.DiscoveryCapabilities.IP) != PayloadParser.DiscoveryCapabilities.IP && payload.Capabilities != PayloadParser.DiscoveryCapabilities.UNKNOWN)
                throw new NotImplementedException("BLE Commissioning is not supported yet");

            // Discover the Node
            ODNode? commissionableNode = await DiscoveryService.Shared.Find(payload.VendorID, payload.ProductID, payload.Discriminator, payload.DiscriminatorLength == 12);
            if (commissionableNode == null)
                return null;

            try
            {
                // Establish PASE session
                unsecureSession = SessionManager.GetUnsecureSession(new IPEndPoint(commissionableNode.Address!, commissionableNode.Port), true);
                PASE pase = new PASE(unsecureSession);
                paseSecureSession = await pase.EstablishSecureSession(payload.Passcode);
                if (paseSecureSession == null)
                    throw new IOException("PASE pairing failed");
                
                // Get Basic Commissioning Info
                GeneralCommissioningCluster commissioning = new GeneralCommissioningCluster(0);
                GeneralCommissioningCluster.BasicCommissioningInfo basicInfo = await commissioning.GetBasicCommissioningInfo(paseSecureSession);
                ushort expiration = Math.Min(Math.Max((ushort)90, basicInfo.FailSafeExpiryLengthSeconds), basicInfo.MaxCumulativeFailsafeSeconds);
                
                // Arm Fail Safe
                GeneralCommissioningCluster.ArmFailSafeResponse? failSafe = await commissioning.ArmFailSafe(paseSecureSession, expiration, 42);


                // Set regulatory information
                await commissioning.SetRegulatoryConfig(paseSecureSession, GeneralCommissioningCluster.RegulatoryLocationTypeEnum.IndoorOutdoor, RegionInfo.CurrentRegion.TwoLetterISORegionName, 42);

                // Configure Date/Time
                try
                {
                    TimeSynchronizationCluster timeSync = new TimeSynchronizationCluster(0);
                    bool success = await timeSync.SetUTCTime(paseSecureSession, DateTime.UtcNow, TimeSynchronizationCluster.GranularityEnum.MillisecondsGranularity, TimeSynchronizationCluster.TimeSourceEnum.NonMatterNTP);
                    if (!success)
                        Console.WriteLine("Failed to set UTC Time");
                    var rules = TimeZoneInfo.Local.GetAdjustmentRules();
                    List<TimeSynchronizationCluster.TimeZone> zones = new List<TimeSynchronizationCluster.TimeZone>();
                    foreach (var rule in rules)
                    {
                        if (rule.DateEnd > DateTime.Now)
                        {
                            zones.Add(new TimeSynchronizationCluster.TimeZone()
                            {
                                Offset = (int)(rule.BaseUtcOffsetDelta + TimeZoneInfo.Local.BaseUtcOffset).TotalSeconds,
                                ValidAt = TimeUtil.Max(rule.DateStart, TimeUtil.EPOCH),
                                Name = TimeZoneInfo.Local.DisplayName.Truncate(64)
                            });
                        }
                    }
                    await timeSync.SetTimeZone(paseSecureSession, zones);
                }
                catch (Exception) {
                    Console.WriteLine("Failed to update time sync cluster (likely doesn't exist)");
                }

                // Validate Device Attestation Certificate (DAC)
                NodeOperationalCredentialsCluster operationalCredentials = new NodeOperationalCredentialsCluster(0);
                NodeOperationalCredentialsCluster.CertificateChainResponse? dacResp = await operationalCredentials.CertificateChainRequest(paseSecureSession, NodeOperationalCredentialsCluster.CertificateChainTypeEnum.DACCertificate);
                NodeOperationalCredentialsCluster.CertificateChainResponse? paiResp = await operationalCredentials.CertificateChainRequest(paseSecureSession, NodeOperationalCredentialsCluster.CertificateChainTypeEnum.PAICertificate);
                
                byte[] nonce = RandomNumberGenerator.GetBytes(32);
                NodeOperationalCredentialsCluster.AttestationResponse? resp = await operationalCredentials.AttestationRequest(paseSecureSession, nonce);

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
                NodeOperationalCredentialsCluster.CSRResponse? csr = await operationalCredentials.CSRRequest(paseSecureSession, nonce, false);
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
                NodeOperationalCredentialsCluster.NOCResponse? nocAdded = await operationalCredentials.AddNOC(paseSecureSession, nodeCert.GetMatterCertBytes(), null, fabric.EpochKey, fabric.Commissioner!.NodeID!.Value, 0xFFF1);
                if (nocAdded.Value.StatusCode != NodeOperationalCredentialsCluster.NodeOperationalCertStatusEnum.OK)
                    throw new IOException($"Failed to add new Network Operational Certificate: Error ({nocAdded.Value.StatusCode}): {nocAdded.Value.DebugText}");
                
                // Set Fabric Label
                NodeOperationalCredentialsCluster.NOCResponse? nocUpdated = await operationalCredentials.UpdateFabricLabel(paseSecureSession, (!string.IsNullOrEmpty(fabric.CommonName) ? fabric.CommonName : "MatterDotNet"));
                if (nocAdded.Value.StatusCode != NodeOperationalCredentialsCluster.NodeOperationalCertStatusEnum.OK)
                    Console.WriteLine("Failed to update fabric label");

                Node? node = Node.CreateTemp(nodeCert, fabric, commissionableNode);

                //TODO - Configure Network (WiFi / Thread)
                //TODO - Operational Discovery if commissioning over BT-LE

                // Establish CASE session
                caseSecureSession = await node!.GetCASESession(unsecureSession);

                // Done
                GeneralCommissioningCluster.CommissioningCompleteResponse? complete = await commissioning.CommissioningComplete(caseSecureSession);
                if (complete.Value.ErrorCode != GeneralCommissioningCluster.CommissioningErrorEnum.OK)
                    throw new InvalidOperationException(complete.Value.ErrorCode + ": " + complete.Value.DebugText);
                nodes.Add(node.ID, node);
                Console.WriteLine("Commissioning Complete!");

                return node;
            }
            finally
            {
                if (paseSecureSession != null)
                    paseSecureSession.Dispose();
                if (caseSecureSession != null)
                    caseSecureSession.Dispose();
                if (unsecureSession != null)
                    unsecureSession.Dispose();
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
            NodeOperationalCredentialsCluster ops = node.Root.GetCluster<NodeOperationalCredentialsCluster>();
            byte index = await ops.GetCurrentFabricIndex(session);
            var response = await ops.RemoveFabric(session, index);
            bool success = response.Value.StatusCode == NodeOperationalCredentialsCluster.NodeOperationalCertStatusEnum.OK;
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
