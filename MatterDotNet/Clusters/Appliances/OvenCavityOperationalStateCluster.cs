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

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

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
        [SetsRequiredMembers]
        public OvenCavityOperationalState(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected OvenCavityOperationalState(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            PhaseList = new ReadAttribute<string[]?>(cluster, endPoint, 0, true) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    string[] list = new string[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetString(i, false)!;
                    return list;
                }
            };
            CurrentPhase = new ReadAttribute<byte?>(cluster, endPoint, 1, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            CountdownTime = new ReadAttribute<TimeSpan?>(cluster, endPoint, 2, true) {
                Deserialize = x => (TimeSpan?)(dynamic?)x
            };
            OperationalStateList = new ReadAttribute<General.OperationalState.OperationalStateStruct[]>(cluster, endPoint, 3) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    General.OperationalState.OperationalStateStruct[] list = new General.OperationalState.OperationalStateStruct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new General.OperationalState.OperationalStateStruct(reader.GetStruct(i)!);
                    return list;
                }
            };
            OperationalState = new ReadAttribute<OperationalStateEnum>(cluster, endPoint, 4) {
                Deserialize = x => (OperationalStateEnum)DeserializeEnum(x)!
            };
            OperationalError = new ReadAttribute<ErrorState>(cluster, endPoint, 5) {
                Deserialize = x => (ErrorState)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Operational State
        /// </summary>
        public enum OperationalStateEnum : byte {
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
        /// Phase List Attribute
        /// </summary>
        public required ReadAttribute<string[]?> PhaseList { get; init; }

        /// <summary>
        /// Current Phase Attribute
        /// </summary>
        public required ReadAttribute<byte?> CurrentPhase { get; init; }

        /// <summary>
        /// Countdown Time Attribute
        /// </summary>
        public required ReadAttribute<TimeSpan?> CountdownTime { get; init; }

        /// <summary>
        /// Operational State List Attribute
        /// </summary>
        public required ReadAttribute<General.OperationalState.OperationalStateStruct[]> OperationalStateList { get; init; }

        /// <summary>
        /// Operational State Attribute
        /// </summary>
        public required ReadAttribute<OperationalStateEnum> OperationalState { get; init; }

        /// <summary>
        /// Operational Error Attribute
        /// </summary>
        public required ReadAttribute<ErrorState> OperationalError { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Oven Cavity Operational State";
        }
    }
}