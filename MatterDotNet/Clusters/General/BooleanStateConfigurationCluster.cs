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
    /// This cluster is used to configure a boolean sensor.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class BooleanStateConfiguration : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0080;

        /// <summary>
        /// This cluster is used to configure a boolean sensor.
        /// </summary>
        [SetsRequiredMembers]
        public BooleanStateConfiguration(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected BooleanStateConfiguration(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            CurrentSensitivityLevel = new ReadWriteAttribute<byte>(cluster, endPoint, 0) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            SupportedSensitivityLevels = new ReadAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            DefaultSensitivityLevel = new ReadAttribute<byte>(cluster, endPoint, 2) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            AlarmsActive = new ReadAttribute<AlarmMode>(cluster, endPoint, 3) {
                Deserialize = x => (AlarmMode)DeserializeEnum(x)!
            };
            AlarmsSuppressed = new ReadAttribute<AlarmMode>(cluster, endPoint, 4) {
                Deserialize = x => (AlarmMode)DeserializeEnum(x)!
            };
            AlarmsEnabled = new ReadAttribute<AlarmMode>(cluster, endPoint, 5) {
                Deserialize = x => (AlarmMode)DeserializeEnum(x)!
            };
            AlarmsSupported = new ReadAttribute<AlarmMode>(cluster, endPoint, 6) {
                Deserialize = x => (AlarmMode)DeserializeEnum(x)!
            };
            SensorFault = new ReadAttribute<SensorFaultBitmap>(cluster, endPoint, 7) {
                Deserialize = x => (SensorFaultBitmap)DeserializeEnum(x)!
            };
        }

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
            None = 0x0,
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
        public enum SensorFaultBitmap : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
        public async Task<bool> SuppressAlarm(SecureSession session, AlarmMode alarmsToSuppress, CancellationToken token = default) {
            SuppressAlarmPayload requestFields = new SuppressAlarmPayload() {
                AlarmsToSuppress = alarmsToSuppress,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enable Disable Alarm
        /// </summary>
        public async Task<bool> EnableDisableAlarm(SecureSession session, AlarmMode alarmsToEnableDisable, CancellationToken token = default) {
            EnableDisableAlarmPayload requestFields = new EnableDisableAlarmPayload() {
                AlarmsToEnableDisable = alarmsToEnableDisable,
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
        /// Current Sensitivity Level Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> CurrentSensitivityLevel { get; init; }

        /// <summary>
        /// Supported Sensitivity Levels Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> SupportedSensitivityLevels { get; init; }

        /// <summary>
        /// Default Sensitivity Level Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> DefaultSensitivityLevel { get; init; }

        /// <summary>
        /// Alarms Active Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<AlarmMode> AlarmsActive { get; init; }

        /// <summary>
        /// Alarms Suppressed Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<AlarmMode> AlarmsSuppressed { get; init; }

        /// <summary>
        /// Alarms Enabled Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<AlarmMode> AlarmsEnabled { get; init; }

        /// <summary>
        /// Alarms Supported Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<AlarmMode> AlarmsSupported { get; init; }

        /// <summary>
        /// Sensor Fault Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SensorFaultBitmap> SensorFault { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Boolean State Configuration";
        }
    }
}