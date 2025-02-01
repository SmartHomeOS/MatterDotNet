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
        [SetsRequiredMembers]
        public Thermostat(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected Thermostat(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            LocalTemperature = new ReadAttribute<decimal?>(cluster, endPoint, 0, true) {
                Deserialize = x => (decimal?)(dynamic?)x
            };
            OutdoorTemperature = new ReadAttribute<decimal?>(cluster, endPoint, 1, true) {
                Deserialize = x => (decimal?)(dynamic?)x
            };
            Occupancy = new ReadAttribute<OccupancyBitmap>(cluster, endPoint, 2) {
                Deserialize = x => (OccupancyBitmap)DeserializeEnum(x)!
            };
            AbsMinHeatSetpointLimit = new ReadAttribute<decimal>(cluster, endPoint, 3) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 7.00M

            };
            AbsMaxHeatSetpointLimit = new ReadAttribute<decimal>(cluster, endPoint, 4) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 30.00M

            };
            AbsMinCoolSetpointLimit = new ReadAttribute<decimal>(cluster, endPoint, 5) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 16.00M

            };
            AbsMaxCoolSetpointLimit = new ReadAttribute<decimal>(cluster, endPoint, 6) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 32.00M

            };
            PICoolingDemand = new ReadAttribute<byte>(cluster, endPoint, 7) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            PIHeatingDemand = new ReadAttribute<byte>(cluster, endPoint, 8) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            HVACSystemTypeConfiguration = new ReadWriteAttribute<HVACSystemType>(cluster, endPoint, 9) {
                Deserialize = x => (HVACSystemType)DeserializeEnum(x)!
            };
            LocalTemperatureCalibration = new ReadWriteAttribute<sbyte>(cluster, endPoint, 16) {
                Deserialize = x => (sbyte?)(dynamic?)x ?? 0x00

            };
            OccupiedCoolingSetpoint = new ReadWriteAttribute<decimal>(cluster, endPoint, 17) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 26.00M

            };
            OccupiedHeatingSetpoint = new ReadWriteAttribute<decimal>(cluster, endPoint, 18) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 20.00M

            };
            UnoccupiedCoolingSetpoint = new ReadWriteAttribute<decimal>(cluster, endPoint, 19) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 26.00M

            };
            UnoccupiedHeatingSetpoint = new ReadWriteAttribute<decimal>(cluster, endPoint, 20) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 20.00M

            };
            MinHeatSetpointLimit = new ReadWriteAttribute<decimal>(cluster, endPoint, 21) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 7.00M

            };
            MaxHeatSetpointLimit = new ReadWriteAttribute<decimal>(cluster, endPoint, 22) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 30.00M

            };
            MinCoolSetpointLimit = new ReadWriteAttribute<decimal>(cluster, endPoint, 23) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 16.00M

            };
            MaxCoolSetpointLimit = new ReadWriteAttribute<decimal>(cluster, endPoint, 24) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 32.00M

            };
            MinSetpointDeadBand = new ReadWriteAttribute<sbyte>(cluster, endPoint, 25) {
                Deserialize = x => (sbyte?)(dynamic?)x ?? 25

            };
            RemoteSensing = new ReadWriteAttribute<RemoteSensingBitmap>(cluster, endPoint, 26) {
                Deserialize = x => (RemoteSensingBitmap)DeserializeEnum(x)!
            };
            ControlSequenceOfOperation = new ReadWriteAttribute<ControlSequenceOfOperationEnum>(cluster, endPoint, 27) {
                Deserialize = x => (ControlSequenceOfOperationEnum)DeserializeEnum(x)!
            };
            SystemMode = new ReadWriteAttribute<SystemModeEnum>(cluster, endPoint, 28) {
                Deserialize = x => (SystemModeEnum)DeserializeEnum(x)!
            };
            ThermostatRunningMode = new ReadAttribute<ThermostatRunningModeEnum>(cluster, endPoint, 30) {
                Deserialize = x => (ThermostatRunningModeEnum)DeserializeEnum(x)!
            };
            StartOfWeek = new ReadAttribute<StartOfWeekEnum>(cluster, endPoint, 32) {
                Deserialize = x => (StartOfWeekEnum)DeserializeEnum(x)!
            };
            NumberOfWeeklyTransitions = new ReadAttribute<byte>(cluster, endPoint, 33) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            NumberOfDailyTransitions = new ReadAttribute<byte>(cluster, endPoint, 34) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            TemperatureSetpointHold = new ReadWriteAttribute<TemperatureSetpointHoldEnum>(cluster, endPoint, 35) {
                Deserialize = x => (TemperatureSetpointHoldEnum)DeserializeEnum(x)!
            };
            TemperatureSetpointHoldDuration = new ReadWriteAttribute<ushort?>(cluster, endPoint, 36, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            ThermostatProgrammingOperationMode = new ReadWriteAttribute<ProgrammingOperationMode>(cluster, endPoint, 37) {
                Deserialize = x => (ProgrammingOperationMode)DeserializeEnum(x)!
            };
            ThermostatRunningState = new ReadAttribute<RelayState>(cluster, endPoint, 41) {
                Deserialize = x => (RelayState)DeserializeEnum(x)!
            };
            SetpointChangeSource = new ReadAttribute<SetpointChangeSourceEnum>(cluster, endPoint, 48) {
                Deserialize = x => (SetpointChangeSourceEnum)DeserializeEnum(x)!
            };
            SetpointChangeAmount = new ReadAttribute<short?>(cluster, endPoint, 49, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            SetpointChangeSourceTimestamp = new ReadAttribute<DateTime>(cluster, endPoint, 50) {
                Deserialize = x => TimeUtil.FromEpochSeconds((uint)(dynamic?)x) ?? TimeUtil.EPOCH

            };
            OccupiedSetback = new ReadWriteAttribute<byte?>(cluster, endPoint, 52, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            OccupiedSetbackMin = new ReadAttribute<byte?>(cluster, endPoint, 53, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            OccupiedSetbackMax = new ReadAttribute<byte?>(cluster, endPoint, 54, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            UnoccupiedSetback = new ReadWriteAttribute<byte?>(cluster, endPoint, 55, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            UnoccupiedSetbackMin = new ReadAttribute<byte?>(cluster, endPoint, 56, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            UnoccupiedSetbackMax = new ReadAttribute<byte?>(cluster, endPoint, 57, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            EmergencyHeatDelta = new ReadWriteAttribute<byte>(cluster, endPoint, 58) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0xFF

            };
            ACType = new ReadWriteAttribute<ACTypeEnum>(cluster, endPoint, 64) {
                Deserialize = x => (ACTypeEnum)DeserializeEnum(x)!
            };
            ACCapacity = new ReadWriteAttribute<ushort>(cluster, endPoint, 65) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            ACRefrigerantType = new ReadWriteAttribute<ACRefrigerantTypeEnum>(cluster, endPoint, 66) {
                Deserialize = x => (ACRefrigerantTypeEnum)DeserializeEnum(x)!
            };
            ACCompressorType = new ReadWriteAttribute<ACCompressorTypeEnum>(cluster, endPoint, 67) {
                Deserialize = x => (ACCompressorTypeEnum)DeserializeEnum(x)!
            };
            ACErrorCode = new ReadWriteAttribute<ACErrorCodeBitmap>(cluster, endPoint, 68) {
                Deserialize = x => (ACErrorCodeBitmap)DeserializeEnum(x)!
            };
            ACLouverPosition = new ReadWriteAttribute<ACLouverPositionEnum>(cluster, endPoint, 69) {
                Deserialize = x => (ACLouverPositionEnum)DeserializeEnum(x)!
            };
            ACCoilTemperature = new ReadAttribute<decimal?>(cluster, endPoint, 70, true) {
                Deserialize = x => (decimal?)(dynamic?)x
            };
            ACCapacityformat = new ReadWriteAttribute<ACCapacityFormat>(cluster, endPoint, 71) {
                Deserialize = x => (ACCapacityFormat)DeserializeEnum(x)!
            };
            PresetTypes = new ReadAttribute<PresetType[]>(cluster, endPoint, 72) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    PresetType[] list = new PresetType[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new PresetType(reader.GetStruct(i)!);
                    return list;
                }
            };
            ScheduleTypes = new ReadAttribute<ScheduleType[]>(cluster, endPoint, 73) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ScheduleType[] list = new ScheduleType[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ScheduleType(reader.GetStruct(i)!);
                    return list;
                }
            };
            NumberOfPresets = new ReadAttribute<byte>(cluster, endPoint, 74) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            NumberOfSchedules = new ReadAttribute<byte>(cluster, endPoint, 75) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            NumberOfScheduleTransitions = new ReadAttribute<byte>(cluster, endPoint, 76) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
            NumberOfScheduleTransitionPerDay = new ReadAttribute<byte?>(cluster, endPoint, 77, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            ActivePresetHandle = new ReadAttribute<byte[]?>(cluster, endPoint, 78, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            ActiveScheduleHandle = new ReadAttribute<byte[]?>(cluster, endPoint, 79, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            Presets = new ReadWriteAttribute<Preset[]>(cluster, endPoint, 80) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Preset[] list = new Preset[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Preset(reader.GetStruct(i)!);
                    return list;
                }
            };
            Schedules = new ReadWriteAttribute<Schedule[]>(cluster, endPoint, 81) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Schedule[] list = new Schedule[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Schedule(reader.GetStruct(i)!);
                    return list;
                }
            };
            SetpointHoldExpiryTimestamp = new ReadAttribute<DateTime?>(cluster, endPoint, 82, true) {
                Deserialize = x => TimeUtil.FromEpochSeconds((uint)(dynamic?)x)
            };
        }

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
        public enum SystemModeEnum : byte {
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
            /// <summary>
            /// 
            /// </summary>
            FanOnly = 7,
            /// <summary>
            /// 
            /// </summary>
            Dry = 8,
            /// <summary>
            /// 
            /// </summary>
            Sleep = 9,
        }

        /// <summary>
        /// Thermostat Running Mode
        /// </summary>
        public enum ThermostatRunningModeEnum : byte {
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
        /// Start Of Week
        /// </summary>
        public enum StartOfWeekEnum : byte {
            /// <summary>
            /// 
            /// </summary>
            Sunday = 0,
            /// <summary>
            /// 
            /// </summary>
            Monday = 1,
            /// <summary>
            /// 
            /// </summary>
            Tuesday = 2,
            /// <summary>
            /// 
            /// </summary>
            Wednesday = 3,
            /// <summary>
            /// 
            /// </summary>
            Thursday = 4,
            /// <summary>
            /// 
            /// </summary>
            Friday = 5,
            /// <summary>
            /// 
            /// </summary>
            Saturday = 6,
        }

        /// <summary>
        /// Control Sequence Of Operation
        /// </summary>
        public enum ControlSequenceOfOperationEnum : byte {
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
        /// Temperature Setpoint Hold
        /// </summary>
        public enum TemperatureSetpointHoldEnum : byte {
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
        /// Setpoint Raise Lower Mode
        /// </summary>
        public enum SetpointRaiseLowerMode : byte {
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
        /// AC Capacity Format
        /// </summary>
        public enum ACCapacityFormat : byte {
            /// <summary>
            /// British Thermal Unit per Hour
            /// </summary>
            BTUh = 0,
        }

        /// <summary>
        /// AC Compressor Type
        /// </summary>
        public enum ACCompressorTypeEnum : byte {
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
        public enum ACLouverPositionEnum : byte {
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
        public enum ACRefrigerantTypeEnum : byte {
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
        public enum ACTypeEnum : byte {
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
        /// Setpoint Change Source
        /// </summary>
        public enum SetpointChangeSourceEnum : byte {
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
        /// Preset Scenario
        /// </summary>
        public enum PresetScenario : byte {
            /// <summary>
            /// The thermostat-controlled area is occupied
            /// </summary>
            Occupied = 0x1,
            /// <summary>
            /// The thermostat-controlled area is unoccupied
            /// </summary>
            Unoccupied = 0x2,
            /// <summary>
            /// Users are likely to be sleeping
            /// </summary>
            Sleep = 0x3,
            /// <summary>
            /// Users are likely to be waking up
            /// </summary>
            Wake = 0x4,
            /// <summary>
            /// Users are on vacation
            /// </summary>
            Vacation = 0x5,
            /// <summary>
            /// Users are likely to be going to sleep
            /// </summary>
            GoingToSleep = 0x6,
            /// <summary>
            /// Custom presets
            /// </summary>
            UserDefined = 0xFE,
        }

        /// <summary>
        /// AC Error Code
        /// </summary>
        [Flags]
        public enum ACErrorCodeBitmap : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Compressor Failure or Refrigerant Leakage
            /// </summary>
            CompressorFail = 0x0001,
            /// <summary>
            /// Room Temperature Sensor Failure
            /// </summary>
            RoomSensorFail = 0x0002,
            /// <summary>
            /// Outdoor Temperature Sensor Failure
            /// </summary>
            OutdoorSensorFail = 0x0004,
            /// <summary>
            /// Indoor Coil Temperature Sensor Failure
            /// </summary>
            CoilSensorFail = 0x0008,
            /// <summary>
            /// Fan Failure
            /// </summary>
            FanFail = 0x0010,
        }

        /// <summary>
        /// HVAC System Type
        /// </summary>
        [Flags]
        public enum HVACSystemType : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
        public enum OccupancyBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
            None = 0x0,
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
            None = 0x0,
            /// <summary>
            /// Supports presets
            /// </summary>
            SupportsPresets = 0x0001,
            /// <summary>
            /// Supports setpoints
            /// </summary>
            SupportsSetpoints = 0x0002,
            /// <summary>
            /// Supports user-provided names
            /// </summary>
            SupportsNames = 0x0004,
            /// <summary>
            /// Supports transitioning to SystemModeOff
            /// </summary>
            SupportsOff = 0x0008,
        }

        /// <summary>
        /// Relay State
        /// </summary>
        [Flags]
        public enum RelayState : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Heat Stage On
            /// </summary>
            Heat = 0x0001,
            /// <summary>
            /// Cool Stage On
            /// </summary>
            Cool = 0x0002,
            /// <summary>
            /// Fan Stage On
            /// </summary>
            Fan = 0x0004,
            /// <summary>
            /// Heat 2 Stage On
            /// </summary>
            HeatStage2 = 0x0008,
            /// <summary>
            /// Cool 2 Stage On
            /// </summary>
            CoolStage2 = 0x0010,
            /// <summary>
            /// Fan 2 Stage On
            /// </summary>
            FanStage2 = 0x0020,
            /// <summary>
            /// Fan 3 Stage On
            /// </summary>
            FanStage3 = 0x0040,
        }

        /// <summary>
        /// Remote Sensing
        /// </summary>
        [Flags]
        public enum RemoteSensingBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
            None = 0x0,
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
            None = 0x0,
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
            None = 0x0,
            /// <summary>
            /// Preset may be automatically activated by the thermostat
            /// </summary>
            Automatic = 0x0001,
            /// <summary>
            /// Preset supports user-provided names
            /// </summary>
            SupportsNames = 0x0002,
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
                HeatSetpoint = reader.GetDecimal(1, true);
                CoolSetpoint = reader.GetDecimal(2, true);
            }
            public required ushort TransitionTime { get; set; }
            public required decimal? HeatSetpoint { get; set; }
            public required decimal? CoolSetpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, TransitionTime, 1439);
                writer.WriteDecimal(1, HeatSetpoint);
                writer.WriteDecimal(2, CoolSetpoint);
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
                SystemMode = (SystemModeEnum)reader.GetUShort(0)!.Value;
                NumberOfSchedules = reader.GetByte(1)!.Value;
                ScheduleTypeFeatures = (ScheduleTypeFeatures)reader.GetUInt(2)!.Value;
            }
            public required SystemModeEnum SystemMode { get; set; }
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
                CoolingSetpoint = reader.GetDecimal(3, true);
                HeatingSetpoint = reader.GetDecimal(4, true);
                BuiltIn = reader.GetBool(5, true);
            }
            public required byte[]? PresetHandle { get; set; }
            public required PresetScenario PresetScenario { get; set; }
            public string? Name { get; set; }
            public decimal? CoolingSetpoint { get; set; } = 0x0A28;
            public decimal? HeatingSetpoint { get; set; } = 0x07D0;
            public required bool? BuiltIn { get; set; } = false;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, PresetHandle, 16);
                writer.WriteUShort(1, (ushort)PresetScenario);
                if (Name != null)
                    writer.WriteString(2, Name, 64);
                if (CoolingSetpoint != null)
                    writer.WriteDecimal(3, CoolingSetpoint);
                if (HeatingSetpoint != null)
                    writer.WriteDecimal(4, HeatingSetpoint);
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
                SystemMode = (SystemModeEnum)reader.GetUShort(1)!.Value;
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
            public required SystemModeEnum SystemMode { get; set; }
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
                SystemMode = (SystemModeEnum?)reader.GetUShort(3, true);
                CoolingSetpoint = reader.GetDecimal(4, true);
                HeatingSetpoint = reader.GetDecimal(5, true);
            }
            public required ScheduleDayOfWeek DayOfWeek { get; set; }
            public required ushort TransitionTime { get; set; }
            public byte[]? PresetHandle { get; set; }
            public SystemModeEnum? SystemMode { get; set; }
            public decimal? CoolingSetpoint { get; set; }
            public decimal? HeatingSetpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)DayOfWeek);
                writer.WriteUShort(1, TransitionTime, 1439);
                if (PresetHandle != null)
                    writer.WriteBytes(2, PresetHandle, 16);
                if (SystemMode != null)
                    writer.WriteUShort(3, (ushort)SystemMode);
                if (CoolingSetpoint != null)
                    writer.WriteDecimal(4, CoolingSetpoint);
                if (HeatingSetpoint != null)
                    writer.WriteDecimal(5, HeatingSetpoint);
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
        public async Task<bool> SetpointRaiseLower(SecureSession session, SetpointRaiseLowerMode mode, sbyte amount, CancellationToken token = default) {
            SetpointRaiseLowerPayload requestFields = new SetpointRaiseLowerPayload() {
                Mode = mode,
                Amount = amount,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Weekly Schedule
        /// </summary>
        public async Task<bool> SetWeeklySchedule(SecureSession session, byte numberOfTransitionsForSequence, ScheduleDayOfWeek dayOfWeekForSequence, ScheduleMode modeForSequence, WeeklyScheduleTransition[] transitions, CancellationToken token = default) {
            SetWeeklySchedulePayload requestFields = new SetWeeklySchedulePayload() {
                NumberOfTransitionsForSequence = numberOfTransitionsForSequence,
                DayOfWeekForSequence = dayOfWeekForSequence,
                ModeForSequence = modeForSequence,
                Transitions = transitions,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Get Weekly Schedule
        /// </summary>
        public async Task<GetWeeklyScheduleResponse?> GetWeeklySchedule(SecureSession session, ScheduleDayOfWeek daysToReturn, ScheduleMode modeToReturn, CancellationToken token = default) {
            GetWeeklySchedulePayload requestFields = new GetWeeklySchedulePayload() {
                DaysToReturn = daysToReturn,
                ModeToReturn = modeToReturn,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
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
        public async Task<bool> ClearWeeklySchedule(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Active Schedule Request
        /// </summary>
        public async Task<bool> SetActiveScheduleRequest(SecureSession session, byte[] scheduleHandle, CancellationToken token = default) {
            SetActiveScheduleRequestPayload requestFields = new SetActiveScheduleRequestPayload() {
                ScheduleHandle = scheduleHandle,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Set Active Preset Request
        /// </summary>
        public async Task<bool> SetActivePresetRequest(SecureSession session, byte[]? presetHandle, CancellationToken token = default) {
            SetActivePresetRequestPayload requestFields = new SetActivePresetRequestPayload() {
                PresetHandle = presetHandle,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Atomic Request
        /// </summary>
        public async Task<AtomicResponse?> AtomicRequest(SecureSession session, AtomicRequestType requestType, uint[] attributeRequests, ushort? timeout, CancellationToken token = default) {
            AtomicRequestPayload requestFields = new AtomicRequestPayload() {
                RequestType = requestType,
                AttributeRequests = attributeRequests,
                Timeout = timeout,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0xFE, requestFields, token);
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
        /// Local Temperature [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> LocalTemperature { get; init; }

        /// <summary>
        /// Outdoor Temperature [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> OutdoorTemperature { get; init; }

        /// <summary>
        /// Occupancy Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<OccupancyBitmap> Occupancy { get; init; }

        /// <summary>
        /// Abs Min Heat Setpoint Limit [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal> AbsMinHeatSetpointLimit { get; init; }

        /// <summary>
        /// Abs Max Heat Setpoint Limit [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal> AbsMaxHeatSetpointLimit { get; init; }

        /// <summary>
        /// Abs Min Cool Setpoint Limit [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal> AbsMinCoolSetpointLimit { get; init; }

        /// <summary>
        /// Abs Max Cool Setpoint Limit [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal> AbsMaxCoolSetpointLimit { get; init; }

        /// <summary>
        /// PI Cooling Demand Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> PICoolingDemand { get; init; }

        /// <summary>
        /// PI Heating Demand Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> PIHeatingDemand { get; init; }

        /// <summary>
        /// HVAC System Type Configuration Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<HVACSystemType> HVACSystemTypeConfiguration { get; init; }

        /// <summary>
        /// Local Temperature Calibration Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<sbyte> LocalTemperatureCalibration { get; init; }

        /// <summary>
        /// Occupied Cooling Setpoint [°C] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<decimal> OccupiedCoolingSetpoint { get; init; }

        /// <summary>
        /// Occupied Heating Setpoint [°C] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<decimal> OccupiedHeatingSetpoint { get; init; }

        /// <summary>
        /// Unoccupied Cooling Setpoint [°C] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<decimal> UnoccupiedCoolingSetpoint { get; init; }

        /// <summary>
        /// Unoccupied Heating Setpoint [°C] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<decimal> UnoccupiedHeatingSetpoint { get; init; }

        /// <summary>
        /// Min Heat Setpoint Limit [°C] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<decimal> MinHeatSetpointLimit { get; init; }

        /// <summary>
        /// Max Heat Setpoint Limit [°C] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<decimal> MaxHeatSetpointLimit { get; init; }

        /// <summary>
        /// Min Cool Setpoint Limit [°C] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<decimal> MinCoolSetpointLimit { get; init; }

        /// <summary>
        /// Max Cool Setpoint Limit [°C] Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<decimal> MaxCoolSetpointLimit { get; init; }

        /// <summary>
        /// Min Setpoint Dead Band Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<sbyte> MinSetpointDeadBand { get; init; }

        /// <summary>
        /// Remote Sensing Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<RemoteSensingBitmap> RemoteSensing { get; init; }

        /// <summary>
        /// Control Sequence Of Operation Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ControlSequenceOfOperationEnum> ControlSequenceOfOperation { get; init; }

        /// <summary>
        /// System Mode Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<SystemModeEnum> SystemMode { get; init; }

        /// <summary>
        /// Thermostat Running Mode Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ThermostatRunningModeEnum> ThermostatRunningMode { get; init; }

        /// <summary>
        /// Start Of Week Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<StartOfWeekEnum> StartOfWeek { get; init; }

        /// <summary>
        /// Number Of Weekly Transitions Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfWeeklyTransitions { get; init; }

        /// <summary>
        /// Number Of Daily Transitions Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfDailyTransitions { get; init; }

        /// <summary>
        /// Temperature Setpoint Hold Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<TemperatureSetpointHoldEnum> TemperatureSetpointHold { get; init; }

        /// <summary>
        /// Temperature Setpoint Hold Duration Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort?> TemperatureSetpointHoldDuration { get; init; }

        /// <summary>
        /// Thermostat Programming Operation Mode Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ProgrammingOperationMode> ThermostatProgrammingOperationMode { get; init; }

        /// <summary>
        /// Thermostat Running State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<RelayState> ThermostatRunningState { get; init; }

        /// <summary>
        /// Setpoint Change Source Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SetpointChangeSourceEnum> SetpointChangeSource { get; init; }

        /// <summary>
        /// Setpoint Change Amount Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<short?> SetpointChangeAmount { get; init; }

        /// <summary>
        /// Setpoint Change Source Timestamp Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<DateTime> SetpointChangeSourceTimestamp { get; init; }

        /// <summary>
        /// Occupied Setback Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> OccupiedSetback { get; init; }

        /// <summary>
        /// Occupied Setback Min Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> OccupiedSetbackMin { get; init; }

        /// <summary>
        /// Occupied Setback Max Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> OccupiedSetbackMax { get; init; }

        /// <summary>
        /// Unoccupied Setback Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> UnoccupiedSetback { get; init; }

        /// <summary>
        /// Unoccupied Setback Min Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> UnoccupiedSetbackMin { get; init; }

        /// <summary>
        /// Unoccupied Setback Max Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> UnoccupiedSetbackMax { get; init; }

        /// <summary>
        /// Emergency Heat Delta Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> EmergencyHeatDelta { get; init; }

        /// <summary>
        /// AC Type Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ACTypeEnum> ACType { get; init; }

        /// <summary>
        /// AC Capacity Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> ACCapacity { get; init; }

        /// <summary>
        /// AC Refrigerant Type Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ACRefrigerantTypeEnum> ACRefrigerantType { get; init; }

        /// <summary>
        /// AC Compressor Type Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ACCompressorTypeEnum> ACCompressorType { get; init; }

        /// <summary>
        /// AC Error Code Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ACErrorCodeBitmap> ACErrorCode { get; init; }

        /// <summary>
        /// AC Louver Position Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ACLouverPositionEnum> ACLouverPosition { get; init; }

        /// <summary>
        /// AC Coil Temperature [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> ACCoilTemperature { get; init; }

        /// <summary>
        /// AC Capacityformat Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ACCapacityFormat> ACCapacityformat { get; init; }

        /// <summary>
        /// Preset Types Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<PresetType[]> PresetTypes { get; init; }

        /// <summary>
        /// Schedule Types Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ScheduleType[]> ScheduleTypes { get; init; }

        /// <summary>
        /// Number Of Presets Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfPresets { get; init; }

        /// <summary>
        /// Number Of Schedules Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfSchedules { get; init; }

        /// <summary>
        /// Number Of Schedule Transitions Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfScheduleTransitions { get; init; }

        /// <summary>
        /// Number Of Schedule Transition Per Day Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> NumberOfScheduleTransitionPerDay { get; init; }

        /// <summary>
        /// Active Preset Handle Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> ActivePresetHandle { get; init; }

        /// <summary>
        /// Active Schedule Handle Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> ActiveScheduleHandle { get; init; }

        /// <summary>
        /// Presets Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<Preset[]> Presets { get; init; }

        /// <summary>
        /// Schedules Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<Schedule[]> Schedules { get; init; }

        /// <summary>
        /// Setpoint Hold Expiry Timestamp Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<DateTime?> SetpointHoldExpiryTimestamp { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thermostat";
        }
    }
}