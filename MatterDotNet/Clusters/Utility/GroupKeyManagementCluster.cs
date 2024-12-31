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
using MatterDotNet.Protocol;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Utility
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
        public GroupKeyManagementCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }

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
        /// <summary>
        /// Group Info Map
        /// </summary>
        public record GroupInfoMap : TLVPayload {
            [SetsRequiredMembers]
            internal GroupInfoMap(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                GroupId = reader.GetUShort(1)!.Value;
                {
                    Endpoints = new List<ushort>();
                    foreach (var item in (List<object>)fields[2]) {
                        Endpoints.Add(reader.GetUShort(-1)!.Value);
                    }
                }
                GroupName = reader.GetString(3, true);
            }
            public required ushort GroupId { get; set; }
            public required List<ushort> Endpoints { get; set; }
            public string? GroupName { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, GroupId);
                {
                    Constrain(Endpoints, 1);
                    writer.StartList(2);
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
        /// Group Key Map
        /// </summary>
        public record GroupKeyMap : TLVPayload {
            [SetsRequiredMembers]
            internal GroupKeyMap(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                GroupId = reader.GetUShort(1)!.Value;
                GroupKeySetID = reader.GetUShort(2)!.Value;
            }
            public required ushort GroupId { get; set; }
            public required ushort GroupKeySetID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, GroupId);
                writer.WriteUShort(2, GroupKeySetID, 65535, 1);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Group Key Set
        /// </summary>
        public record GroupKeySet : TLVPayload {
            [SetsRequiredMembers]
            internal GroupKeySet(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                GroupKeySetID = reader.GetUShort(0)!.Value;
                GroupKeySecurityPolicy = (GroupKeySecurityPolicyEnum)reader.GetUShort(1)!.Value;
                EpochKey0 = reader.GetBytes(2, false)!;
                EpochStartTime0 = reader.GetULong(3)!.Value;
                EpochKey1 = reader.GetBytes(4, false)!;
                EpochStartTime1 = reader.GetULong(5)!.Value;
                EpochKey2 = reader.GetBytes(6, false)!;
                EpochStartTime2 = reader.GetULong(7)!.Value;
                GroupKeyMulticastPolicy = (GroupKeyMulticastPolicyEnum)reader.GetUShort(8, true)!.Value;
            }
            public required ushort GroupKeySetID { get; set; }
            public required GroupKeySecurityPolicyEnum GroupKeySecurityPolicy { get; set; }
            public required byte[]? EpochKey0 { get; set; }
            public required ulong? EpochStartTime0 { get; set; }
            public required byte[]? EpochKey1 { get; set; }
            public required ulong? EpochStartTime1 { get; set; }
            public required byte[]? EpochKey2 { get; set; }
            public required ulong? EpochStartTime2 { get; set; }
            public GroupKeyMulticastPolicyEnum? GroupKeyMulticastPolicy { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.WriteUShort(1, (ushort)GroupKeySecurityPolicy);
                writer.WriteBytes(2, EpochKey0, 16);
                writer.WriteULong(3, EpochStartTime0);
                writer.WriteBytes(4, EpochKey1, 16);
                writer.WriteULong(5, EpochStartTime1);
                writer.WriteBytes(6, EpochKey2, 16);
                writer.WriteULong(7, EpochStartTime2);
                if (GroupKeyMulticastPolicy != null)
                    writer.WriteUShort(8, (ushort)GroupKeyMulticastPolicy);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record KeySetWriteCommandPayload : TLVPayload {
            public required GroupKeySet GroupKeySet { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                GroupKeySet.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record KeySetReadCommandPayload : TLVPayload {
            public required ushort GroupKeySetID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Key Set Read Response  Command - Reply from server
        /// </summary>
        public struct KeySetReadResponseCommand() {
            public required GroupKeySet GroupKeySet { get; set; }
        }

        private record KeySetRemoveCommandPayload : TLVPayload {
            public required ushort GroupKeySetID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupKeySetID);
                writer.EndContainer();
            }
        }

        private record KeySetReadAllIndicesCommandPayload : TLVPayload {
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Key Set Read All Indices Response  Command - Reply from server
        /// </summary>
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
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Key Set Read  Command
        /// </summary>
        public async Task<KeySetReadResponseCommand?> KeySetReadCommand(SecureSession session, ushort GroupKeySetID) {
            KeySetReadCommandPayload requestFields = new KeySetReadCommandPayload() {
                GroupKeySetID = GroupKeySetID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x01, requestFields);
            if (!ValidateResponse(resp))
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
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Key Set Read All Indices  Command
        /// </summary>
        public async Task<KeySetReadAllIndicesResponseCommand?> KeySetReadAllIndicesCommand(SecureSession session) {
            KeySetReadAllIndicesCommandPayload requestFields = new KeySetReadAllIndicesCommandPayload() {
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x04, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new KeySetReadAllIndicesResponseCommand() {
                GroupKeySetIDs = (List<ushort>)GetField(resp, 0),
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
        /// Get the Group Key Map attribute
        /// </summary>
        public async Task<List<GroupKeyMap>> GetGroupKeyMap (SecureSession session) {
            return (List<GroupKeyMap>?)(dynamic?)await GetAttribute(session, 0) ?? new List<GroupKeyMap>();
        }

        /// <summary>
        /// Set the Group Key Map attribute
        /// </summary>
        public async Task SetGroupKeyMap (SecureSession session, List<GroupKeyMap>? value) {
            await SetAttribute(session, 0, value ?? new List<GroupKeyMap>());
        }

        /// <summary>
        /// Get the Group Table attribute
        /// </summary>
        public async Task<List<GroupInfoMap>> GetGroupTable (SecureSession session) {
            return (List<GroupInfoMap>?)(dynamic?)await GetAttribute(session, 1) ?? new List<GroupInfoMap>();
        }

        /// <summary>
        /// Get the Max Groups Per Fabric attribute
        /// </summary>
        public async Task<ushort> GetMaxGroupsPerFabric (SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2) ?? 0;
        }

        /// <summary>
        /// Get the Max Group Keys Per Fabric attribute
        /// </summary>
        public async Task<ushort> GetMaxGroupKeysPerFabric (SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3) ?? 1;
        }
        #endregion Attributes
    }
}