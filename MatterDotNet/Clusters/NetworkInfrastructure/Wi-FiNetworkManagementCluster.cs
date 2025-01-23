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

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;

namespace MatterDotNet.Clusters.NetworkInfrastructure
{
    /// <summary>
    /// Functionality to retrieve operational information about a managed Wi-Fi network.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class WiFiNetworkManagement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0451;

        /// <summary>
        /// Functionality to retrieve operational information about a managed Wi-Fi network.
        /// </summary>
        public WiFiNetworkManagement(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected WiFiNetworkManagement(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Payloads
        /// <summary>
        /// Network Passphrase Response - Reply from server
        /// </summary>
        public struct NetworkPassphraseResponse() {
            public required byte[] Passphrase { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Network Passphrase Request
        /// </summary>
        public async Task<NetworkPassphraseResponse?> NetworkPassphraseRequest(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkPassphraseResponse() {
                Passphrase = (byte[])GetField(resp, 0),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the SSID attribute
        /// </summary>
        public async Task<byte[]?> GetSSID(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Passphrase Surrogate attribute
        /// </summary>
        public async Task<ulong?> GetPassphraseSurrogate(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 1, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Wi-Fi Network Management";
        }
    }
}