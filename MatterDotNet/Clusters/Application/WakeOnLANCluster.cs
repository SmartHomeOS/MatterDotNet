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
//
// WARNING: This file was auto-generated. Do not edit.

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using System.Net;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Wake On LAN Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class WakeOnLANCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0503;

        /// <summary>
        /// Wake On LAN Cluster
        /// </summary>
        public WakeOnLANCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected WakeOnLANCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Attributes
        /// <summary>
        /// Get the MAC Address attribute
        /// </summary>
        public async Task<string> GetMACAddress(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Link Local Address attribute
        /// </summary>
        public async Task<IPAddress> GetLinkLocalAddress(SecureSession session) {
            return (IPAddress)(dynamic?)(await GetAttribute(session, 1))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Wake On LAN Cluster";
        }
    }
}