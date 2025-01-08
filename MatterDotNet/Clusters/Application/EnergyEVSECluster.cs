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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Energy EVSE Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class EnergyEVSECluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0099;

        /// <summary>
        /// Energy EVSE Cluster
        /// </summary>
        public EnergyEVSECluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected EnergyEVSECluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        /// Energy Transfer Stopped Reason
        /// </summary>
        public enum EnergyTransferStoppedReasonEnum {
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
        /// Fault State
        /// </summary>
        public enum FaultStateEnum {
            /// <summary>
            /// The EVSE is not in an error state.
            /// </summary>
            NoError = 0,
            /// <summary>
            /// The EVSE is unable to obtain electrical measurements.
            /// </summary>
            MeterFailure = 1,
            /// <summary>
            /// The EVSE input voltage level is too high.
            /// </summary>
            OverVoltage = 2,
            /// <summary>
            /// The EVSE input voltage level is too low.
            /// </summary>
            UnderVoltage = 3,
            /// <summary>
            /// The EVSE detected charging current higher than allowed by charger.
            /// </summary>
            OverCurrent = 4,
            /// <summary>
            /// The EVSE detected voltage on charging pins when the contactor is open.
            /// </summary>
            ContactWetFailure = 5,
            /// <summary>
            /// The EVSE detected absence of voltage after enabling contactor.
            /// </summary>
            ContactDryFailure = 6,
            /// <summary>
            /// The EVSE has an unbalanced current supply.
            /// </summary>
            GroundFault = 7,
            /// <summary>
            /// The EVSE has detected a loss in power.
            /// </summary>
            PowerLoss = 8,
            /// <summary>
            /// The EVSE has detected another power quality issue (e.g. phase imbalance).
            /// </summary>
            PowerQuality = 9,
            /// <summary>
            /// The EVSE pilot signal amplitude short circuited to ground.
            /// </summary>
            PilotShortCircuit = 10,
            /// <summary>
            /// The emergency stop button was pressed.
            /// </summary>
            EmergencyStop = 11,
            /// <summary>
            /// The EVSE detected that the cable has been disconnected.
            /// </summary>
            EVDisconnected = 12,
            /// <summary>
            /// The EVSE could not determine proper power supply level.
            /// </summary>
            WrongPowerSupply = 13,
            /// <summary>
            /// The EVSE detected Live and Neutral are swapped.
            /// </summary>
            LiveNeutralSwap = 14,
            /// <summary>
            /// The EVSE internal temperature is too high.
            /// </summary>
            OverTemperature = 15,
            /// <summary>
            /// Any other reason.
            /// </summary>
            Other = 255,
        }

        /// <summary>
        /// State
        /// </summary>
        public enum StateEnum {
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
        public enum SupplyStateEnum {
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
            /// The EV is not currently allowed to charge or discharge due to Diagnostics Mode.
            /// </summary>
            DisabledDiagnostics = 4,
        }

        /// <summary>
        /// Target Day Of Week Bitmap
        /// </summary>
        [Flags]
        public enum TargetDayOfWeekBitmap {
            /// <summary>
            /// Sunday
            /// </summary>
            Sunday = 1,
            /// <summary>
            /// Monday
            /// </summary>
            Monday = 2,
            /// <summary>
            /// Tuesday
            /// </summary>
            Tuesday = 4,
            /// <summary>
            /// Wednesday
            /// </summary>
            Wednesday = 8,
            /// <summary>
            /// Thursday
            /// </summary>
            Thursday = 16,
            /// <summary>
            /// Friday
            /// </summary>
            Friday = 32,
            /// <summary>
            /// Saturday
            /// </summary>
            Saturday = 64,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Charging Target Schedule
        /// </summary>
        public record ChargingTargetSchedule : TLVPayload {
            /// <summary>
            /// Charging Target Schedule
            /// </summary>
            public ChargingTargetSchedule() { }

            [SetsRequiredMembers]
            internal ChargingTargetSchedule(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                DayOfWeekForSequence = (TargetDayOfWeekBitmap)reader.GetUShort(0)!.Value;
                {
                    ChargingTargets = new List<ChargingTarget>();
                    foreach (var item in (List<object>)fields[1]) {
                        ChargingTargets.Add(new ChargingTarget((object[])item));
                    }
                }
            }
            public required TargetDayOfWeekBitmap DayOfWeekForSequence { get; set; }
            public required List<ChargingTarget> ChargingTargets { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)DayOfWeekForSequence);
                {
                    Constrain(ChargingTargets, 0, 10);
                    writer.StartList(1);
                    foreach (var item in ChargingTargets) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Charging Target
        /// </summary>
        public record ChargingTarget : TLVPayload {
            /// <summary>
            /// Charging Target
            /// </summary>
            public ChargingTarget() { }

            [SetsRequiredMembers]
            internal ChargingTarget(object[] fields) {
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
        #endregion Records

        #region Payloads
        /// <summary>
        /// Get Targets Response - Reply from server
        /// </summary>
        public struct GetTargetsResponse() {
            public required List<ChargingTargetSchedule> ChargingTargetSchedules { get; set; }
        }

        private record EnableChargingPayload : TLVPayload {
            public required DateTime? ChargingEnabledUntil { get; set; } = null;
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
            public required DateTime? DischargingEnabledUntil { get; set; } = null;
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
            public required List<ChargingTargetSchedule> ChargingTargetSchedules { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    Constrain(ChargingTargetSchedules, 0, 7);
                    writer.StartList(0);
                    foreach (var item in ChargingTargetSchedules) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Disable
        /// </summary>
        public async Task<bool> Disable(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x01);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enable Charging
        /// </summary>
        public async Task<bool> EnableCharging(SecureSession session, ushort commandTimeoutMS, DateTime ChargingEnabledUntil, long MinimumChargeCurrent, long MaximumChargeCurrent) {
            EnableChargingPayload requestFields = new EnableChargingPayload() {
                ChargingEnabledUntil = ChargingEnabledUntil,
                MinimumChargeCurrent = MinimumChargeCurrent,
                MaximumChargeCurrent = MaximumChargeCurrent,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enable Discharging
        /// </summary>
        public async Task<bool> EnableDischarging(SecureSession session, ushort commandTimeoutMS, DateTime DischargingEnabledUntil, long MaximumDischargeCurrent) {
            EnableDischargingPayload requestFields = new EnableDischargingPayload() {
                DischargingEnabledUntil = DischargingEnabledUntil,
                MaximumDischargeCurrent = MaximumDischargeCurrent,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Start Diagnostics
        /// </summary>
        public async Task<bool> StartDiagnostics(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x04);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Targets
        /// </summary>
        public async Task<bool> SetTargets(SecureSession session, ushort commandTimeoutMS, List<ChargingTargetSchedule> ChargingTargetSchedules) {
            SetTargetsPayload requestFields = new SetTargetsPayload() {
                ChargingTargetSchedules = ChargingTargetSchedules,
            };
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Targets
        /// </summary>
        public async Task<GetTargetsResponse?> GetTargets(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x06);
            if (!ValidateResponse(resp))
                return null;
            return new GetTargetsResponse() {
                ChargingTargetSchedules = (List<ChargingTargetSchedule>)GetField(resp, 0),
            };
        }

        /// <summary>
        /// Clear Targets
        /// </summary>
        public async Task<bool> ClearTargets(SecureSession session, ushort commandTimeoutMS) {
            InvokeResponseIB resp = await InteractionManager.ExecTimedCommand(session, endPoint, cluster, commandTimeoutMS, 0x07);
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
        public async Task<StateEnum?> GetState(SecureSession session) {
            return (StateEnum?)await GetEnumAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Supply State attribute
        /// </summary>
        public async Task<SupplyStateEnum> GetSupplyState(SecureSession session) {
            return (SupplyStateEnum)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Fault State attribute
        /// </summary>
        public async Task<FaultStateEnum> GetFaultState(SecureSession session) {
            return (FaultStateEnum)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the Charging Enabled Until attribute
        /// </summary>
        public async Task<DateTime?> GetChargingEnabledUntil(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 3, true) ?? TimeUtil.EPOCH;
        }

        /// <summary>
        /// Get the Discharging Enabled Until attribute
        /// </summary>
        public async Task<DateTime?> GetDischargingEnabledUntil(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 4, true) ?? TimeUtil.EPOCH;
        }

        /// <summary>
        /// Get the Circuit Capacity attribute
        /// </summary>
        public async Task<long> GetCircuitCapacity(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 5) ?? 0;
        }

        /// <summary>
        /// Get the Minimum Charge Current attribute
        /// </summary>
        public async Task<long> GetMinimumChargeCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 6) ?? 6000;
        }

        /// <summary>
        /// Get the Maximum Charge Current attribute
        /// </summary>
        public async Task<long> GetMaximumChargeCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 7) ?? 0;
        }

        /// <summary>
        /// Get the Maximum Discharge Current attribute
        /// </summary>
        public async Task<long> GetMaximumDischargeCurrent(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 8) ?? 0;
        }

        /// <summary>
        /// Get the User Maximum Charge Current attribute
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
            return (DateTime?)(dynamic?)await GetAttribute(session, 35, true) ?? null;
        }

        /// <summary>
        /// Get the Next Charge Target Time attribute
        /// </summary>
        public async Task<DateTime?> GetNextChargeTargetTime(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 36, true) ?? null;
        }

        /// <summary>
        /// Get the Next Charge Required Energy attribute
        /// </summary>
        public async Task<long?> GetNextChargeRequiredEnergy(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 37, true) ?? null;
        }

        /// <summary>
        /// Get the Next Charge Target SoC attribute
        /// </summary>
        public async Task<byte?> GetNextChargeTargetSoC(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 38, true) ?? null;
        }

        /// <summary>
        /// Get the Approximate EV Efficiency attribute
        /// </summary>
        public async Task<ushort?> GetApproximateEVEfficiency(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 39, true) ?? null;
        }

        /// <summary>
        /// Set the Approximate EV Efficiency attribute
        /// </summary>
        public async Task SetApproximateEVEfficiency (SecureSession session, ushort? value = null) {
            await SetAttribute(session, 39, value, true);
        }

        /// <summary>
        /// Get the State Of Charge attribute
        /// </summary>
        public async Task<byte?> GetStateOfCharge(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 48, true) ?? null;
        }

        /// <summary>
        /// Get the Battery Capacity attribute
        /// </summary>
        public async Task<long?> GetBatteryCapacity(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 49, true) ?? null;
        }

        /// <summary>
        /// Get the Vehicle ID attribute
        /// </summary>
        public async Task<string?> GetVehicleID(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 50, true) ?? null;
        }

        /// <summary>
        /// Get the Session ID attribute
        /// </summary>
        public async Task<uint?> GetSessionID(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 64, true) ?? null;
        }

        /// <summary>
        /// Get the Session Duration attribute
        /// </summary>
        public async Task<TimeSpan?> GetSessionDuration(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 65, true);
        }

        /// <summary>
        /// Get the Session Energy Charged attribute
        /// </summary>
        public async Task<long?> GetSessionEnergyCharged(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 66, true) ?? null;
        }

        /// <summary>
        /// Get the Session Energy Discharged attribute
        /// </summary>
        public async Task<long?> GetSessionEnergyDischarged(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 67, true) ?? null;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Energy EVSE Cluster";
        }
    }
}