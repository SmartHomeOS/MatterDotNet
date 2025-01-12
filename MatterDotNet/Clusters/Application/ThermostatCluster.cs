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
    /// Thermostat Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 6)]
    public class ThermostatCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0201;

        /// <summary>
        /// Thermostat Cluster
        /// </summary>
        public ThermostatCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ThermostatCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Thermostat is capable of managing a heating device
            /// </summary>
            Heating = 1,
            /// <summary>
            /// Thermostat is capable of managing a cooling device
            /// </summary>
            Cooling = 2,
            /// <summary>
            /// Supports Occupied and Unoccupied setpoints
            /// </summary>
            Occupancy = 4,
            /// <summary>
            /// Supports remote configuration of a weekly schedule of setpoint transitions
            /// </summary>
            ScheduleConfiguration = 8,
            /// <summary>
            /// Supports configurable setback (or span)
            /// </summary>
            Setback = 16,
            /// <summary>
            /// Supports a System Mode of Auto
            /// </summary>
            AutoMode = 32,
            /// <summary>
            /// Thermostat does not expose the LocalTemperature Value in the LocalTemperature attribute
            /// </summary>
            LocalTemperatureNotExposed = 64,
        }

        /// <summary>
        /// AC Capacity Format
        /// </summary>
        public enum ACCapacityFormatEnum {
            /// <summary>
            /// British Thermal Unit per Hour
            /// </summary>
            BTUh = 0,
        }

        /// <summary>
        /// AC Compressor Type
        /// </summary>
        public enum ACCompressorTypeEnum {
            /// <summary>
            /// Unknown compressor type
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Max working ambient 43 °C
            /// </summary>
            T1 = 1,
            /// <summary>
            /// Max working ambient 35 °C
            /// </summary>
            T2 = 2,
            /// <summary>
            /// Max working ambient 52 °C
            /// </summary>
            T3 = 3,
        }

        /// <summary>
        /// AC Louver Position
        /// </summary>
        public enum ACLouverPositionEnum {
            /// <summary>
            /// Fully Closed
            /// </summary>
            Closed = 1,
            /// <summary>
            /// Fully Open
            /// </summary>
            Open = 2,
            /// <summary>
            /// Quarter Open
            /// </summary>
            Quarter = 3,
            /// <summary>
            /// Half Open
            /// </summary>
            Half = 4,
            /// <summary>
            /// Three Quarters Open
            /// </summary>
            ThreeQuarters = 5,
        }

        /// <summary>
        /// AC Refrigerant Type
        /// </summary>
        public enum ACRefrigerantTypeEnum {
            /// <summary>
            /// Unknown Refrigerant Type
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// R22 Refrigerant
            /// </summary>
            R22 = 1,
            /// <summary>
            /// R410a Refrigerant
            /// </summary>
            R410a = 2,
            /// <summary>
            /// R407c Refrigerant
            /// </summary>
            R407c = 3,
        }

        /// <summary>
        /// AC Type
        /// </summary>
        public enum ACTypeEnum {
            /// <summary>
            /// Unknown AC Type
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Cooling and Fixed Speed
            /// </summary>
            CoolingFixed = 1,
            /// <summary>
            /// Heat Pump and Fixed Speed
            /// </summary>
            HeatPumpFixed = 2,
            /// <summary>
            /// Cooling and Inverter
            /// </summary>
            CoolingInverter = 3,
            /// <summary>
            /// Heat Pump and Inverter
            /// </summary>
            HeatPumpInverter = 4,
        }

        /// <summary>
        /// Control Sequence Of Operation
        /// </summary>
        public enum ControlSequenceOfOperationEnum {
            /// <summary>
            /// Heat and Emergency are not possible
            /// </summary>
            CoolingOnly = 0,
            /// <summary>
            /// Heat and Emergency are not possible
            /// </summary>
            CoolingWithReheat = 1,
            /// <summary>
            /// Cool and precooling (see ref_HvacTerms) are not possible
            /// </summary>
            HeatingOnly = 2,
            /// <summary>
            /// Cool and precooling are not possible
            /// </summary>
            HeatingWithReheat = 3,
            /// <summary>
            /// All modes are possible
            /// </summary>
            CoolingAndHeating = 4,
            /// <summary>
            /// All modes are possible
            /// </summary>
            CoolingAndHeatingWithReheat = 5,
        }

        /// <summary>
        /// Setpoint Change Source
        /// </summary>
        public enum SetpointChangeSourceEnum {
            /// <summary>
            /// Manual, user-initiated setpoint change via the thermostat
            /// </summary>
            Manual = 0,
            /// <summary>
            /// Schedule/internal programming-initiated setpoint change
            /// </summary>
            Schedule = 1,
            /// <summary>
            /// Externally-initiated setpoint change (e.g., DRLC cluster command, attribute write)
            /// </summary>
            External = 2,
        }

        /// <summary>
        /// Setpoint Raise Lower Mode
        /// </summary>
        public enum SetpointRaiseLowerModeEnum {
            /// <summary>
            /// Adjust Heat Setpoint
            /// </summary>
            Heat = 0,
            /// <summary>
            /// Adjust Cool Setpoint
            /// </summary>
            Cool = 1,
            /// <summary>
            /// Adjust Heat Setpoint and Cool Setpoint
            /// </summary>
            Both = 2,
        }

        /// <summary>
        /// Start Of Week
        /// </summary>
        public enum StartOfWeekEnum {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
        }

        /// <summary>
        /// System Mode
        /// </summary>
        public enum SystemModeEnum {
            /// <summary>
            /// The Thermostat does not generate demand for Cooling or Heating
            /// </summary>
            Off = 0,
            /// <summary>
            /// Demand is generated for either Cooling or Heating, as required
            /// </summary>
            Auto = 1,
            /// <summary>
            /// Demand is only generated for Cooling
            /// </summary>
            Cool = 3,
            /// <summary>
            /// Demand is only generated for Heating
            /// </summary>
            Heat = 4,
            /// <summary>
            /// 2 stage heating is in use to achieve desired temperature
            /// </summary>
            EmergencyHeat = 5,
            /// <summary>
            /// (see ref_HvacTerms)
            /// </summary>
            Precooling = 6,
            FanOnly = 7,
            Dry = 8,
            Sleep = 9,
        }

        /// <summary>
        /// Temperature Setpoint Hold
        /// </summary>
        public enum TemperatureSetpointHoldEnum {
            /// <summary>
            /// Follow scheduling program
            /// </summary>
            SetpointHoldOff = 0,
            /// <summary>
            /// Maintain current setpoint, regardless of schedule transitions
            /// </summary>
            SetpointHoldOn = 1,
        }

        /// <summary>
        /// Thermostat Running Mode
        /// </summary>
        public enum ThermostatRunningModeEnum {
            /// <summary>
            /// The Thermostat does not generate demand for Cooling or Heating
            /// </summary>
            Off = 0,
            /// <summary>
            /// Demand is only generated for Cooling
            /// </summary>
            Cool = 3,
            /// <summary>
            /// Demand is only generated for Heating
            /// </summary>
            Heat = 4,
        }

        /// <summary>
        /// AC Error Code Bitmap
        /// </summary>
        [Flags]
        public enum ACErrorCodeBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Compressor Failure or Refrigerant Leakage
            /// </summary>
            CompressorFail = 1,
            /// <summary>
            /// Room Temperature Sensor Failure
            /// </summary>
            RoomSensorFail = 2,
            /// <summary>
            /// Outdoor Temperature Sensor Failure
            /// </summary>
            OutdoorSensorFail = 4,
            /// <summary>
            /// Indoor Coil Temperature Sensor Failure
            /// </summary>
            CoilSensorFail = 8,
            /// <summary>
            /// Fan Failure
            /// </summary>
            FanFail = 16,
        }

        /// <summary>
        /// Alarm Code Bitmap
        /// </summary>
        [Flags]
        public enum AlarmCodeBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Initialization failure. The device failed to complete initialization at power-up.
            /// </summary>
            Initialization = 1,
            /// <summary>
            /// Hardware failure
            /// </summary>
            Hardware = 2,
            /// <summary>
            /// Self-calibration failure
            /// </summary>
            SelfCalibration = 4,
        }

        /// <summary>
        /// HVAC System Type Bitmap
        /// </summary>
        [Flags]
        public enum HVACSystemTypeBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Stage of cooling the HVAC system is using.
            /// </summary>
            CoolingStage = 1,
            /// <summary>
            /// Stage of heating the HVAC system is using.
            /// </summary>
            HeatingStage = 1,
            /// <summary>
            /// Is the heating type Heat Pump.
            /// </summary>
            HeatingIsHeatPump = 16,
            /// <summary>
            /// Does the HVAC system use fuel.
            /// </summary>
            HeatingUsesFuel = 32,
        }

        /// <summary>
        /// Programming Operation Mode Bitmap
        /// </summary>
        [Flags]
        public enum ProgrammingOperationModeBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Schedule programming mode. This enables any programmed weekly schedule configurations.
            /// </summary>
            ScheduleActive = 1,
            /// <summary>
            /// Auto/recovery mode
            /// </summary>
            AutoRecovery = 2,
            /// <summary>
            /// Economy/EnergyStar mode
            /// </summary>
            Economy = 4,
        }

        /// <summary>
        /// Relay State Bitmap
        /// </summary>
        [Flags]
        public enum RelayStateBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Heat State On
            /// </summary>
            Heat = 1,
            /// <summary>
            /// Cool State On
            /// </summary>
            Cool = 2,
            /// <summary>
            /// Fan State On
            /// </summary>
            Fan = 4,
            /// <summary>
            /// Heat 2 State On
            /// </summary>
            HeatStage2 = 8,
            /// <summary>
            /// Cool 2 State On
            /// </summary>
            CoolStage2 = 16,
            /// <summary>
            /// Fan 2 State On
            /// </summary>
            FanStage2 = 32,
            /// <summary>
            /// Fan 3 Stage On
            /// </summary>
            FanStage3 = 64,
        }

        /// <summary>
        /// Remote Sensing Bitmap
        /// </summary>
        [Flags]
        public enum RemoteSensingBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Calculated Local Temperature is derived from a remote node
            /// </summary>
            LocalTemperature = 1,
            /// <summary>
            /// OutdoorTemperature is derived from a remote node
            /// </summary>
            OutdoorTemperature = 2,
            /// <summary>
            /// Occupancy is derived from a remote node
            /// </summary>
            Occupancy = 4,
        }

        /// <summary>
        /// Schedule Day Of Week Bitmap
        /// </summary>
        [Flags]
        public enum ScheduleDayOfWeekBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
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
            /// <summary>
            /// Away or Vacation
            /// </summary>
            Away = 128,
        }

        /// <summary>
        /// Schedule Mode Bitmap
        /// </summary>
        [Flags]
        public enum ScheduleModeBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Adjust Heat Setpoint
            /// </summary>
            HeatSetpointPresent = 1,
            /// <summary>
            /// Adjust Cool Setpoint
            /// </summary>
            CoolSetpointPresent = 2,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Weekly Schedule Transition
        /// </summary>
        public record WeeklyScheduleTransition : TLVPayload {
            /// <summary>
            /// Weekly Schedule Transition
            /// </summary>
            public WeeklyScheduleTransition() { }

            /// <summary>
            /// Weekly Schedule Transition
            /// </summary>
            [SetsRequiredMembers]
            public WeeklyScheduleTransition(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                TransitionTime = reader.GetUShort(0)!.Value;
                HeatSetpoint = reader.GetShort(1, true);
                CoolSetpoint = reader.GetShort(2, true);
            }
            public required ushort TransitionTime { get; set; }
            public required short? HeatSetpoint { get; set; }
            public required short? CoolSetpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, TransitionTime, 1439);
                writer.WriteShort(1, HeatSetpoint);
                writer.WriteShort(2, CoolSetpoint);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record SetpointRaiseLowerPayload : TLVPayload {
            public required SetpointRaiseLowerModeEnum Mode { get; set; }
            public required sbyte Amount { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Mode);
                writer.WriteSByte(1, Amount);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Weekly Schedule Response - Reply from server
        /// </summary>
        public struct GetWeeklyScheduleResponse() {
            public required byte NumberOfTransitionsForSequence { get; set; }
            public required ScheduleDayOfWeekBitmap DayOfWeekForSequence { get; set; }
            public required ScheduleModeBitmap ModeForSequence { get; set; }
            public required WeeklyScheduleTransition[] Transitions { get; set; }
        }

        private record SetWeeklySchedulePayload : TLVPayload {
            public required byte NumberOfTransitionsForSequence { get; set; }
            public required ScheduleDayOfWeekBitmap DayOfWeekForSequence { get; set; }
            public required ScheduleModeBitmap ModeForSequence { get; set; }
            public required WeeklyScheduleTransition[] Transitions { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, NumberOfTransitionsForSequence);
                writer.WriteUShort(1, (ushort)DayOfWeekForSequence);
                writer.WriteUShort(2, (ushort)ModeForSequence);
                {
                    Constrain(Transitions, 0, 10);
                    writer.StartArray(3);
                    foreach (var item in Transitions) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Relay Status Log Response - Reply from server
        /// </summary>
        public struct GetRelayStatusLogResponse() {
            public required ushort TimeOfDay { get; set; }
            public required RelayStateBitmap RelayStatus { get; set; }
            public required short? LocalTemperature { get; set; }
            public required byte? HumidityInPercentage { get; set; }
            public required short SetPoint { get; set; }
            public required ushort UnreadEntries { get; set; }
        }

        private record GetWeeklySchedulePayload : TLVPayload {
            public required ScheduleDayOfWeekBitmap DaysToReturn { get; set; }
            public required ScheduleModeBitmap ModeToReturn { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)DaysToReturn);
                writer.WriteUShort(1, (ushort)ModeToReturn);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Setpoint Raise Lower
        /// </summary>
        public async Task<bool> SetpointRaiseLower(SecureSession session, SetpointRaiseLowerModeEnum Mode, sbyte Amount) {
            SetpointRaiseLowerPayload requestFields = new SetpointRaiseLowerPayload() {
                Mode = Mode,
                Amount = Amount,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Weekly Schedule
        /// </summary>
        public async Task<bool> SetWeeklySchedule(SecureSession session, byte NumberOfTransitionsForSequence, ScheduleDayOfWeekBitmap DayOfWeekForSequence, ScheduleModeBitmap ModeForSequence, WeeklyScheduleTransition[] Transitions) {
            SetWeeklySchedulePayload requestFields = new SetWeeklySchedulePayload() {
                NumberOfTransitionsForSequence = NumberOfTransitionsForSequence,
                DayOfWeekForSequence = DayOfWeekForSequence,
                ModeForSequence = ModeForSequence,
                Transitions = Transitions,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Weekly Schedule
        /// </summary>
        public async Task<GetWeeklyScheduleResponse?> GetWeeklySchedule(SecureSession session, ScheduleDayOfWeekBitmap DaysToReturn, ScheduleModeBitmap ModeToReturn) {
            GetWeeklySchedulePayload requestFields = new GetWeeklySchedulePayload() {
                DaysToReturn = DaysToReturn,
                ModeToReturn = ModeToReturn,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetWeeklyScheduleResponse() {
                NumberOfTransitionsForSequence = (byte)GetField(resp, 0),
                DayOfWeekForSequence = (ScheduleDayOfWeekBitmap)(byte)GetField(resp, 1),
                ModeForSequence = (ScheduleModeBitmap)(byte)GetField(resp, 2),
                Transitions = GetArrayField<WeeklyScheduleTransition>(resp, 3),
            };
        }

        /// <summary>
        /// Clear Weekly Schedule
        /// </summary>
        public async Task<bool> ClearWeeklySchedule(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Relay Status Log
        /// </summary>
        public async Task<GetRelayStatusLogResponse?> GetRelayStatusLog(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04);
            if (!ValidateResponse(resp))
                return null;
            return new GetRelayStatusLogResponse() {
                TimeOfDay = (ushort)GetField(resp, 0),
                RelayStatus = (RelayStateBitmap)(byte)GetField(resp, 1),
                LocalTemperature = (short?)GetField(resp, 2),
                HumidityInPercentage = (byte?)GetField(resp, 3),
                SetPoint = (short)GetField(resp, 4),
                UnreadEntries = (ushort)GetField(resp, 5),
            };
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
        /// Get the Local Temperature attribute
        /// </summary>
        public async Task<short?> GetLocalTemperature(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 0, true) ?? null;
        }

        /// <summary>
        /// Get the Outdoor Temperature attribute
        /// </summary>
        public async Task<short?> GetOutdoorTemperature(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 1, true) ?? null;
        }

        /// <summary>
        /// Get the Occupancy attribute
        /// </summary>
        public async Task<OccupancySensingCluster.OccupancyBitmap> GetOccupancy(SecureSession session) {
            return (OccupancySensingCluster.OccupancyBitmap?)(dynamic?)await GetAttribute(session, 2) ?? (OccupancySensingCluster.OccupancyBitmap)1;
        }

        /// <summary>
        /// Get the Abs Min Heat Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetAbsMinHeatSetpointLimit(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 3) ?? 700;
        }

        /// <summary>
        /// Get the Abs Max Heat Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetAbsMaxHeatSetpointLimit(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 4) ?? 3000;
        }

        /// <summary>
        /// Get the Abs Min Cool Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetAbsMinCoolSetpointLimit(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 5) ?? 1600;
        }

        /// <summary>
        /// Get the Abs Max Cool Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetAbsMaxCoolSetpointLimit(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 6) ?? 3200;
        }

        /// <summary>
        /// Get the PI Cooling Demand attribute
        /// </summary>
        public async Task<byte> GetPICoolingDemand(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 7))!;
        }

        /// <summary>
        /// Get the PI Heating Demand attribute
        /// </summary>
        public async Task<byte> GetPIHeatingDemand(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 8))!;
        }

        /// <summary>
        /// Get the HVAC System Type Configuration attribute
        /// </summary>
        public async Task<HVACSystemTypeBitmap> GetHVACSystemTypeConfiguration(SecureSession session) {
            return (HVACSystemTypeBitmap)await GetEnumAttribute(session, 9);
        }

        /// <summary>
        /// Set the HVAC System Type Configuration attribute
        /// </summary>
        public async Task SetHVACSystemTypeConfiguration (SecureSession session, HVACSystemTypeBitmap value) {
            await SetAttribute(session, 9, value);
        }

        /// <summary>
        /// Get the Local Temperature Calibration attribute
        /// </summary>
        public async Task<sbyte> GetLocalTemperatureCalibration(SecureSession session) {
            return (sbyte?)(dynamic?)await GetAttribute(session, 16) ?? 0;
        }

        /// <summary>
        /// Set the Local Temperature Calibration attribute
        /// </summary>
        public async Task SetLocalTemperatureCalibration (SecureSession session, sbyte? value = 0) {
            await SetAttribute(session, 16, value);
        }

        /// <summary>
        /// Get the Occupied Cooling Setpoint attribute
        /// </summary>
        public async Task<short> GetOccupiedCoolingSetpoint(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 17) ?? 2600;
        }

        /// <summary>
        /// Set the Occupied Cooling Setpoint attribute
        /// </summary>
        public async Task SetOccupiedCoolingSetpoint (SecureSession session, short? value = 2600) {
            await SetAttribute(session, 17, value);
        }

        /// <summary>
        /// Get the Occupied Heating Setpoint attribute
        /// </summary>
        public async Task<short> GetOccupiedHeatingSetpoint(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 18) ?? 2000;
        }

        /// <summary>
        /// Set the Occupied Heating Setpoint attribute
        /// </summary>
        public async Task SetOccupiedHeatingSetpoint (SecureSession session, short? value = 2000) {
            await SetAttribute(session, 18, value);
        }

        /// <summary>
        /// Get the Unoccupied Cooling Setpoint attribute
        /// </summary>
        public async Task<short> GetUnoccupiedCoolingSetpoint(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 19) ?? 2600;
        }

        /// <summary>
        /// Set the Unoccupied Cooling Setpoint attribute
        /// </summary>
        public async Task SetUnoccupiedCoolingSetpoint (SecureSession session, short? value = 2600) {
            await SetAttribute(session, 19, value);
        }

        /// <summary>
        /// Get the Unoccupied Heating Setpoint attribute
        /// </summary>
        public async Task<short> GetUnoccupiedHeatingSetpoint(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 20) ?? 2000;
        }

        /// <summary>
        /// Set the Unoccupied Heating Setpoint attribute
        /// </summary>
        public async Task SetUnoccupiedHeatingSetpoint (SecureSession session, short? value = 2000) {
            await SetAttribute(session, 20, value);
        }

        /// <summary>
        /// Get the Min Heat Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetMinHeatSetpointLimit(SecureSession session) {
            return (short)(dynamic?)(await GetAttribute(session, 21))!;
        }

        /// <summary>
        /// Set the Min Heat Setpoint Limit attribute
        /// </summary>
        public async Task SetMinHeatSetpointLimit (SecureSession session, short value) {
            await SetAttribute(session, 21, value);
        }

        /// <summary>
        /// Get the Max Heat Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetMaxHeatSetpointLimit(SecureSession session) {
            return (short)(dynamic?)(await GetAttribute(session, 22))!;
        }

        /// <summary>
        /// Set the Max Heat Setpoint Limit attribute
        /// </summary>
        public async Task SetMaxHeatSetpointLimit (SecureSession session, short value) {
            await SetAttribute(session, 22, value);
        }

        /// <summary>
        /// Get the Min Cool Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetMinCoolSetpointLimit(SecureSession session) {
            return (short)(dynamic?)(await GetAttribute(session, 23))!;
        }

        /// <summary>
        /// Set the Min Cool Setpoint Limit attribute
        /// </summary>
        public async Task SetMinCoolSetpointLimit (SecureSession session, short value) {
            await SetAttribute(session, 23, value);
        }

        /// <summary>
        /// Get the Max Cool Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetMaxCoolSetpointLimit(SecureSession session) {
            return (short)(dynamic?)(await GetAttribute(session, 24))!;
        }

        /// <summary>
        /// Set the Max Cool Setpoint Limit attribute
        /// </summary>
        public async Task SetMaxCoolSetpointLimit (SecureSession session, short value) {
            await SetAttribute(session, 24, value);
        }

        /// <summary>
        /// Get the Min Setpoint Dead Band attribute
        /// </summary>
        public async Task<sbyte> GetMinSetpointDeadBand(SecureSession session) {
            return (sbyte?)(dynamic?)await GetAttribute(session, 25) ?? 25;
        }

        /// <summary>
        /// Set the Min Setpoint Dead Band attribute
        /// </summary>
        public async Task SetMinSetpointDeadBand (SecureSession session, sbyte? value = 25) {
            await SetAttribute(session, 25, value);
        }

        /// <summary>
        /// Get the Remote Sensing attribute
        /// </summary>
        public async Task<RemoteSensingBitmap> GetRemoteSensing(SecureSession session) {
            return (RemoteSensingBitmap)await GetEnumAttribute(session, 26);
        }

        /// <summary>
        /// Set the Remote Sensing attribute
        /// </summary>
        public async Task SetRemoteSensing (SecureSession session, RemoteSensingBitmap value) {
            await SetAttribute(session, 26, value);
        }

        /// <summary>
        /// Get the Control Sequence Of Operation attribute
        /// </summary>
        public async Task<ControlSequenceOfOperationEnum> GetControlSequenceOfOperation(SecureSession session) {
            return (ControlSequenceOfOperationEnum)await GetEnumAttribute(session, 27);
        }

        /// <summary>
        /// Set the Control Sequence Of Operation attribute
        /// </summary>
        public async Task SetControlSequenceOfOperation (SecureSession session, ControlSequenceOfOperationEnum value) {
            await SetAttribute(session, 27, value);
        }

        /// <summary>
        /// Get the System Mode attribute
        /// </summary>
        public async Task<SystemModeEnum> GetSystemMode(SecureSession session) {
            return (SystemModeEnum)await GetEnumAttribute(session, 28);
        }

        /// <summary>
        /// Set the System Mode attribute
        /// </summary>
        public async Task SetSystemMode (SecureSession session, SystemModeEnum value) {
            await SetAttribute(session, 28, value);
        }

        /// <summary>
        /// Get the Alarm Mask attribute
        /// </summary>
        public async Task<AlarmCodeBitmap> GetAlarmMask(SecureSession session) {
            return (AlarmCodeBitmap)await GetEnumAttribute(session, 29);
        }

        /// <summary>
        /// Get the Thermostat Running Mode attribute
        /// </summary>
        public async Task<ThermostatRunningModeEnum> GetThermostatRunningMode(SecureSession session) {
            return (ThermostatRunningModeEnum)await GetEnumAttribute(session, 30);
        }

        /// <summary>
        /// Get the Start Of Week attribute
        /// </summary>
        public async Task<StartOfWeekEnum> GetStartOfWeek(SecureSession session) {
            return (StartOfWeekEnum)await GetEnumAttribute(session, 32);
        }

        /// <summary>
        /// Get the Number Of Weekly Transitions attribute
        /// </summary>
        public async Task<byte> GetNumberOfWeeklyTransitions(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 33) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Daily Transitions attribute
        /// </summary>
        public async Task<byte> GetNumberOfDailyTransitions(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 34) ?? 0;
        }

        /// <summary>
        /// Get the Temperature Setpoint Hold attribute
        /// </summary>
        public async Task<TemperatureSetpointHoldEnum> GetTemperatureSetpointHold(SecureSession session) {
            return (TemperatureSetpointHoldEnum)await GetEnumAttribute(session, 35);
        }

        /// <summary>
        /// Set the Temperature Setpoint Hold attribute
        /// </summary>
        public async Task SetTemperatureSetpointHold (SecureSession session, TemperatureSetpointHoldEnum value) {
            await SetAttribute(session, 35, value);
        }

        /// <summary>
        /// Get the Temperature Setpoint Hold Duration attribute
        /// </summary>
        public async Task<ushort?> GetTemperatureSetpointHoldDuration(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 36, true) ?? null;
        }

        /// <summary>
        /// Set the Temperature Setpoint Hold Duration attribute
        /// </summary>
        public async Task SetTemperatureSetpointHoldDuration (SecureSession session, ushort? value = null) {
            await SetAttribute(session, 36, value, true);
        }

        /// <summary>
        /// Get the Thermostat Programming Operation Mode attribute
        /// </summary>
        public async Task<ProgrammingOperationModeBitmap> GetThermostatProgrammingOperationMode(SecureSession session) {
            return (ProgrammingOperationModeBitmap)await GetEnumAttribute(session, 37);
        }

        /// <summary>
        /// Set the Thermostat Programming Operation Mode attribute
        /// </summary>
        public async Task SetThermostatProgrammingOperationMode (SecureSession session, ProgrammingOperationModeBitmap value) {
            await SetAttribute(session, 37, value);
        }

        /// <summary>
        /// Get the Thermostat Running State attribute
        /// </summary>
        public async Task<RelayStateBitmap> GetThermostatRunningState(SecureSession session) {
            return (RelayStateBitmap)await GetEnumAttribute(session, 41);
        }

        /// <summary>
        /// Get the Setpoint Change Source attribute
        /// </summary>
        public async Task<SetpointChangeSourceEnum> GetSetpointChangeSource(SecureSession session) {
            return (SetpointChangeSourceEnum)await GetEnumAttribute(session, 48);
        }

        /// <summary>
        /// Get the Setpoint Change Amount attribute
        /// </summary>
        public async Task<short?> GetSetpointChangeAmount(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 49, true) ?? null;
        }

        /// <summary>
        /// Get the Setpoint Change Source Timestamp attribute
        /// </summary>
        public async Task<DateTime> GetSetpointChangeSourceTimestamp(SecureSession session) {
            return TimeUtil.FromEpochSeconds((uint?)(dynamic?)await GetAttribute(session, 50)) ?? TimeUtil.EPOCH;
        }

        /// <summary>
        /// Get the Occupied Setback attribute
        /// </summary>
        public async Task<byte?> GetOccupiedSetback(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 52, true) ?? null;
        }

        /// <summary>
        /// Set the Occupied Setback attribute
        /// </summary>
        public async Task SetOccupiedSetback (SecureSession session, byte? value = null) {
            await SetAttribute(session, 52, value, true);
        }

        /// <summary>
        /// Get the Occupied Setback Min attribute
        /// </summary>
        public async Task<byte?> GetOccupiedSetbackMin(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 53, true) ?? null;
        }

        /// <summary>
        /// Get the Occupied Setback Max attribute
        /// </summary>
        public async Task<byte?> GetOccupiedSetbackMax(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 54, true) ?? null;
        }

        /// <summary>
        /// Get the Unoccupied Setback attribute
        /// </summary>
        public async Task<byte?> GetUnoccupiedSetback(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 55, true) ?? null;
        }

        /// <summary>
        /// Set the Unoccupied Setback attribute
        /// </summary>
        public async Task SetUnoccupiedSetback (SecureSession session, byte? value = null) {
            await SetAttribute(session, 55, value, true);
        }

        /// <summary>
        /// Get the Unoccupied Setback Min attribute
        /// </summary>
        public async Task<byte?> GetUnoccupiedSetbackMin(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 56, true) ?? null;
        }

        /// <summary>
        /// Get the Unoccupied Setback Max attribute
        /// </summary>
        public async Task<byte?> GetUnoccupiedSetbackMax(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 57, true) ?? null;
        }

        /// <summary>
        /// Get the Emergency Heat Delta attribute
        /// </summary>
        public async Task<byte> GetEmergencyHeatDelta(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 58) ?? 255;
        }

        /// <summary>
        /// Set the Emergency Heat Delta attribute
        /// </summary>
        public async Task SetEmergencyHeatDelta (SecureSession session, byte? value = 255) {
            await SetAttribute(session, 58, value);
        }

        /// <summary>
        /// Get the AC Type attribute
        /// </summary>
        public async Task<ACTypeEnum> GetACType(SecureSession session) {
            return (ACTypeEnum)await GetEnumAttribute(session, 64);
        }

        /// <summary>
        /// Set the AC Type attribute
        /// </summary>
        public async Task SetACType (SecureSession session, ACTypeEnum value) {
            await SetAttribute(session, 64, value);
        }

        /// <summary>
        /// Get the AC Capacity attribute
        /// </summary>
        public async Task<ushort> GetACCapacity(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 65) ?? 0;
        }

        /// <summary>
        /// Set the AC Capacity attribute
        /// </summary>
        public async Task SetACCapacity (SecureSession session, ushort? value = 0) {
            await SetAttribute(session, 65, value);
        }

        /// <summary>
        /// Get the AC Refrigerant Type attribute
        /// </summary>
        public async Task<ACRefrigerantTypeEnum> GetACRefrigerantType(SecureSession session) {
            return (ACRefrigerantTypeEnum)await GetEnumAttribute(session, 66);
        }

        /// <summary>
        /// Set the AC Refrigerant Type attribute
        /// </summary>
        public async Task SetACRefrigerantType (SecureSession session, ACRefrigerantTypeEnum value) {
            await SetAttribute(session, 66, value);
        }

        /// <summary>
        /// Get the AC Compressor Type attribute
        /// </summary>
        public async Task<ACCompressorTypeEnum> GetACCompressorType(SecureSession session) {
            return (ACCompressorTypeEnum)await GetEnumAttribute(session, 67);
        }

        /// <summary>
        /// Set the AC Compressor Type attribute
        /// </summary>
        public async Task SetACCompressorType (SecureSession session, ACCompressorTypeEnum value) {
            await SetAttribute(session, 67, value);
        }

        /// <summary>
        /// Get the AC Error Code attribute
        /// </summary>
        public async Task<ACErrorCodeBitmap> GetACErrorCode(SecureSession session) {
            return (ACErrorCodeBitmap)await GetEnumAttribute(session, 68);
        }

        /// <summary>
        /// Set the AC Error Code attribute
        /// </summary>
        public async Task SetACErrorCode (SecureSession session, ACErrorCodeBitmap value) {
            await SetAttribute(session, 68, value);
        }

        /// <summary>
        /// Get the AC Louver Position attribute
        /// </summary>
        public async Task<ACLouverPositionEnum> GetACLouverPosition(SecureSession session) {
            return (ACLouverPositionEnum)await GetEnumAttribute(session, 69);
        }

        /// <summary>
        /// Set the AC Louver Position attribute
        /// </summary>
        public async Task SetACLouverPosition (SecureSession session, ACLouverPositionEnum value) {
            await SetAttribute(session, 69, value);
        }

        /// <summary>
        /// Get the AC Coil Temperature attribute
        /// </summary>
        public async Task<short?> GetACCoilTemperature(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 70, true) ?? null;
        }

        /// <summary>
        /// Get the AC Capacity Format attribute
        /// </summary>
        public async Task<ACCapacityFormatEnum> GetACCapacityFormat(SecureSession session) {
            return (ACCapacityFormatEnum)await GetEnumAttribute(session, 71);
        }

        /// <summary>
        /// Set the AC Capacity Format attribute
        /// </summary>
        public async Task SetACCapacityFormat (SecureSession session, ACCapacityFormatEnum value) {
            await SetAttribute(session, 71, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thermostat Cluster";
        }
    }
}