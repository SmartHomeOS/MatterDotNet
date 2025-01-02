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
using MatterDotNet.OperationalDiscovery;
using MatterDotNet.PKI;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Net;
using System.Text;

namespace MatterDotNet.Entities
{
    /// <summary>
    /// A Node within the matter fabric
    /// </summary>
    public class Node
    {
        private Fabric? fabric;
        private EndPoint root;
        private OperationalCertificate? noc;
        ODNode connection;

        private Node(ODNode connection)
        {
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
        public ulong ID { get { return noc!.NodeID; } }

        /// <summary>
        /// Get a secure session for the node
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        public async Task<SecureSession> GetSession()
        {
            using (SessionContext session = SessionManager.GetUnsecureSession(new IPEndPoint(connection.Address!, connection.Port), true))
            {
                CASE caseSession = new CASE(session);
                //TODO - Use OD session params
                SecureSession? secSession = await caseSession.EstablishSecureSession(fabric!, noc!.NodeID, fabric!.EpochKey);
                if (secSession == null)
                    throw new IOException("CASE pairing failed");
                return secSession;
            }
        }

        internal static async Task<Node?> Enumerate(OperationalCertificate noc, Fabric fabric)
        {
            string operationalInstanceName = $"{Convert.ToHexString(fabric.CompressedFabricID)}-{noc.NodeID:X16}";
            ODNode? opInfo = await DiscoveryService.Shared.Find(operationalInstanceName);
            if (opInfo == null)
                return null;
            return await Enumerate(noc, fabric, opInfo);
        }

        internal static async Task<Node> Enumerate(OperationalCertificate noc, Fabric fabric, ODNode opInfo)
        {
            Node node = new Node(opInfo);
            node.Provision(noc, fabric);
            using (SecureSession session = await node.GetSession())
                await Populate(session, node);
            return node;
        }

        internal static async Task<Node> Enumerate(SecureSession session, ODNode opInfo)
        {
            Node node = new Node(opInfo);
            await Populate(session, node);
            return node;
        }

        internal void Provision(OperationalCertificate noc, Fabric fabric)
        {
            this.noc = noc;
            this.fabric = fabric;
        }

        private static async Task Populate(SecureSession session, Node node)
        {
            List<ushort> eps = await node.Root.GetCluster<DescriptorCluster>().GetPartsList(session);
            foreach (ushort index in eps)
                node.Root.AddChild(new EndPoint(index, node));
            foreach (EndPoint child in node.Root.Children)
            {
                List<ushort> childEps = await child.GetCluster<DescriptorCluster>().GetPartsList(session);
                foreach (ushort childEp in childEps)
                {
                    child.AddChild(node.Root.RemoveChild(childEp)!);
                }
            }
            await node.Root.EnumerateClusters(session);
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
