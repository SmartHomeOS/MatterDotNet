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
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Group Key Management Cluster is the mechanism by which group keys are managed.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class GroupKeyManagement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x003F;

        /// <summary>
        /// The Group Key Management Cluster is the mechanism by which group keys are managed.
        /// </summary>
        [SetsRequiredMembers]
        public GroupKeyManagement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected GroupKeyManagement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            GroupKeyMap = new ReadWriteAttribute<GroupKeyMapStruct[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    GroupKeyMapStruct[] list = new GroupKeyMapStruct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new GroupKeyMapStruct(reader.GetStruct(i)!);
                    return list;
                }
            };
            GroupTable = new ReadAttribute<GroupInfoMap[]>(cluster, endPoint, 1) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    GroupInfoMap[] list = new GroupInfoMap[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new GroupInfoMap(reader.GetStruct(i)!);
                    return list;
                }
            };
            MaxGroupsPerFabric = new ReadAttribute<ushort>(cluster, endPoint, 2) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            MaxGroupKeysPerFabric = new ReadAttribute<ushort>(cluster, endPoint, 3) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// The ability to support CacheAndSync security policy and MCSP.
            /// </summary>
            CacheAndSync = 1,
        }

        /// <summary>
        /// Group Key Security Policy
        /// </summary>
        public enum GroupKeySecurityPolicy : byte {
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
        /// <summary>
        /// Group Key Map
        /// </summary>
        public record GroupKeyMapStruct : TLVPayload {
            /// <summary>
            /// Group Key Map
            /// </summary>
            public GroupKeyMapStruct() { }

            /// <summary>
            /// Group Key Map
            /// </summary>
            [SetsRequiredMembers]
            public GroupKeyMapStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                GroupId = reader.GetUShort(1)!.Value;
                GroupKeySetID = reader.GetUShort(2)!.Value;
            }
            public required ushort GroupId { get; set; }
            public required ushort GroupKeySetID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, GroupId);
                writer.WriteUShort(2, GroupKeySetID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Group Info Map
        /// </summary>
        public record GroupInfoMap : TLVPayload {
            /// <summary>
            /// Group Info Map
            /// </summary>
            public GroupInfoMap() { }

            /// <summary>
            /// Group Info Map
            /// </summary>
            [SetsRequiredMembers]
            public GroupInfoMap(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                GroupId = reader.GetUShort(1)!.Value;
                {
                    Endpoints = new ushort[reader.GetStruct(2)!.Length];
                    for (int n = 0; n < Endpoints.Length; n++) {
                        Endpoints[n] = reader.GetUShort(n)!.Value;
                    }
                }
                GroupName = reader.GetString(3, true, 16);
            }
            public required ushort GroupId { get; set; }
            public required ushort[] Endpoints { get; set; }
            public string? GroupName { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, GroupId);
                {
                    writer.StartArray(2);
                    foreach (var item in Endpoints) {
                        writer.WriteUShort(-1, item);
                    }
                    writer.EndContainer();
                }
                if (GroupName != null)
                    writer.WriteString(3, GroupName, 16);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Group Key Set
        /// </summary>
        public record GroupKeySet : TLVPayload {
            /// <summary>
            /// Group Key Set
            /// </summary>
            public GroupKeySet() { }

            /// <summary>
            /// Group Key Set
            /// </summary>
            [SetsRequiredMembers]
            public GroupKeySet(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                GroupKeySetID = reader.GetUShort(0)!.Value;
                GroupKeySecurityPolicy = (GroupKeySecurityPolicy)reader.GetUShort(1)!.Value;
                EpochKey0 = reader.GetBytes(2, false, 16)!;
                EpochStartTime0 = TimeUtil.FromEpochUS(reader.GetULong(3, true));
                EpochKey1 = reader.GetBytes(4, false, 16)!;
                EpochStartTime1 = TimeUtil.FromEpochUS(reader.GetULong(5, true));
                EpochKey2 = reader.GetBytes(6, false, 16)!;
                EpochStartTime2 = TimeUtil.FromEpochUS(reader.GetULong(7, true));
            }
            public required ushort GroupKeySetID { get; set; }
            public required GroupKeySecurityPolicy GroupKeySecurityPolicy { get; set; }
            public required byte[]? EpochKey0 { get; set; }
            public required DateTime? EpochStartTime0 { get; set; }
            public required byte[]? EpochKey1 { get; set; }
            public required DateTime? EpochStartTime1 { get; set; }
            public required byte[]? EpochKey2 { get; set; }
            public required DateTime? EpochStartTime2 { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.WriteUShort(1, (ushort)GroupKeySecurityPolicy);
                writer.WriteBytes(2, EpochKey0, 16);
                if (!EpochStartTime0.HasValue)
                    writer.WriteNull(3);
                else
                    writer.WriteULong(3, TimeUtil.ToEpochUS(EpochStartTime0!.Value));
                writer.WriteBytes(4, EpochKey1, 16);
                if (!EpochStartTime1.HasValue)
                    writer.WriteNull(5);
                else
                    writer.WriteULong(5, TimeUtil.ToEpochUS(EpochStartTime1!.Value));
                writer.WriteBytes(6, EpochKey2, 16);
                if (!EpochStartTime2.HasValue)
                    writer.WriteNull(7);
                else
                    writer.WriteULong(7, TimeUtil.ToEpochUS(EpochStartTime2!.Value));
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record KeySetWritePayload : TLVPayload {
            public required GroupKeySet GroupKeySet { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                GroupKeySet.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record KeySetReadPayload : TLVPayload {
            public required ushort GroupKeySetID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Key Set Read Response - Reply from server
        /// </summary>
        public struct KeySetReadResponse() {
            public required GroupKeySet GroupKeySet { get; set; }
        }

        private record KeySetRemovePayload : TLVPayload {
            public required ushort GroupKeySetID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Key Set Read All Indices Response - Reply from server
        /// </summary>
        public struct KeySetReadAllIndicesResponse() {
            public required ushort[] GroupKeySetIDs { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Key Set Write
        /// </summary>
        public async Task<bool> KeySetWrite(SecureSession session, GroupKeySet groupKeySet) {
            KeySetWritePayload requestFields = new KeySetWritePayload() {
                GroupKeySet = groupKeySet,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Key Set Read
        /// </summary>
        public async Task<KeySetReadResponse?> KeySetRead(SecureSession session, ushort groupKeySetID) {
            KeySetReadPayload requestFields = new KeySetReadPayload() {
                GroupKeySetID = groupKeySetID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new KeySetReadResponse() {
                GroupKeySet = (GroupKeySet)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Key Set Remove
        /// </summary>
        public async Task<bool> KeySetRemove(SecureSession session, ushort groupKeySetID) {
            KeySetRemovePayload requestFields = new KeySetRemovePayload() {
                GroupKeySetID = groupKeySetID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Key Set Read All Indices
        /// </summary>
        public async Task<KeySetReadAllIndicesResponse?> KeySetReadAllIndices(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04);
            if (!ValidateResponse(resp))
                return null;
            return new KeySetReadAllIndicesResponse() {
                GroupKeySetIDs = (ushort[])GetField(resp, 0),
            };
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
        /// Group Key Map Attribute
        /// </summary>
        public required ReadWriteAttribute<GroupKeyMapStruct[]> GroupKeyMap { get; init; }

        /// <summary>
        /// Group Table Attribute
        /// </summary>
        public required ReadAttribute<GroupInfoMap[]> GroupTable { get; init; }

        /// <summary>
        /// Max Groups Per Fabric Attribute
        /// </summary>
        public required ReadAttribute<ushort> MaxGroupsPerFabric { get; init; }

        /// <summary>
        /// Max Group Keys Per Fabric Attribute
        /// </summary>
        public required ReadAttribute<ushort> MaxGroupKeysPerFabric { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Group Key Management";
        }
    }
}