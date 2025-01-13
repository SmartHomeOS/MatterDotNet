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
    /// Device Energy Management Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class DeviceEnergyManagementCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0098;

        /// <summary>
        /// Device Energy Management Cluster
        /// </summary>
        public DeviceEnergyManagementCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected DeviceEnergyManagementCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        /// Adjustment Cause
        /// </summary>
        public enum AdjustmentCauseEnum {
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
        /// Cause
        /// </summary>
        public enum CauseEnum {
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
        /// Cost Type
        /// </summary>
        public enum CostTypeEnum {
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
        /// ESA State
        /// </summary>
        public enum ESAStateEnum {
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
        /// ESA Type
        /// </summary>
        public enum ESATypeEnum {
            /// <summary>
            /// EV Supply Equipment
            /// </summary>
            EVSE = 0,
            /// <summary>
            /// Space heating appliance
            /// </summary>
            SpaceHeating = 1,
            /// <summary>
            /// Water heating appliance
            /// </summary>
            WaterHeating = 2,
            /// <summary>
            /// Space cooling appliance
            /// </summary>
            SpaceCooling = 3,
            /// <summary>
            /// Space heating and cooling appliance
            /// </summary>
            SpaceHeatingCooling = 4,
            /// <summary>
            /// Battery Electric Storage System
            /// </summary>
            BatteryStorage = 5,
            /// <summary>
            /// Solar PV inverter
            /// </summary>
            SolarPV = 6,
            /// <summary>
            /// Fridge / Freezer
            /// </summary>
            FridgeFreezer = 7,
            /// <summary>
            /// Washing Machine
            /// </summary>
            WashingMachine = 8,
            /// <summary>
            /// Dishwasher
            /// </summary>
            Dishwasher = 9,
            /// <summary>
            /// Cooking appliance
            /// </summary>
            Cooking = 10,
            /// <summary>
            /// Home water pump (e.g. drinking well)
            /// </summary>
            HomeWaterPump = 11,
            /// <summary>
            /// Irrigation water pump
            /// </summary>
            IrrigationWaterPump = 12,
            /// <summary>
            /// Pool pump
            /// </summary>
            PoolPump = 13,
            /// <summary>
            /// Other appliance type
            /// </summary>
            Other = 255,
        }

        /// <summary>
        /// Forecast Update Reason
        /// </summary>
        public enum ForecastUpdateReasonEnum {
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
        public enum OptOutStateEnum {
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
        public enum PowerAdjustReasonEnum {
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
            public required long? NominalPower { get; set; }
            public required long? MaximumEnergy { get; set; }
            public required sbyte? LoadControl { get; set; }
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
                CostType = (CostTypeEnum)reader.GetUShort(0)!.Value;
                Value = reader.GetInt(1)!.Value;
                DecimalPoints = reader.GetByte(2)!.Value;
                Currency = reader.GetUShort(3, true);
            }
            public required CostTypeEnum CostType { get; set; }
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
        /// Forecast
        /// </summary>
        public record Forecast : TLVPayload {
            /// <summary>
            /// Forecast
            /// </summary>
            public Forecast() { }

            /// <summary>
            /// Forecast
            /// </summary>
            [SetsRequiredMembers]
            public Forecast(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ForecastID = reader.GetUInt(0)!.Value;
                ActiveSlotNumber = reader.GetUShort(1, true);
                StartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(2))!.Value;
                EndTime = TimeUtil.FromEpochSeconds(reader.GetUInt(3))!.Value;
                EarliestStartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(4, true));
                LatestEndTime = TimeUtil.FromEpochSeconds(reader.GetUInt(5, true));
                IsPausable = reader.GetBool(6)!.Value;
                {
                    Slots = new Slot[((object[])fields[7]).Length];
                    for (int i = 0; i < Slots.Length; i++) {
                        Slots[i] = new Slot((object[])fields[-1]);
                    }
                }
                ForecastUpdateReason = (ForecastUpdateReasonEnum)reader.GetUShort(8)!.Value;
            }
            public required uint ForecastID { get; set; } = 0;
            public required ushort? ActiveSlotNumber { get; set; } = 0;
            public required DateTime StartTime { get; set; }
            public required DateTime EndTime { get; set; }
            public required DateTime? EarliestStartTime { get; set; }
            public required DateTime? LatestEndTime { get; set; }
            public required bool IsPausable { get; set; }
            public required Slot[] Slots { get; set; }
            public required ForecastUpdateReasonEnum ForecastUpdateReason { get; set; }
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
                    PowerAdjustCapabilityField = new PowerAdjust[((object[])fields[0]).Length];
                    for (int i = 0; i < PowerAdjustCapabilityField.Length; i++) {
                        PowerAdjustCapabilityField[i] = new PowerAdjust((object[])fields[-1]);
                    }
                }
                Cause = (PowerAdjustReasonEnum)reader.GetUShort(1)!.Value;
            }
            public required PowerAdjust[]? PowerAdjustCapabilityField { get; set; } = null;
            public required PowerAdjustReasonEnum Cause { get; set; }
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
            public required long? NominalPower { get; set; }
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
                    Costs = new Cost[((object[])fields[13]).Length];
                    for (int i = 0; i < Costs.Length; i++) {
                        Costs[i] = new Cost((object[])fields[-1]);
                    }
                }
                MinPowerAdjustment = reader.GetLong(14)!.Value;
                MaxPowerAdjustment = reader.GetLong(15)!.Value;
                MinDurationAdjustment = TimeUtil.FromSeconds(reader.GetUInt(16, true));
                MaxDurationAdjustment = TimeUtil.FromSeconds(reader.GetUInt(17, true));
            }
            public required TimeSpan MinDuration { get; set; }
            public required TimeSpan MaxDuration { get; set; }
            public required TimeSpan DefaultDuration { get; set; }
            public required TimeSpan ElapsedSlotTime { get; set; }
            public required TimeSpan RemainingSlotTime { get; set; }
            public required bool? SlotIsPausable { get; set; }
            public required TimeSpan? MinPauseDuration { get; set; }
            public required TimeSpan? MaxPauseDuration { get; set; }
            public required ushort? ManufacturerESAState { get; set; }
            public required long? NominalPower { get; set; }
            public required long? MinPower { get; set; }
            public required long? MaxPower { get; set; }
            public required long? NominalEnergy { get; set; }
            public Cost[]? Costs { get; set; }
            public required long MinPowerAdjustment { get; set; }
            public required long MaxPowerAdjustment { get; set; }
            public required TimeSpan? MinDurationAdjustment { get; set; }
            public required TimeSpan? MaxDurationAdjustment { get; set; }
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
                writer.WriteLong(14, MinPowerAdjustment);
                writer.WriteLong(15, MaxPowerAdjustment);
                if (MinDurationAdjustment != null)
                    writer.WriteUInt(16, (uint)MinDurationAdjustment!.Value.TotalSeconds);
                if (MaxDurationAdjustment != null)
                    writer.WriteUInt(17, (uint)MaxDurationAdjustment!.Value.TotalSeconds);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record PowerAdjustRequestPayload : TLVPayload {
            public required long Power { get; set; }
            public required TimeSpan Duration { get; set; }
            public required AdjustmentCauseEnum Cause { get; set; }
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
            public required AdjustmentCauseEnum Cause { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, TimeUtil.ToEpochSeconds(RequestedStartTime));
                writer.WriteUShort(1, (ushort)Cause);
                writer.EndContainer();
            }
        }

        private record PauseRequestPayload : TLVPayload {
            public required TimeSpan Duration { get; set; }
            public required AdjustmentCauseEnum Cause { get; set; }
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
            public required AdjustmentCauseEnum Cause { get; set; }
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
            public required AdjustmentCauseEnum Cause { get; set; }
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
        public async Task<bool> PowerAdjustRequest(SecureSession session, long Power, TimeSpan Duration, AdjustmentCauseEnum Cause) {
            PowerAdjustRequestPayload requestFields = new PowerAdjustRequestPayload() {
                Power = Power,
                Duration = Duration,
                Cause = Cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Power Adjust Request
        /// </summary>
        public async Task<bool> CancelPowerAdjustRequest(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Start Time Adjust Request
        /// </summary>
        public async Task<bool> StartTimeAdjustRequest(SecureSession session, DateTime RequestedStartTime, AdjustmentCauseEnum Cause) {
            StartTimeAdjustRequestPayload requestFields = new StartTimeAdjustRequestPayload() {
                RequestedStartTime = RequestedStartTime,
                Cause = Cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Pause Request
        /// </summary>
        public async Task<bool> PauseRequest(SecureSession session, TimeSpan Duration, AdjustmentCauseEnum Cause) {
            PauseRequestPayload requestFields = new PauseRequestPayload() {
                Duration = Duration,
                Cause = Cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Resume Request
        /// </summary>
        public async Task<bool> ResumeRequest(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Modify Forecast Request
        /// </summary>
        public async Task<bool> ModifyForecastRequest(SecureSession session, uint ForecastID, SlotAdjustment[] SlotAdjustments, AdjustmentCauseEnum Cause) {
            ModifyForecastRequestPayload requestFields = new ModifyForecastRequestPayload() {
                ForecastID = ForecastID,
                SlotAdjustments = SlotAdjustments,
                Cause = Cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Request Constraint Based Forecast
        /// </summary>
        public async Task<bool> RequestConstraintBasedForecast(SecureSession session, Constraints[] Constraints, AdjustmentCauseEnum Cause) {
            RequestConstraintBasedForecastPayload requestFields = new RequestConstraintBasedForecastPayload() {
                Constraints = Constraints,
                Cause = Cause,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Cancel Request
        /// </summary>
        public async Task<bool> CancelRequest(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07);
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
        /// Get the ESA Type attribute
        /// </summary>
        public async Task<ESATypeEnum> GetESAType(SecureSession session) {
            return (ESATypeEnum?)await GetEnumAttribute(session, 0) ?? ESATypeEnum.Other;
        }

        /// <summary>
        /// Get the ESA Can Generate attribute
        /// </summary>
        public async Task<bool> GetESACanGenerate(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 1) ?? false;
        }

        /// <summary>
        /// Get the ESA State attribute
        /// </summary>
        public async Task<ESAStateEnum> GetESAState(SecureSession session) {
            return (ESAStateEnum)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the Abs Min Power attribute
        /// </summary>
        public async Task<long> GetAbsMinPower(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 3) ?? 0;
        }

        /// <summary>
        /// Get the Abs Max Power attribute
        /// </summary>
        public async Task<long> GetAbsMaxPower(SecureSession session) {
            return (long?)(dynamic?)await GetAttribute(session, 4) ?? 0;
        }

        /// <summary>
        /// Get the Power Adjustment Capability attribute
        /// </summary>
        public async Task<PowerAdjustCapability?> GetPowerAdjustmentCapability(SecureSession session) {
            return new PowerAdjustCapability((object[])(await GetAttribute(session, 5))!) ?? null;
        }

        /// <summary>
        /// Get the Forecast attribute
        /// </summary>
        public async Task<Forecast?> GetForecast(SecureSession session) {
            return new Forecast((object[])(await GetAttribute(session, 6))!) ?? null;
        }

        /// <summary>
        /// Get the Opt Out State attribute
        /// </summary>
        public async Task<OptOutStateEnum> GetOptOutState(SecureSession session) {
            return (OptOutStateEnum)await GetEnumAttribute(session, 7);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Device Energy Management Cluster";
        }
    }
}