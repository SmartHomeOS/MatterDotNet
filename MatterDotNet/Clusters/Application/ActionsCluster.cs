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
    /// Actions Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ActionsCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0025;

        /// <summary>
        /// Actions Cluster
        /// </summary>
        public ActionsCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ActionsCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Action Error
        /// </summary>
        public enum ActionErrorEnum {
            /// <summary>
            /// Other reason not listed in the row(s) below
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// The action was interrupted by another command or interaction
            /// </summary>
            Interrupted = 1,
        }

        /// <summary>
        /// Action State
        /// </summary>
        public enum ActionStateEnum {
            /// <summary>
            /// The action is not active
            /// </summary>
            Inactive = 0,
            /// <summary>
            /// The action is active
            /// </summary>
            Active = 1,
            /// <summary>
            /// The action has been paused
            /// </summary>
            Paused = 2,
            /// <summary>
            /// The action has been disabled
            /// </summary>
            Disabled = 3,
        }

        /// <summary>
        /// Action Type
        /// </summary>
        public enum ActionTypeEnum {
            /// <summary>
            /// Use this only when none of the other values applies
            /// </summary>
            Other = 0,
            /// <summary>
            /// Bring the endpoints into a certain state
            /// </summary>
            Scene = 1,
            /// <summary>
            /// A sequence of states with a certain time pattern
            /// </summary>
            Sequence = 2,
            /// <summary>
            /// Control an automation (e.g. motion sensor controlling lights)
            /// </summary>
            Automation = 3,
            /// <summary>
            /// Sequence that will run when something doesn't happen
            /// </summary>
            Exception = 4,
            /// <summary>
            /// Use the endpoints to send a message to user
            /// </summary>
            Notification = 5,
            /// <summary>
            /// Higher priority notification
            /// </summary>
            Alarm = 6,
        }

        /// <summary>
        /// Endpoint List Type
        /// </summary>
        public enum EndpointListTypeEnum {
            /// <summary>
            /// Another group of endpoints
            /// </summary>
            Other = 0,
            /// <summary>
            /// User-configured group of endpoints where an endpoint can be in only one room
            /// </summary>
            Room = 1,
            /// <summary>
            /// User-configured group of endpoints where an endpoint can be in any number of zones
            /// </summary>
            Zone = 2,
        }

        /// <summary>
        /// Command Bits
        /// </summary>
        [Flags]
        public enum CommandBits {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Indicate support for InstantAction command
            /// </summary>
            InstantAction = 1,
            /// <summary>
            /// Indicate support for InstantActionWithTransition command
            /// </summary>
            InstantActionWithTransition = 2,
            /// <summary>
            /// Indicate support for StartAction command
            /// </summary>
            StartAction = 4,
            /// <summary>
            /// Indicate support for StartActionWithDuration command
            /// </summary>
            StartActionWithDuration = 8,
            /// <summary>
            /// Indicate support for StopAction command
            /// </summary>
            StopAction = 16,
            /// <summary>
            /// Indicate support for PauseAction command
            /// </summary>
            PauseAction = 32,
            /// <summary>
            /// Indicate support for PauseActionWithDuration command
            /// </summary>
            PauseActionWithDuration = 64,
            /// <summary>
            /// Indicate support for ResumeAction command
            /// </summary>
            ResumeAction = 128,
            /// <summary>
            /// Indicate support for EnableAction command
            /// </summary>
            EnableAction = 256,
            /// <summary>
            /// Indicate support for EnableActionWithDuration command
            /// </summary>
            EnableActionWithDuration = 512,
            /// <summary>
            /// Indicate support for DisableAction command
            /// </summary>
            DisableAction = 1024,
            /// <summary>
            /// Indicate support for DisableActionWithDuration command
            /// </summary>
            DisableActionWithDuration = 2048,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Action
        /// </summary>
        public record Action : TLVPayload {
            /// <summary>
            /// Action
            /// </summary>
            public Action() { }

            /// <summary>
            /// Action
            /// </summary>
            [SetsRequiredMembers]
            public Action(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ActionID = reader.GetUShort(0)!.Value;
                Name = reader.GetString(1, false)!;
                Type = (ActionTypeEnum)reader.GetUShort(2)!.Value;
                EndpointListID = reader.GetUShort(3)!.Value;
                SupportedCommands = (CommandBits)reader.GetUShort(4)!.Value;
                State = (ActionStateEnum)reader.GetUShort(5)!.Value;
            }
            public required ushort ActionID { get; set; }
            public required string Name { get; set; }
            public required ActionTypeEnum Type { get; set; }
            public required ushort EndpointListID { get; set; }
            public required CommandBits SupportedCommands { get; set; }
            public required ActionStateEnum State { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                writer.WriteString(1, Name);
                writer.WriteUShort(2, (ushort)Type);
                writer.WriteUShort(3, EndpointListID);
                writer.WriteUShort(4, (ushort)SupportedCommands);
                writer.WriteUShort(5, (ushort)State);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Endpoint List
        /// </summary>
        public record EndpointList : TLVPayload {
            /// <summary>
            /// Endpoint List
            /// </summary>
            public EndpointList() { }

            /// <summary>
            /// Endpoint List
            /// </summary>
            [SetsRequiredMembers]
            public EndpointList(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                EndpointListID = reader.GetUShort(0)!.Value;
                Name = reader.GetString(1, false)!;
                Type = (EndpointListTypeEnum)reader.GetUShort(2)!.Value;
                {
                    Endpoints = new ushort[((object[])fields[3]).Length];
                    for (int i = 0; i < Endpoints.Length; i++) {
                        Endpoints[i] = reader.GetUShort(-1)!.Value;
                    }
                }
            }
            public required ushort EndpointListID { get; set; }
            public required string Name { get; set; }
            public required EndpointListTypeEnum Type { get; set; }
            public required ushort[] Endpoints { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, EndpointListID);
                writer.WriteString(1, Name);
                writer.WriteUShort(2, (ushort)Type);
                {
                    Constrain(Endpoints, 0, 256);
                    writer.StartArray(3);
                    foreach (var item in Endpoints) {
                        writer.WriteUShort(-1, item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record InstantActionPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.EndContainer();
            }
        }

        private record InstantActionWithTransitionPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            public required ushort TransitionTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.WriteUShort(2, TransitionTime);
                writer.EndContainer();
            }
        }

        private record StartActionPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.EndContainer();
            }
        }

        private record StartActionWithDurationPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            public required uint Duration { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.WriteUInt(2, Duration);
                writer.EndContainer();
            }
        }

        private record StopActionPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.EndContainer();
            }
        }

        private record PauseActionPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.EndContainer();
            }
        }

        private record PauseActionWithDurationPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            public required uint Duration { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.WriteUInt(2, Duration);
                writer.EndContainer();
            }
        }

        private record ResumeActionPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.EndContainer();
            }
        }

        private record EnableActionPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.EndContainer();
            }
        }

        private record EnableActionWithDurationPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            public required uint Duration { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.WriteUInt(2, Duration);
                writer.EndContainer();
            }
        }

        private record DisableActionPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.EndContainer();
            }
        }

        private record DisableActionWithDurationPayload : TLVPayload {
            public required ushort ActionID { get; set; }
            public uint? InvokeID { get; set; }
            public required uint Duration { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ActionID);
                if (InvokeID != null)
                    writer.WriteUInt(1, InvokeID);
                writer.WriteUInt(2, Duration);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Instant Action
        /// </summary>
        public async Task<bool> InstantAction(SecureSession session, ushort ActionID, uint? InvokeID) {
            InstantActionPayload requestFields = new InstantActionPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Instant Action With Transition
        /// </summary>
        public async Task<bool> InstantActionWithTransition(SecureSession session, ushort ActionID, uint? InvokeID, ushort TransitionTime) {
            InstantActionWithTransitionPayload requestFields = new InstantActionWithTransitionPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
                TransitionTime = TransitionTime,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Start Action
        /// </summary>
        public async Task<bool> StartAction(SecureSession session, ushort ActionID, uint? InvokeID) {
            StartActionPayload requestFields = new StartActionPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Start Action With Duration
        /// </summary>
        public async Task<bool> StartActionWithDuration(SecureSession session, ushort ActionID, uint? InvokeID, uint Duration) {
            StartActionWithDurationPayload requestFields = new StartActionWithDurationPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
                Duration = Duration,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Stop Action
        /// </summary>
        public async Task<bool> StopAction(SecureSession session, ushort ActionID, uint? InvokeID) {
            StopActionPayload requestFields = new StopActionPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Pause Action
        /// </summary>
        public async Task<bool> PauseAction(SecureSession session, ushort ActionID, uint? InvokeID) {
            PauseActionPayload requestFields = new PauseActionPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Pause Action With Duration
        /// </summary>
        public async Task<bool> PauseActionWithDuration(SecureSession session, ushort ActionID, uint? InvokeID, uint Duration) {
            PauseActionWithDurationPayload requestFields = new PauseActionWithDurationPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
                Duration = Duration,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Resume Action
        /// </summary>
        public async Task<bool> ResumeAction(SecureSession session, ushort ActionID, uint? InvokeID) {
            ResumeActionPayload requestFields = new ResumeActionPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enable Action
        /// </summary>
        public async Task<bool> EnableAction(SecureSession session, ushort ActionID, uint? InvokeID) {
            EnableActionPayload requestFields = new EnableActionPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x08, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enable Action With Duration
        /// </summary>
        public async Task<bool> EnableActionWithDuration(SecureSession session, ushort ActionID, uint? InvokeID, uint Duration) {
            EnableActionWithDurationPayload requestFields = new EnableActionWithDurationPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
                Duration = Duration,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Disable Action
        /// </summary>
        public async Task<bool> DisableAction(SecureSession session, ushort ActionID, uint? InvokeID) {
            DisableActionPayload requestFields = new DisableActionPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0A, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Disable Action With Duration
        /// </summary>
        public async Task<bool> DisableActionWithDuration(SecureSession session, ushort ActionID, uint? InvokeID, uint Duration) {
            DisableActionWithDurationPayload requestFields = new DisableActionWithDurationPayload() {
                ActionID = ActionID,
                InvokeID = InvokeID,
                Duration = Duration,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0B, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Action List attribute
        /// </summary>
        public async Task<Action[]> GetActionList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            Action[] list = new Action[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Action(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Endpoint Lists attribute
        /// </summary>
        public async Task<EndpointList[]> GetEndpointLists(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            EndpointList[] list = new EndpointList[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new EndpointList(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Setup URL attribute
        /// </summary>
        public async Task<string> GetSetupURL(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 2) ?? "";
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Actions Cluster";
        }
    }
}