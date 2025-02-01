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
    /// The Ethernet Network Diagnostics Cluster provides a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class EthernetNetworkDiagnostics : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0037;

        /// <summary>
        /// The Ethernet Network Diagnostics Cluster provides a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems.
        /// </summary>
        [SetsRequiredMembers]
        public EthernetNetworkDiagnostics(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected EthernetNetworkDiagnostics(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            PHYRate = new ReadAttribute<PHYRateEnum?>(cluster, endPoint, 0, true) {
                Deserialize = x => (PHYRateEnum?)DeserializeEnum(x)
            };
            FullDuplex = new ReadAttribute<bool?>(cluster, endPoint, 1, true) {
                Deserialize = x => (bool?)(dynamic?)x
            };
            PacketRxCount = new ReadAttribute<ulong>(cluster, endPoint, 2) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            PacketTxCount = new ReadAttribute<ulong>(cluster, endPoint, 3) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            TxErrCount = new ReadAttribute<ulong>(cluster, endPoint, 4) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            CollisionCount = new ReadAttribute<ulong>(cluster, endPoint, 5) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            OverrunCount = new ReadAttribute<ulong>(cluster, endPoint, 6) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            CarrierDetect = new ReadAttribute<bool?>(cluster, endPoint, 7, true) {
                Deserialize = x => (bool?)(dynamic?)x
            };
            TimeSinceReset = new ReadAttribute<ulong>(cluster, endPoint, 8) {
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
        /// PHY Rate
        /// </summary>
        public enum PHYRateEnum : byte {
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
        /// PHY Rate Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<PHYRateEnum?> PHYRate { get; init; }

        /// <summary>
        /// Full Duplex Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool?> FullDuplex { get; init; }

        /// <summary>
        /// Packet Rx Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> PacketRxCount { get; init; }

        /// <summary>
        /// Packet Tx Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> PacketTxCount { get; init; }

        /// <summary>
        /// Tx Err Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> TxErrCount { get; init; }

        /// <summary>
        /// Collision Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> CollisionCount { get; init; }

        /// <summary>
        /// Overrun Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> OverrunCount { get; init; }

        /// <summary>
        /// Carrier Detect Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool?> CarrierDetect { get; init; }

        /// <summary>
        /// Time Since Reset Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> TimeSinceReset { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Ethernet Network Diagnostics";
        }
    }
}