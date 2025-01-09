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
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Util;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Smoke CO Alarm Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class SmokeCOAlarmCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x005C;

        /// <summary>
        /// Smoke CO Alarm Cluster
        /// </summary>
        public SmokeCOAlarmCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected SmokeCOAlarmCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum AlarmStateEnum {
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
        /// Contamination State
        /// </summary>
        public enum ContaminationStateEnum {
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

        /// <summary>
        /// End Of Service
        /// </summary>
        public enum EndOfServiceEnum {
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
        /// Expressed State
        /// </summary>
        public enum ExpressedStateEnum {
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
        public enum MuteStateEnum {
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
        /// Sensitivity
        /// </summary>
        public enum SensitivityEnum {
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
        /// Get the Expressed State attribute
        /// </summary>
        public async Task<ExpressedStateEnum> GetExpressedState(SecureSession session) {
            return (ExpressedStateEnum)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Smoke State attribute
        /// </summary>
        public async Task<AlarmStateEnum> GetSmokeState(SecureSession session) {
            return (AlarmStateEnum)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the CO State attribute
        /// </summary>
        public async Task<AlarmStateEnum> GetCOState(SecureSession session) {
            return (AlarmStateEnum)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the Battery Alert attribute
        /// </summary>
        public async Task<AlarmStateEnum> GetBatteryAlert(SecureSession session) {
            return (AlarmStateEnum)await GetEnumAttribute(session, 3);
        }

        /// <summary>
        /// Get the Device Muted attribute
        /// </summary>
        public async Task<MuteStateEnum> GetDeviceMuted(SecureSession session) {
            return (MuteStateEnum)await GetEnumAttribute(session, 4);
        }

        /// <summary>
        /// Get the Test In Progress attribute
        /// </summary>
        public async Task<bool> GetTestInProgress(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 5))!;
        }

        /// <summary>
        /// Get the Hardware Fault Alert attribute
        /// </summary>
        public async Task<bool> GetHardwareFaultAlert(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 6))!;
        }

        /// <summary>
        /// Get the End Of Service Alert attribute
        /// </summary>
        public async Task<EndOfServiceEnum> GetEndOfServiceAlert(SecureSession session) {
            return (EndOfServiceEnum)await GetEnumAttribute(session, 7);
        }

        /// <summary>
        /// Get the Interconnect Smoke Alarm attribute
        /// </summary>
        public async Task<AlarmStateEnum> GetInterconnectSmokeAlarm(SecureSession session) {
            return (AlarmStateEnum)await GetEnumAttribute(session, 8);
        }

        /// <summary>
        /// Get the Interconnect CO Alarm attribute
        /// </summary>
        public async Task<AlarmStateEnum> GetInterconnectCOAlarm(SecureSession session) {
            return (AlarmStateEnum)await GetEnumAttribute(session, 9);
        }

        /// <summary>
        /// Get the Contamination State attribute
        /// </summary>
        public async Task<ContaminationStateEnum> GetContaminationState(SecureSession session) {
            return (ContaminationStateEnum)await GetEnumAttribute(session, 10);
        }

        /// <summary>
        /// Get the Smoke Sensitivity Level attribute
        /// </summary>
        public async Task<SensitivityEnum> GetSmokeSensitivityLevel(SecureSession session) {
            return (SensitivityEnum)await GetEnumAttribute(session, 11);
        }

        /// <summary>
        /// Set the Smoke Sensitivity Level attribute
        /// </summary>
        public async Task SetSmokeSensitivityLevel (SecureSession session, SensitivityEnum value) {
            await SetAttribute(session, 11, value);
        }

        /// <summary>
        /// Get the Expiry Date attribute
        /// </summary>
        public async Task<DateTime> GetExpiryDate(SecureSession session) {
            return (DateTime)(dynamic?)(await GetAttribute(session, 12))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Smoke CO Alarm Cluster";
        }
    }
}