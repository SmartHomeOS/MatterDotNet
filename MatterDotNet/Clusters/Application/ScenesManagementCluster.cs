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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Scenes Management Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ScenesManagementCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0062;

        /// <summary>
        /// Scenes Management Cluster
        /// </summary>
        public ScenesManagementCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ScenesManagementCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// The ability to store a name for a scene.
            /// </summary>
            SceneNames = 1,
        }

        /// <summary>
        /// Copy Mode Bitmap
        /// </summary>
        [Flags]
        public enum CopyModeBitmap {
            /// <summary>
            /// Copy all scenes in the scene table
            /// </summary>
            CopyAllScenes = 1,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Attribute Value Pair
        /// </summary>
        public record AttributeValuePair : TLVPayload {
            /// <summary>
            /// Attribute Value Pair
            /// </summary>
            public AttributeValuePair() { }

            [SetsRequiredMembers]
            internal AttributeValuePair(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                AttributeID = reader.GetUInt(0)!.Value;
                ValueUnsigned8 = reader.GetByte(1, true);
                ValueSigned8 = reader.GetSByte(2, true);
                ValueUnsigned16 = reader.GetUShort(3, true);
                ValueSigned16 = reader.GetShort(4, true);
                ValueUnsigned32 = reader.GetUInt(5, true);
                ValueSigned32 = reader.GetInt(6, true);
                ValueUnsigned64 = reader.GetULong(7, true);
                ValueSigned64 = reader.GetLong(8, true);
            }
            public required uint AttributeID { get; set; }
            public byte? ValueUnsigned8 { get; set; }
            public sbyte? ValueSigned8 { get; set; }
            public ushort? ValueUnsigned16 { get; set; }
            public short? ValueSigned16 { get; set; }
            public uint? ValueUnsigned32 { get; set; }
            public int ? ValueSigned32 { get; set; }
            public ulong? ValueUnsigned64 { get; set; }
            public long? ValueSigned64 { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, AttributeID);
                if (ValueUnsigned8 != null)
                    writer.WriteByte(1, ValueUnsigned8);
                if (ValueSigned8 != null)
                    writer.WriteSByte(2, ValueSigned8);
                if (ValueUnsigned16 != null)
                    writer.WriteUShort(3, ValueUnsigned16);
                if (ValueSigned16 != null)
                    writer.WriteShort(4, ValueSigned16);
                if (ValueUnsigned32 != null)
                    writer.WriteUInt(5, ValueUnsigned32);
                if (ValueSigned32 != null)
                    writer.WriteInt(6, ValueSigned32);
                if (ValueUnsigned64 != null)
                    writer.WriteULong(7, ValueUnsigned64);
                if (ValueSigned64 != null)
                    writer.WriteLong(8, ValueSigned64);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Extension Field Set
        /// </summary>
        public record ExtensionFieldSet : TLVPayload {
            /// <summary>
            /// Extension Field Set
            /// </summary>
            public ExtensionFieldSet() { }

            [SetsRequiredMembers]
            internal ExtensionFieldSet(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ClusterID = reader.GetUInt(0)!.Value;
                {
                    AttributeValueList = new List<AttributeValuePair>();
                    foreach (var item in (List<object>)fields[1]) {
                        AttributeValueList.Add(new AttributeValuePair((object[])item));
                    }
                }
            }
            public required uint ClusterID { get; set; }
            public required List<AttributeValuePair> AttributeValueList { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, ClusterID);
                {
                    writer.StartList(1);
                    foreach (var item in AttributeValueList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Logical  Scene  Table
        /// </summary>
        public record LogicalSceneTable : TLVPayload {
            /// <summary>
            /// Logical  Scene  Table
            /// </summary>
            public LogicalSceneTable() { }

            [SetsRequiredMembers]
            internal LogicalSceneTable(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                SceneGroupID = reader.GetUShort(0)!.Value;
                SceneID = reader.GetByte(1)!.Value;
                SceneName = reader.GetString(2, false)!;
                SceneTransitionTime = reader.GetUInt(3)!.Value;
                {
                    ExtensionFields = new List<ExtensionFieldSet>();
                    foreach (var item in (List<object>)fields[4]) {
                        ExtensionFields.Add(new ExtensionFieldSet((object[])item));
                    }
                }
            }
            public required ushort SceneGroupID { get; set; }
            public required byte SceneID { get; set; }
            public required string SceneName { get; set; }
            public required uint SceneTransitionTime { get; set; } = 0;
            public required List<ExtensionFieldSet> ExtensionFields { get; set; } = new List<ExtensionFieldSet>();
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, SceneGroupID);
                writer.WriteByte(1, SceneID, 254);
                writer.WriteString(2, SceneName, 16);
                writer.WriteUInt(3, SceneTransitionTime, 60000000);
                {
                    writer.StartList(4);
                    foreach (var item in ExtensionFields) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Scene Info
        /// </summary>
        public record SceneInfo : TLVPayload {
            /// <summary>
            /// Scene Info
            /// </summary>
            public SceneInfo() { }

            [SetsRequiredMembers]
            internal SceneInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                SceneCount = reader.GetByte(0)!.Value;
                CurrentScene = reader.GetByte(1)!.Value;
                CurrentGroup = reader.GetUShort(2)!.Value;
                SceneValid = reader.GetBool(3)!.Value;
                RemainingCapacity = reader.GetByte(4)!.Value;
            }
            public required byte SceneCount { get; set; } = 0;
            public required byte CurrentScene { get; set; } = 0xFF;
            public required ushort CurrentGroup { get; set; } = 0;
            public required bool SceneValid { get; set; } = false;
            public required byte RemainingCapacity { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, SceneCount);
                writer.WriteByte(1, CurrentScene);
                writer.WriteUShort(2, CurrentGroup);
                writer.WriteBool(3, SceneValid);
                writer.WriteByte(4, RemainingCapacity, 253);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record AddScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            public required uint TransitionTime { get; set; }
            public required string SceneName { get; set; }
            public required List<ExtensionFieldSet> ExtensionFieldSetStructs { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID, 254);
                writer.WriteUInt(2, TransitionTime, 60000000);
                writer.WriteString(3, SceneName, 16);
                {
                    writer.StartList(4);
                    foreach (var item in ExtensionFieldSetStructs) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Add Scene Response - Reply from server
        /// </summary>
        public struct AddSceneResponse() {
            public required byte Status { get; set; }
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
        }

        private record ViewScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID, 254);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// View Scene Response - Reply from server
        /// </summary>
        public struct ViewSceneResponse() {
            public required byte Status { get; set; }
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            public required uint TransitionTime { get; set; }
            public required string SceneName { get; set; }
            public required List<ExtensionFieldSet> ExtensionFieldSetStructs { get; set; }
        }

        private record RemoveScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID, 254);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Remove Scene Response - Reply from server
        /// </summary>
        public struct RemoveSceneResponse() {
            public required byte Status { get; set; }
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
        }

        private record RemoveAllScenesPayload : TLVPayload {
            public required ushort GroupID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Remove All Scenes Response - Reply from server
        /// </summary>
        public struct RemoveAllScenesResponse() {
            public required byte Status { get; set; }
            public required ushort GroupID { get; set; }
        }

        private record StoreScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID, 254);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Store Scene Response - Reply from server
        /// </summary>
        public struct StoreSceneResponse() {
            public required byte Status { get; set; }
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
        }

        private record RecallScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            public uint? TransitionTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID, 254);
                if (TransitionTime != null)
                    writer.WriteUInt(2, TransitionTime, 60000000);
                writer.EndContainer();
            }
        }

        private record GetSceneMembershipPayload : TLVPayload {
            public required ushort GroupID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Scene Membership Response - Reply from server
        /// </summary>
        public struct GetSceneMembershipResponse() {
            public required byte Status { get; set; }
            public required byte? Capacity { get; set; }
            public required ushort GroupID { get; set; }
            public required List<byte> SceneList { get; set; }
        }

        private record CopyScenePayload : TLVPayload {
            public required CopyModeBitmap Mode { get; set; }
            public required ushort GroupIdentifierFrom { get; set; }
            public required byte SceneIdentifierFrom { get; set; }
            public required ushort GroupIdentifierTo { get; set; }
            public required byte SceneIdentifierTo { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Mode);
                writer.WriteUShort(1, GroupIdentifierFrom);
                writer.WriteByte(2, SceneIdentifierFrom, 254);
                writer.WriteUShort(3, GroupIdentifierTo);
                writer.WriteByte(4, SceneIdentifierTo, 254);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Copy Scene Response - Reply from server
        /// </summary>
        public struct CopySceneResponse() {
            public required byte Status { get; set; }
            public required ushort GroupIdentifierFrom { get; set; }
            public required byte SceneIdentifierFrom { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Add Scene
        /// </summary>
        public async Task<AddSceneResponse?> AddScene(SecureSession session, ushort GroupID, byte SceneID, uint TransitionTime, string SceneName, List<ExtensionFieldSet> ExtensionFieldSetStructs) {
            AddScenePayload requestFields = new AddScenePayload() {
                GroupID = GroupID,
                SceneID = SceneID,
                TransitionTime = TransitionTime,
                SceneName = SceneName,
                ExtensionFieldSetStructs = ExtensionFieldSetStructs,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new AddSceneResponse() {
                Status = (byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                SceneID = (byte)GetField(resp, 2),
            };
        }

        /// <summary>
        /// View Scene
        /// </summary>
        public async Task<ViewSceneResponse?> ViewScene(SecureSession session, ushort GroupID, byte SceneID) {
            ViewScenePayload requestFields = new ViewScenePayload() {
                GroupID = GroupID,
                SceneID = SceneID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ViewSceneResponse() {
                Status = (byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                SceneID = (byte)GetField(resp, 2),
                TransitionTime = (uint)GetField(resp, 3),
                SceneName = (string)GetField(resp, 4),
                ExtensionFieldSetStructs = (List<ExtensionFieldSet>)GetField(resp, 5),
            };
        }

        /// <summary>
        /// Remove Scene
        /// </summary>
        public async Task<RemoveSceneResponse?> RemoveScene(SecureSession session, ushort GroupID, byte SceneID) {
            RemoveScenePayload requestFields = new RemoveScenePayload() {
                GroupID = GroupID,
                SceneID = SceneID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new RemoveSceneResponse() {
                Status = (byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                SceneID = (byte)GetField(resp, 2),
            };
        }

        /// <summary>
        /// Remove All Scenes
        /// </summary>
        public async Task<RemoveAllScenesResponse?> RemoveAllScenes(SecureSession session, ushort GroupID) {
            RemoveAllScenesPayload requestFields = new RemoveAllScenesPayload() {
                GroupID = GroupID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new RemoveAllScenesResponse() {
                Status = (byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Store Scene
        /// </summary>
        public async Task<StoreSceneResponse?> StoreScene(SecureSession session, ushort GroupID, byte SceneID) {
            StoreScenePayload requestFields = new StoreScenePayload() {
                GroupID = GroupID,
                SceneID = SceneID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new StoreSceneResponse() {
                Status = (byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                SceneID = (byte)GetField(resp, 2),
            };
        }

        /// <summary>
        /// Recall Scene
        /// </summary>
        public async Task<bool> RecallScene(SecureSession session, ushort GroupID, byte SceneID, uint? TransitionTime) {
            RecallScenePayload requestFields = new RecallScenePayload() {
                GroupID = GroupID,
                SceneID = SceneID,
                TransitionTime = TransitionTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Scene Membership
        /// </summary>
        public async Task<GetSceneMembershipResponse?> GetSceneMembership(SecureSession session, ushort GroupID) {
            GetSceneMembershipPayload requestFields = new GetSceneMembershipPayload() {
                GroupID = GroupID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetSceneMembershipResponse() {
                Status = (byte)GetField(resp, 0),
                Capacity = (byte)GetField(resp, 1),
                GroupID = (ushort)GetField(resp, 2),
                SceneList = (List<byte>)GetField(resp, 3),
            };
        }

        /// <summary>
        /// Copy Scene
        /// </summary>
        public async Task<CopySceneResponse?> CopyScene(SecureSession session, CopyModeBitmap Mode, ushort GroupIdentifierFrom, byte SceneIdentifierFrom, ushort GroupIdentifierTo, byte SceneIdentifierTo) {
            CopyScenePayload requestFields = new CopyScenePayload() {
                Mode = Mode,
                GroupIdentifierFrom = GroupIdentifierFrom,
                SceneIdentifierFrom = SceneIdentifierFrom,
                GroupIdentifierTo = GroupIdentifierTo,
                SceneIdentifierTo = SceneIdentifierTo,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x40, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new CopySceneResponse() {
                Status = (byte)GetField(resp, 0),
                GroupIdentifierFrom = (ushort)GetField(resp, 1),
                SceneIdentifierFrom = (byte)GetField(resp, 2),
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
        /// Get the Last Configured By attribute
        /// </summary>
        public async Task<ulong?> GetLastConfiguredBy(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 0, true) ?? null;
        }

        /// <summary>
        /// Get the Scene Table Size attribute
        /// </summary>
        public async Task<ushort> GetSceneTableSize(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1) ?? 16;
        }

        /// <summary>
        /// Get the Fabric Scene Info attribute
        /// </summary>
        public async Task<List<SceneInfo>> GetFabricSceneInfo(SecureSession session) {
            List<SceneInfo> list = new List<SceneInfo>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 2))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new SceneInfo(reader.GetStruct(i)!));
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Scenes Management Cluster";
        }
    }
}