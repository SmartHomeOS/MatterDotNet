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
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.HVAC
{
    /// <summary>
    /// This cluster is used to configure a valve.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ValveConfigurationandControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0081;

        /// <summary>
        /// This cluster is used to configure a valve.
        /// </summary>
        [SetsRequiredMembers]
        public ValveConfigurationandControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ValveConfigurationandControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            OpenDuration = new ReadAttribute<TimeSpan?>(cluster, endPoint, 0, true) {
                Deserialize = x => (TimeSpan?)(dynamic?)x
            };
            DefaultOpenDuration = new ReadWriteAttribute<TimeSpan?>(cluster, endPoint, 1, true) {
                Deserialize = x => (TimeSpan?)(dynamic?)x
            };
            AutoCloseTime = new ReadAttribute<DateTime?>(cluster, endPoint, 2, true) {
                Deserialize = x => (DateTime?)(dynamic?)x
            };
            RemainingDuration = new ReadAttribute<TimeSpan?>(cluster, endPoint, 3, true) {
                Deserialize = x => (TimeSpan?)(dynamic?)x
            };
            CurrentState = new ReadAttribute<ValveState?>(cluster, endPoint, 4, true) {
                Deserialize = x => (ValveState?)DeserializeEnum(x)
            };
            TargetState = new ReadAttribute<ValveState?>(cluster, endPoint, 5, true) {
                Deserialize = x => (ValveState?)DeserializeEnum(x)
            };
            CurrentLevel = new ReadAttribute<byte?>(cluster, endPoint, 6, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            TargetLevel = new ReadAttribute<byte?>(cluster, endPoint, 7, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            DefaultOpenLevel = new ReadWriteAttribute<byte>(cluster, endPoint, 8) {
                Deserialize = x => (byte?)(dynamic?)x ?? 100

            };
            ValveFault = new ReadAttribute<ValveFaultBitmap>(cluster, endPoint, 9) {
                Deserialize = x => (ValveFaultBitmap)DeserializeEnum(x)!
            };
            LevelStep = new ReadAttribute<byte>(cluster, endPoint, 10) {
                Deserialize = x => (byte)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// UTC time is used for time indications
            /// </summary>
            TimeSync = 1,
            /// <summary>
            /// Device supports setting the specific position of the valve
            /// </summary>
            Level = 2,
        }

        /// <summary>
        /// Valve State
        /// </summary>
        public enum ValveState : byte {
            /// <summary>
            /// Valve is in closed position
            /// </summary>
            Closed = 0,
            /// <summary>
            /// Valve is in open position
            /// </summary>
            Open = 1,
            /// <summary>
            /// Valve is transitioning between closed and open positions or between levels
            /// </summary>
            Transitioning = 2,
        }

        /// <summary>
        /// Status Code
        /// </summary>
        public enum StatusCode : byte {
            FailureDueToFault = 2,
        }

        /// <summary>
        /// Valve Fault
        /// </summary>
        [Flags]
        public enum ValveFaultBitmap : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Unspecified fault detected
            /// </summary>
            GeneralFault = 0x0001,
            /// <summary>
            /// Valve is blocked
            /// </summary>
            Blocked = 0x0002,
            /// <summary>
            /// Valve has detected a leak
            /// </summary>
            Leaking = 0x0004,
            /// <summary>
            /// No valve is connected to controller
            /// </summary>
            NotConnected = 0x0008,
            /// <summary>
            /// Short circuit is detected
            /// </summary>
            ShortCircuit = 0x0010,
            /// <summary>
            /// The available current has been exceeded
            /// </summary>
            CurrentExceeded = 0x0020,
        }
        #endregion Enums

        #region Payloads
        private record OpenPayload : TLVPayload {
            public TimeSpan? OpenDuration { get; set; }
            public byte? TargetLevel { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (OpenDuration != null)
                    writer.WriteUInt(0, (uint)OpenDuration!.Value.TotalSeconds);
                if (TargetLevel != null)
                    writer.WriteByte(1, TargetLevel);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Open
        /// </summary>
        public async Task<bool> Open(SecureSession session, TimeSpan? openDuration, byte? targetLevel) {
            OpenPayload requestFields = new OpenPayload() {
                OpenDuration = openDuration,
                TargetLevel = targetLevel,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Close
        /// </summary>
        public async Task<bool> Close(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
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
        /// Open Duration Attribute
        /// </summary>
        public required ReadAttribute<TimeSpan?> OpenDuration { get; init; }

        /// <summary>
        /// Default Open Duration Attribute
        /// </summary>
        public required ReadWriteAttribute<TimeSpan?> DefaultOpenDuration { get; init; }

        /// <summary>
        /// Auto Close Time Attribute
        /// </summary>
        public required ReadAttribute<DateTime?> AutoCloseTime { get; init; }

        /// <summary>
        /// Remaining Duration Attribute
        /// </summary>
        public required ReadAttribute<TimeSpan?> RemainingDuration { get; init; }

        /// <summary>
        /// Current State Attribute
        /// </summary>
        public required ReadAttribute<ValveState?> CurrentState { get; init; }

        /// <summary>
        /// Target State Attribute
        /// </summary>
        public required ReadAttribute<ValveState?> TargetState { get; init; }

        /// <summary>
        /// Current Level [%] Attribute
        /// </summary>
        public required ReadAttribute<byte?> CurrentLevel { get; init; }

        /// <summary>
        /// Target Level [%] Attribute
        /// </summary>
        public required ReadAttribute<byte?> TargetLevel { get; init; }

        /// <summary>
        /// Default Open Level [%] Attribute
        /// </summary>
        public required ReadWriteAttribute<byte> DefaultOpenLevel { get; init; }

        /// <summary>
        /// Valve Fault Attribute
        /// </summary>
        public required ReadAttribute<ValveFaultBitmap> ValveFault { get; init; }

        /// <summary>
        /// Level Step Attribute
        /// </summary>
        public required ReadAttribute<byte> LevelStep { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Valve Configuration and Control";
        }
    }
}