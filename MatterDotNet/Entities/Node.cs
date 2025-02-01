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

using MatterDotNet.Clusters.General;
using MatterDotNet.OperationalDiscovery;
using MatterDotNet.PKI;
using MatterDotNet.Protocol.Connection;
using MatterDotNet.Protocol.Cryptography;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MatterDotNet.Entities
{
    /// <summary>
    /// A Node within the matter fabric
    /// </summary>
    public class Node
    {
        private Fabric fabric;
        private EndPoint root;
        private OperationalCertificate noc;
        ODNode connection;
        private byte[]? resumptionId = null;
        private byte[]? sharedSecret = null;

        private Node(ODNode connection, Fabric fabric, OperationalCertificate noc)
        {
            this.noc = noc;
            this.fabric = fabric;
            this.root = new EndPoint(0);
            root.SetNode(this);
            this.connection = connection;
        }

        /// <summary>
        /// Root End Point
        /// </summary>
        public EndPoint Root { get { return root; } }

        /// <summary>
        /// Node ID
        /// </summary>
        public ulong ID { get { return noc.NodeID!.Value; } }

        /// <summary>
        /// Get a secure session for the node
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        public async Task<SecureSession> GetCASESession(CancellationToken token = default)
        {
            if (connection.IP4Address != null)
            {
                using (SessionContext session = SessionManager.GetUnsecureSession(new IPEndPoint(connection.IP4Address!, connection.Port), true))
                    return await GetCASESession(session, token);
            }
            else if (connection.IP6Address != null)
            {
                using (SessionContext session = SessionManager.GetUnsecureSession(new IPEndPoint(connection.IP6Address!, connection.Port), true))
                    return await GetCASESession(session, token);
            }
            else
            {
                using (SessionContext session = SessionManager.GetUnsecureSession(new BLEEndPoint(connection.BTAddress!), true))
                    return await GetCASESession(session, token);
            }
        }

        /// <summary>
        /// Open a commissioning window with a new randomly generateed PIN Code
        /// </summary>
        /// <param name="session"></param>
        /// <param name="timeoutSec"></param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public async Task<string?> OpenEnhancedCommissioning(SecureSession session, ushort timeoutSec = 300)
        {
            if (!root.HasCluster<AdministratorCommissioning>())
                throw new PlatformNotSupportedException("Admin commissioning cluster not found");
            uint passcode = Crypto.GeneratePasscode();
            ushort discriminator = (ushort)(Random.Shared.Next() & 0xFFF);
            string pin = CommissioningPayload.GeneratePIN(discriminator, passcode);
            SPAKE2Plus spake = new SPAKE2Plus();
            AdministratorCommissioning com = root.GetCluster<AdministratorCommissioning>();
            byte[] salt = RandomNumberGenerator.GetBytes(32);
            if (!await com.OpenCommissioningWindow(session, 10000, timeoutSec, spake.CommissioneePakeInput(passcode, 10000, salt), discriminator, 10000, salt))
                return null;
            return pin;
        }

        /// <summary>
        /// Returns true if any of the Nodes EndPoints are of the provided type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasType(DeviceTypeEnum type)
        {
            return root.HasType(type);
        }

        /// <summary>
        /// Find the first EndPoint of the given type or return null
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public EndPoint? FirstEndPoint(DeviceTypeEnum type)
        {
            return root.Find(type);
        }

        /// <summary>
        /// Get a secure session for the node
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        internal async Task<SecureSession> GetCASESession(SessionContext session, CancellationToken token = default)
        {
            CASE caseProtocol = new CASE(session);
            //TODO - Use OD session params
            SecureSession? caseSession;
            if (sharedSecret != null && resumptionId != null)
                caseSession = await caseProtocol.ResumeSecureSession(fabric, noc, resumptionId, sharedSecret, token);
            else
                caseSession = await caseProtocol.EstablishSecureSession(fabric, noc, token);
            if (caseSession == null)
                throw new IOException("CASE pairing failed");
            resumptionId = caseSession.ResumptionID;
            sharedSecret = caseSession.SharedSecret;
            return caseSession;
        }

        internal static Node CreateTemp(OperationalCertificate noc, Fabric fabric, ODNode opInfo, EndPoint rootNode)
        {
            Node node = new Node(opInfo, fabric, noc);
            rootNode.SetNode(node);
            node.root = rootNode;
            return node;
        }

        internal static async Task<Node?> Enumerate(OperationalCertificate noc, Fabric fabric)
        {
            string operationalInstanceName = $"{Convert.ToHexString(fabric.CompressedFabricID)}-{noc.NodeID:X16}";
            ODNode? opInfo = await IPDiscoveryService.Shared.Find(operationalInstanceName);
            if (opInfo == null)
                return null;
            return await Enumerate(noc, fabric, opInfo);
        }

        internal static async Task<Node> Enumerate(OperationalCertificate noc, Fabric fabric, ODNode opInfo)
        {
            Node node = new Node(opInfo, fabric, noc);
            using (SecureSession session = await node.GetCASESession())
                await Populate(session, node);
            return node;
        }

        internal static async Task Populate(SecureSession session, Node node)
        {
            ushort[] eps = await node.Root.GetCluster<Descriptor>().PartsList.Get(session);
            foreach (ushort index in eps)
                node.Root.AddChild(new EndPoint(index, node));
            foreach (EndPoint child in node.Root.Children)
            {
                ushort[] childEps = await child.GetCluster<Descriptor>().PartsList.Get(session);
                foreach (ushort childEp in childEps)
                {
                    child.AddChild(node.Root.RemoveChild(childEp)!);
                }
            }
            await node.Root.EnumerateClusters(session);
        }

        internal string OperationalInstanceName
        {
            get
            {
                return $"{Convert.ToHexString(fabric.CompressedFabricID)}-{noc.NodeID:X16}";
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Node {noc.NodeID:X16}:");
            sb.AppendLine(root.ToString("\t"));
            return sb.ToString();
        }
    }
}
