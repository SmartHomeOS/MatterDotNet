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

using MatterDotNet.Attributes;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Wi-Fi Network Diagnostics Cluster provides a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class WiFiNetworkDiagnostics : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0036;

        /// <summary>
        /// The Wi-Fi Network Diagnostics Cluster provides a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems.
        /// </summary>
        [SetsRequiredMembers]
        public WiFiNetworkDiagnostics(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected WiFiNetworkDiagnostics(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            BSSID = new ReadAttribute<byte[]?>(cluster, endPoint, 0, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            SecurityType = new ReadAttribute<SecurityTypeEnum?>(cluster, endPoint, 1, true) {
                Deserialize = x => (SecurityTypeEnum?)DeserializeEnum(x)
            };
            WiFiVersion = new ReadAttribute<WiFiVersionEnum?>(cluster, endPoint, 2, true) {
                Deserialize = x => (WiFiVersionEnum?)DeserializeEnum(x)
            };
            ChannelNumber = new ReadAttribute<ushort?>(cluster, endPoint, 3, true) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            RSSI = new ReadAttribute<sbyte?>(cluster, endPoint, 4, true) {
                Deserialize = x => (sbyte?)(dynamic?)x
            };
            BeaconLostCount = new ReadAttribute<uint?>(cluster, endPoint, 5, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x00000000

            };
            BeaconRxCount = new ReadAttribute<uint?>(cluster, endPoint, 6, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x00000000

            };
            PacketMulticastRxCount = new ReadAttribute<uint?>(cluster, endPoint, 7, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x00000000

            };
            PacketMulticastTxCount = new ReadAttribute<uint?>(cluster, endPoint, 8, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x00000000

            };
            PacketUnicastRxCount = new ReadAttribute<uint?>(cluster, endPoint, 9, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x00000000

            };
            PacketUnicastTxCount = new ReadAttribute<uint?>(cluster, endPoint, 10, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x00000000

            };
            CurrentMaxRate = new ReadAttribute<ulong?>(cluster, endPoint, 11, true) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            OverrunCount = new ReadAttribute<ulong?>(cluster, endPoint, 12, true) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
        }

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
        /// Security Type
        /// </summary>
        public enum SecurityTypeEnum : byte {
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
        public enum WiFiVersionEnum : byte {
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

        /// <summary>
        /// Association Failure Cause
        /// </summary>
        public enum AssociationFailureCause : byte {
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
        public enum ConnectionStatus : byte {
            /// <summary>
            /// Indicate the node is connected
            /// </summary>
            Connected = 0,
            /// <summary>
            /// Indicate the node is not connected
            /// </summary>
            NotConnected = 1,
        }
        #endregion Enums

        #region Payloads
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Reset Counts
        /// </summary>
        public async Task<bool> ResetCounts(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, null, token);
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
        /// BSSID Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> BSSID { get; init; }

        /// <summary>
        /// Security Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SecurityTypeEnum?> SecurityType { get; init; }

        /// <summary>
        /// WiFi Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<WiFiVersionEnum?> WiFiVersion { get; init; }

        /// <summary>
        /// Channel Number Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> ChannelNumber { get; init; }

        /// <summary>
        /// RSSI Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<sbyte?> RSSI { get; init; }

        /// <summary>
        /// Beacon Lost Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> BeaconLostCount { get; init; }

        /// <summary>
        /// Beacon Rx Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> BeaconRxCount { get; init; }

        /// <summary>
        /// Packet Multicast Rx Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> PacketMulticastRxCount { get; init; }

        /// <summary>
        /// Packet Multicast Tx Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> PacketMulticastTxCount { get; init; }

        /// <summary>
        /// Packet Unicast Rx Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> PacketUnicastRxCount { get; init; }

        /// <summary>
        /// Packet Unicast Tx Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> PacketUnicastTxCount { get; init; }

        /// <summary>
        /// Current Max Rate Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> CurrentMaxRate { get; init; }

        /// <summary>
        /// Overrun Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> OverrunCount { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Wi-Fi Network Diagnostics";
        }
    }
}