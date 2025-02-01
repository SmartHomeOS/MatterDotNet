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
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Attributes and commands for scene configuration and manipulation.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ScenesManagement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0062;

        /// <summary>
        /// Attributes and commands for scene configuration and manipulation.
        /// </summary>
        [SetsRequiredMembers]
        public ScenesManagement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ScenesManagement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            LastConfiguredBy = new ReadAttribute<ulong?>(cluster, endPoint, 0, true) {
                Deserialize = x => (ulong?)(dynamic?)x
            };
            SceneTableSize = new ReadAttribute<ushort>(cluster, endPoint, 1) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 16

            };
            FabricSceneInfo = new ReadAttribute<SceneInfo[]>(cluster, endPoint, 2) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    SceneInfo[] list = new SceneInfo[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new SceneInfo(reader.GetStruct(i)!);
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
            /// The ability to store a name for a scene.
            /// </summary>
            SceneNames = 1,
        }

        /// <summary>
        /// Copy Mode
        /// </summary>
        [Flags]
        public enum CopyMode : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Copy all scenes in the scene table
            /// </summary>
            CopyAllScenes = 0x01,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Scene Info
        /// </summary>
        public record SceneInfo : TLVPayload {
            /// <summary>
            /// Scene Info
            /// </summary>
            public SceneInfo() { }

            /// <summary>
            /// Scene Info
            /// </summary>
            [SetsRequiredMembers]
            public SceneInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                SceneCount = reader.GetByte(0)!.Value;
                CurrentScene = reader.GetByte(1)!.Value;
                CurrentGroup = reader.GetUShort(2)!.Value;
                SceneValid = reader.GetBool(3)!.Value;
                RemainingCapacity = reader.GetByte(4)!.Value;
            }
            public required byte SceneCount { get; set; }
            public required byte CurrentScene { get; set; }
            public required ushort CurrentGroup { get; set; }
            public required bool SceneValid { get; set; }
            public required byte RemainingCapacity { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, SceneCount);
                writer.WriteByte(1, CurrentScene);
                writer.WriteUShort(2, CurrentGroup);
                writer.WriteBool(3, SceneValid);
                writer.WriteByte(4, RemainingCapacity);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Attribute Value Pair
        /// </summary>
        public record AttributeValuePair : TLVPayload {
            /// <summary>
            /// Attribute Value Pair
            /// </summary>
            public AttributeValuePair() { }

            /// <summary>
            /// Attribute Value Pair
            /// </summary>
            [SetsRequiredMembers]
            public AttributeValuePair(object[] fields) {
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
            public int? ValueSigned32 { get; set; }
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

            /// <summary>
            /// Extension Field Set
            /// </summary>
            [SetsRequiredMembers]
            public ExtensionFieldSet(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ClusterID = reader.GetUInt(0)!.Value;
                {
                    AttributeValueList = new AttributeValuePair[reader.GetStruct(1)!.Length];
                    for (int n = 0; n < AttributeValueList.Length; n++) {
                        AttributeValueList[n] = new AttributeValuePair((object[])((object[])fields[1])[n]);
                    }
                }
            }
            public required uint ClusterID { get; set; }
            public required AttributeValuePair[] AttributeValueList { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, ClusterID);
                {
                    writer.StartArray(1);
                    foreach (var item in AttributeValueList) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
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
            public required ExtensionFieldSet[] ExtensionFieldSets { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID);
                writer.WriteUInt(2, TransitionTime);
                writer.WriteString(3, SceneName);
                {
                    writer.StartArray(4);
                    foreach (var item in ExtensionFieldSets) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        private record ViewScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID);
                writer.EndContainer();
            }
        }

        private record RemoveScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID);
                writer.EndContainer();
            }
        }

        private record RemoveAllScenesPayload : TLVPayload {
            public required ushort GroupID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.EndContainer();
            }
        }

        private record StoreScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID);
                writer.EndContainer();
            }
        }

        private record RecallScenePayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            public uint? TransitionTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteByte(1, SceneID);
                if (TransitionTime != null)
                    writer.WriteUInt(2, TransitionTime);
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

        private record CopyScenePayload : TLVPayload {
            public required CopyMode Mode { get; set; }
            public required ushort GroupIdentifierFrom { get; set; }
            public required byte SceneIdentifierFrom { get; set; }
            public required ushort GroupIdentifierTo { get; set; }
            public required byte SceneIdentifierTo { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)Mode);
                writer.WriteUShort(1, GroupIdentifierFrom);
                writer.WriteByte(2, SceneIdentifierFrom);
                writer.WriteUShort(3, GroupIdentifierTo);
                writer.WriteByte(4, SceneIdentifierTo);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Add Scene Response - Reply from server
        /// </summary>
        public struct AddSceneResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
        }

        /// <summary>
        /// View Scene Response - Reply from server
        /// </summary>
        public struct ViewSceneResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
            public uint? TransitionTime { get; set; }
            public string? SceneName { get; set; }
            public ExtensionFieldSet[]? ExtensionFieldSets { get; set; }
        }

        /// <summary>
        /// Remove Scene Response - Reply from server
        /// </summary>
        public struct RemoveSceneResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
        }

        /// <summary>
        /// Remove All Scenes Response - Reply from server
        /// </summary>
        public struct RemoveAllScenesResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupID { get; set; }
        }

        /// <summary>
        /// Store Scene Response - Reply from server
        /// </summary>
        public struct StoreSceneResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupID { get; set; }
            public required byte SceneID { get; set; }
        }

        /// <summary>
        /// Get Scene Membership Response - Reply from server
        /// </summary>
        public struct GetSceneMembershipResponse() {
            public required IMStatusCode Status { get; set; }
            public required byte? Capacity { get; set; }
            public required ushort GroupID { get; set; }
            public byte[]? SceneList { get; set; }
        }

        /// <summary>
        /// Copy Scene Response - Reply from server
        /// </summary>
        public struct CopySceneResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupIdentifierFrom { get; set; }
            public required byte SceneIdentifierFrom { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Add Scene
        /// </summary>
        public async Task<AddSceneResponse?> AddScene(SecureSession session, ushort groupID, byte sceneID, uint transitionTime, string sceneName, ExtensionFieldSet[] extensionFieldSets, CancellationToken token = default) {
            AddScenePayload requestFields = new AddScenePayload() {
                GroupID = groupID,
                SceneID = sceneID,
                TransitionTime = transitionTime,
                SceneName = sceneName,
                ExtensionFieldSets = extensionFieldSets,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new AddSceneResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                SceneID = (byte)GetField(resp, 2),
            };
        }

        /// <summary>
        /// View Scene
        /// </summary>
        public async Task<ViewSceneResponse?> ViewScene(SecureSession session, ushort groupID, byte sceneID, CancellationToken token = default) {
            ViewScenePayload requestFields = new ViewScenePayload() {
                GroupID = groupID,
                SceneID = sceneID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new ViewSceneResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                SceneID = (byte)GetField(resp, 2),
                TransitionTime = (uint?)GetOptionalField(resp, 3),
                SceneName = (string?)GetOptionalField(resp, 4),
                ExtensionFieldSets = GetOptionalArrayField<ExtensionFieldSet>(resp, 5),
            };
        }

        /// <summary>
        /// Remove Scene
        /// </summary>
        public async Task<RemoveSceneResponse?> RemoveScene(SecureSession session, ushort groupID, byte sceneID, CancellationToken token = default) {
            RemoveScenePayload requestFields = new RemoveScenePayload() {
                GroupID = groupID,
                SceneID = sceneID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new RemoveSceneResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                SceneID = (byte)GetField(resp, 2),
            };
        }

        /// <summary>
        /// Remove All Scenes
        /// </summary>
        public async Task<RemoveAllScenesResponse?> RemoveAllScenes(SecureSession session, ushort groupID, CancellationToken token = default) {
            RemoveAllScenesPayload requestFields = new RemoveAllScenesPayload() {
                GroupID = groupID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new RemoveAllScenesResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Store Scene
        /// </summary>
        public async Task<StoreSceneResponse?> StoreScene(SecureSession session, ushort groupID, byte sceneID, CancellationToken token = default) {
            StoreScenePayload requestFields = new StoreScenePayload() {
                GroupID = groupID,
                SceneID = sceneID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new StoreSceneResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                SceneID = (byte)GetField(resp, 2),
            };
        }

        /// <summary>
        /// Recall Scene
        /// </summary>
        public async Task<bool> RecallScene(SecureSession session, ushort groupID, byte sceneID, uint? transitionTime, CancellationToken token = default) {
            RecallScenePayload requestFields = new RecallScenePayload() {
                GroupID = groupID,
                SceneID = sceneID,
                TransitionTime = transitionTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Scene Membership
        /// </summary>
        public async Task<GetSceneMembershipResponse?> GetSceneMembership(SecureSession session, ushort groupID, CancellationToken token = default) {
            GetSceneMembershipPayload requestFields = new GetSceneMembershipPayload() {
                GroupID = groupID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new GetSceneMembershipResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                Capacity = (byte?)GetField(resp, 1),
                GroupID = (ushort)GetField(resp, 2),
                SceneList = (byte[]?)GetOptionalField(resp, 3),
            };
        }

        /// <summary>
        /// Copy Scene
        /// </summary>
        public async Task<CopySceneResponse?> CopyScene(SecureSession session, CopyMode mode, ushort groupIdentifierFrom, byte sceneIdentifierFrom, ushort groupIdentifierTo, byte sceneIdentifierTo, CancellationToken token = default) {
            CopyScenePayload requestFields = new CopyScenePayload() {
                Mode = mode,
                GroupIdentifierFrom = groupIdentifierFrom,
                SceneIdentifierFrom = sceneIdentifierFrom,
                GroupIdentifierTo = groupIdentifierTo,
                SceneIdentifierTo = sceneIdentifierTo,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x40, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new CopySceneResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
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
        /// Last Configured By Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong?> LastConfiguredBy { get; init; }

        /// <summary>
        /// Scene Table Size Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> SceneTableSize { get; init; }

        /// <summary>
        /// Fabric Scene Info Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SceneInfo[]> FabricSceneInfo { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Scenes Management";
        }
    }
}