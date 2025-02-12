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
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// This cluster provides an interface for passing messages to be presented by a device.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class Messages : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0097;

        /// <summary>
        /// This cluster provides an interface for passing messages to be presented by a device.
        /// </summary>
        [SetsRequiredMembers]
        public Messages(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected Messages(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            MessagesAttribute = new ReadAttribute<Message[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Message[] list = new Message[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Message(reader.GetStruct(i)!);
                    return list;
                }
            };
            ActiveMessageIDs = new ReadAttribute<byte[][]>(cluster, endPoint, 1) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    byte[][] list = new byte[reader.Count][];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetBytes(i, false, 8)!;
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
            ReceivedConfirmation = 1,
            ConfirmationResponse = 2,
            ConfirmationReply = 4,
            ProtectedMessages = 8,
        }

        /// <summary>
        /// Future Message Preference
        /// </summary>
        public enum FutureMessagePreference : byte {
            /// <summary>
            /// Similar messages are allowed
            /// </summary>
            Allowed = 0,
            /// <summary>
            /// Similar messages should be sent more often
            /// </summary>
            Increased = 1,
            /// <summary>
            /// Similar messages should be sent less often
            /// </summary>
            Reduced = 2,
            /// <summary>
            /// Similar messages should not be sent
            /// </summary>
            Disallowed = 3,
            /// <summary>
            /// No further messages should be sent
            /// </summary>
            Banned = 4,
        }

        /// <summary>
        /// Message Priority
        /// </summary>
        public enum MessagePriority : byte {
            /// <summary>
            /// Message to be transferred with a low level of importance
            /// </summary>
            Low = 0,
            /// <summary>
            /// Message to be transferred with a medium level of importance
            /// </summary>
            Medium = 1,
            /// <summary>
            /// Message to be transferred with a high level of importance
            /// </summary>
            High = 2,
            /// <summary>
            /// Message to be transferred with a critical level of importance
            /// </summary>
            Critical = 3,
        }

        /// <summary>
        /// Message Control
        /// </summary>
        [Flags]
        public enum MessageControl : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Message requires confirmation from user
            /// </summary>
            ConfirmationRequired = 0x01,
            /// <summary>
            /// Message requires response from user
            /// </summary>
            ResponseRequired = 0x02,
            /// <summary>
            /// Message supports reply message from user
            /// </summary>
            ReplyMessage = 0x04,
            /// <summary>
            /// Message has already been confirmed
            /// </summary>
            MessageConfirmed = 0x08,
            /// <summary>
            /// Message required PIN/password protection
            /// </summary>
            MessageProtected = 0x10,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Message
        /// </summary>
        public record Message : TLVPayload {
            /// <summary>
            /// Message
            /// </summary>
            public Message() { }

            /// <summary>
            /// Message
            /// </summary>
            [SetsRequiredMembers]
            public Message(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MessageID = new Guid(reader.GetBytes(0, false, 16, 16)!);
                Priority = (MessagePriority)reader.GetUShort(1)!.Value;
                MessageControl = (MessageControl)reader.GetUInt(2)!.Value;
                StartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(3, true));
                Duration = reader.GetULong(4, true);
                MessageText = reader.GetString(5, false, 256)!;
                {
                    Responses = new MessageResponseOption[reader.GetStruct(6)!.Length];
                    for (int n = 0; n < Responses.Length; n++) {
                        Responses[n] = new MessageResponseOption((object[])((object[])fields[6])[n]);
                    }
                }
            }
            public required Guid MessageID { get; set; }
            public required MessagePriority Priority { get; set; }
            public required MessageControl MessageControl { get; set; }
            public required DateTime? StartTime { get; set; } = TimeUtil.EPOCH;
            public required ulong? Duration { get; set; } = 0;
            public required string MessageText { get; set; }
            public MessageResponseOption[]? Responses { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, MessageID.ToByteArray());
                writer.WriteUShort(1, (ushort)Priority);
                writer.WriteUInt(2, (uint)MessageControl);
                if (!StartTime.HasValue)
                    writer.WriteNull(3);
                else
                    writer.WriteUInt(3, TimeUtil.ToEpochSeconds(StartTime!.Value));
                writer.WriteULong(4, Duration);
                writer.WriteString(5, MessageText, 256);
                if (Responses != null)
                {
                    Constrain(Responses, 0, 4);
                    writer.StartArray(6);
                    foreach (var item in Responses) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Message Response Option
        /// </summary>
        public record MessageResponseOption : TLVPayload {
            /// <summary>
            /// Message Response Option
            /// </summary>
            public MessageResponseOption() { }

            /// <summary>
            /// Message Response Option
            /// </summary>
            [SetsRequiredMembers]
            public MessageResponseOption(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MessageResponseID = reader.GetUInt(0, true);
                Label = reader.GetString(1, true, 32);
            }
            public uint? MessageResponseID { get; set; }
            public string? Label { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (MessageResponseID != null)
                    writer.WriteUInt(0, MessageResponseID);
                if (Label != null)
                    writer.WriteString(1, Label, 32);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record PresentMessagesRequestPayload : TLVPayload {
            public required Guid MessageID { get; set; }
            public required MessagePriority Priority { get; set; }
            public required MessageControl MessageControl { get; set; }
            public required DateTime? StartTime { get; set; }
            public required ulong? Duration { get; set; }
            public required string MessageText { get; set; }
            public MessageResponseOption[]? Responses { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, MessageID.ToByteArray());
                writer.WriteUShort(1, (ushort)Priority);
                writer.WriteUInt(2, (uint)MessageControl);
                if (!StartTime.HasValue)
                    writer.WriteNull(3);
                else
                    writer.WriteUInt(3, TimeUtil.ToEpochSeconds(StartTime!.Value));
                writer.WriteULong(4, Duration);
                writer.WriteString(5, MessageText, 256);
                if (Responses != null)
                {
                    Constrain(Responses, 0, 4);
                    writer.StartArray(6);
                    foreach (var item in Responses) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        private record CancelMessagesRequestPayload : TLVPayload {
            public required byte[][] MessageIDs { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in MessageIDs) {
                        writer.WriteBytes(-1, item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Present Messages Request
        /// </summary>
        public async Task<bool> PresentMessagesRequest(SecureSession session, Guid messageID, MessagePriority priority, MessageControl messageControl, DateTime? startTime, ulong? duration, string messageText, MessageResponseOption[]? responses, CancellationToken token = default) {
            PresentMessagesRequestPayload requestFields = new PresentMessagesRequestPayload() {
                MessageID = messageID,
                Priority = priority,
                MessageControl = messageControl,
                StartTime = startTime,
                Duration = duration,
                MessageText = messageText,
                Responses = responses,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Messages Request
        /// </summary>
        public async Task<bool> CancelMessagesRequest(SecureSession session, byte[][] messageIDs, CancellationToken token = default) {
            CancelMessagesRequestPayload requestFields = new CancelMessagesRequestPayload() {
                MessageIDs = messageIDs,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
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
        /// Messages Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Message[]> MessagesAttribute { get; init; }

        /// <summary>
        /// Active Message I Ds Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[][]> ActiveMessageIDs { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Messages";
        }
    }
}