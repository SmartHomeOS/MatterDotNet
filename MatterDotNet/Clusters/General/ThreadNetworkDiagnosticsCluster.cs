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
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Thread Network Diagnostics Cluster provides a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ThreadNetworkDiagnostics : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0035;

        /// <summary>
        /// The Thread Network Diagnostics Cluster provides a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems
        /// </summary>
        [SetsRequiredMembers]
        public ThreadNetworkDiagnostics(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ThreadNetworkDiagnostics(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Channel = new ReadAttribute<ushort?>(cluster, endPoint, 0, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            RoutingRole = new ReadAttribute<RoutingRoleEnum?>(cluster, endPoint, 1, true) {
                Deserialize = x => (RoutingRoleEnum?)DeserializeEnum(x)
            };
            NetworkName = new ReadAttribute<string?>(cluster, endPoint, 2, true) {
                Deserialize = x => (string?)(dynamic?)x
            };
            PanId = new ReadAttribute<ushort?>(cluster, endPoint, 3, true) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            ExtendedPanId = new ReadAttribute<ulong?>(cluster, endPoint, 4, true) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            MeshLocalPrefix = new ReadAttribute<byte[]?>(cluster, endPoint, 5, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            OverrunCount = new ReadAttribute<ulong>(cluster, endPoint, 6) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            NeighborTable = new ReadAttribute<NeighborTableStruct[]>(cluster, endPoint, 7) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    NeighborTableStruct[] list = new NeighborTableStruct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new NeighborTableStruct(reader.GetStruct(i)!);
                    return list;
                }
            };
            RouteTable = new ReadAttribute<RouteTableStruct[]>(cluster, endPoint, 8) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    RouteTableStruct[] list = new RouteTableStruct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new RouteTableStruct(reader.GetStruct(i)!);
                    return list;
                }
            };
            PartitionId = new ReadAttribute<uint?>(cluster, endPoint, 9, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            Weighting = new ReadAttribute<ushort?>(cluster, endPoint, 10, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            DataVersion = new ReadAttribute<ushort?>(cluster, endPoint, 11, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            StableDataVersion = new ReadAttribute<ushort?>(cluster, endPoint, 12, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            LeaderRouterId = new ReadAttribute<byte?>(cluster, endPoint, 13, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            DetachedRoleCount = new ReadAttribute<ushort>(cluster, endPoint, 14) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            ChildRoleCount = new ReadAttribute<ushort>(cluster, endPoint, 15) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            RouterRoleCount = new ReadAttribute<ushort>(cluster, endPoint, 16) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            LeaderRoleCount = new ReadAttribute<ushort>(cluster, endPoint, 17) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            AttachAttemptCount = new ReadAttribute<ushort>(cluster, endPoint, 18) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            PartitionIdChangeCount = new ReadAttribute<ushort>(cluster, endPoint, 19) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            BetterPartitionAttachAttemptCount = new ReadAttribute<ushort>(cluster, endPoint, 20) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            ParentChangeCount = new ReadAttribute<ushort>(cluster, endPoint, 21) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            TxTotalCount = new ReadAttribute<uint>(cluster, endPoint, 22) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxUnicastCount = new ReadAttribute<uint>(cluster, endPoint, 23) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxBroadcastCount = new ReadAttribute<uint>(cluster, endPoint, 24) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxAckRequestedCount = new ReadAttribute<uint>(cluster, endPoint, 25) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxAckedCount = new ReadAttribute<uint>(cluster, endPoint, 26) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxNoAckRequestedCount = new ReadAttribute<uint>(cluster, endPoint, 27) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxDataCount = new ReadAttribute<uint>(cluster, endPoint, 28) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxDataPollCount = new ReadAttribute<uint>(cluster, endPoint, 29) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxBeaconCount = new ReadAttribute<uint>(cluster, endPoint, 30) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxBeaconRequestCount = new ReadAttribute<uint>(cluster, endPoint, 31) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxOtherCount = new ReadAttribute<uint>(cluster, endPoint, 32) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxRetryCount = new ReadAttribute<uint>(cluster, endPoint, 33) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxDirectMaxRetryExpiryCount = new ReadAttribute<uint>(cluster, endPoint, 34) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxIndirectMaxRetryExpiryCount = new ReadAttribute<uint>(cluster, endPoint, 35) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxErrCcaCount = new ReadAttribute<uint>(cluster, endPoint, 36) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxErrAbortCount = new ReadAttribute<uint>(cluster, endPoint, 37) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            TxErrBusyChannelCount = new ReadAttribute<uint>(cluster, endPoint, 38) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxTotalCount = new ReadAttribute<uint>(cluster, endPoint, 39) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxUnicastCount = new ReadAttribute<uint>(cluster, endPoint, 40) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxBroadcastCount = new ReadAttribute<uint>(cluster, endPoint, 41) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxDataCount = new ReadAttribute<uint>(cluster, endPoint, 42) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxDataPollCount = new ReadAttribute<uint>(cluster, endPoint, 43) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxBeaconCount = new ReadAttribute<uint>(cluster, endPoint, 44) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxBeaconRequestCount = new ReadAttribute<uint>(cluster, endPoint, 45) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxOtherCount = new ReadAttribute<uint>(cluster, endPoint, 46) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxAddressFilteredCount = new ReadAttribute<uint>(cluster, endPoint, 47) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxDestAddrFilteredCount = new ReadAttribute<uint>(cluster, endPoint, 48) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxDuplicatedCount = new ReadAttribute<uint>(cluster, endPoint, 49) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxErrNoFrameCount = new ReadAttribute<uint>(cluster, endPoint, 50) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxErrUnknownNeighborCount = new ReadAttribute<uint>(cluster, endPoint, 51) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxErrInvalidSrcAddrCount = new ReadAttribute<uint>(cluster, endPoint, 52) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxErrSecCount = new ReadAttribute<uint>(cluster, endPoint, 53) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxErrFcsCount = new ReadAttribute<uint>(cluster, endPoint, 54) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            RxErrOtherCount = new ReadAttribute<uint>(cluster, endPoint, 55) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            ActiveTimestamp = new ReadAttribute<ulong?>(cluster, endPoint, 56, true) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            PendingTimestamp = new ReadAttribute<ulong?>(cluster, endPoint, 57, true) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            Delay = new ReadAttribute<uint?>(cluster, endPoint, 58, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x0000

            };
            SecurityPolicy = new ReadAttribute<SecurityPolicyStruct?>(cluster, endPoint, 59, true) {
                Deserialize = x => new SecurityPolicyStruct((object[])x!)
            };
            ChannelPage0Mask = new ReadAttribute<byte[]?>(cluster, endPoint, 60, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            OperationalDatasetComponents = new ReadAttribute<OperationalDatasetComponentsStruct?>(cluster, endPoint, 61, true) {
                Deserialize = x => new OperationalDatasetComponentsStruct((object[])x!)
            };
            ActiveNetworkFaultsList = new ReadAttribute<NetworkFault[]>(cluster, endPoint, 62) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    NetworkFault[] list = new NetworkFault[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (NetworkFault)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
        }

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
        /// Network Fault
        /// </summary>
        public enum NetworkFault : byte {
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
        public enum RoutingRoleEnum : byte {
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

        /// <summary>
        /// Connection Status
        /// </summary>
        public enum ConnectionStatus : byte {
            /// <summary>
            /// Node is connected
            /// </summary>
            Connected = 0,
            /// <summary>
            /// Node is not connected
            /// </summary>
            NotConnected = 1,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Neighbor Table
        /// </summary>
        public record NeighborTableStruct : TLVPayload {
            /// <summary>
            /// Neighbor Table
            /// </summary>
            public NeighborTableStruct() { }

            /// <summary>
            /// Neighbor Table
            /// </summary>
            [SetsRequiredMembers]
            public NeighborTableStruct(object[] fields) {
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
            public required sbyte? AverageRssi { get; set; }
            public required sbyte? LastRssi { get; set; }
            public required byte FrameErrorRate { get; set; }
            public required byte MessageErrorRate { get; set; }
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
                writer.WriteByte(5, LQI);
                writer.WriteSByte(6, AverageRssi);
                writer.WriteSByte(7, LastRssi);
                writer.WriteByte(8, FrameErrorRate);
                writer.WriteByte(9, MessageErrorRate);
                writer.WriteBool(10, RxOnWhenIdle);
                writer.WriteBool(11, FullThreadDevice);
                writer.WriteBool(12, FullNetworkData);
                writer.WriteBool(13, IsChild);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Route Table
        /// </summary>
        public record RouteTableStruct : TLVPayload {
            /// <summary>
            /// Route Table
            /// </summary>
            public RouteTableStruct() { }

            /// <summary>
            /// Route Table
            /// </summary>
            [SetsRequiredMembers]
            public RouteTableStruct(object[] fields) {
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
        public record SecurityPolicyStruct : TLVPayload {
            /// <summary>
            /// Security Policy
            /// </summary>
            public SecurityPolicyStruct() { }

            /// <summary>
            /// Security Policy
            /// </summary>
            [SetsRequiredMembers]
            public SecurityPolicyStruct(object[] fields) {
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

        /// <summary>
        /// Operational Dataset Components
        /// </summary>
        public record OperationalDatasetComponentsStruct : TLVPayload {
            /// <summary>
            /// Operational Dataset Components
            /// </summary>
            public OperationalDatasetComponentsStruct() { }

            /// <summary>
            /// Operational Dataset Components
            /// </summary>
            [SetsRequiredMembers]
            public OperationalDatasetComponentsStruct(object[] fields) {
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
        #endregion Records

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
        /// Channel Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> Channel { get; init; }

        /// <summary>
        /// Routing Role Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<RoutingRoleEnum?> RoutingRole { get; init; }

        /// <summary>
        /// Network Name Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string?> NetworkName { get; init; }

        /// <summary>
        /// Pan Id Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> PanId { get; init; }

        /// <summary>
        /// Extended Pan Id Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> ExtendedPanId { get; init; }

        /// <summary>
        /// Mesh Local Prefix Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> MeshLocalPrefix { get; init; }

        /// <summary>
        /// Overrun Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> OverrunCount { get; init; }

        /// <summary>
        /// Neighbor Table Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<NeighborTableStruct[]> NeighborTable { get; init; }

        /// <summary>
        /// Route Table Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<RouteTableStruct[]> RouteTable { get; init; }

        /// <summary>
        /// Partition Id Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> PartitionId { get; init; }

        /// <summary>
        /// Weighting Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> Weighting { get; init; }

        /// <summary>
        /// Data Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> DataVersion { get; init; }

        /// <summary>
        /// Stable Data Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> StableDataVersion { get; init; }

        /// <summary>
        /// Leader Router Id Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> LeaderRouterId { get; init; }

        /// <summary>
        /// Detached Role Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> DetachedRoleCount { get; init; }

        /// <summary>
        /// Child Role Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> ChildRoleCount { get; init; }

        /// <summary>
        /// Router Role Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> RouterRoleCount { get; init; }

        /// <summary>
        /// Leader Role Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> LeaderRoleCount { get; init; }

        /// <summary>
        /// Attach Attempt Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> AttachAttemptCount { get; init; }

        /// <summary>
        /// Partition Id Change Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> PartitionIdChangeCount { get; init; }

        /// <summary>
        /// Better Partition Attach Attempt Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> BetterPartitionAttachAttemptCount { get; init; }

        /// <summary>
        /// Parent Change Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> ParentChangeCount { get; init; }

        /// <summary>
        /// Tx Total Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxTotalCount { get; init; }

        /// <summary>
        /// Tx Unicast Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxUnicastCount { get; init; }

        /// <summary>
        /// Tx Broadcast Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxBroadcastCount { get; init; }

        /// <summary>
        /// Tx Ack Requested Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxAckRequestedCount { get; init; }

        /// <summary>
        /// Tx Acked Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxAckedCount { get; init; }

        /// <summary>
        /// Tx No Ack Requested Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxNoAckRequestedCount { get; init; }

        /// <summary>
        /// Tx Data Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxDataCount { get; init; }

        /// <summary>
        /// Tx Data Poll Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxDataPollCount { get; init; }

        /// <summary>
        /// Tx Beacon Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxBeaconCount { get; init; }

        /// <summary>
        /// Tx Beacon Request Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxBeaconRequestCount { get; init; }

        /// <summary>
        /// Tx Other Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxOtherCount { get; init; }

        /// <summary>
        /// Tx Retry Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxRetryCount { get; init; }

        /// <summary>
        /// Tx Direct Max Retry Expiry Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxDirectMaxRetryExpiryCount { get; init; }

        /// <summary>
        /// Tx Indirect Max Retry Expiry Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxIndirectMaxRetryExpiryCount { get; init; }

        /// <summary>
        /// Tx Err Cca Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxErrCcaCount { get; init; }

        /// <summary>
        /// Tx Err Abort Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxErrAbortCount { get; init; }

        /// <summary>
        /// Tx Err Busy Channel Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TxErrBusyChannelCount { get; init; }

        /// <summary>
        /// Rx Total Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxTotalCount { get; init; }

        /// <summary>
        /// Rx Unicast Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxUnicastCount { get; init; }

        /// <summary>
        /// Rx Broadcast Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxBroadcastCount { get; init; }

        /// <summary>
        /// Rx Data Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxDataCount { get; init; }

        /// <summary>
        /// Rx Data Poll Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxDataPollCount { get; init; }

        /// <summary>
        /// Rx Beacon Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxBeaconCount { get; init; }

        /// <summary>
        /// Rx Beacon Request Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxBeaconRequestCount { get; init; }

        /// <summary>
        /// Rx Other Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxOtherCount { get; init; }

        /// <summary>
        /// Rx Address Filtered Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxAddressFilteredCount { get; init; }

        /// <summary>
        /// Rx Dest Addr Filtered Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxDestAddrFilteredCount { get; init; }

        /// <summary>
        /// Rx Duplicated Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxDuplicatedCount { get; init; }

        /// <summary>
        /// Rx Err No Frame Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxErrNoFrameCount { get; init; }

        /// <summary>
        /// Rx Err Unknown Neighbor Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxErrUnknownNeighborCount { get; init; }

        /// <summary>
        /// Rx Err Invalid Src Addr Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxErrInvalidSrcAddrCount { get; init; }

        /// <summary>
        /// Rx Err Sec Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxErrSecCount { get; init; }

        /// <summary>
        /// Rx Err Fcs Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxErrFcsCount { get; init; }

        /// <summary>
        /// Rx Err Other Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> RxErrOtherCount { get; init; }

        /// <summary>
        /// Active Timestamp Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> ActiveTimestamp { get; init; }

        /// <summary>
        /// Pending Timestamp Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> PendingTimestamp { get; init; }

        /// <summary>
        /// Delay Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> Delay { get; init; }

        /// <summary>
        /// Security Policy Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SecurityPolicyStruct?> SecurityPolicy { get; init; }

        /// <summary>
        /// Channel Page0 Mask Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> ChannelPage0Mask { get; init; }

        /// <summary>
        /// Operational Dataset Components Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<OperationalDatasetComponentsStruct?> OperationalDatasetComponents { get; init; }

        /// <summary>
        /// Active Network Faults List Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<NetworkFault[]> ActiveNetworkFaultsList { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thread Network Diagnostics";
        }
    }
}