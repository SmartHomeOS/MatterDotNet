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
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Ethernet Network Diagnostics Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class EthernetNetworkDiagnosticsCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0037;

        /// <summary>
        /// Ethernet Network Diagnostics Cluster
        /// </summary>
        public EthernetNetworkDiagnosticsCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected EthernetNetworkDiagnosticsCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Node makes available the counts for the number of received and transmitted packets on the ethernet interface.
            /// </summary>
            PacketCounts = 1,
            /// <summary>
            /// Node makes available the counts for the number of errors that have occurred during the reception and transmission of packets on the ethernet interface.
            /// </summary>
            ErrorCounts = 2,
        }

        /// <summary>
        /// PHY Rate
        /// </summary>
        public enum PHYRateEnum {
            /// <summary>
            /// PHY rate is 10Mbps
            /// </summary>
            Rate10M = 0,
            /// <summary>
            /// PHY rate is 100Mbps
            /// </summary>
            Rate100M = 1,
            /// <summary>
            /// PHY rate is 1Gbps
            /// </summary>
            Rate1G = 2,
            /// <summary>
            /// PHY rate is 2.5Gbps
            /// </summary>
            Rate2_5G = 3,
            /// <summary>
            /// PHY rate is 5Gbps
            /// </summary>
            Rate5G = 4,
            /// <summary>
            /// PHY rate is 10Gbps
            /// </summary>
            Rate10G = 5,
            /// <summary>
            /// PHY rate is 40Gbps
            /// </summary>
            Rate40G = 6,
            /// <summary>
            /// PHY rate is 100Gbps
            /// </summary>
            Rate100G = 7,
            /// <summary>
            /// PHY rate is 200Gbps
            /// </summary>
            Rate200G = 8,
            /// <summary>
            /// PHY rate is 400Gbps
            /// </summary>
            Rate400G = 9,
        }
        #endregion Enums

        #region Payloads
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Reset Counts
        /// </summary>
        public async Task<bool> ResetCounts(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Features supported by this cluster
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<Feature> GetSupportedFeatures(SecureSession session)
        {
            return (Feature)(byte)(await GetAttribute(session, 0xFFFC))!;
        }

        /// <summary>
        /// Returns true when the feature is supported by the cluster
        /// </summary>
        /// <param name="session"></param>
        /// <param name="feature"></param>
        /// <returns></returns>
        public async Task<bool> Supports(SecureSession session, Feature feature)
        {
            return ((feature & await GetSupportedFeatures(session)) != 0);
        }

        /// <summary>
        /// Get the PHY Rate attribute
        /// </summary>
        public async Task<PHYRateEnum?> GetPHYRate(SecureSession session) {
            return (PHYRateEnum?)await GetEnumAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Full Duplex attribute
        /// </summary>
        public async Task<bool?> GetFullDuplex(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 1, true) ?? false;
        }

        /// <summary>
        /// Get the Packet Rx Count attribute
        /// </summary>
        public async Task<ulong> GetPacketRxCount(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 2) ?? 0;
        }

        /// <summary>
        /// Get the Packet Tx Count attribute
        /// </summary>
        public async Task<ulong> GetPacketTxCount(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 3) ?? 0;
        }

        /// <summary>
        /// Get the Tx Err Count attribute
        /// </summary>
        public async Task<ulong> GetTxErrCount(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 4) ?? 0;
        }

        /// <summary>
        /// Get the Collision Count attribute
        /// </summary>
        public async Task<ulong> GetCollisionCount(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 5) ?? 0;
        }

        /// <summary>
        /// Get the Overrun Count attribute
        /// </summary>
        public async Task<ulong> GetOverrunCount(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 6) ?? 0;
        }

        /// <summary>
        /// Get the Carrier Detect attribute
        /// </summary>
        public async Task<bool?> GetCarrierDetect(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 7, true) ?? false;
        }

        /// <summary>
        /// Get the Time Since Reset attribute
        /// </summary>
        public async Task<ulong> GetTimeSinceReset(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 8) ?? 0;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Ethernet Network Diagnostics Cluster";
        }
    }
}