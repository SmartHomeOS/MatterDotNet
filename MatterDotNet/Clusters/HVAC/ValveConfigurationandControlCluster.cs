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
        public ValveConfigurationandControl(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ValveConfigurationandControl(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
            Closed = 0x0,
            /// <summary>
            /// Valve is in open position
            /// </summary>
            Open = 0x1,
            /// <summary>
            /// Valve is transitioning between closed and open positions or between levels
            /// </summary>
            Transitioning = 0x2,
        }

        /// <summary>
        /// Status Code
        /// </summary>
        public enum StatusCode : byte {
            FailureDueToFault = 0x02,
        }

        /// <summary>
        /// Valve Fault
        /// </summary>
        [Flags]
        public enum ValveFault : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            GeneralFault = 0x01,
            Blocked = 0x02,
            Leaking = 0x04,
            NotConnected = 0x08,
            ShortCircuit = 0x10,
            CurrentExceeded = 0x20,
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
        /// Get the Open Duration attribute
        /// </summary>
        public async Task<TimeSpan?> GetOpenDuration(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Default Open Duration attribute
        /// </summary>
        public async Task<TimeSpan?> GetDefaultOpenDuration(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 1, true);
        }

        /// <summary>
        /// Set the Default Open Duration attribute
        /// </summary>
        public async Task SetDefaultOpenDuration (SecureSession session, TimeSpan? value) {
            await SetAttribute(session, 1, value, true);
        }

        /// <summary>
        /// Get the Auto Close Time attribute
        /// </summary>
        public async Task<DateTime?> GetAutoCloseTime(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 2, true);
        }

        /// <summary>
        /// Get the Remaining Duration attribute
        /// </summary>
        public async Task<TimeSpan?> GetRemainingDuration(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 3, true);
        }

        /// <summary>
        /// Get the Current State attribute
        /// </summary>
        public async Task<ValveState?> GetCurrentState(SecureSession session) {
            return (ValveState?)await GetEnumAttribute(session, 4, true);
        }

        /// <summary>
        /// Get the Target State attribute
        /// </summary>
        public async Task<ValveState?> GetTargetState(SecureSession session) {
            return (ValveState?)await GetEnumAttribute(session, 5, true);
        }

        /// <summary>
        /// Get the Current Level [%] attribute
        /// </summary>
        public async Task<byte?> GetCurrentLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 6, true);
        }

        /// <summary>
        /// Get the Target Level [%] attribute
        /// </summary>
        public async Task<byte?> GetTargetLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 7, true);
        }

        /// <summary>
        /// Get the Default Open Level [%] attribute
        /// </summary>
        public async Task<byte> GetDefaultOpenLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 8) ?? 100;
        }

        /// <summary>
        /// Set the Default Open Level attribute
        /// </summary>
        public async Task SetDefaultOpenLevel (SecureSession session, byte? value = 100) {
            await SetAttribute(session, 8, value);
        }

        /// <summary>
        /// Get the Valve Fault attribute
        /// </summary>
        public async Task<ValveFault> GetValveFault(SecureSession session) {
            return (ValveFault)await GetEnumAttribute(session, 9);
        }

        /// <summary>
        /// Get the Level Step attribute
        /// </summary>
        public async Task<byte> GetLevelStep(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 10))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Valve Configuration and Control";
        }
    }
}