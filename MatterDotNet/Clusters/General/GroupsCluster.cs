﻿// MatterDotNet Copyright (C) 2025 
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
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Attributes and commands for group configuration and manipulation.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class Groups : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0004;

        /// <summary>
        /// Attributes and commands for group configuration and manipulation.
        /// </summary>
        [SetsRequiredMembers]
        public Groups(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected Groups(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            NameSupport = new ReadAttribute<NameSupportBitmap>(cluster, endPoint, 0) {
                Deserialize = x => (NameSupportBitmap)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// The ability to store a name for a group.
            /// </summary>
            GroupNames = 1,
        }

        /// <summary>
        /// Name Support
        /// </summary>
        [Flags]
        public enum NameSupportBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// The ability to store a name for a group.
            /// </summary>
            GroupNames = 0x80,
        }
        #endregion Enums

        #region Payloads
        private record AddGroupPayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required string GroupName { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteString(1, GroupName, 16);
                writer.EndContainer();
            }
        }

        private record ViewGroupPayload : TLVPayload {
            public required ushort GroupID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.EndContainer();
            }
        }

        private record GetGroupMembershipPayload : TLVPayload {
            public required ushort[] GroupList { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in GroupList) {
                        writer.WriteUShort(-1, item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        private record RemoveGroupPayload : TLVPayload {
            public required ushort GroupID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.EndContainer();
            }
        }

        private record AddGroupIfIdentifyingPayload : TLVPayload {
            public required ushort GroupID { get; set; }
            public required string GroupName { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, GroupID);
                writer.WriteString(1, GroupName, 16);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Add Group Response - Reply from server
        /// </summary>
        public struct AddGroupResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupID { get; set; }
        }

        /// <summary>
        /// View Group Response - Reply from server
        /// </summary>
        public struct ViewGroupResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupID { get; set; }
            public required string GroupName { get; set; }
        }

        /// <summary>
        /// Get Group Membership Response - Reply from server
        /// </summary>
        public struct GetGroupMembershipResponse() {
            public required byte? Capacity { get; set; }
            public required ushort[] GroupList { get; set; }
        }

        /// <summary>
        /// Remove Group Response - Reply from server
        /// </summary>
        public struct RemoveGroupResponse() {
            public required IMStatusCode Status { get; set; }
            public required ushort GroupID { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Add Group
        /// </summary>
        public async Task<AddGroupResponse?> AddGroup(SecureSession session, ushort groupID, string groupName, CancellationToken token = default) {
            AddGroupPayload requestFields = new AddGroupPayload() {
                GroupID = groupID,
                GroupName = groupName,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new AddGroupResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
            };
        }

        /// <summary>
        /// View Group
        /// </summary>
        public async Task<ViewGroupResponse?> ViewGroup(SecureSession session, ushort groupID, CancellationToken token = default) {
            ViewGroupPayload requestFields = new ViewGroupPayload() {
                GroupID = groupID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new ViewGroupResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
                GroupName = (string)GetField(resp, 2),
            };
        }

        /// <summary>
        /// Get Group Membership
        /// </summary>
        public async Task<GetGroupMembershipResponse?> GetGroupMembership(SecureSession session, ushort[] groupList, CancellationToken token = default) {
            GetGroupMembershipPayload requestFields = new GetGroupMembershipPayload() {
                GroupList = groupList,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new GetGroupMembershipResponse() {
                Capacity = (byte?)GetField(resp, 0),
                GroupList = (ushort[])GetField(resp, 1),
            };
        }

        /// <summary>
        /// Remove Group
        /// </summary>
        public async Task<RemoveGroupResponse?> RemoveGroup(SecureSession session, ushort groupID, CancellationToken token = default) {
            RemoveGroupPayload requestFields = new RemoveGroupPayload() {
                GroupID = groupID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields, token);
            if (!ValidateResponse(resp))
                return null;
            return new RemoveGroupResponse() {
                Status = (IMStatusCode)(byte)GetField(resp, 0),
                GroupID = (ushort)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Remove All Groups
        /// </summary>
        public async Task<bool> RemoveAllGroups(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add Group If Identifying
        /// </summary>
        public async Task<bool> AddGroupIfIdentifying(SecureSession session, ushort groupID, string groupName, CancellationToken token = default) {
            AddGroupIfIdentifyingPayload requestFields = new AddGroupIfIdentifyingPayload() {
                GroupID = groupID,
                GroupName = groupName,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields, token);
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
        /// Name Support Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<NameSupportBitmap> NameSupport { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Groups";
        }
    }
}