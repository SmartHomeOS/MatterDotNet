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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Thread Network Diagnostics Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ThreadNetworkDiagnosticsCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0035;

        /// <summary>
        /// Thread Network Diagnostics Cluster
        /// </summary>
        public ThreadNetworkDiagnosticsCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ThreadNetworkDiagnosticsCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Server supports the counts for the number of received and transmitted packets on the Thread interface.
            /// </summary>
            PacketCounts = 1,
            /// <summary>
            /// Server supports the counts for the number of errors that have occurred during the reception and transmission of packets on the Thread interface.
            /// </summary>
            ErrorCounts = 2,
            /// <summary>
            /// Server supports the counts for various MLE layer happenings.
            /// </summary>
            MLECounts = 4,
            /// <summary>
            /// Server supports the counts for various MAC layer happenings.
            /// </summary>
            MACCounts = 8,
        }

        /// <summary>
        /// Connection Status
        /// </summary>
        public enum ConnectionStatusEnum {
            /// <summary>
            /// Node is connected
            /// </summary>
            Connected = 0,
            /// <summary>
            /// Node is not connected
            /// </summary>
            NotConnected = 1,
        }

        /// <summary>
        /// Network Fault
        /// </summary>
        public enum NetworkFaultEnum {
            /// <summary>
            /// Indicates an unspecified fault.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// Indicates the Thread link is down.
            /// </summary>
            LinkDown = 1,
            /// <summary>
            /// Indicates there has been Thread hardware failure.
            /// </summary>
            HardwareFailure = 2,
            /// <summary>
            /// Indicates the Thread network is jammed.
            /// </summary>
            NetworkJammed = 3,
        }

        /// <summary>
        /// Routing Role
        /// </summary>
        public enum RoutingRoleEnum {
            /// <summary>
            /// Unspecified routing role.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The Node does not currently have a role as a result of the Thread interface not currently being configured or operational.
            /// </summary>
            Unassigned = 1,
            /// <summary>
            /// The Node acts as a Sleepy End Device with RX-off-when-idle sleepy radio behavior.
            /// </summary>
            SleepyEndDevice = 2,
            /// <summary>
            /// The Node acts as an End Device without RX-off-when-idle sleepy radio behavior.
            /// </summary>
            EndDevice = 3,
            /// <summary>
            /// The Node acts as an Router Eligible End Device.
            /// </summary>
            REED = 4,
            /// <summary>
            /// The Node acts as a Router Device.
            /// </summary>
            Router = 5,
            /// <summary>
            /// The Node acts as a Leader Device.
            /// </summary>
            Leader = 6,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Neighbor Table
        /// </summary>
        public record NeighborTable : TLVPayload {
            /// <summary>
            /// Neighbor Table
            /// </summary>
            public NeighborTable() { }

            [SetsRequiredMembers]
            internal NeighborTable(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ExtAddress = reader.GetULong(0)!.Value;
                Age = reader.GetUInt(1)!.Value;
                Rloc16 = reader.GetUShort(2)!.Value;
                LinkFrameCounter = reader.GetUInt(3)!.Value;
                MleFrameCounter = reader.GetUInt(4)!.Value;
                LQI = reader.GetByte(5)!.Value;
                AverageRssi = reader.GetSByte(6, true);
                LastRssi = reader.GetSByte(7, true);
                FrameErrorRate = reader.GetByte(8)!.Value;
                MessageErrorRate = reader.GetByte(9)!.Value;
                RxOnWhenIdle = reader.GetBool(10)!.Value;
                FullThreadDevice = reader.GetBool(11)!.Value;
                FullNetworkData = reader.GetBool(12)!.Value;
                IsChild = reader.GetBool(13)!.Value;
            }
            public required ulong ExtAddress { get; set; }
            public required uint Age { get; set; }
            public required ushort Rloc16 { get; set; }
            public required uint LinkFrameCounter { get; set; }
            public required uint MleFrameCounter { get; set; }
            public required byte LQI { get; set; }
            public required sbyte? AverageRssi { get; set; } = null;
            public required sbyte? LastRssi { get; set; } = null;
            public required byte FrameErrorRate { get; set; } = 0;
            public required byte MessageErrorRate { get; set; } = 0;
            public required bool RxOnWhenIdle { get; set; }
            public required bool FullThreadDevice { get; set; }
            public required bool FullNetworkData { get; set; }
            public required bool IsChild { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, ExtAddress);
                writer.WriteUInt(1, Age);
                writer.WriteUShort(2, Rloc16);
                writer.WriteUInt(3, LinkFrameCounter);
                writer.WriteUInt(4, MleFrameCounter);
                writer.WriteByte(5, LQI, 255);
                writer.WriteSByte(6, AverageRssi, 0, -128);
                writer.WriteSByte(7, LastRssi, 0, -128);
                writer.WriteByte(8, FrameErrorRate, 100);
                writer.WriteByte(9, MessageErrorRate, 100);
                writer.WriteBool(10, RxOnWhenIdle);
                writer.WriteBool(11, FullThreadDevice);
                writer.WriteBool(12, FullNetworkData);
                writer.WriteBool(13, IsChild);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Operational Dataset Components
        /// </summary>
        public record OperationalDatasetComponents : TLVPayload {
            /// <summary>
            /// Operational Dataset Components
            /// </summary>
            public OperationalDatasetComponents() { }

            [SetsRequiredMembers]
            internal OperationalDatasetComponents(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ActiveTimestampPresent = reader.GetBool(0)!.Value;
                PendingTimestampPresent = reader.GetBool(1)!.Value;
                MasterKeyPresent = reader.GetBool(2)!.Value;
                NetworkNamePresent = reader.GetBool(3)!.Value;
                ExtendedPanIdPresent = reader.GetBool(4)!.Value;
                MeshLocalPrefixPresent = reader.GetBool(5)!.Value;
                DelayPresent = reader.GetBool(6)!.Value;
                PanIdPresent = reader.GetBool(7)!.Value;
                ChannelPresent = reader.GetBool(8)!.Value;
                PskcPresent = reader.GetBool(9)!.Value;
                SecurityPolicyPresent = reader.GetBool(10)!.Value;
                ChannelMaskPresent = reader.GetBool(11)!.Value;
            }
            public required bool ActiveTimestampPresent { get; set; }
            public required bool PendingTimestampPresent { get; set; }
            public required bool MasterKeyPresent { get; set; }
            public required bool NetworkNamePresent { get; set; }
            public required bool ExtendedPanIdPresent { get; set; }
            public required bool MeshLocalPrefixPresent { get; set; }
            public required bool DelayPresent { get; set; }
            public required bool PanIdPresent { get; set; }
            public required bool ChannelPresent { get; set; }
            public required bool PskcPresent { get; set; }
            public required bool SecurityPolicyPresent { get; set; }
            public required bool ChannelMaskPresent { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBool(0, ActiveTimestampPresent);
                writer.WriteBool(1, PendingTimestampPresent);
                writer.WriteBool(2, MasterKeyPresent);
                writer.WriteBool(3, NetworkNamePresent);
                writer.WriteBool(4, ExtendedPanIdPresent);
                writer.WriteBool(5, MeshLocalPrefixPresent);
                writer.WriteBool(6, DelayPresent);
                writer.WriteBool(7, PanIdPresent);
                writer.WriteBool(8, ChannelPresent);
                writer.WriteBool(9, PskcPresent);
                writer.WriteBool(10, SecurityPolicyPresent);
                writer.WriteBool(11, ChannelMaskPresent);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Route Table
        /// </summary>
        public record RouteTable : TLVPayload {
            /// <summary>
            /// Route Table
            /// </summary>
            public RouteTable() { }

            [SetsRequiredMembers]
            internal RouteTable(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ExtAddress = reader.GetULong(0)!.Value;
                Rloc16 = reader.GetUShort(1)!.Value;
                RouterId = reader.GetByte(2)!.Value;
                NextHop = reader.GetByte(3)!.Value;
                PathCost = reader.GetByte(4)!.Value;
                LQIIn = reader.GetByte(5)!.Value;
                LQIOut = reader.GetByte(6)!.Value;
                Age = reader.GetByte(7)!.Value;
                Allocated = reader.GetBool(8)!.Value;
                LinkEstablished = reader.GetBool(9)!.Value;
            }
            public required ulong ExtAddress { get; set; }
            public required ushort Rloc16 { get; set; }
            public required byte RouterId { get; set; }
            public required byte NextHop { get; set; }
            public required byte PathCost { get; set; }
            public required byte LQIIn { get; set; }
            public required byte LQIOut { get; set; }
            public required byte Age { get; set; }
            public required bool Allocated { get; set; }
            public required bool LinkEstablished { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, ExtAddress);
                writer.WriteUShort(1, Rloc16);
                writer.WriteByte(2, RouterId);
                writer.WriteByte(3, NextHop);
                writer.WriteByte(4, PathCost);
                writer.WriteByte(5, LQIIn);
                writer.WriteByte(6, LQIOut);
                writer.WriteByte(7, Age);
                writer.WriteBool(8, Allocated);
                writer.WriteBool(9, LinkEstablished);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Security Policy
        /// </summary>
        public record SecurityPolicy : TLVPayload {
            /// <summary>
            /// Security Policy
            /// </summary>
            public SecurityPolicy() { }

            [SetsRequiredMembers]
            internal SecurityPolicy(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                RotationTime = reader.GetUShort(0)!.Value;
                Flags = reader.GetUShort(1)!.Value;
            }
            public required ushort RotationTime { get; set; }
            public required ushort Flags { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, RotationTime);
                writer.WriteUShort(1, Flags);
                writer.EndContainer();
            }
        }
        #endregion Records

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
        /// Get the Channel attribute
        /// </summary>
        public async Task<ushort?> GetChannel(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Routing Role attribute
        /// </summary>
        public async Task<RoutingRoleEnum?> GetRoutingRole(SecureSession session) {
            return (RoutingRoleEnum?)await GetEnumAttribute(session, 1, true);
        }

        /// <summary>
        /// Get the Network Name attribute
        /// </summary>
        public async Task<string?> GetNetworkName(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 2, true);
        }

        /// <summary>
        /// Get the Pan Id attribute
        /// </summary>
        public async Task<ushort?> GetPanId(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3, true);
        }

        /// <summary>
        /// Get the Extended Pan Id attribute
        /// </summary>
        public async Task<ulong?> GetExtendedPanId(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 4, true);
        }

        /// <summary>
        /// Get the Mesh Local Prefix attribute
        /// </summary>
        public async Task<byte[]?> GetMeshLocalPrefix(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 5, true);
        }

        /// <summary>
        /// Get the Overrun Count attribute
        /// </summary>
        public async Task<ulong> GetOverrunCount(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 6) ?? 0;
        }

        /// <summary>
        /// Get the Neighbor Table attribute
        /// </summary>
        public async Task<List<NeighborTable>> GetNeighborTable(SecureSession session) {
            List<NeighborTable> list = new List<NeighborTable>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 7))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new NeighborTable(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Get the Route Table attribute
        /// </summary>
        public async Task<List<RouteTable>> GetRouteTable(SecureSession session) {
            List<RouteTable> list = new List<RouteTable>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 8))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new RouteTable(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Get the Partition Id attribute
        /// </summary>
        public async Task<uint?> GetPartitionId(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 9, true);
        }

        /// <summary>
        /// Get the Weighting attribute
        /// </summary>
        public async Task<ushort?> GetWeighting(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 10, true);
        }

        /// <summary>
        /// Get the Data Version attribute
        /// </summary>
        public async Task<ushort?> GetDataVersion(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 11, true);
        }

        /// <summary>
        /// Get the Stable Data Version attribute
        /// </summary>
        public async Task<ushort?> GetStableDataVersion(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 12, true);
        }

        /// <summary>
        /// Get the Leader Router Id attribute
        /// </summary>
        public async Task<byte?> GetLeaderRouterId(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 13, true);
        }

        /// <summary>
        /// Get the Detached Role Count attribute
        /// </summary>
        public async Task<ushort> GetDetachedRoleCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 14) ?? 0;
        }

        /// <summary>
        /// Get the Child Role Count attribute
        /// </summary>
        public async Task<ushort> GetChildRoleCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 15) ?? 0;
        }

        /// <summary>
        /// Get the Router Role Count attribute
        /// </summary>
        public async Task<ushort> GetRouterRoleCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16) ?? 0;
        }

        /// <summary>
        /// Get the Leader Role Count attribute
        /// </summary>
        public async Task<ushort> GetLeaderRoleCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 17) ?? 0;
        }

        /// <summary>
        /// Get the Attach Attempt Count attribute
        /// </summary>
        public async Task<ushort> GetAttachAttemptCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 18) ?? 0;
        }

        /// <summary>
        /// Get the Partition Id Change Count attribute
        /// </summary>
        public async Task<ushort> GetPartitionIdChangeCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 19) ?? 0;
        }

        /// <summary>
        /// Get the Better Partition Attach Attempt Count attribute
        /// </summary>
        public async Task<ushort> GetBetterPartitionAttachAttemptCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 20) ?? 0;
        }

        /// <summary>
        /// Get the Parent Change Count attribute
        /// </summary>
        public async Task<ushort> GetParentChangeCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 21) ?? 0;
        }

        /// <summary>
        /// Get the Tx Total Count attribute
        /// </summary>
        public async Task<uint> GetTxTotalCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 22) ?? 0;
        }

        /// <summary>
        /// Get the Tx Unicast Count attribute
        /// </summary>
        public async Task<uint> GetTxUnicastCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 23) ?? 0;
        }

        /// <summary>
        /// Get the Tx Broadcast Count attribute
        /// </summary>
        public async Task<uint> GetTxBroadcastCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 24) ?? 0;
        }

        /// <summary>
        /// Get the Tx Ack Requested Count attribute
        /// </summary>
        public async Task<uint> GetTxAckRequestedCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 25) ?? 0;
        }

        /// <summary>
        /// Get the Tx Acked Count attribute
        /// </summary>
        public async Task<uint> GetTxAckedCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 26) ?? 0;
        }

        /// <summary>
        /// Get the Tx No Ack Requested Count attribute
        /// </summary>
        public async Task<uint> GetTxNoAckRequestedCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 27) ?? 0;
        }

        /// <summary>
        /// Get the Tx Data Count attribute
        /// </summary>
        public async Task<uint> GetTxDataCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 28) ?? 0;
        }

        /// <summary>
        /// Get the Tx Data Poll Count attribute
        /// </summary>
        public async Task<uint> GetTxDataPollCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 29) ?? 0;
        }

        /// <summary>
        /// Get the Tx Beacon Count attribute
        /// </summary>
        public async Task<uint> GetTxBeaconCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 30) ?? 0;
        }

        /// <summary>
        /// Get the Tx Beacon Request Count attribute
        /// </summary>
        public async Task<uint> GetTxBeaconRequestCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 31) ?? 0;
        }

        /// <summary>
        /// Get the Tx Other Count attribute
        /// </summary>
        public async Task<uint> GetTxOtherCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 32) ?? 0;
        }

        /// <summary>
        /// Get the Tx Retry Count attribute
        /// </summary>
        public async Task<uint> GetTxRetryCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 33) ?? 0;
        }

        /// <summary>
        /// Get the Tx Direct Max Retry Expiry Count attribute
        /// </summary>
        public async Task<uint> GetTxDirectMaxRetryExpiryCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 34) ?? 0;
        }

        /// <summary>
        /// Get the Tx Indirect Max Retry Expiry Count attribute
        /// </summary>
        public async Task<uint> GetTxIndirectMaxRetryExpiryCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 35) ?? 0;
        }

        /// <summary>
        /// Get the Tx Err Cca Count attribute
        /// </summary>
        public async Task<uint> GetTxErrCcaCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 36) ?? 0;
        }

        /// <summary>
        /// Get the Tx Err Abort Count attribute
        /// </summary>
        public async Task<uint> GetTxErrAbortCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 37) ?? 0;
        }

        /// <summary>
        /// Get the Tx Err Busy Channel Count attribute
        /// </summary>
        public async Task<uint> GetTxErrBusyChannelCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 38) ?? 0;
        }

        /// <summary>
        /// Get the Rx Total Count attribute
        /// </summary>
        public async Task<uint> GetRxTotalCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 39) ?? 0;
        }

        /// <summary>
        /// Get the Rx Unicast Count attribute
        /// </summary>
        public async Task<uint> GetRxUnicastCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 40) ?? 0;
        }

        /// <summary>
        /// Get the Rx Broadcast Count attribute
        /// </summary>
        public async Task<uint> GetRxBroadcastCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 41) ?? 0;
        }

        /// <summary>
        /// Get the Rx Data Count attribute
        /// </summary>
        public async Task<uint> GetRxDataCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 42) ?? 0;
        }

        /// <summary>
        /// Get the Rx Data Poll Count attribute
        /// </summary>
        public async Task<uint> GetRxDataPollCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 43) ?? 0;
        }

        /// <summary>
        /// Get the Rx Beacon Count attribute
        /// </summary>
        public async Task<uint> GetRxBeaconCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 44) ?? 0;
        }

        /// <summary>
        /// Get the Rx Beacon Request Count attribute
        /// </summary>
        public async Task<uint> GetRxBeaconRequestCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 45) ?? 0;
        }

        /// <summary>
        /// Get the Rx Other Count attribute
        /// </summary>
        public async Task<uint> GetRxOtherCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 46) ?? 0;
        }

        /// <summary>
        /// Get the Rx Address Filtered Count attribute
        /// </summary>
        public async Task<uint> GetRxAddressFilteredCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 47) ?? 0;
        }

        /// <summary>
        /// Get the Rx Dest Addr Filtered Count attribute
        /// </summary>
        public async Task<uint> GetRxDestAddrFilteredCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 48) ?? 0;
        }

        /// <summary>
        /// Get the Rx Duplicated Count attribute
        /// </summary>
        public async Task<uint> GetRxDuplicatedCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 49) ?? 0;
        }

        /// <summary>
        /// Get the Rx Err No Frame Count attribute
        /// </summary>
        public async Task<uint> GetRxErrNoFrameCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 50) ?? 0;
        }

        /// <summary>
        /// Get the Rx Err Unknown Neighbor Count attribute
        /// </summary>
        public async Task<uint> GetRxErrUnknownNeighborCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 51) ?? 0;
        }

        /// <summary>
        /// Get the Rx Err Invalid Src Addr Count attribute
        /// </summary>
        public async Task<uint> GetRxErrInvalidSrcAddrCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 52) ?? 0;
        }

        /// <summary>
        /// Get the Rx Err Sec Count attribute
        /// </summary>
        public async Task<uint> GetRxErrSecCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 53) ?? 0;
        }

        /// <summary>
        /// Get the Rx Err Fcs Count attribute
        /// </summary>
        public async Task<uint> GetRxErrFcsCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 54) ?? 0;
        }

        /// <summary>
        /// Get the Rx Err Other Count attribute
        /// </summary>
        public async Task<uint> GetRxErrOtherCount(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 55) ?? 0;
        }

        /// <summary>
        /// Get the Active Timestamp attribute
        /// </summary>
        public async Task<ulong?> GetActiveTimestamp(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 56, true) ?? 0;
        }

        /// <summary>
        /// Get the Pending Timestamp attribute
        /// </summary>
        public async Task<ulong?> GetPendingTimestamp(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 57, true) ?? 0;
        }

        /// <summary>
        /// Get the Delay attribute
        /// </summary>
        public async Task<uint?> GetDelay(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 58, true) ?? 0;
        }

        /// <summary>
        /// Get the Security Policy attribute
        /// </summary>
        public async Task<SecurityPolicy?> GetSecurityPolicy(SecureSession session) {
            return new SecurityPolicy((object[])(await GetAttribute(session, 59))!);
        }

        /// <summary>
        /// Get the Channel Page0 Mask attribute
        /// </summary>
        public async Task<byte[]?> GetChannelPage0Mask(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 60, true);
        }

        /// <summary>
        /// Get the Operational Dataset Components attribute
        /// </summary>
        public async Task<OperationalDatasetComponents?> GetOperationalDatasetComponents(SecureSession session) {
            return new OperationalDatasetComponents((object[])(await GetAttribute(session, 61))!);
        }

        /// <summary>
        /// Get the Active Network Faults List attribute
        /// </summary>
        public async Task<List<NetworkFaultEnum>> GetActiveNetworkFaultsList(SecureSession session) {
            List<NetworkFaultEnum> list = new List<NetworkFaultEnum>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 62))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add((NetworkFaultEnum)reader.GetUShort(i)!.Value);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thread Network Diagnostics Cluster";
        }
    }
}