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
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.MeasurementAndSensing
{
    /// <summary>
    /// This cluster provides an interface for observing and managing the state of smoke and CO alarms.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class SmokeCOAlarm : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x005C;

        /// <summary>
        /// This cluster provides an interface for observing and managing the state of smoke and CO alarms.
        /// </summary>
        [SetsRequiredMembers]
        public SmokeCOAlarm(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected SmokeCOAlarm(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            ExpressedState = new ReadAttribute<ExpressedStateEnum>(cluster, endPoint, 0) {
                Deserialize = x => (ExpressedStateEnum)DeserializeEnum(x)!
            };
            SmokeState = new ReadAttribute<AlarmState>(cluster, endPoint, 1) {
                Deserialize = x => (AlarmState)DeserializeEnum(x)!
            };
            COState = new ReadAttribute<AlarmState>(cluster, endPoint, 2) {
                Deserialize = x => (AlarmState)DeserializeEnum(x)!
            };
            BatteryAlert = new ReadAttribute<AlarmState>(cluster, endPoint, 3) {
                Deserialize = x => (AlarmState)DeserializeEnum(x)!
            };
            DeviceMuted = new ReadAttribute<MuteState>(cluster, endPoint, 4) {
                Deserialize = x => (MuteState)DeserializeEnum(x)!
            };
            TestInProgress = new ReadAttribute<bool>(cluster, endPoint, 5) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            HardwareFaultAlert = new ReadAttribute<bool>(cluster, endPoint, 6) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            EndOfServiceAlert = new ReadAttribute<EndOfService>(cluster, endPoint, 7) {
                Deserialize = x => (EndOfService)DeserializeEnum(x)!
            };
            InterconnectSmokeAlarm = new ReadAttribute<AlarmState>(cluster, endPoint, 8) {
                Deserialize = x => (AlarmState)DeserializeEnum(x)!
            };
            InterconnectCOAlarm = new ReadAttribute<AlarmState>(cluster, endPoint, 9) {
                Deserialize = x => (AlarmState)DeserializeEnum(x)!
            };
            ContaminationState = new ReadAttribute<ContaminationStateEnum>(cluster, endPoint, 10) {
                Deserialize = x => (ContaminationStateEnum)DeserializeEnum(x)!
            };
            SmokeSensitivityLevel = new ReadWriteAttribute<Sensitivity>(cluster, endPoint, 11) {
                Deserialize = x => (Sensitivity)DeserializeEnum(x)!
            };
            ExpiryDate = new ReadAttribute<DateTime>(cluster, endPoint, 12) {
                Deserialize = x => TimeUtil.FromEpochSeconds((uint)(dynamic?)x)!.Value
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports Smoke alarm
            /// </summary>
            SmokeAlarm = 1,
            /// <summary>
            /// Supports CO alarm
            /// </summary>
            COAlarm = 2,
        }

        /// <summary>
        /// Alarm State
        /// </summary>
        public enum AlarmState : byte {
            /// <summary>
            /// Nominal state, the device is not alarming
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Warning state
            /// </summary>
            Warning = 1,
            /// <summary>
            /// Critical state
            /// </summary>
            Critical = 2,
        }

        /// <summary>
        /// Sensitivity
        /// </summary>
        public enum Sensitivity : byte {
            /// <summary>
            /// High sensitivity
            /// </summary>
            High = 0,
            /// <summary>
            /// Standard Sensitivity
            /// </summary>
            Standard = 1,
            /// <summary>
            /// Low sensitivity
            /// </summary>
            Low = 2,
        }

        /// <summary>
        /// Expressed State
        /// </summary>
        public enum ExpressedStateEnum : byte {
            /// <summary>
            /// Nominal state, the device is not alarming
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Smoke Alarm state
            /// </summary>
            SmokeAlarm = 1,
            /// <summary>
            /// CO Alarm state
            /// </summary>
            COAlarm = 2,
            /// <summary>
            /// Battery Alert State
            /// </summary>
            BatteryAlert = 3,
            /// <summary>
            /// Test in Progress
            /// </summary>
            Testing = 4,
            /// <summary>
            /// Hardware Fault Alert State
            /// </summary>
            HardwareFault = 5,
            /// <summary>
            /// End of Service Alert State
            /// </summary>
            EndOfService = 6,
            /// <summary>
            /// Interconnected Smoke Alarm State
            /// </summary>
            InterconnectSmoke = 7,
            /// <summary>
            /// Interconnected CO Alarm State
            /// </summary>
            InterconnectCO = 8,
        }

        /// <summary>
        /// Mute State
        /// </summary>
        public enum MuteState : byte {
            /// <summary>
            /// Not Muted
            /// </summary>
            NotMuted = 0,
            /// <summary>
            /// Muted
            /// </summary>
            Muted = 1,
        }

        /// <summary>
        /// End Of Service
        /// </summary>
        public enum EndOfService : byte {
            /// <summary>
            /// Device has not expired
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Device has reached its end of service
            /// </summary>
            Expired = 1,
        }

        /// <summary>
        /// Contamination State
        /// </summary>
        public enum ContaminationStateEnum : byte {
            /// <summary>
            /// Nominal state, the sensor is not contaminated
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Low contamination
            /// </summary>
            Low = 1,
            /// <summary>
            /// Warning state
            /// </summary>
            Warning = 2,
            /// <summary>
            /// Critical state, will cause nuisance alarms
            /// </summary>
            Critical = 3,
        }
        #endregion Enums

        #region Payloads
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Self Test Request
        /// </summary>
        public async Task<bool> SelfTestRequest(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
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
        /// Expressed State Attribute
        /// </summary>
        public required ReadAttribute<ExpressedStateEnum> ExpressedState { get; init; }

        /// <summary>
        /// Smoke State Attribute
        /// </summary>
        public required ReadAttribute<AlarmState> SmokeState { get; init; }

        /// <summary>
        /// CO State Attribute
        /// </summary>
        public required ReadAttribute<AlarmState> COState { get; init; }

        /// <summary>
        /// Battery Alert Attribute
        /// </summary>
        public required ReadAttribute<AlarmState> BatteryAlert { get; init; }

        /// <summary>
        /// Device Muted Attribute
        /// </summary>
        public required ReadAttribute<MuteState> DeviceMuted { get; init; }

        /// <summary>
        /// Test In Progress Attribute
        /// </summary>
        public required ReadAttribute<bool> TestInProgress { get; init; }

        /// <summary>
        /// Hardware Fault Alert Attribute
        /// </summary>
        public required ReadAttribute<bool> HardwareFaultAlert { get; init; }

        /// <summary>
        /// End Of Service Alert Attribute
        /// </summary>
        public required ReadAttribute<EndOfService> EndOfServiceAlert { get; init; }

        /// <summary>
        /// Interconnect Smoke Alarm Attribute
        /// </summary>
        public required ReadAttribute<AlarmState> InterconnectSmokeAlarm { get; init; }

        /// <summary>
        /// Interconnect CO Alarm Attribute
        /// </summary>
        public required ReadAttribute<AlarmState> InterconnectCOAlarm { get; init; }

        /// <summary>
        /// Contamination State Attribute
        /// </summary>
        public required ReadAttribute<ContaminationStateEnum> ContaminationState { get; init; }

        /// <summary>
        /// Smoke Sensitivity Level Attribute
        /// </summary>
        public required ReadWriteAttribute<Sensitivity> SmokeSensitivityLevel { get; init; }

        /// <summary>
        /// Expiry Date Attribute
        /// </summary>
        public required ReadAttribute<DateTime> ExpiryDate { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Smoke CO Alarm";
        }
    }
}