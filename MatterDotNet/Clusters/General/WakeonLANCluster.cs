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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// This cluster provides an interface for managing low power mode on a device that supports the Wake On LAN protocol.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class WakeonLAN : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0503;

        /// <summary>
        /// This cluster provides an interface for managing low power mode on a device that supports the Wake On LAN protocol.
        /// </summary>
        public WakeonLAN(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected WakeonLAN(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public async Task<byte[]> GetLinkLocalAddress(SecureSession session) {
            return (byte[])(dynamic?)(await GetAttribute(session, 1))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Wake on LAN";
        }
    }
}