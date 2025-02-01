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

using MatterDotNet.Attributes;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Robots
{
    /// <summary>
    /// This cluster supports remotely monitoring and, where supported, changing the operational state of a Robotic Vacuum.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class RVCOperationalState : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0061;

        /// <summary>
        /// This cluster supports remotely monitoring and, where supported, changing the operational state of a Robotic Vacuum.
        /// </summary>
        [SetsRequiredMembers]
        public RVCOperationalState(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected RVCOperationalState(uint cluster, ushort endPoint) : base(cluster, endPoint) {
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
            OperationalState = new ReadAttribute<byte>(cluster, endPoint, 4) {
                Deserialize = x => (byte)(dynamic?)x!
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
            /// <summary>
            /// The device is stopped
            /// </summary>
            Stopped = 0x0,
            /// <summary>
            /// The device is operating
            /// </summary>
            Running = 0x1,
            /// <summary>
            /// The device is paused during an operation
            /// </summary>
            Paused = 0x2,
            /// <summary>
            /// The device is in an error state
            /// </summary>
            Error = 0x3,
            /// <summary>
            /// The device is en route to the charging dock
            /// </summary>
            SeekingCharger = 0x40,
            /// <summary>
            /// The device is charging
            /// </summary>
            Charging = 0x41,
            /// <summary>
            /// The device is on the dock, not charging
            /// </summary>
            Docked = 0x42,
        }

        /// <summary>
        /// Error State
        /// </summary>
        public enum ErrorState : byte {
            /// <summary>
            /// The device is not in an error state
            /// </summary>
            NoError = 0x0,
            /// <summary>
            /// The device is unable to start or resume operation
            /// </summary>
            UnableToStartOrResume = 0x1,
            /// <summary>
            /// The device was unable to complete the current operation
            /// </summary>
            UnableToCompleteOperation = 0x2,
            /// <summary>
            /// The device cannot process the command in its current state
            /// </summary>
            CommandInvalidInState = 0x3,
            /// <summary>
            /// The device has failed to find or reach the charging dock
            /// </summary>
            FailedToFindChargingDock = 0x40,
            /// <summary>
            /// The device is stuck and requires manual intervention
            /// </summary>
            Stuck = 0x41,
            /// <summary>
            /// The device has detected that its dust bin is missing
            /// </summary>
            DustBinMissing = 0x42,
            /// <summary>
            /// The device has detected that its dust bin is full
            /// </summary>
            DustBinFull = 0x43,
            /// <summary>
            /// The device has detected that its water tank is empty
            /// </summary>
            WaterTankEmpty = 0x44,
            /// <summary>
            /// The device has detected that its water tank is missing
            /// </summary>
            WaterTankMissing = 0x45,
            /// <summary>
            /// The device has detected that its water tank lid is open
            /// </summary>
            WaterTankLidOpen = 0x46,
            /// <summary>
            /// The device has detected that its cleaning pad is missing
            /// </summary>
            MopCleaningPadMissing = 0x47,
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
        public async Task<OperationalCommandResponse?> Pause(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new OperationalCommandResponse() {
                CommandResponseState = (ErrorState)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Resume
        /// </summary>
        public async Task<OperationalCommandResponse?> Resume(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new OperationalCommandResponse() {
                CommandResponseState = (ErrorState)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Go Home
        /// </summary>
        public async Task<OperationalCommandResponse?> GoHome(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x80, null, token);
            if (!ValidateResponse(resp))
                return null;
            return new OperationalCommandResponse() {
                CommandResponseState = (ErrorState)GetField(resp, 0),
            };
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Phase List Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string[]?> PhaseList { get; init; }

        /// <summary>
        /// Current Phase Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> CurrentPhase { get; init; }

        /// <summary>
        /// Countdown Time Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<TimeSpan?> CountdownTime { get; init; }

        /// <summary>
        /// Operational State List Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<General.OperationalState.OperationalStateStruct[]> OperationalStateList { get; init; }

        /// <summary>
        /// Operational State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> OperationalState { get; init; }

        /// <summary>
        /// Operational Error Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ErrorState> OperationalError { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "RVC Operational State";
        }
    }
}