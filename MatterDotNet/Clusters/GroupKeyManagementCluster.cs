// MatterDotNet Copyright (C) 2024 
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
using MatterDotNet.Protocol;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// Group Key Management Cluster
    /// </summary>
    public class GroupKeyManagementCluster : ClusterBase
    {
        private const uint CLUSTER_ID = 0x003F;

        /// <summary>
        /// Group Key Management Cluster
        /// </summary>
        public GroupKeyManagementCluster(ushort endPoint) : base(endPoint) { }

        #region Enums
        /// <summary>
        /// Group Key Multicast Policy
        /// </summary>
        public enum GroupKeyMulticastPolicyEnum {
            /// <summary>
            /// Indicates filtering of multicast messages for a specific Group ID
            /// </summary>
            PerGroupID = 0,
            /// <summary>
            /// Indicates not filtering of multicast messages
            /// </summary>
            AllNodes = 1,
        }

        /// <summary>
        /// Group Key Security Policy
        /// </summary>
        public enum GroupKeySecurityPolicyEnum {
            /// <summary>
            /// Message counter synchronization using trust-first
            /// </summary>
            TrustFirst = 0,
            /// <summary>
            /// Message counter synchronization using cache-and-sync
            /// </summary>
            CacheAndSync = 1,
        }
        #endregion Enums

        #region Records
        public record GroupInfoMap : TLVPayload {
            public required ushort GroupId { get; set; }
            public required List<ushort> Endpoints { get; set; }
            public required string GroupName { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, GroupId);
                {
                    writer.StartList(2);
                    foreach (var item in Endpoints) {
                    }
                    writer.EndContainer();
                }
                writer.WriteString(3, GroupName);
                writer.EndContainer();
            }
        }

        public record GroupKeyMap : TLVPayload {
            public required ushort GroupId { get; set; }
            public required ushort GroupKeySetID { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, GroupId);
                writer.WriteUShort(2, GroupKeySetID);
                writer.EndContainer();
            }
        }

        public record GroupKeySet : TLVPayload {
            public required ushort GroupKeySetID { get; set; }
            public required GroupKeySecurityPolicyEnum GroupKeySecurityPolicy { get; set; }
            public required byte[] EpochKey0 { get; set; }
            public required ulong EpochStartTime0 { get; set; }
            public required byte[] EpochKey1 { get; set; }
            public required ulong EpochStartTime1 { get; set; }
            public required byte[] EpochKey2 { get; set; }
            public required ulong EpochStartTime2 { get; set; }
            public required GroupKeyMulticastPolicyEnum GroupKeyMulticastPolicy { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.WriteUShort(1, (ushort)GroupKeySecurityPolicy);
                writer.WriteBytes(2, EpochKey0);
                writer.WriteULong(3, EpochStartTime0);
                writer.WriteBytes(4, EpochKey1);
                writer.WriteULong(5, EpochStartTime1);
                writer.WriteBytes(6, EpochKey2);
                writer.WriteULong(7, EpochStartTime2);
                writer.WriteUShort(8, (ushort)GroupKeyMulticastPolicy);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record KeySetWriteCommandPayload : TLVPayload {
            public required GroupKeySet GroupKeySet { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                GroupKeySet.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record KeySetReadCommandPayload : TLVPayload {
            public required ushort GroupKeySetID { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.EndContainer();
            }
        }

        public struct KeySetReadResponseCommand() {
            public required GroupKeySet GroupKeySet { get; set; }
        }

        private record KeySetRemoveCommandPayload : TLVPayload {
            public required ushort GroupKeySetID { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.EndContainer();
            }
        }

        private record KeySetReadAllIndicesCommandPayload : TLVPayload {
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.EndContainer();
            }
        }

        public struct KeySetReadAllIndicesResponseCommand() {
            public required List<ushort> GroupKeySetIDs { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Key Set Write  Command
        /// </summary>
        public async Task<bool> KeySetWriteCommand(SecureSession session, GroupKeySet GroupKeySet) {
            KeySetWriteCommandPayload requestFields = new KeySetWriteCommandPayload() {
                GroupKeySet = GroupKeySet,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            return validateResponse(resp);
        }

        /// <summary>
        /// Key Set Read  Command
        /// </summary>
        public async Task<KeySetReadResponseCommand?> KeySetReadCommand(SecureSession session, ushort GroupKeySetID) {
            KeySetReadCommandPayload requestFields = new KeySetReadCommandPayload() {
                GroupKeySetID = GroupKeySetID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x01, requestFields);
            if (!validateResponse(resp))
                return null;
            return new KeySetReadResponseCommand() {
                GroupKeySet = (GroupKeySet)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Key Set Remove  Command
        /// </summary>
        public async Task<bool> KeySetRemoveCommand(SecureSession session, ushort GroupKeySetID) {
            KeySetRemoveCommandPayload requestFields = new KeySetRemoveCommandPayload() {
                GroupKeySetID = GroupKeySetID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x03, requestFields);
            return validateResponse(resp);
        }

        /// <summary>
        /// Key Set Read All Indices  Command
        /// </summary>
        public async Task<KeySetReadAllIndicesResponseCommand?> KeySetReadAllIndicesCommand(SecureSession session) {
            KeySetReadAllIndicesCommandPayload requestFields = new KeySetReadAllIndicesCommandPayload() {
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x04, requestFields);
            if (!validateResponse(resp))
                return null;
            return new KeySetReadAllIndicesResponseCommand() {
                GroupKeySetIDs = (List<ushort>)GetField(resp, 0),
            };
        }
        #endregion Commands

        #region Attributes
        public List<GroupKeyMap> GroupKeyMapField { get; set; } = [];

        public List<GroupInfoMap> GroupTable { get; } = [];

        public ushort MaxGroupsPerFabric { get; } = 0;

        public ushort MaxGroupKeysPerFabric { get; } = 1;
        #endregion Attributes
    }
}