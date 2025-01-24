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

namespace MatterDotNet.Clusters.EnergyManagement
{
    /// <summary>
    /// Electric Vehicle Supply Equipment (EVSE) is equipment used to charge an Electric Vehicle (EV) or Plug-In Hybrid Electric Vehicle. This cluster provides an interface to the functionality of Electric Vehicle Supply Equipment (EVSE) management.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class EnergyEVSE : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0099;

        /// <summary>
        /// Electric Vehicle Supply Equipment (EVSE) is equipment used to charge an Electric Vehicle (EV) or Plug-In Hybrid Electric Vehicle. This cluster provides an interface to the functionality of Electric Vehicle Supply Equipment (EVSE) management.
        /// </summary>
        public EnergyEVSE(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected EnergyEVSE(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// EVSE supports storing user charging preferences
            /// </summary>
            ChargingPreferences = 1,
            /// <summary>
            /// EVSE supports reporting of vehicle State of Charge (SoC)
            /// </summary>
            SoCReporting = 2,
            /// <summary>
            /// EVSE supports PLC to support Plug and Charge
            /// </summary>
            PlugAndCharge = 4,
            /// <summary>
            /// EVSE is fitted with an RFID reader
            /// </summary>
            RFID = 8,
            /// <summary>
            /// EVSE supports bi-directional charging / discharging
            /// </summary>
            V2X = 16,
        }

        /// <summary>
        /// State
        /// </summary>
        public enum State : byte {
            /// <summary>
            /// The EV is not plugged in.
            /// </summary>
            NotPluggedIn = 0,
            /// <summary>
            /// The EV is plugged in, but not demanding current.
            /// </summary>
            PluggedInNoDemand = 1,
            /// <summary>
            /// The EV is plugged in and is demanding current, but EVSE is not allowing current to flow.
            /// </summary>
            PluggedInDemand = 2,
            /// <summary>
            /// The EV is plugged in, charging is in progress, and current is flowing
            /// </summary>
            PluggedInCharging = 3,
            /// <summary>
            /// The EV is plugged in, discharging is in progress, and current is flowing
            /// </summary>
            PluggedInDischarging = 4,
            /// <summary>
            /// The EVSE is transitioning from any plugged-in state to NotPluggedIn
            /// </summary>
            SessionEnding = 5,
            /// <summary>
            /// There is a fault (see FaultState Attribute attribute)
            /// </summary>
            Fault = 6,
        }

        /// <summary>
        /// Supply State
        /// </summary>
        public enum SupplyState : byte {
            /// <summary>
            /// The EV is not currently allowed to charge or discharge
            /// </summary>
            Disabled = 0,
            /// <summary>
            /// The EV is currently allowed to charge
            /// </summary>
            ChargingEnabled = 1,
            /// <summary>
            /// The EV is currently allowed to discharge
            /// </summary>
            DischargingEnabled = 2,
            /// <summary>
            /// The EV is not currently allowed to charge or discharge due to an error. The error must be cleared before operation can continue.
            /// </summary>
            DisabledError = 3,
            /// <summary>
            /// The EV is not currently allowed to charge or discharge due to self-diagnostics mode.
            /// </summary>
            DisabledDiagnostics = 4,
            /// <summary>
            /// The EV is currently allowed to charge and discharge
            /// </summary>
            Enabled = 5,
        }

        /// <summary>
        /// Fault State
        /// </summary>
        public enum FaultState : byte {
            /// <summary>
            /// The EVSE is not in an error state.
            /// </summary>
            NoError = 0x0,
            /// <summary>
            /// The EVSE is unable to obtain electrical measurements.
            /// </summary>
            MeterFailure = 0x1,
            /// <summary>
            /// The EVSE input voltage level is too high.
            /// </summary>
            OverVoltage = 0x2,
            /// <summary>
            /// The EVSE input voltage level is too low.
            /// </summary>
            UnderVoltage = 0x3,
            /// <summary>
            /// The EVSE detected charging current higher than allowed by charger.
            /// </summary>
            OverCurrent = 0x4,
            /// <summary>
            /// The EVSE detected voltage on charging pins when the contactor is open.
            /// </summary>
            ContactWetFailure = 0x5,
            /// <summary>
            /// The EVSE detected absence of voltage after enabling contactor.
            /// </summary>
            ContactDryFailure = 0x6,
            /// <summary>
            /// The EVSE has an unbalanced current supply.
            /// </summary>
            GroundFault = 0x7,
            /// <summary>
            /// The EVSE has detected a loss in power.
            /// </summary>
            PowerLoss = 0x8,
            /// <summary>
            /// The EVSE has detected another power quality issue (e.g. phase imbalance).
            /// </summary>
            PowerQuality = 0x9,
            /// <summary>
            /// The EVSE pilot signal amplitude short circuited to ground.
            /// </summary>
            PilotShortCircuit = 0xA,
            /// <summary>
            /// The emergency stop button was pressed.
            /// </summary>
            EmergencyStop = 0xB,
            /// <summary>
            /// The EVSE detected that the cable has been disconnected.
            /// </summary>
            EVDisconnected = 0xC,
            /// <summary>
            /// The EVSE could not determine proper power supply level.
            /// </summary>
            WrongPowerSupply = 0xD,
            /// <summary>
            /// The EVSE detected Live and Neutral are swapped.
            /// </summary>
            LiveNeutralSwap = 0xE,
            /// <summary>
            /// The EVSE internal temperature is too high.
            /// </summary>
            OverTemperature = 0xF,
            /// <summary>
            /// Any other reason.
            /// </summary>
            Other = 0xFF,
        }

        /// <summary>
        /// Energy Transfer Stopped Reason
        /// </summary>
        public enum EnergyTransferStoppedReason : byte {
            /// <summary>
            /// The EV decided to stop
            /// </summary>
            EVStopped = 0,
            /// <summary>
            /// The EVSE decided to stop
            /// </summary>
            EVSEStopped = 1,
            /// <summary>
            /// An other unknown reason
            /// </summary>
            Other = 2,
        }

        /// <summary>
        /// Target Day Of Week
        /// </summary>
        [Flags]
        public enum TargetDayOfWeek : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Sunday
            /// </summary>
            Sunday = 0x01,
            /// <summary>
            /// Monday
            /// </summary>
            Monday = 0x02,
            /// <summary>
            /// Tuesday
            /// </summary>
            Tuesday = 0x04,
            /// <summary>
            /// Wednesday
            /// </summary>
            Wednesday = 0x08,
            /// <summary>
            /// Thursday
            /// </summary>
            Thursday = 0x10,
            /// <summary>
            /// Friday
            /// </summary>
            Friday = 0x20,
            /// <summary>
            /// Saturday
            /// </summary>
            Saturday = 0x40,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Charging Target
        /// </summary>
        public record ChargingTarget : TLVPayload {
            /// <summary>
            /// Charging Target
            /// </summary>
            public ChargingTarget() { }

            /// <summary>
            /// Charging Target
            /// </summary>
            [SetsRequiredMembers]
            public ChargingTarget(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                TargetTimeMinutesPastMidnight = reader.GetUShort(0)!.Value;
                TargetSoC = reader.GetByte(1, true);
                AddedEnergy = reader.GetLong(2, true);
            }
            public required ushort TargetTimeMinutesPastMidnight { get; set; } = 0;
            public byte? TargetSoC { get; set; } = 0;
            public long? AddedEnergy { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, TargetTimeMinutesPastMidnight, 1439);
                if (TargetSoC != null)
                    writer.WriteByte(1, TargetSoC);
                if (AddedEnergy != null)
                    writer.WriteLong(2, AddedEnergy, long.MaxValue, 0);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Charging Target Schedule
        /// </summary>
        public record ChargingTargetSchedule : TLVPayload {
            /// <summary>
            /// Charging Target Schedule
            /// </summary>
            public ChargingTargetSchedule() { }

            /// <summary>
            /// Charging Target Schedule
            /// </summary>
            [SetsRequiredMembers]
            public ChargingTargetSchedule(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                DayOfWeekForSequence = (TargetDayOfWeek)reader.GetUInt(0)!.Value;
                {
                    ChargingTargets = new ChargingTarget[reader.GetStruct(1)!.Length];
                    for (int n = 0; n < ChargingTargets.Length; n++) {
                        ChargingTargets[n] = new ChargingTarget((object[])((object[])fields[1])[n]);
                    }
                }
            }
            public required TargetDayOfWeek DayOfWeekForSequence { get; set; }
            public required ChargingTarget[] ChargingTargets { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)DayOfWeekForSequence);
                {
                    Constrain(ChargingTargets, 0, 10);
                    writer.StartArray(1);
                    foreach (var item in ChargingTargets) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record EnableChargingPayload : TLVPayload {
            public required DateTime? ChargingEnabledUntil { get; set; }
            public required long MinimumChargeCurrent { get; set; }
            public required long MaximumChargeCurrent { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (!ChargingEnabledUntil.HasValue)
                    writer.WriteNull(0);
                else
                    writer.WriteUInt(0, TimeUtil.ToEpochSeconds(ChargingEnabledUntil!.Value));
                writer.WriteLong(1, MinimumChargeCurrent, long.MaxValue, 0);
                writer.WriteLong(2, MaximumChargeCurrent, long.MaxValue, 0);
                writer.EndContainer();
            }
        }

        private record EnableDischargingPayload : TLVPayload {
            public required DateTime? DischargingEnabledUntil { get; set; }
            public required long MaximumDischargeCurrent { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (!DischargingEnabledUntil.HasValue)
                    writer.WriteNull(0);
                else
                    writer.WriteUInt(0, TimeUtil.ToEpochSeconds(DischargingEnabledUntil!.Value));
                writer.WriteLong(1, MaximumDischargeCurrent, long.MaxValue, 0);
                writer.EndContainer();
            }
        }

        private record SetTargetsPayload : TLVPayload {
            public required ChargingTargetSchedule[] ChargingTargetSchedules { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    Constrain(ChargingTargetSchedules, 0, 7);
                    writer.StartArray(0);
                    foreach (var item in ChargingTargetSchedules) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Targets Response - Reply from server
        /// </summary>
        public struct GetTargetsResponse() {
            public required ChargingTargetSchedule[] ChargingTargetSchedules { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Disable
        /// </summary>
        public async Task<bool> Disable(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x01, commandTimeoutMS);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enable Charging
        /// </summary>
        public async Task<bool> EnableCharging(SecureSession session, ushort commandTimeoutMS, DateTime? chargingEnabledUntil, long minimumChargeCurrent, long maximumChargeCurrent) {
            EnableChargingPayload requestFields = new EnableChargingPayload() {
                ChargingEnabledUntil = chargingEnabledUntil,
                MinimumChargeCurrent = minimumChargeCurrent,
                MaximumChargeCurrent = maximumChargeCurrent,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x02, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enable Discharging
        /// </summary>
        public async Task<bool> EnableDischarging(SecureSession session, ushort commandTimeoutMS, DateTime? dischargingEnabledUntil, long maximumDischargeCurrent) {
            EnableDischargingPayload requestFields = new EnableDischargingPayload() {
                DischargingEnabledUntil = dischargingEnabledUntil,
                MaximumDischargeCurrent = maximumDischargeCurrent,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x03, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Start Diagnostics
        /// </summary>
        public async Task<bool> StartDiagnostics(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x04, commandTimeoutMS);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Targets
        /// </summary>
        public async Task<bool> SetTargets(SecureSession session, ushort commandTimeoutMS, ChargingTargetSchedule[] chargingTargetSchedules) {
            SetTargetsPayload requestFields = new SetTargetsPayload() {
                ChargingTargetSchedules = chargingTargetSchedules,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x05, commandTimeoutMS, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Targets
        /// </summary>
        public async Task<GetTargetsResponse?> GetTargets(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x06, commandTimeoutMS);
            if (!ValidateResponse(resp))
                return null;
            return new GetTargetsResponse() {
                ChargingTargetSchedules = GetArrayField<ChargingTargetSchedule>(resp, 0),
            };
        }

        /// <summary>
        /// Clear Targets
        /// </summary>
        public async Task<bool> ClearTargets(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, 0x07, commandTimeoutMS);
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
        /// Get the State attribute
        /// </summary>
        public async Task<State?> GetState(SecureSession session) {
            return (State?)await GetEnumAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Supply State attribute
        /// </summary>
        public async Task<SupplyState> GetSupplyState(SecureSession session) {
            return (SupplyState)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Fault State attribute
        /// </summary>
        public async Task<FaultState> GetFaultState(SecureSession session) {
            return (FaultState)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the Charging Enabled Until attribute
        /// </summary>
        public async Task<DateTime?> GetChargingEnabledUntil(SecureSession session) {
            return TimeUtil.FromEpochSeconds((uint)(dynamic?)await GetAttribute(session, 3)) ?? TimeUtil.EPOCH;
        }

        /// <summary>
        /// Get the Discharging Enabled Until attribute
        /// </summary>
        public async Task<DateTime?> GetDischargingEnabledUntil(SecureSession session) {
            return TimeUtil.FromEpochSeconds((uint)(dynamic?)await GetAttribute(session, 4)) ?? TimeUtil.EPOCH;
        }

        /// <summary>
        /// Get the Circuit Capacity [mA] attribute
        /// </summary>
        public async Task<long> GetCircuitCapacity(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 5) ?? 0;
        }

        /// <summary>
        /// Get the Minimum Charge Current [mA] attribute
        /// </summary>
        public async Task<long> GetMinimumChargeCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 6) ?? 6000;
        }

        /// <summary>
        /// Get the Maximum Charge Current [mA] attribute
        /// </summary>
        public async Task<long> GetMaximumChargeCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 7) ?? 0;
        }

        /// <summary>
        /// Get the Maximum Discharge Current [mA] attribute
        /// </summary>
        public async Task<long> GetMaximumDischargeCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 8) ?? 0;
        }

        /// <summary>
        /// Get the User Maximum Charge Current [mA] attribute
        /// </summary>
        public async Task<long> GetUserMaximumChargeCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 9) ?? 0;
        }

        /// <summary>
        /// Set the User Maximum Charge Current attribute
        /// </summary>
        public async Task SetUserMaximumChargeCurrent (SecureSession session, long? value = 0) {
            await SetAttribute(session, 9, value);
        }

        /// <summary>
        /// Get the Randomization Delay Window attribute
        /// </summary>
        public async Task<TimeSpan> GetRandomizationDelayWindow(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 10) ?? TimeSpan.FromSeconds(600);
        }

        /// <summary>
        /// Set the Randomization Delay Window attribute
        /// </summary>
        public async Task SetRandomizationDelayWindow (SecureSession session, TimeSpan? value) {
            await SetAttribute(session, 10, value ?? TimeSpan.FromSeconds(600));
        }

        /// <summary>
        /// Get the Next Charge Start Time attribute
        /// </summary>
        public async Task<DateTime?> GetNextChargeStartTime(SecureSession session) {
            return TimeUtil.FromEpochSeconds((uint)(dynamic?)await GetAttribute(session, 35));
        }

        /// <summary>
        /// Get the Next Charge Target Time attribute
        /// </summary>
        public async Task<DateTime?> GetNextChargeTargetTime(SecureSession session) {
            return TimeUtil.FromEpochSeconds((uint)(dynamic?)await GetAttribute(session, 36));
        }

        /// <summary>
        /// Get the Next Charge Required Energy [mWh] attribute
        /// </summary>
        public async Task<long?> GetNextChargeRequiredEnergy(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 37, true);
        }

        /// <summary>
        /// Get the Next Charge Target SoC [%] attribute
        /// </summary>
        public async Task<byte?> GetNextChargeTargetSoC(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 38, true);
        }

        /// <summary>
        /// Get the Approximate EV Efficiency attribute
        /// </summary>
        public async Task<ushort?> GetApproximateEVEfficiency(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 39, true);
        }

        /// <summary>
        /// Set the Approximate EV Efficiency attribute
        /// </summary>
        public async Task SetApproximateEVEfficiency (SecureSession session, ushort? value) {
            await SetAttribute(session, 39, value, true);
        }

        /// <summary>
        /// Get the State Of Charge [%] attribute
        /// </summary>
        public async Task<byte?> GetStateOfCharge(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 48, true);
        }

        /// <summary>
        /// Get the Battery Capacity [mWh] attribute
        /// </summary>
        public async Task<long?> GetBatteryCapacity(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 49, true);
        }

        /// <summary>
        /// Get the Vehicle ID attribute
        /// </summary>
        public async Task<string?> GetVehicleID(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 50, true);
        }

        /// <summary>
        /// Get the Session ID attribute
        /// </summary>
        public async Task<uint?> GetSessionID(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 64, true);
        }

        /// <summary>
        /// Get the Session Duration attribute
        /// </summary>
        public async Task<TimeSpan?> GetSessionDuration(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 65, true);
        }

        /// <summary>
        /// Get the Session Energy Charged [mWh] attribute
        /// </summary>
        public async Task<long?> GetSessionEnergyCharged(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 66, true);
        }

        /// <summary>
        /// Get the Session Energy Discharged [mWh] attribute
        /// </summary>
        public async Task<long?> GetSessionEnergyDischarged(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 67, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Energy EVSE";
        }
    }
}