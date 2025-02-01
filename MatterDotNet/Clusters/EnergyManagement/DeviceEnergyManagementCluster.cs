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
    /// This cluster allows a client to manage the power draw of a device. An example of such a client could be an Energy Management System (EMS) which controls an Energy Smart Appliance (ESA).
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class DeviceEnergyManagement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0098;

        /// <summary>
        /// This cluster allows a client to manage the power draw of a device. An example of such a client could be an Energy Management System (EMS) which controls an Energy Smart Appliance (ESA).
        /// </summary>
        [SetsRequiredMembers]
        public DeviceEnergyManagement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected DeviceEnergyManagement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            ESAType = new ReadAttribute<ESATypeEnum>(cluster, endPoint, 0) {
                Deserialize = x => (ESATypeEnum)DeserializeEnum(x)!
            };
            ESACanGenerate = new ReadAttribute<bool>(cluster, endPoint, 1) {
                Deserialize = x => (bool?)(dynamic?)x ?? false

            };
            ESAState = new ReadAttribute<ESAStateEnum>(cluster, endPoint, 2) {
                Deserialize = x => (ESAStateEnum)DeserializeEnum(x)!
            };
            AbsMinPower = new ReadAttribute<long>(cluster, endPoint, 3) {
                Deserialize = x => (long?)(dynamic?)x ?? 0

            };
            AbsMaxPower = new ReadAttribute<long>(cluster, endPoint, 4) {
                Deserialize = x => (long?)(dynamic?)x ?? 0

            };
            PowerAdjustmentCapability = new ReadAttribute<PowerAdjustCapability?>(cluster, endPoint, 5, true) {
                Deserialize = x => new PowerAdjustCapability((object[])x!)
            };
            Forecast = new ReadAttribute<ForecastStruct?>(cluster, endPoint, 6, true) {
                Deserialize = x => new ForecastStruct((object[])x!)
            };
            OptOutState = new ReadAttribute<OptOutStateEnum>(cluster, endPoint, 7) {
                Deserialize = x => (OptOutStateEnum)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Allows an EMS to make a temporary power adjustment (within the limits offered by the ESA).
            /// </summary>
            PowerAdjustment = 1,
            /// <summary>
            /// Allows an ESA to advertise its indicative future power consumption vs time.
            /// </summary>
            PowerForecastReporting = 2,
            /// <summary>
            /// Allows an ESA to advertise its indicative future state vs time.
            /// </summary>
            StateForecastReporting = 4,
            /// <summary>
            /// Allows an EMS to delay an ESA's planned operation.
            /// </summary>
            StartTimeAdjustment = 8,
            /// <summary>
            /// Allows an EMS to pause an ESA's planned operation.
            /// </summary>
            Pausable = 16,
            /// <summary>
            /// Allows an EMS to adjust an ESA's planned operation.
            /// </summary>
            ForecastAdjustment = 32,
            /// <summary>
            /// Allows an EMS to request constraints to an ESA's planned operation.
            /// </summary>
            ConstraintBasedAdjustment = 64,
        }

        /// <summary>
        /// Cost Type
        /// </summary>
        public enum CostType : byte {
            /// <summary>
            /// Financial cost
            /// </summary>
            Financial = 0,
            /// <summary>
            /// Grid CO2e grams cost
            /// </summary>
            GHGEmissions = 1,
            /// <summary>
            /// Consumer comfort impact cost
            /// </summary>
            Comfort = 2,
            /// <summary>
            /// Temperature impact cost
            /// </summary>
            Temperature = 3,
        }

        /// <summary>
        /// ESA Type
        /// </summary>
        public enum ESATypeEnum : byte {
            /// <summary>
            /// EV Supply Equipment
            /// </summary>
            EVSE = 0x0,
            /// <summary>
            /// Space heating appliance
            /// </summary>
            SpaceHeating = 0x1,
            /// <summary>
            /// Water heating appliance
            /// </summary>
            WaterHeating = 0x2,
            /// <summary>
            /// Space cooling appliance
            /// </summary>
            SpaceCooling = 0x3,
            /// <summary>
            /// Space heating and cooling appliance
            /// </summary>
            SpaceHeatingCooling = 0x4,
            /// <summary>
            /// Battery Electric Storage System
            /// </summary>
            BatteryStorage = 0x5,
            /// <summary>
            /// Solar PV inverter
            /// </summary>
            SolarPV = 0x6,
            /// <summary>
            /// Fridge / Freezer
            /// </summary>
            FridgeFreezer = 0x7,
            /// <summary>
            /// Washing Machine
            /// </summary>
            WashingMachine = 0x8,
            /// <summary>
            /// Dishwasher
            /// </summary>
            Dishwasher = 0x9,
            /// <summary>
            /// Cooking appliance
            /// </summary>
            Cooking = 0xA,
            /// <summary>
            /// Home water pump (e.g. drinking well)
            /// </summary>
            HomeWaterPump = 0xB,
            /// <summary>
            /// Irrigation water pump
            /// </summary>
            IrrigationWaterPump = 0xC,
            /// <summary>
            /// Pool pump
            /// </summary>
            PoolPump = 0xD,
            /// <summary>
            /// Other appliance type
            /// </summary>
            Other = 0xFF,
        }

        /// <summary>
        /// ESA State
        /// </summary>
        public enum ESAStateEnum : byte {
            /// <summary>
            /// The ESA is not available to the EMS (e.g. start-up, maintenance mode)
            /// </summary>
            Offline = 0,
            /// <summary>
            /// The ESA is working normally and can be controlled by the EMS
            /// </summary>
            Online = 1,
            /// <summary>
            /// The ESA has developed a fault and cannot provide service
            /// </summary>
            Fault = 2,
            /// <summary>
            /// The ESA is in the middle of a power adjustment event
            /// </summary>
            PowerAdjustActive = 3,
            /// <summary>
            /// The ESA is currently paused by a client using the PauseRequest command
            /// </summary>
            Paused = 4,
        }

        /// <summary>
        /// Cause
        /// </summary>
        public enum Cause : byte {
            /// <summary>
            /// The ESA completed the power adjustment as requested
            /// </summary>
            NormalCompletion = 0,
            /// <summary>
            /// The ESA was set to offline
            /// </summary>
            Offline = 1,
            /// <summary>
            /// The ESA has developed a fault could not complete the adjustment
            /// </summary>
            Fault = 2,
            /// <summary>
            /// The user has disabled the ESA's flexibility capability
            /// </summary>
            UserOptOut = 3,
            /// <summary>
            /// The adjustment was cancelled by a client
            /// </summary>
            Cancelled = 4,
        }

        /// <summary>
        /// Adjustment Cause
        /// </summary>
        public enum AdjustmentCause : byte {
            /// <summary>
            /// The adjustment is to optimize the local energy usage
            /// </summary>
            LocalOptimization = 0,
            /// <summary>
            /// The adjustment is to optimize the grid energy usage
            /// </summary>
            GridOptimization = 1,
        }

        /// <summary>
        /// Forecast Update Reason
        /// </summary>
        public enum ForecastUpdateReason : byte {
            /// <summary>
            /// The update was due to internal ESA device optimization
            /// </summary>
            InternalOptimization = 0,
            /// <summary>
            /// The update was due to local EMS optimization
            /// </summary>
            LocalOptimization = 1,
            /// <summary>
            /// The update was due to grid optimization
            /// </summary>
            GridOptimization = 2,
        }

        /// <summary>
        /// Opt Out State
        /// </summary>
        public enum OptOutStateEnum : byte {
            /// <summary>
            /// The user has not opted out of either local or grid optimizations
            /// </summary>
            NoOptOut = 0,
            /// <summary>
            /// The user has opted out of local EMS optimizations only
            /// </summary>
            LocalOptOut = 1,
            /// <summary>
            /// The user has opted out of grid EMS optimizations only
            /// </summary>
            GridOptOut = 2,
            /// <summary>
            /// The user has opted out of all external optimizations
            /// </summary>
            OptOut = 3,
        }

        /// <summary>
        /// Power Adjust Reason
        /// </summary>
        public enum PowerAdjustReason : byte {
            /// <summary>
            /// There is no Power Adjustment active
            /// </summary>
            NoAdjustment = 0,
            /// <summary>
            /// There is PowerAdjustment active due to local EMS optimization
            /// </summary>
            LocalOptimizationAdjustment = 1,
            /// <summary>
            /// There is PowerAdjustment active due to local EMS optimization
            /// </summary>
            GridOptimizationAdjustment = 2,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Cost
        /// </summary>
        public record Cost : TLVPayload {
            /// <summary>
            /// Cost
            /// </summary>
            public Cost() { }

            /// <summary>
            /// Cost
            /// </summary>
            [SetsRequiredMembers]
            public Cost(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CostType = (CostType)reader.GetUShort(0)!.Value;
                Value = reader.GetInt(1)!.Value;
                DecimalPoints = reader.GetByte(2)!.Value;
                Currency = reader.GetUShort(3, true);
            }
            public required CostType CostType { get; set; }
            public required int Value { get; set; } = 0;
            public required byte DecimalPoints { get; set; } = 0;
            public ushort? Currency { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)CostType);
                writer.WriteInt(1, Value);
                writer.WriteByte(2, DecimalPoints);
                if (Currency != null)
                    writer.WriteUShort(3, Currency, 999);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Power Adjust Capability
        /// </summary>
        public record PowerAdjustCapability : TLVPayload {
            /// <summary>
            /// Power Adjust Capability
            /// </summary>
            public PowerAdjustCapability() { }

            /// <summary>
            /// Power Adjust Capability
            /// </summary>
            [SetsRequiredMembers]
            public PowerAdjustCapability(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                {
                    PowerAdjustCapabilityField = new PowerAdjust[reader.GetStruct(0)!.Length];
                    for (int n = 0; n < PowerAdjustCapabilityField.Length; n++) {
                        PowerAdjustCapabilityField[n] = new PowerAdjust((object[])((object[])fields[0])[n]);
                    }
                }
                Cause = (PowerAdjustReason)reader.GetUShort(1)!.Value;
            }
            public required PowerAdjust[]? PowerAdjustCapabilityField { get; set; }
            public required PowerAdjustReason Cause { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (PowerAdjustCapabilityField != null)
                {
                    Constrain(PowerAdjustCapabilityField, 0, 8);
                    writer.StartArray(0);
                    foreach (var item in PowerAdjustCapabilityField) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                else
                    writer.WriteNull(0);
                writer.WriteUShort(1, (ushort)Cause);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Power Adjust
        /// </summary>
        public record PowerAdjust : TLVPayload {
            /// <summary>
            /// Power Adjust
            /// </summary>
            public PowerAdjust() { }

            /// <summary>
            /// Power Adjust
            /// </summary>
            [SetsRequiredMembers]
            public PowerAdjust(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MinPower = reader.GetLong(0)!.Value;
                MaxPower = reader.GetLong(1)!.Value;
                MinDuration = TimeUtil.FromSeconds(reader.GetUInt(2))!.Value;
                MaxDuration = TimeUtil.FromSeconds(reader.GetUInt(3))!.Value;
            }
            public required long MinPower { get; set; } = 0;
            public required long MaxPower { get; set; } = 0;
            public required TimeSpan MinDuration { get; set; } = TimeSpan.FromSeconds(0);
            public required TimeSpan MaxDuration { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteLong(0, MinPower);
                writer.WriteLong(1, MaxPower);
                writer.WriteUInt(2, (uint)MinDuration.TotalSeconds);
                writer.WriteUInt(3, (uint)MaxDuration.TotalSeconds);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Forecast
        /// </summary>
        public record ForecastStruct : TLVPayload {
            /// <summary>
            /// Forecast
            /// </summary>
            public ForecastStruct() { }

            /// <summary>
            /// Forecast
            /// </summary>
            [SetsRequiredMembers]
            public ForecastStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ForecastID = reader.GetUInt(0)!.Value;
                ActiveSlotNumber = reader.GetUShort(1, true);
                StartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(2))!.Value;
                EndTime = TimeUtil.FromEpochSeconds(reader.GetUInt(3))!.Value;
                EarliestStartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(4, true));
                LatestEndTime = TimeUtil.FromEpochSeconds(reader.GetUInt(5, true));
                IsPausable = reader.GetBool(6)!.Value;
                {
                    Slots = new Slot[reader.GetStruct(7)!.Length];
                    for (int n = 0; n < Slots.Length; n++) {
                        Slots[n] = new Slot((object[])((object[])fields[7])[n]);
                    }
                }
                ForecastUpdateReason = (ForecastUpdateReason)reader.GetUShort(8)!.Value;
            }
            public required uint ForecastID { get; set; } = 0;
            public required ushort? ActiveSlotNumber { get; set; } = 0;
            public required DateTime StartTime { get; set; }
            public required DateTime EndTime { get; set; }
            public DateTime? EarliestStartTime { get; set; }
            public DateTime? LatestEndTime { get; set; }
            public required bool IsPausable { get; set; }
            public required Slot[] Slots { get; set; }
            public required ForecastUpdateReason ForecastUpdateReason { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, ForecastID);
                writer.WriteUShort(1, ActiveSlotNumber);
                writer.WriteUInt(2, TimeUtil.ToEpochSeconds(StartTime));
                writer.WriteUInt(3, TimeUtil.ToEpochSeconds(EndTime));
                if (EarliestStartTime != null)
                    writer.WriteUInt(4, TimeUtil.ToEpochSeconds(EarliestStartTime!.Value));
                if (LatestEndTime != null)
                    writer.WriteUInt(5, TimeUtil.ToEpochSeconds(LatestEndTime!.Value));
                writer.WriteBool(6, IsPausable);
                {
                    Constrain(Slots, 0, 10);
                    writer.StartArray(7);
                    foreach (var item in Slots) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.WriteUShort(8, (ushort)ForecastUpdateReason);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Slot
        /// </summary>
        public record Slot : TLVPayload {
            /// <summary>
            /// Slot
            /// </summary>
            public Slot() { }

            /// <summary>
            /// Slot
            /// </summary>
            [SetsRequiredMembers]
            public Slot(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MinDuration = TimeUtil.FromSeconds(reader.GetUInt(0))!.Value;
                MaxDuration = TimeUtil.FromSeconds(reader.GetUInt(1))!.Value;
                DefaultDuration = TimeUtil.FromSeconds(reader.GetUInt(2))!.Value;
                ElapsedSlotTime = TimeUtil.FromSeconds(reader.GetUInt(3))!.Value;
                RemainingSlotTime = TimeUtil.FromSeconds(reader.GetUInt(4))!.Value;
                SlotIsPausable = reader.GetBool(5, true);
                MinPauseDuration = TimeUtil.FromSeconds(reader.GetUInt(6, true));
                MaxPauseDuration = TimeUtil.FromSeconds(reader.GetUInt(7, true));
                ManufacturerESAState = reader.GetUShort(8, true);
                NominalPower = reader.GetLong(9, true);
                MinPower = reader.GetLong(10, true);
                MaxPower = reader.GetLong(11, true);
                NominalEnergy = reader.GetLong(12, true);
                {
                    Costs = new Cost[reader.GetStruct(13)!.Length];
                    for (int n = 0; n < Costs.Length; n++) {
                        Costs[n] = new Cost((object[])((object[])fields[13])[n]);
                    }
                }
                MinPowerAdjustment = reader.GetLong(14, true);
                MaxPowerAdjustment = reader.GetLong(15, true);
                MinDurationAdjustment = TimeUtil.FromSeconds(reader.GetUInt(16, true));
                MaxDurationAdjustment = TimeUtil.FromSeconds(reader.GetUInt(17, true));
            }
            public required TimeSpan MinDuration { get; set; }
            public required TimeSpan MaxDuration { get; set; }
            public required TimeSpan DefaultDuration { get; set; }
            public required TimeSpan ElapsedSlotTime { get; set; }
            public required TimeSpan RemainingSlotTime { get; set; }
            public bool? SlotIsPausable { get; set; }
            public TimeSpan? MinPauseDuration { get; set; }
            public TimeSpan? MaxPauseDuration { get; set; }
            public ushort? ManufacturerESAState { get; set; }
            public long? NominalPower { get; set; }
            public long? MinPower { get; set; }
            public long? MaxPower { get; set; }
            public long? NominalEnergy { get; set; }
            public Cost[]? Costs { get; set; }
            public long? MinPowerAdjustment { get; set; }
            public long? MaxPowerAdjustment { get; set; }
            public TimeSpan? MinDurationAdjustment { get; set; }
            public TimeSpan? MaxDurationAdjustment { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)MinDuration.TotalSeconds);
                writer.WriteUInt(1, (uint)MaxDuration.TotalSeconds);
                writer.WriteUInt(2, (uint)DefaultDuration.TotalSeconds);
                writer.WriteUInt(3, (uint)ElapsedSlotTime.TotalSeconds);
                writer.WriteUInt(4, (uint)RemainingSlotTime.TotalSeconds);
                if (SlotIsPausable != null)
                    writer.WriteBool(5, SlotIsPausable);
                if (MinPauseDuration != null)
                    writer.WriteUInt(6, (uint)MinPauseDuration!.Value.TotalSeconds);
                if (MaxPauseDuration != null)
                    writer.WriteUInt(7, (uint)MaxPauseDuration!.Value.TotalSeconds);
                if (ManufacturerESAState != null)
                    writer.WriteUShort(8, ManufacturerESAState);
                if (NominalPower != null)
                    writer.WriteLong(9, NominalPower);
                if (MinPower != null)
                    writer.WriteLong(10, MinPower);
                if (MaxPower != null)
                    writer.WriteLong(11, MaxPower);
                if (NominalEnergy != null)
                    writer.WriteLong(12, NominalEnergy);
                if (Costs != null)
                {
                    Constrain(Costs, 0, 5);
                    writer.StartArray(13);
                    foreach (var item in Costs) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                if (MinPowerAdjustment != null)
                    writer.WriteLong(14, MinPowerAdjustment);
                if (MaxPowerAdjustment != null)
                    writer.WriteLong(15, MaxPowerAdjustment);
                if (MinDurationAdjustment != null)
                    writer.WriteUInt(16, (uint)MinDurationAdjustment!.Value.TotalSeconds);
                if (MaxDurationAdjustment != null)
                    writer.WriteUInt(17, (uint)MaxDurationAdjustment!.Value.TotalSeconds);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Slot Adjustment
        /// </summary>
        public record SlotAdjustment : TLVPayload {
            /// <summary>
            /// Slot Adjustment
            /// </summary>
            public SlotAdjustment() { }

            /// <summary>
            /// Slot Adjustment
            /// </summary>
            [SetsRequiredMembers]
            public SlotAdjustment(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                SlotIndex = reader.GetByte(0)!.Value;
                NominalPower = reader.GetLong(1, true);
                Duration = TimeUtil.FromSeconds(reader.GetUInt(2))!.Value;
            }
            public required byte SlotIndex { get; set; }
            public long? NominalPower { get; set; }
            public required TimeSpan Duration { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, SlotIndex);
                if (NominalPower != null)
                    writer.WriteLong(1, NominalPower);
                writer.WriteUInt(2, (uint)Duration.TotalSeconds);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Constraints
        /// </summary>
        public record Constraints : TLVPayload {
            /// <summary>
            /// Constraints
            /// </summary>
            public Constraints() { }

            /// <summary>
            /// Constraints
            /// </summary>
            [SetsRequiredMembers]
            public Constraints(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                StartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(0))!.Value;
                Duration = TimeUtil.FromSeconds(reader.GetUInt(1))!.Value;
                NominalPower = reader.GetLong(2, true);
                MaximumEnergy = reader.GetLong(3, true);
                LoadControl = reader.GetSByte(4, true);
            }
            public required DateTime StartTime { get; set; }
            public required TimeSpan Duration { get; set; }
            public long? NominalPower { get; set; }
            public long? MaximumEnergy { get; set; }
            public sbyte? LoadControl { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, TimeUtil.ToEpochSeconds(StartTime));
                writer.WriteUInt(1, (uint)Duration.TotalSeconds, 86400);
                if (NominalPower != null)
                    writer.WriteLong(2, NominalPower);
                if (MaximumEnergy != null)
                    writer.WriteLong(3, MaximumEnergy);
                if (LoadControl != null)
                    writer.WriteSByte(4, LoadControl);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record PowerAdjustRequestPayload : TLVPayload {
            public required long Power { get; set; }
            public required TimeSpan Duration { get; set; }
            public required AdjustmentCause Cause { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteLong(0, Power);
                writer.WriteUInt(1, (uint)Duration.TotalSeconds);
                writer.WriteUShort(2, (ushort)Cause);
                writer.EndContainer();
            }
        }

        private record StartTimeAdjustRequestPayload : TLVPayload {
            public required DateTime RequestedStartTime { get; set; }
            public required AdjustmentCause Cause { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, TimeUtil.ToEpochSeconds(RequestedStartTime));
                writer.WriteUShort(1, (ushort)Cause);
                writer.EndContainer();
            }
        }

        private record PauseRequestPayload : TLVPayload {
            public required TimeSpan Duration { get; set; }
            public required AdjustmentCause Cause { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)Duration.TotalSeconds);
                writer.WriteUShort(1, (ushort)Cause);
                writer.EndContainer();
            }
        }

        private record ModifyForecastRequestPayload : TLVPayload {
            public required uint ForecastID { get; set; }
            public required SlotAdjustment[] SlotAdjustments { get; set; }
            public required AdjustmentCause Cause { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, ForecastID);
                {
                    Constrain(SlotAdjustments, 0, 10);
                    writer.StartArray(1);
                    foreach (var item in SlotAdjustments) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.WriteUShort(2, (ushort)Cause);
                writer.EndContainer();
            }
        }

        private record RequestConstraintBasedForecastPayload : TLVPayload {
            public required Constraints[] Constraints { get; set; }
            public required AdjustmentCause Cause { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    Constrain(Constraints, 0, 10);
                    writer.StartArray(0);
                    foreach (var item in Constraints) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.WriteUShort(1, (ushort)Cause);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Power Adjust Request
        /// </summary>
        public async Task<bool> PowerAdjustRequest(SecureSession session, long power, TimeSpan duration, AdjustmentCause cause, CancellationToken token = default) {
            PowerAdjustRequestPayload requestFields = new PowerAdjustRequestPayload() {
                Power = power,
                Duration = duration,
                Cause = cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Power Adjust Request
        /// </summary>
        public async Task<bool> CancelPowerAdjustRequest(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Start Time Adjust Request
        /// </summary>
        public async Task<bool> StartTimeAdjustRequest(SecureSession session, DateTime requestedStartTime, AdjustmentCause cause, CancellationToken token = default) {
            StartTimeAdjustRequestPayload requestFields = new StartTimeAdjustRequestPayload() {
                RequestedStartTime = requestedStartTime,
                Cause = cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Pause Request
        /// </summary>
        public async Task<bool> PauseRequest(SecureSession session, TimeSpan duration, AdjustmentCause cause, CancellationToken token = default) {
            PauseRequestPayload requestFields = new PauseRequestPayload() {
                Duration = duration,
                Cause = cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Resume Request
        /// </summary>
        public async Task<bool> ResumeRequest(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, null, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Modify Forecast Request
        /// </summary>
        public async Task<bool> ModifyForecastRequest(SecureSession session, uint forecastID, SlotAdjustment[] slotAdjustments, AdjustmentCause cause, CancellationToken token = default) {
            ModifyForecastRequestPayload requestFields = new ModifyForecastRequestPayload() {
                ForecastID = forecastID,
                SlotAdjustments = slotAdjustments,
                Cause = cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Request Constraint Based Forecast
        /// </summary>
        public async Task<bool> RequestConstraintBasedForecast(SecureSession session, Constraints[] constraints, AdjustmentCause cause, CancellationToken token = default) {
            RequestConstraintBasedForecastPayload requestFields = new RequestConstraintBasedForecastPayload() {
                Constraints = constraints,
                Cause = cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Request
        /// </summary>
        public async Task<bool> CancelRequest(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, null, token);
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
        /// ESA Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ESATypeEnum> ESAType { get; init; }

        /// <summary>
        /// ESA Can Generate Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> ESACanGenerate { get; init; }

        /// <summary>
        /// ESA State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ESAStateEnum> ESAState { get; init; }

        /// <summary>
        /// Abs Min Power [mW] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long> AbsMinPower { get; init; }

        /// <summary>
        /// Abs Max Power [mW] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<long> AbsMaxPower { get; init; }

        /// <summary>
        /// Power Adjustment Capability Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<PowerAdjustCapability?> PowerAdjustmentCapability { get; init; }

        /// <summary>
        /// Forecast Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ForecastStruct?> Forecast { get; init; }

        /// <summary>
        /// Opt Out State Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<OptOutStateEnum> OptOutState { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Device Energy Management";
        }
    }
}