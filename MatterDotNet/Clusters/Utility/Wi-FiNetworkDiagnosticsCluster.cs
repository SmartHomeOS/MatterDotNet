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
    /// Wi-Fi Network Diagnostics Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class WiFiNetworkDiagnosticsCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0036;

        /// <summary>
        /// Wi-Fi Network Diagnostics Cluster
        /// </summary>
        public WiFiNetworkDiagnosticsCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected WiFiNetworkDiagnosticsCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Node makes available the counts for the number of received and transmitted packets on the Wi-Fi interface.
            /// </summary>
            PacketCounts = 1,
            /// <summary>
            /// Node makes available the counts for the number of errors that have occurred during the reception and transmission of packets on the Wi-Fi interface.
            /// </summary>
            ErrorCounts = 2,
        }

        /// <summary>
        /// Association Failure Cause
        /// </summary>
        public enum AssociationFailureCauseEnum {
            /// <summary>
            /// The reason for the failure is unknown.
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// An error occurred during association.
            /// </summary>
            AssociationFailed = 1,
            /// <summary>
            /// An error occurred during authentication.
            /// </summary>
            AuthenticationFailed = 2,
            /// <summary>
            /// The specified SSID could not be found.
            /// </summary>
            SsidNotFound = 3,
        }

        /// <summary>
        /// Connection Status
        /// </summary>
        public enum ConnectionStatusEnum {
            /// <summary>
            /// Indicate the node is connected
            /// </summary>
            Connected = 0,
            /// <summary>
            /// Indicate the node is not connected
            /// </summary>
            NotConnected = 1,
        }

        /// <summary>
        /// Security Type
        /// </summary>
        public enum SecurityTypeEnum {
            /// <summary>
            /// Indicate the usage of an unspecified Wi-Fi security type
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// Indicate the usage of no Wi-Fi security
            /// </summary>
            None = 1,
            /// <summary>
            /// Indicate the usage of WEP Wi-Fi security
            /// </summary>
            WEP = 2,
            /// <summary>
            /// Indicate the usage of WPA Wi-Fi security
            /// </summary>
            WPA = 3,
            /// <summary>
            /// Indicate the usage of WPA2 Wi-Fi security
            /// </summary>
            WPA2 = 4,
            /// <summary>
            /// Indicate the usage of WPA3 Wi-Fi security
            /// </summary>
            WPA3 = 5,
        }

        /// <summary>
        /// WiFi Version
        /// </summary>
        public enum WiFiVersionEnum {
            /// <summary>
            /// Indicate the network interface is currently using 802.11a against the wireless access point.
            /// </summary>
            A = 0,
            /// <summary>
            /// Indicate the network interface is currently using 802.11b against the wireless access point.
            /// </summary>
            B = 1,
            /// <summary>
            /// Indicate the network interface is currently using 802.11g against the wireless access point.
            /// </summary>
            G = 2,
            /// <summary>
            /// Indicate the network interface is currently using 802.11n against the wireless access point.
            /// </summary>
            N = 3,
            /// <summary>
            /// Indicate the network interface is currently using 802.11ac against the wireless access point.
            /// </summary>
            Ac = 4,
            /// <summary>
            /// Indicate the network interface is currently using 802.11ax against the wireless access point.
            /// </summary>
            Ax = 5,
            /// <summary>
            /// Indicate the network interface is currently using 802.11ah against the wireless access point.
            /// </summary>
            Ah = 6,
        }
        #endregion Enums

        #region Payloads
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Reset Counts
        /// </summary>
        public async Task<bool> ResetCounts(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00);
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
        /// Get the BSSID attribute
        /// </summary>
        public async Task<byte[]?> GetBSSID(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 0, true) ?? null;
        }

        /// <summary>
        /// Get the Security Type attribute
        /// </summary>
        public async Task<SecurityTypeEnum?> GetSecurityType(SecureSession session) {
            return (SecurityTypeEnum?)await GetEnumAttribute(session, 1, true);
        }

        /// <summary>
        /// Get the WiFi Version attribute
        /// </summary>
        public async Task<WiFiVersionEnum?> GetWiFiVersion(SecureSession session) {
            return (WiFiVersionEnum?)await GetEnumAttribute(session, 2, true);
        }

        /// <summary>
        /// Get the Channel Number attribute
        /// </summary>
        public async Task<ushort?> GetChannelNumber(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3, true) ?? null;
        }

        /// <summary>
        /// Get the RSSI attribute
        /// </summary>
        public async Task<sbyte?> GetRSSI(SecureSession session) {
            return (sbyte?)(dynamic?)await GetAttribute(session, 4, true) ?? null;
        }

        /// <summary>
        /// Get the Beacon Lost Count attribute
        /// </summary>
        public async Task<uint?> GetBeaconLostCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 5, true) ?? 0;
        }

        /// <summary>
        /// Get the Beacon Rx Count attribute
        /// </summary>
        public async Task<uint?> GetBeaconRxCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 6, true) ?? 0;
        }

        /// <summary>
        /// Get the Packet Multicast Rx Count attribute
        /// </summary>
        public async Task<uint?> GetPacketMulticastRxCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 7, true) ?? 0;
        }

        /// <summary>
        /// Get the Packet Multicast Tx Count attribute
        /// </summary>
        public async Task<uint?> GetPacketMulticastTxCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 8, true) ?? 0;
        }

        /// <summary>
        /// Get the Packet Unicast Rx Count attribute
        /// </summary>
        public async Task<uint?> GetPacketUnicastRxCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 9, true) ?? 0;
        }

        /// <summary>
        /// Get the Packet Unicast Tx Count attribute
        /// </summary>
        public async Task<uint?> GetPacketUnicastTxCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 10, true) ?? 0;
        }

        /// <summary>
        /// Get the Current Max Rate attribute
        /// </summary>
        public async Task<ulong?> GetCurrentMaxRate(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 11, true) ?? 0;
        }

        /// <summary>
        /// Get the Overrun Count attribute
        /// </summary>
        public async Task<ulong?> GetOverrunCount(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 12, true) ?? 0;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Wi-Fi Network Diagnostics Cluster";
        }
    }
}