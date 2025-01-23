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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// This cluster supports remotely monitoring and, where supported, changing the operational state of any device where a state machine is a part of the operation.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class OperationalState : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0060;

        /// <summary>
        /// This cluster supports remotely monitoring and, where supported, changing the operational state of any device where a state machine is a part of the operation.
        /// </summary>
        public OperationalState(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected OperationalState(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Operational State
        /// </summary>
        public enum OperationalStateEnum : byte {
            Stopped = 0x00,
            Running = 0x01,
            Paused = 0x02,
            Error = 0x03,
        }

        /// <summary>
        /// Error State
        /// </summary>
        public enum ErrorStateEnum : byte {
            NoError = 0x00,
            UnableToStartOrResume = 0x01,
            UnableToCompleteOperation = 0x02,
            CommandInvalidInState = 0x03,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Operational State
        /// </summary>
        public record OperationalStatePayload : TLVPayload {
            /// <summary>
            /// Operational State
            /// </summary>
            public OperationalStatePayload() { }

            /// <summary>
            /// Operational State
            /// </summary>
            [SetsRequiredMembers]
            public OperationalStatePayload(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                OperationalStateID = reader.GetByte(0)!.Value;
                OperationalStateLabel = reader.GetString(1, true, 64);
            }
            public required byte OperationalStateID { get; set; }
            public string? OperationalStateLabel { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, OperationalStateID);
                if (OperationalStateLabel != null)
                    writer.WriteString(1, OperationalStateLabel, 64);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Error State
        /// </summary>
        public record ErrorState : TLVPayload {
            /// <summary>
            /// Error State
            /// </summary>
            public ErrorState() { }

            /// <summary>
            /// Error State
            /// </summary>
            [SetsRequiredMembers]
            public ErrorState(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ErrorStateID = reader.GetByte(0)!.Value;
                ErrorStateLabel = reader.GetString(1, true, 64);
                ErrorStateDetails = reader.GetString(2, true, 64);
            }
            public required byte ErrorStateID { get; set; }
            public string? ErrorStateLabel { get; set; }
            public string? ErrorStateDetails { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, ErrorStateID);
                if (ErrorStateLabel != null)
                    writer.WriteString(1, ErrorStateLabel, 64);
                if (ErrorStateDetails != null)
                    writer.WriteString(2, ErrorStateDetails, 64);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        /// <summary>
        /// Operational Command Response - Reply from server
        /// </summary>
        public struct OperationalCommandResponse() {
            public required ErrorState CommandResponseState { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Pause
        /// </summary>
        public async Task<OperationalCommandResponse?> Pause(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            if (!ValidateResponse(resp))
                return null;
            return new OperationalCommandResponse() {
                CommandResponseState = (ErrorState)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Stop
        /// </summary>
        public async Task<OperationalCommandResponse?> Stop(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            if (!ValidateResponse(resp))
                return null;
            return new OperationalCommandResponse() {
                CommandResponseState = (ErrorState)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Start
        /// </summary>
        public async Task<OperationalCommandResponse?> Start(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02);
            if (!ValidateResponse(resp))
                return null;
            return new OperationalCommandResponse() {
                CommandResponseState = (ErrorState)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Resume
        /// </summary>
        public async Task<OperationalCommandResponse?> Resume(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03);
            if (!ValidateResponse(resp))
                return null;
            return new OperationalCommandResponse() {
                CommandResponseState = (ErrorState)GetField(resp, 0),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the Phase List attribute
        /// </summary>
        public async Task<string[]?> GetPhaseList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            string[] list = new string[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetString(i, false)!;
            return list;
        }

        /// <summary>
        /// Get the Current Phase attribute
        /// </summary>
        public async Task<byte?> GetCurrentPhase(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1, true);
        }

        /// <summary>
        /// Get the Countdown Time attribute
        /// </summary>
        public async Task<TimeSpan?> GetCountdownTime(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 2, true);
        }

        /// <summary>
        /// Get the Operational State List attribute
        /// </summary>
        public async Task<OperationalStatePayload[]> GetOperationalStateList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 3))!);
            OperationalStatePayload[] list = new OperationalStatePayload[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new OperationalStatePayload(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Operational State attribute
        /// </summary>
        public async Task<OperationalStateEnum> GetOperationalState(SecureSession session) {
            return (OperationalStateEnum)await GetEnumAttribute(session, 4);
        }

        /// <summary>
        /// Get the Operational Error attribute
        /// </summary>
        public async Task<ErrorState> GetOperationalError(SecureSession session) {
            return new ErrorState((object[])(await GetAttribute(session, 5))!);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Operational State";
        }
    }
}