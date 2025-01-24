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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// This cluster is used to configure a boolean sensor.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class BooleanStateConfiguration : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0080;

        /// <summary>
        /// This cluster is used to configure a boolean sensor.
        /// </summary>
        public BooleanStateConfiguration(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected BooleanStateConfiguration(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports visual alarms
            /// </summary>
            Visual = 1,
            /// <summary>
            /// Supports audible alarms
            /// </summary>
            Audible = 2,
            /// <summary>
            /// Supports ability to suppress or acknowledge alarms
            /// </summary>
            AlarmSuppress = 4,
            /// <summary>
            /// Supports ability to set sensor sensitivity
            /// </summary>
            SensitivityLevel = 8,
        }

        /// <summary>
        /// Alarm Mode
        /// </summary>
        [Flags]
        public enum AlarmMode : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Visual alarming
            /// </summary>
            Visual = 0x01,
            /// <summary>
            /// Audible alarming
            /// </summary>
            Audible = 0x02,
        }

        /// <summary>
        /// Sensor Fault
        /// </summary>
        [Flags]
        public enum SensorFault : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Unspecified fault detected
            /// </summary>
            GeneralFault = 0x0001,
        }
        #endregion Enums

        #region Payloads
        private record SuppressAlarmPayload : TLVPayload {
            public required AlarmMode AlarmsToSuppress { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)AlarmsToSuppress);
                writer.EndContainer();
            }
        }

        private record EnableDisableAlarmPayload : TLVPayload {
            public required AlarmMode AlarmsToEnableDisable { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)AlarmsToEnableDisable);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Suppress Alarm
        /// </summary>
        public async Task<bool> SuppressAlarm(SecureSession session, AlarmMode alarmsToSuppress) {
            SuppressAlarmPayload requestFields = new SuppressAlarmPayload() {
                AlarmsToSuppress = alarmsToSuppress,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enable Disable Alarm
        /// </summary>
        public async Task<bool> EnableDisableAlarm(SecureSession session, AlarmMode alarmsToEnableDisable) {
            EnableDisableAlarmPayload requestFields = new EnableDisableAlarmPayload() {
                AlarmsToEnableDisable = alarmsToEnableDisable,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
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
        /// Get the Current Sensitivity Level attribute
        /// </summary>
        public async Task<byte> GetCurrentSensitivityLevel(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Set the Current Sensitivity Level attribute
        /// </summary>
        public async Task SetCurrentSensitivityLevel (SecureSession session, byte value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Supported Sensitivity Levels attribute
        /// </summary>
        public async Task<byte> GetSupportedSensitivityLevels(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Default Sensitivity Level attribute
        /// </summary>
        public async Task<byte> GetDefaultSensitivityLevel(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Alarms Active attribute
        /// </summary>
        public async Task<AlarmMode> GetAlarmsActive(SecureSession session) {
            return (AlarmMode)await GetEnumAttribute(session, 3);
        }

        /// <summary>
        /// Get the Alarms Suppressed attribute
        /// </summary>
        public async Task<AlarmMode> GetAlarmsSuppressed(SecureSession session) {
            return (AlarmMode)await GetEnumAttribute(session, 4);
        }

        /// <summary>
        /// Get the Alarms Enabled attribute
        /// </summary>
        public async Task<AlarmMode> GetAlarmsEnabled(SecureSession session) {
            return (AlarmMode)await GetEnumAttribute(session, 5);
        }

        /// <summary>
        /// Get the Alarms Supported attribute
        /// </summary>
        public async Task<AlarmMode> GetAlarmsSupported(SecureSession session) {
            return (AlarmMode)await GetEnumAttribute(session, 6);
        }

        /// <summary>
        /// Get the Sensor Fault attribute
        /// </summary>
        public async Task<SensorFault> GetSensorFault(SecureSession session) {
            return (SensorFault)await GetEnumAttribute(session, 7);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Boolean State Configuration";
        }
    }
}