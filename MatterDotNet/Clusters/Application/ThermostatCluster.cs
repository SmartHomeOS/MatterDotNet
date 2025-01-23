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
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.HVAC
{
    /// <summary>
    /// An interface for configuring and controlling the functionality of a thermostat.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 7)]
    public class Thermostat : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0201;

        /// <summary>
        /// An interface for configuring and controlling the functionality of a thermostat.
        /// </summary>
        public Thermostat(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected Thermostat(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
            /// <summary>
            /// Supports enhanced schedules
            /// </summary>
            MatterScheduleConfiguration = 128,
            /// <summary>
            /// Thermostat supports setpoint presets
            /// </summary>
            Presets = 256,
        }

        /// <summary>
        /// System Mode
        /// </summary>
        public enum SystemMode : byte {
            /// <summary>
            /// The Thermostat does not generate demand for Cooling or Heating
            /// </summary>
            Off = 0x00,
            /// <summary>
            /// Demand is generated for either Cooling or Heating, as required
            /// </summary>
            Auto = 0x01,
            /// <summary>
            /// Demand is only generated for Cooling
            /// </summary>
            Cool = 0x03,
            /// <summary>
            /// Demand is only generated for Heating
            /// </summary>
            Heat = 0x04,
            /// <summary>
            /// 2 stage heating is in use to achieve desired temperature
            /// </summary>
            EmergencyHeat = 0x05,
            /// <summary>
            /// (see ref_HvacTerms)
            /// </summary>
            Precooling = 0x06,
            /// <summary>
            /// 
            /// </summary>
            FanOnly = 0x07,
            /// <summary>
            /// 
            /// </summary>
            Dry = 0x08,
            /// <summary>
            /// 
            /// </summary>
            Sleep = 0x09,
        }

        /// <summary>
        /// Thermostat Running Mode
        /// </summary>
        public enum ThermostatRunningMode : byte {
            /// <summary>
            /// The Thermostat does not generate demand for Cooling or Heating
            /// </summary>
            Off = 0x00,
            /// <summary>
            /// Demand is only generated for Cooling
            /// </summary>
            Cool = 0x03,
            /// <summary>
            /// Demand is only generated for Heating
            /// </summary>
            Heat = 0x04,
        }

        /// <summary>
        /// Start Of Week
        /// </summary>
        public enum StartOfWeek : byte {
            /// <summary>
            /// 
            /// </summary>
            Sunday = 0x00,
            /// <summary>
            /// 
            /// </summary>
            Monday = 0x01,
            /// <summary>
            /// 
            /// </summary>
            Tuesday = 0x02,
            /// <summary>
            /// 
            /// </summary>
            Wednesday = 0x03,
            /// <summary>
            /// 
            /// </summary>
            Thursday = 0x04,
            /// <summary>
            /// 
            /// </summary>
            Friday = 0x05,
            /// <summary>
            /// 
            /// </summary>
            Saturday = 0x06,
        }

        /// <summary>
        /// Control Sequence Of Operation
        /// </summary>
        public enum ControlSequenceOfOperation : byte {
            /// <summary>
            /// Heat and Emergency are not possible
            /// </summary>
            CoolingOnly = 0x00,
            /// <summary>
            /// Heat and Emergency are not possible
            /// </summary>
            CoolingWithReheat = 0x01,
            /// <summary>
            /// Cool and precooling (see ref_HvacTerms) are not possible
            /// </summary>
            HeatingOnly = 0x02,
            /// <summary>
            /// Cool and precooling are not possible
            /// </summary>
            HeatingWithReheat = 0x03,
            /// <summary>
            /// All modes are possible
            /// </summary>
            CoolingAndHeating = 0x04,
            /// <summary>
            /// All modes are possible
            /// </summary>
            CoolingAndHeatingWithReheat = 0x05,
        }

        /// <summary>
        /// Temperature Setpoint Hold
        /// </summary>
        public enum TemperatureSetpointHold : byte {
            /// <summary>
            /// Follow scheduling program
            /// </summary>
            SetpointHoldOff = 0x00,
            /// <summary>
            /// Maintain current setpoint, regardless of schedule transitions
            /// </summary>
            SetpointHoldOn = 0x01,
        }

        /// <summary>
        /// Setpoint Raise Lower Mode
        /// </summary>
        public enum SetpointRaiseLowerMode : byte {
            /// <summary>
            /// Adjust Heat Setpoint
            /// </summary>
            Heat = 0x00,
            /// <summary>
            /// Adjust Cool Setpoint
            /// </summary>
            Cool = 0x01,
            /// <summary>
            /// Adjust Heat Setpoint and Cool Setpoint
            /// </summary>
            Both = 0x02,
        }

        /// <summary>
        /// AC Capacity Format
        /// </summary>
        public enum ACCapacityFormat : byte {
            /// <summary>
            /// British Thermal Unit per Hour
            /// </summary>
            BTUh = 0x00,
        }

        /// <summary>
        /// AC Compressor Type
        /// </summary>
        public enum ACCompressorType : byte {
            /// <summary>
            /// Unknown compressor type
            /// </summary>
            Unknown = 0x00,
            /// <summary>
            /// Max working ambient 43 °C
            /// </summary>
            T1 = 0x01,
            /// <summary>
            /// Max working ambient 35 °C
            /// </summary>
            T2 = 0x02,
            /// <summary>
            /// Max working ambient 52 °C
            /// </summary>
            T3 = 0x03,
        }

        /// <summary>
        /// AC Louver Position
        /// </summary>
        public enum ACLouverPosition : byte {
            /// <summary>
            /// Fully Closed
            /// </summary>
            Closed = 0x01,
            /// <summary>
            /// Fully Open
            /// </summary>
            Open = 0x02,
            /// <summary>
            /// Quarter Open
            /// </summary>
            Quarter = 0x03,
            /// <summary>
            /// Half Open
            /// </summary>
            Half = 0x04,
            /// <summary>
            /// Three Quarters Open
            /// </summary>
            ThreeQuarters = 0x05,
        }

        /// <summary>
        /// AC Refrigerant Type
        /// </summary>
        public enum ACRefrigerantType : byte {
            /// <summary>
            /// Unknown Refrigerant Type
            /// </summary>
            Unknown = 0x00,
            /// <summary>
            /// R22 Refrigerant
            /// </summary>
            R22 = 0x01,
            /// <summary>
            /// R410a Refrigerant
            /// </summary>
            R410a = 0x02,
            /// <summary>
            /// R407c Refrigerant
            /// </summary>
            R407c = 0x03,
        }

        /// <summary>
        /// AC Type
        /// </summary>
        public enum ACType : byte {
            /// <summary>
            /// Unknown AC Type
            /// </summary>
            Unknown = 0x00,
            /// <summary>
            /// Cooling and Fixed Speed
            /// </summary>
            CoolingFixed = 0x01,
            /// <summary>
            /// Heat Pump and Fixed Speed
            /// </summary>
            HeatPumpFixed = 0x02,
            /// <summary>
            /// Cooling and Inverter
            /// </summary>
            CoolingInverter = 0x03,
            /// <summary>
            /// Heat Pump and Inverter
            /// </summary>
            HeatPumpInverter = 0x04,
        }

        /// <summary>
        /// Setpoint Change Source
        /// </summary>
        public enum SetpointChangeSource : byte {
            /// <summary>
            /// Manual, user-initiated setpoint change via the thermostat
            /// </summary>
            Manual = 0x00,
            /// <summary>
            /// Schedule/internal programming-initiated setpoint change
            /// </summary>
            Schedule = 0x01,
            /// <summary>
            /// Externally-initiated setpoint change (e.g., DRLC cluster command, attribute write)
            /// </summary>
            External = 0x02,
        }

        /// <summary>
        /// Preset Scenario
        /// </summary>
        public enum PresetScenario : byte {
            /// <summary>
            /// The thermostat-controlled area is occupied
            /// </summary>
            Occupied = 0x01,
            /// <summary>
            /// The thermostat-controlled area is unoccupied
            /// </summary>
            Unoccupied = 0x02,
            /// <summary>
            /// Users are likely to be sleeping
            /// </summary>
            Sleep = 0x03,
            /// <summary>
            /// Users are likely to be waking up
            /// </summary>
            Wake = 0x04,
            /// <summary>
            /// Users are on vacation
            /// </summary>
            Vacation = 0x05,
            /// <summary>
            /// Users are likely to be going to sleep
            /// </summary>
            GoingToSleep = 0x06,
            /// <summary>
            /// Custom presets
            /// </summary>
            UserDefined = 0xFE,
        }

        /// <summary>
        /// AC Error Code
        /// </summary>
        [Flags]
        public enum ACErrorCode : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Compressor Failure or Refrigerant Leakage
            /// </summary>
            CompressorFail = 0x01,
            /// <summary>
            /// Room Temperature Sensor Failure
            /// </summary>
            RoomSensorFail = 0x02,
            /// <summary>
            /// Outdoor Temperature Sensor Failure
            /// </summary>
            OutdoorSensorFail = 0x04,
            /// <summary>
            /// Indoor Coil Temperature Sensor Failure
            /// </summary>
            CoilSensorFail = 0x08,
            /// <summary>
            /// Fan Failure
            /// </summary>
            FanFail = 0x10,
        }

        /// <summary>
        /// HVAC System Type
        /// </summary>
        [Flags]
        public enum HVACSystemType : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Stage of cooling the HVAC system is using.
            /// </summary>
            CoolingStage = 0x03,
            /// <summary>
            /// Stage of heating the HVAC system is using.
            /// </summary>
            HeatingStage = 0x0C,
            /// <summary>
            /// Is the heating type Heat Pump.
            /// </summary>
            HeatingIsHeatPump = 0x10,
            /// <summary>
            /// Does the HVAC system use fuel.
            /// </summary>
            HeatingUsesFuel = 0x20,
        }

        /// <summary>
        /// Occupancy
        /// </summary>
        [Flags]
        public enum Occupancy : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Indicates the occupancy state
            /// </summary>
            Occupied = 0x01,
        }

        /// <summary>
        /// Programming Operation Mode
        /// </summary>
        [Flags]
        public enum ProgrammingOperationMode : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Schedule programming mode. This enables any programmed weekly schedule configurations.
            /// </summary>
            ScheduleActive = 0x01,
            /// <summary>
            /// Auto/recovery mode
            /// </summary>
            AutoRecovery = 0x02,
            /// <summary>
            /// Economy/EnergyStar mode
            /// </summary>
            Economy = 0x04,
        }

        /// <summary>
        /// Schedule Type Features
        /// </summary>
        [Flags]
        public enum ScheduleTypeFeatures : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            SupportsPresets = 0x01,
            SupportsSetpoints = 0x02,
            SupportsNames = 0x04,
            SupportsOff = 0x08,
        }

        /// <summary>
        /// Relay State
        /// </summary>
        [Flags]
        public enum RelayState : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Heat = 0x01,
            Cool = 0x02,
            Fan = 0x04,
            HeatStage2 = 0x08,
            CoolStage2 = 0x10,
            FanStage2 = 0x20,
            FanStage3 = 0x40,
        }

        /// <summary>
        /// Remote Sensing
        /// </summary>
        [Flags]
        public enum RemoteSensing : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Calculated Local Temperature is derived from a remote node
            /// </summary>
            LocalTemperature = 0x01,
            /// <summary>
            /// OutdoorTemperature is derived from a remote node
            /// </summary>
            OutdoorTemperature = 0x02,
            /// <summary>
            /// Occupancy is derived from a remote node
            /// </summary>
            Occupancy = 0x04,
        }

        /// <summary>
        /// Schedule Day Of Week
        /// </summary>
        [Flags]
        public enum ScheduleDayOfWeek : byte {
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
            /// <summary>
            /// Away or Vacation
            /// </summary>
            Away = 0x80,
        }

        /// <summary>
        /// Schedule Mode
        /// </summary>
        [Flags]
        public enum ScheduleMode : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Adjust Heat Setpoint
            /// </summary>
            HeatSetpointPresent = 0x01,
            /// <summary>
            /// Adjust Cool Setpoint
            /// </summary>
            CoolSetpointPresent = 0x02,
        }

        /// <summary>
        /// Preset Type Features
        /// </summary>
        [Flags]
        public enum PresetTypeFeatures : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            Automatic = 0x01,
            SupportsNames = 0x02,
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

        /// <summary>
        /// Schedule Type
        /// </summary>
        public record ScheduleType : TLVPayload {
            /// <summary>
            /// Schedule Type
            /// </summary>
            public ScheduleType() { }

            /// <summary>
            /// Schedule Type
            /// </summary>
            [SetsRequiredMembers]
            public ScheduleType(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                SystemMode = (SystemMode)reader.GetUShort(0)!.Value;
                NumberOfSchedules = reader.GetByte(1)!.Value;
                ScheduleTypeFeatures = (ScheduleTypeFeatures)reader.GetUInt(2)!.Value;
            }
            public required SystemMode SystemMode { get; set; }
            public required byte NumberOfSchedules { get; set; } = 0;
            public required ScheduleTypeFeatures ScheduleTypeFeatures { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)SystemMode);
                writer.WriteByte(1, NumberOfSchedules);
                writer.WriteUInt(2, (uint)ScheduleTypeFeatures);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Preset
        /// </summary>
        public record Preset : TLVPayload {
            /// <summary>
            /// Preset
            /// </summary>
            public Preset() { }

            /// <summary>
            /// Preset
            /// </summary>
            [SetsRequiredMembers]
            public Preset(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                PresetHandle = reader.GetBytes(0, false, 16)!;
                PresetScenario = (PresetScenario)reader.GetUShort(1)!.Value;
                Name = reader.GetString(2, true, 64);
                CoolingSetpoint = reader.GetShort(3, true);
                HeatingSetpoint = reader.GetShort(4, true);
                BuiltIn = reader.GetBool(5, true);
            }
            public required byte[]? PresetHandle { get; set; }
            public required PresetScenario PresetScenario { get; set; }
            public string? Name { get; set; }
            public short? CoolingSetpoint { get; set; } = 0x0A28;
            public short? HeatingSetpoint { get; set; } = 0x07D0;
            public required bool? BuiltIn { get; set; } = false;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, PresetHandle, 16);
                writer.WriteUShort(1, (ushort)PresetScenario);
                if (Name != null)
                    writer.WriteString(2, Name, 64);
                if (CoolingSetpoint != null)
                    writer.WriteShort(3, CoolingSetpoint);
                if (HeatingSetpoint != null)
                    writer.WriteShort(4, HeatingSetpoint);
                writer.WriteBool(5, BuiltIn);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Preset Type
        /// </summary>
        public record PresetType : TLVPayload {
            /// <summary>
            /// Preset Type
            /// </summary>
            public PresetType() { }

            /// <summary>
            /// Preset Type
            /// </summary>
            [SetsRequiredMembers]
            public PresetType(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                PresetScenario = (PresetScenario)reader.GetUShort(0)!.Value;
                NumberOfPresets = reader.GetByte(1)!.Value;
                PresetTypeFeatures = (PresetTypeFeatures)reader.GetUInt(2)!.Value;
            }
            public required PresetScenario PresetScenario { get; set; }
            public required byte NumberOfPresets { get; set; } = 0;
            public required PresetTypeFeatures PresetTypeFeatures { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)PresetScenario);
                writer.WriteByte(1, NumberOfPresets);
                writer.WriteUInt(2, (uint)PresetTypeFeatures);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Schedule
        /// </summary>
        public record Schedule : TLVPayload {
            /// <summary>
            /// Schedule
            /// </summary>
            public Schedule() { }

            /// <summary>
            /// Schedule
            /// </summary>
            [SetsRequiredMembers]
            public Schedule(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ScheduleHandle = reader.GetBytes(0, false, 16)!;
                SystemMode = (SystemMode)reader.GetUShort(1)!.Value;
                Name = reader.GetString(2, true, 64);
                PresetHandle = reader.GetBytes(3, true, 16);
                {
                    Transitions = new ScheduleTransition[reader.GetStruct(4)!.Length];
                    for (int n = 0; n < Transitions.Length; n++) {
                        Transitions[n] = new ScheduleTransition((object[])((object[])fields[4])[n]);
                    }
                }
                BuiltIn = reader.GetBool(5, true);
            }
            public required byte[]? ScheduleHandle { get; set; }
            public required SystemMode SystemMode { get; set; }
            public string? Name { get; set; }
            public byte[]? PresetHandle { get; set; }
            public required ScheduleTransition[] Transitions { get; set; }
            public required bool? BuiltIn { get; set; } = false;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, ScheduleHandle, 16);
                writer.WriteUShort(1, (ushort)SystemMode);
                if (Name != null)
                    writer.WriteString(2, Name, 64);
                if (PresetHandle != null)
                    writer.WriteBytes(3, PresetHandle, 16);
                {
                    writer.StartArray(4);
                    foreach (var item in Transitions) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.WriteBool(5, BuiltIn);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Schedule Transition
        /// </summary>
        public record ScheduleTransition : TLVPayload {
            /// <summary>
            /// Schedule Transition
            /// </summary>
            public ScheduleTransition() { }

            /// <summary>
            /// Schedule Transition
            /// </summary>
            [SetsRequiredMembers]
            public ScheduleTransition(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                DayOfWeek = (ScheduleDayOfWeek)reader.GetUInt(0)!.Value;
                TransitionTime = reader.GetUShort(1)!.Value;
                PresetHandle = reader.GetBytes(2, true, 16);
                SystemMode = (SystemMode?)reader.GetUShort(3, true);
                CoolingSetpoint = reader.GetShort(4, true);
                HeatingSetpoint = reader.GetShort(5, true);
            }
            public required ScheduleDayOfWeek DayOfWeek { get; set; }
            public required ushort TransitionTime { get; set; }
            public byte[]? PresetHandle { get; set; }
            public SystemMode? SystemMode { get; set; }
            public short? CoolingSetpoint { get; set; }
            public short? HeatingSetpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)DayOfWeek);
                writer.WriteUShort(1, TransitionTime, 1439);
                if (PresetHandle != null)
                    writer.WriteBytes(2, PresetHandle, 16);
                if (SystemMode != null)
                    writer.WriteUShort(3, (ushort)SystemMode);
                if (CoolingSetpoint != null)
                    writer.WriteShort(4, CoolingSetpoint);
                if (HeatingSetpoint != null)
                    writer.WriteShort(5, HeatingSetpoint);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record SetpointRaiseLowerPayload : TLVPayload {
            public required SetpointRaiseLowerMode Mode { get; set; }
            public required sbyte Amount { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Mode);
                writer.WriteSByte(1, Amount);
                writer.EndContainer();
            }
        }

        private record SetWeeklySchedulePayload : TLVPayload {
            public required byte NumberOfTransitionsForSequence { get; set; }
            public required ScheduleDayOfWeek DayOfWeekForSequence { get; set; }
            public required ScheduleMode ModeForSequence { get; set; }
            public required WeeklyScheduleTransition[] Transitions { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, NumberOfTransitionsForSequence);
                writer.WriteUInt(1, (uint)DayOfWeekForSequence);
                writer.WriteUInt(2, (uint)ModeForSequence);
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

        private record GetWeeklySchedulePayload : TLVPayload {
            public required ScheduleDayOfWeek DaysToReturn { get; set; }
            public required ScheduleMode ModeToReturn { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)DaysToReturn);
                writer.WriteUInt(1, (uint)ModeToReturn);
                writer.EndContainer();
            }
        }

        private record SetActiveScheduleRequestPayload : TLVPayload {
            public required byte[] ScheduleHandle { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, ScheduleHandle, 16);
                writer.EndContainer();
            }
        }

        private record SetActivePresetRequestPayload : TLVPayload {
            public required byte[]? PresetHandle { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, PresetHandle, 16);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Get Weekly Schedule Response - Reply from server
        /// </summary>
        public struct GetWeeklyScheduleResponse() {
            public required byte NumberOfTransitionsForSequence { get; set; }
            public required ScheduleDayOfWeek DayOfWeekForSequence { get; set; }
            public required ScheduleMode ModeForSequence { get; set; }
            public required WeeklyScheduleTransition[] Transitions { get; set; }
        }

        /// <summary>
        /// Atomic Response - Reply from server
        /// </summary>
        public struct AtomicResponse() {
            public required IMStatusCode StatusCode { get; set; }
            public required AtomicAttributeStatus[] AttributeStatus { get; set; }
            public ushort? Timeout { get; set; }
        }

        private record AtomicRequestPayload : TLVPayload {
            public required AtomicRequestType RequestType { get; set; }
            public required uint[] AttributeRequests { get; set; }
            public ushort? Timeout { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)RequestType);
                {
                    writer.StartArray(1);
                    foreach (var item in AttributeRequests) {
                        writer.WriteUInt(-1, item);
                    }
                    writer.EndContainer();
                }
                if (Timeout != null)
                    writer.WriteUShort(2, Timeout);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Setpoint Raise Lower
        /// </summary>
        public async Task<bool> SetpointRaiseLower(SecureSession session, SetpointRaiseLowerMode mode, sbyte amount) {
            SetpointRaiseLowerPayload requestFields = new SetpointRaiseLowerPayload() {
                Mode = mode,
                Amount = amount,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Weekly Schedule
        /// </summary>
        public async Task<bool> SetWeeklySchedule(SecureSession session, byte numberOfTransitionsForSequence, ScheduleDayOfWeek dayOfWeekForSequence, ScheduleMode modeForSequence, WeeklyScheduleTransition[] transitions) {
            SetWeeklySchedulePayload requestFields = new SetWeeklySchedulePayload() {
                NumberOfTransitionsForSequence = numberOfTransitionsForSequence,
                DayOfWeekForSequence = dayOfWeekForSequence,
                ModeForSequence = modeForSequence,
                Transitions = transitions,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Weekly Schedule
        /// </summary>
        public async Task<GetWeeklyScheduleResponse?> GetWeeklySchedule(SecureSession session, ScheduleDayOfWeek daysToReturn, ScheduleMode modeToReturn) {
            GetWeeklySchedulePayload requestFields = new GetWeeklySchedulePayload() {
                DaysToReturn = daysToReturn,
                ModeToReturn = modeToReturn,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new GetWeeklyScheduleResponse() {
                NumberOfTransitionsForSequence = (byte)GetField(resp, 0),
                DayOfWeekForSequence = (ScheduleDayOfWeek)(byte)GetField(resp, 1),
                ModeForSequence = (ScheduleMode)(byte)GetField(resp, 2),
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
        /// Set Active Schedule Request
        /// </summary>
        public async Task<bool> SetActiveScheduleRequest(SecureSession session, byte[] scheduleHandle) {
            SetActiveScheduleRequestPayload requestFields = new SetActiveScheduleRequestPayload() {
                ScheduleHandle = scheduleHandle,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Active Preset Request
        /// </summary>
        public async Task<bool> SetActivePresetRequest(SecureSession session, byte[]? presetHandle) {
            SetActivePresetRequestPayload requestFields = new SetActivePresetRequestPayload() {
                PresetHandle = presetHandle,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Atomic Request
        /// </summary>
        public async Task<AtomicResponse?> AtomicRequest(SecureSession session, AtomicRequestType requestType, uint[] attributeRequests, ushort? timeout) {
            AtomicRequestPayload requestFields = new AtomicRequestPayload() {
                RequestType = requestType,
                AttributeRequests = attributeRequests,
                Timeout = timeout,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0xFE, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new AtomicResponse() {
                StatusCode = (IMStatusCode)(byte)GetField(resp, 0),
                AttributeStatus = (AtomicAttributeStatus[])GetField(resp, 1),
                Timeout = (ushort?)GetOptionalField(resp, 2),
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
            return (short?)(dynamic?)await GetAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Outdoor Temperature attribute
        /// </summary>
        public async Task<short?> GetOutdoorTemperature(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 1, true);
        }

        /// <summary>
        /// Get the Occupancy attribute
        /// </summary>
        public async Task<Occupancy> GetOccupancy(SecureSession session) {
            return (Occupancy)await GetEnumAttribute(session, 2);
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
        public async Task<HVACSystemType> GetHVACSystemTypeConfiguration(SecureSession session) {
            return (HVACSystemType)await GetEnumAttribute(session, 9);
        }

        /// <summary>
        /// Set the HVAC System Type Configuration attribute
        /// </summary>
        public async Task SetHVACSystemTypeConfiguration (SecureSession session, HVACSystemType value) {
            await SetAttribute(session, 9, value);
        }

        /// <summary>
        /// Get the Local Temperature Calibration attribute
        /// </summary>
        public async Task<sbyte> GetLocalTemperatureCalibration(SecureSession session) {
            return (sbyte?)(dynamic?)await GetAttribute(session, 16) ?? 0x00;
        }

        /// <summary>
        /// Set the Local Temperature Calibration attribute
        /// </summary>
        public async Task SetLocalTemperatureCalibration (SecureSession session, sbyte? value = 0x00) {
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
            return (short?)(dynamic?)await GetAttribute(session, 21) ?? 700;
        }

        /// <summary>
        /// Set the Min Heat Setpoint Limit attribute
        /// </summary>
        public async Task SetMinHeatSetpointLimit (SecureSession session, short? value = 700) {
            await SetAttribute(session, 21, value);
        }

        /// <summary>
        /// Get the Max Heat Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetMaxHeatSetpointLimit(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 22) ?? 3000;
        }

        /// <summary>
        /// Set the Max Heat Setpoint Limit attribute
        /// </summary>
        public async Task SetMaxHeatSetpointLimit (SecureSession session, short? value = 3000) {
            await SetAttribute(session, 22, value);
        }

        /// <summary>
        /// Get the Min Cool Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetMinCoolSetpointLimit(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 23) ?? 1600;
        }

        /// <summary>
        /// Set the Min Cool Setpoint Limit attribute
        /// </summary>
        public async Task SetMinCoolSetpointLimit (SecureSession session, short? value = 1600) {
            await SetAttribute(session, 23, value);
        }

        /// <summary>
        /// Get the Max Cool Setpoint Limit attribute
        /// </summary>
        public async Task<short> GetMaxCoolSetpointLimit(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 24) ?? 3200;
        }

        /// <summary>
        /// Set the Max Cool Setpoint Limit attribute
        /// </summary>
        public async Task SetMaxCoolSetpointLimit (SecureSession session, short? value = 3200) {
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
        public async Task<RemoteSensing> GetRemoteSensing(SecureSession session) {
            return (RemoteSensing)await GetEnumAttribute(session, 26);
        }

        /// <summary>
        /// Set the Remote Sensing attribute
        /// </summary>
        public async Task SetRemoteSensing (SecureSession session, RemoteSensing value) {
            await SetAttribute(session, 26, value);
        }

        /// <summary>
        /// Get the Control Sequence Of Operation attribute
        /// </summary>
        public async Task<ControlSequenceOfOperation> GetControlSequenceOfOperation(SecureSession session) {
            return (ControlSequenceOfOperation)await GetEnumAttribute(session, 27);
        }

        /// <summary>
        /// Set the Control Sequence Of Operation attribute
        /// </summary>
        public async Task SetControlSequenceOfOperation (SecureSession session, ControlSequenceOfOperation value) {
            await SetAttribute(session, 27, value);
        }

        /// <summary>
        /// Get the System Mode attribute
        /// </summary>
        public async Task<SystemMode> GetSystemMode(SecureSession session) {
            return (SystemMode)await GetEnumAttribute(session, 28);
        }

        /// <summary>
        /// Set the System Mode attribute
        /// </summary>
        public async Task SetSystemMode (SecureSession session, SystemMode value) {
            await SetAttribute(session, 28, value);
        }

        /// <summary>
        /// Get the Thermostat Running Mode attribute
        /// </summary>
        public async Task<ThermostatRunningMode> GetThermostatRunningMode(SecureSession session) {
            return (ThermostatRunningMode)await GetEnumAttribute(session, 30);
        }

        /// <summary>
        /// Get the Start Of Week attribute
        /// </summary>
        public async Task<StartOfWeek> GetStartOfWeek(SecureSession session) {
            return (StartOfWeek)await GetEnumAttribute(session, 32);
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
        public async Task<TemperatureSetpointHold> GetTemperatureSetpointHold(SecureSession session) {
            return (TemperatureSetpointHold)await GetEnumAttribute(session, 35);
        }

        /// <summary>
        /// Set the Temperature Setpoint Hold attribute
        /// </summary>
        public async Task SetTemperatureSetpointHold (SecureSession session, TemperatureSetpointHold value) {
            await SetAttribute(session, 35, value);
        }

        /// <summary>
        /// Get the Temperature Setpoint Hold Duration attribute
        /// </summary>
        public async Task<ushort?> GetTemperatureSetpointHoldDuration(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 36, true);
        }

        /// <summary>
        /// Set the Temperature Setpoint Hold Duration attribute
        /// </summary>
        public async Task SetTemperatureSetpointHoldDuration (SecureSession session, ushort? value) {
            await SetAttribute(session, 36, value, true);
        }

        /// <summary>
        /// Get the Thermostat Programming Operation Mode attribute
        /// </summary>
        public async Task<ProgrammingOperationMode> GetThermostatProgrammingOperationMode(SecureSession session) {
            return (ProgrammingOperationMode)await GetEnumAttribute(session, 37);
        }

        /// <summary>
        /// Set the Thermostat Programming Operation Mode attribute
        /// </summary>
        public async Task SetThermostatProgrammingOperationMode (SecureSession session, ProgrammingOperationMode value) {
            await SetAttribute(session, 37, value);
        }

        /// <summary>
        /// Get the Thermostat Running State attribute
        /// </summary>
        public async Task<RelayState> GetThermostatRunningState(SecureSession session) {
            return (RelayState)await GetEnumAttribute(session, 41);
        }

        /// <summary>
        /// Get the Setpoint Change Source attribute
        /// </summary>
        public async Task<SetpointChangeSource> GetSetpointChangeSource(SecureSession session) {
            return (SetpointChangeSource)await GetEnumAttribute(session, 48);
        }

        /// <summary>
        /// Get the Setpoint Change Amount attribute
        /// </summary>
        public async Task<short?> GetSetpointChangeAmount(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 49, true);
        }

        /// <summary>
        /// Get the Setpoint Change Source Timestamp attribute
        /// </summary>
        public async Task<DateTime> GetSetpointChangeSourceTimestamp(SecureSession session) {
            return TimeUtil.FromEpochSeconds((uint)(dynamic?)await GetAttribute(session, 50)) ?? TimeUtil.EPOCH;
        }

        /// <summary>
        /// Get the Occupied Setback attribute
        /// </summary>
        public async Task<byte?> GetOccupiedSetback(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 52, true);
        }

        /// <summary>
        /// Set the Occupied Setback attribute
        /// </summary>
        public async Task SetOccupiedSetback (SecureSession session, byte? value) {
            await SetAttribute(session, 52, value, true);
        }

        /// <summary>
        /// Get the Occupied Setback Min attribute
        /// </summary>
        public async Task<byte?> GetOccupiedSetbackMin(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 53, true);
        }

        /// <summary>
        /// Get the Occupied Setback Max attribute
        /// </summary>
        public async Task<byte?> GetOccupiedSetbackMax(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 54, true);
        }

        /// <summary>
        /// Get the Unoccupied Setback attribute
        /// </summary>
        public async Task<byte?> GetUnoccupiedSetback(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 55, true);
        }

        /// <summary>
        /// Set the Unoccupied Setback attribute
        /// </summary>
        public async Task SetUnoccupiedSetback (SecureSession session, byte? value) {
            await SetAttribute(session, 55, value, true);
        }

        /// <summary>
        /// Get the Unoccupied Setback Min attribute
        /// </summary>
        public async Task<byte?> GetUnoccupiedSetbackMin(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 56, true);
        }

        /// <summary>
        /// Get the Unoccupied Setback Max attribute
        /// </summary>
        public async Task<byte?> GetUnoccupiedSetbackMax(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 57, true);
        }

        /// <summary>
        /// Get the Emergency Heat Delta attribute
        /// </summary>
        public async Task<byte> GetEmergencyHeatDelta(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 58) ?? 0xFF;
        }

        /// <summary>
        /// Set the Emergency Heat Delta attribute
        /// </summary>
        public async Task SetEmergencyHeatDelta (SecureSession session, byte? value = 0xFF) {
            await SetAttribute(session, 58, value);
        }

        /// <summary>
        /// Get the AC Type attribute
        /// </summary>
        public async Task<ACType> GetACType(SecureSession session) {
            return (ACType)await GetEnumAttribute(session, 64);
        }

        /// <summary>
        /// Set the AC Type attribute
        /// </summary>
        public async Task SetACType (SecureSession session, ACType value) {
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
        public async Task<ACRefrigerantType> GetACRefrigerantType(SecureSession session) {
            return (ACRefrigerantType)await GetEnumAttribute(session, 66);
        }

        /// <summary>
        /// Set the AC Refrigerant Type attribute
        /// </summary>
        public async Task SetACRefrigerantType (SecureSession session, ACRefrigerantType value) {
            await SetAttribute(session, 66, value);
        }

        /// <summary>
        /// Get the AC Compressor Type attribute
        /// </summary>
        public async Task<ACCompressorType> GetACCompressorType(SecureSession session) {
            return (ACCompressorType)await GetEnumAttribute(session, 67);
        }

        /// <summary>
        /// Set the AC Compressor Type attribute
        /// </summary>
        public async Task SetACCompressorType (SecureSession session, ACCompressorType value) {
            await SetAttribute(session, 67, value);
        }

        /// <summary>
        /// Get the AC Error Code attribute
        /// </summary>
        public async Task<ACErrorCode> GetACErrorCode(SecureSession session) {
            return (ACErrorCode)await GetEnumAttribute(session, 68);
        }

        /// <summary>
        /// Set the AC Error Code attribute
        /// </summary>
        public async Task SetACErrorCode (SecureSession session, ACErrorCode value) {
            await SetAttribute(session, 68, value);
        }

        /// <summary>
        /// Get the AC Louver Position attribute
        /// </summary>
        public async Task<ACLouverPosition> GetACLouverPosition(SecureSession session) {
            return (ACLouverPosition)await GetEnumAttribute(session, 69);
        }

        /// <summary>
        /// Set the AC Louver Position attribute
        /// </summary>
        public async Task SetACLouverPosition (SecureSession session, ACLouverPosition value) {
            await SetAttribute(session, 69, value);
        }

        /// <summary>
        /// Get the AC Coil Temperature attribute
        /// </summary>
        public async Task<short?> GetACCoilTemperature(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 70, true);
        }

        /// <summary>
        /// Get the AC Capacityformat attribute
        /// </summary>
        public async Task<ACCapacityFormat> GetACCapacityformat(SecureSession session) {
            return (ACCapacityFormat)await GetEnumAttribute(session, 71);
        }

        /// <summary>
        /// Set the AC Capacityformat attribute
        /// </summary>
        public async Task SetACCapacityformat (SecureSession session, ACCapacityFormat value) {
            await SetAttribute(session, 71, value);
        }

        /// <summary>
        /// Get the Preset Types attribute
        /// </summary>
        public async Task<PresetType[]> GetPresetTypes(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 72))!);
            PresetType[] list = new PresetType[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new PresetType(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Schedule Types attribute
        /// </summary>
        public async Task<ScheduleType[]> GetScheduleTypes(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 73))!);
            ScheduleType[] list = new ScheduleType[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ScheduleType(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Number Of Presets attribute
        /// </summary>
        public async Task<byte> GetNumberOfPresets(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 74) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Schedules attribute
        /// </summary>
        public async Task<byte> GetNumberOfSchedules(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 75) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Schedule Transitions attribute
        /// </summary>
        public async Task<byte> GetNumberOfScheduleTransitions(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 76) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Schedule Transition Per Day attribute
        /// </summary>
        public async Task<byte?> GetNumberOfScheduleTransitionPerDay(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 77, true);
        }

        /// <summary>
        /// Get the Active Preset Handle attribute
        /// </summary>
        public async Task<byte[]?> GetActivePresetHandle(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 78, true);
        }

        /// <summary>
        /// Get the Active Schedule Handle attribute
        /// </summary>
        public async Task<byte[]?> GetActiveScheduleHandle(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 79, true);
        }

        /// <summary>
        /// Get the Presets attribute
        /// </summary>
        public async Task<Preset[]> GetPresets(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 80))!);
            Preset[] list = new Preset[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Preset(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the Presets attribute
        /// </summary>
        public async Task SetPresets (SecureSession session, Preset[] value) {
            await SetAttribute(session, 80, value);
        }

        /// <summary>
        /// Get the Schedules attribute
        /// </summary>
        public async Task<Schedule[]> GetSchedules(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 81))!);
            Schedule[] list = new Schedule[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Schedule(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the Schedules attribute
        /// </summary>
        public async Task SetSchedules (SecureSession session, Schedule[] value) {
            await SetAttribute(session, 81, value);
        }

        /// <summary>
        /// Get the Setpoint Hold Expiry Timestamp attribute
        /// </summary>
        public async Task<DateTime?> GetSetpointHoldExpiryTimestamp(SecureSession session) {
            return TimeUtil.FromEpochSeconds((uint)(dynamic?)await GetAttribute(session, 82));
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thermostat";
        }
    }
}