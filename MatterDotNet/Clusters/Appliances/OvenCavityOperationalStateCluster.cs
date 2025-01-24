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

namespace MatterDotNet.Clusters.Appliances
{
    /// <summary>
    /// This cluster supports remotely monitoring and, where supported, changing the operational state of an Oven.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class OvenCavityOperationalState : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0048;

        /// <summary>
        /// This cluster supports remotely monitoring and, where supported, changing the operational state of an Oven.
        /// </summary>
        public OvenCavityOperationalState(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected OvenCavityOperationalState(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Operational State
        /// </summary>
        public enum OperationalState : byte {
            Stopped = 0,
            Running = 1,
            Paused = 2,
            Error = 3,
        }

        /// <summary>
        /// Error State
        /// </summary>
        public enum ErrorState : byte {
            NoError = 0,
            UnableToStartOrResume = 1,
            UnableToCompleteOperation = 2,
            CommandInvalidInState = 3,
        }
        #endregion Enums

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
        public async Task<General.OperationalState.OperationalStatePayload[]> GetOperationalStateList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 3))!);
            General.OperationalState.OperationalStatePayload[] list = new General.OperationalState.OperationalStatePayload[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new General.OperationalState.OperationalStatePayload(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Operational State attribute
        /// </summary>
        public async Task<OperationalState> GetOperationalState(SecureSession session) {
            return (OperationalState)await GetEnumAttribute(session, 4);
        }

        /// <summary>
        /// Get the Operational Error attribute
        /// </summary>
        public async Task<General.OperationalState.ErrorStatePayload> GetOperationalError(SecureSession session) {
            return new General.OperationalState.ErrorStatePayload((object[])(await GetAttribute(session, 5))!);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Oven Cavity Operational State";
        }
    }
}