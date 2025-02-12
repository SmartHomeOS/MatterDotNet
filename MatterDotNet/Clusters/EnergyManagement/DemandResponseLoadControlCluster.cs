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

using MatterDotNet.Attributes;
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
    /// This cluster provides an interface to the functionality of Smart Energy Demand Response and Load Control.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 4)]
    public class DemandResponseLoadControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0096;

        /// <summary>
        /// This cluster provides an interface to the functionality of Smart Energy Demand Response and Load Control.
        /// </summary>
        [SetsRequiredMembers]
        public DemandResponseLoadControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected DemandResponseLoadControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            LoadControlPrograms = new ReadAttribute<LoadControlProgram[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    LoadControlProgram[] list = new LoadControlProgram[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new LoadControlProgram(reader.GetStruct(i)!);
                    return list;
                }
            };
            NumberOfLoadControlPrograms = new ReadAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte?)(dynamic?)x ?? 5

            };
            Events = new ReadAttribute<LoadControlEvent[]>(cluster, endPoint, 2) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    LoadControlEvent[] list = new LoadControlEvent[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new LoadControlEvent(reader.GetStruct(i)!);
                    return list;
                }
            };
            ActiveEvents = new ReadAttribute<LoadControlEvent[]>(cluster, endPoint, 3) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    LoadControlEvent[] list = new LoadControlEvent[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new LoadControlEvent(reader.GetStruct(i)!);
                    return list;
                }
            };
            NumberOfEventsPerProgram = new ReadAttribute<byte>(cluster, endPoint, 4) {
                Deserialize = x => (byte?)(dynamic?)x ?? 10

            };
            NumberOfTransitions = new ReadAttribute<byte>(cluster, endPoint, 5) {
                Deserialize = x => (byte?)(dynamic?)x ?? 3

            };
            DefaultRandomStart = new ReadWriteAttribute<byte>(cluster, endPoint, 6) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x1E

            };
            DefaultRandomDuration = new ReadWriteAttribute<byte>(cluster, endPoint, 7) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0

            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports enrollment groups
            /// </summary>
            EnrollmentGroups = 1,
            /// <summary>
            /// Supports temperature offsets
            /// </summary>
            TemperatureOffset = 2,
            /// <summary>
            /// Supports temperature setpoints
            /// </summary>
            TemperatureSetpoint = 4,
            /// <summary>
            /// Supports average load adjustment percentage
            /// </summary>
            LoadAdjustment = 8,
            /// <summary>
            /// Supports duty cycle adjustment
            /// </summary>
            DutyCycle = 16,
            /// <summary>
            /// Supports power savings
            /// </summary>
            PowerSavings = 32,
            /// <summary>
            /// Supports selecting heating source
            /// </summary>
            HeatingSource = 64,
        }

        /// <summary>
        /// Load Control Event Change Source
        /// </summary>
        public enum LoadControlEventChangeSource : byte {
            Automatic = 0,
            UserAction = 1,
        }

        /// <summary>
        /// Load Control Event Status
        /// </summary>
        public enum LoadControlEventStatus : byte {
            Unknown = 0x0,
            Received = 0x1,
            InProgress = 0x2,
            Completed = 0x3,
            OptedOut = 0x4,
            OptedIn = 0x5,
            Canceled = 0x6,
            Superseded = 0x7,
            PartialOptedOut = 0x8,
            PartialOptedIn = 0x9,
            NoParticipation = 0xA,
            Unavailable = 0xB,
            Failed = 0xC,
        }

        /// <summary>
        /// Criticality Level
        /// </summary>
        public enum CriticalityLevel : byte {
            Unknown = 0,
            Green = 1,
            Level1 = 2,
            Level2 = 3,
            Level3 = 4,
            Level4 = 5,
            Level5 = 6,
            Emergency = 7,
            PlannedOutage = 8,
            ServiceDisconnect = 9,
        }

        /// <summary>
        /// Heating Source
        /// </summary>
        public enum HeatingSource : byte {
            Any = 0,
            Electric = 1,
            NonElectric = 2,
        }

        /// <summary>
        /// Device Class
        /// </summary>
        [Flags]
        public enum DeviceClass : uint {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            HVAC = 0x0001,
            StripHeater = 0x0002,
            WaterHeater = 0x0004,
            PoolPump = 0x0008,
            SmartAppliance = 0x0010,
            IrrigationPump = 0x0020,
            CommercialLoad = 0x0040,
            ResidentialLoad = 0x0080,
            ExteriorLighting = 0x0100,
            InteriorLighting = 0x0200,
            EV = 0x0400,
            GenerationSystem = 0x0800,
            SmartInverter = 0x1000,
            EVSE = 0x2000,
            RESU = 0x4000,
            EMS = 0x8000,
            SEM = 0x10000,
        }

        /// <summary>
        /// Event Control
        /// </summary>
        [Flags]
        public enum EventControl : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            RandomStart = 0x0001,
        }

        /// <summary>
        /// Event Transition Control
        /// </summary>
        [Flags]
        public enum EventTransitionControl : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            RandomDuration = 0x0001,
            IgnoreOptOut = 0x0002,
        }

        /// <summary>
        /// Cancel Control
        /// </summary>
        [Flags]
        public enum CancelControl : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            RandomEnd = 0x0001,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Temperature Control
        /// </summary>
        public record TemperatureControl : TLVPayload {
            /// <summary>
            /// Temperature Control
            /// </summary>
            public TemperatureControl() { }

            /// <summary>
            /// Temperature Control
            /// </summary>
            [SetsRequiredMembers]
            public TemperatureControl(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CoolingTempOffset = reader.GetUShort(0, true);
                HeatingtTempOffset = reader.GetUShort(1, true);
                CoolingTempSetpoint = reader.GetDecimal(2, true);
                HeatingTempSetpoint = reader.GetDecimal(3, true);
            }
            public ushort? CoolingTempOffset { get; set; }
            public ushort? HeatingtTempOffset { get; set; }
            public decimal? CoolingTempSetpoint { get; set; }
            public decimal? HeatingTempSetpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (CoolingTempOffset != null)
                    writer.WriteUShort(0, CoolingTempOffset);
                if (HeatingtTempOffset != null)
                    writer.WriteUShort(1, HeatingtTempOffset);
                if (CoolingTempSetpoint != null)
                    writer.WriteDecimal(2, CoolingTempSetpoint);
                if (HeatingTempSetpoint != null)
                    writer.WriteDecimal(3, HeatingTempSetpoint);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Average Load Control
        /// </summary>
        public record AverageLoadControl : TLVPayload {
            /// <summary>
            /// Average Load Control
            /// </summary>
            public AverageLoadControl() { }

            /// <summary>
            /// Average Load Control
            /// </summary>
            [SetsRequiredMembers]
            public AverageLoadControl(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LoadAdjustment = reader.GetSByte(0)!.Value;
            }
            public required sbyte LoadAdjustment { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteSByte(0, LoadAdjustment, 100, -100);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Duty Cycle Control
        /// </summary>
        public record DutyCycleControl : TLVPayload {
            /// <summary>
            /// Duty Cycle Control
            /// </summary>
            public DutyCycleControl() { }

            /// <summary>
            /// Duty Cycle Control
            /// </summary>
            [SetsRequiredMembers]
            public DutyCycleControl(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                DutyCycle = reader.GetByte(0)!.Value;
            }
            public required byte DutyCycle { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, DutyCycle);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Power Savings Control
        /// </summary>
        public record PowerSavingsControl : TLVPayload {
            /// <summary>
            /// Power Savings Control
            /// </summary>
            public PowerSavingsControl() { }

            /// <summary>
            /// Power Savings Control
            /// </summary>
            [SetsRequiredMembers]
            public PowerSavingsControl(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                PowerSavings = reader.GetByte(0)!.Value;
            }
            public required byte PowerSavings { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, PowerSavings);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Heating Source Control
        /// </summary>
        public record HeatingSourceControl : TLVPayload {
            /// <summary>
            /// Heating Source Control
            /// </summary>
            public HeatingSourceControl() { }

            /// <summary>
            /// Heating Source Control
            /// </summary>
            [SetsRequiredMembers]
            public HeatingSourceControl(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                HeatingSource = (HeatingSource)reader.GetUShort(0)!.Value;
            }
            public required HeatingSource HeatingSource { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)HeatingSource);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Load Control Event Transition
        /// </summary>
        public record LoadControlEventTransition : TLVPayload {
            /// <summary>
            /// Load Control Event Transition
            /// </summary>
            public LoadControlEventTransition() { }

            /// <summary>
            /// Load Control Event Transition
            /// </summary>
            [SetsRequiredMembers]
            public LoadControlEventTransition(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Duration = reader.GetUShort(0)!.Value;
                Control = (EventTransitionControl)reader.GetUInt(1)!.Value;
                TemperatureControl = new TemperatureControl((object[])fields[2]);
                AverageLoadControl = new AverageLoadControl((object[])fields[3]);
                DutyCycleControl = new DutyCycleControl((object[])fields[4]);
                PowerSavingsControl = new PowerSavingsControl((object[])fields[5]);
                HeatingSourceControl = new HeatingSourceControl((object[])fields[6]);
            }
            public required ushort Duration { get; set; }
            public required EventTransitionControl Control { get; set; }
            public TemperatureControl? TemperatureControl { get; set; }
            public AverageLoadControl? AverageLoadControl { get; set; }
            public DutyCycleControl? DutyCycleControl { get; set; }
            public PowerSavingsControl? PowerSavingsControl { get; set; }
            public HeatingSourceControl? HeatingSourceControl { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, Duration, 1440);
                writer.WriteUInt(1, (uint)Control);
                if (TemperatureControl != null)
                    TemperatureControl.Serialize(writer, 2);
                if (AverageLoadControl != null)
                    AverageLoadControl.Serialize(writer, 3);
                if (DutyCycleControl != null)
                    DutyCycleControl.Serialize(writer, 4);
                if (PowerSavingsControl != null)
                    PowerSavingsControl.Serialize(writer, 5);
                if (HeatingSourceControl != null)
                    HeatingSourceControl.Serialize(writer, 6);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Load Control Event
        /// </summary>
        public record LoadControlEvent : TLVPayload {
            /// <summary>
            /// Load Control Event
            /// </summary>
            public LoadControlEvent() { }

            /// <summary>
            /// Load Control Event
            /// </summary>
            [SetsRequiredMembers]
            public LoadControlEvent(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                EventID = reader.GetBytes(0, false, 16)!;
                ProgramID = reader.GetBytes(1, false, 16)!;
                Control = (EventControl)reader.GetUInt(2)!.Value;
                DeviceClass = (DeviceClass)reader.GetUInt(3)!.Value;
                EnrollmentGroup = reader.GetByte(4, true);
                Criticality = (CriticalityLevel)reader.GetUShort(5)!.Value;
                StartTime = TimeUtil.FromEpochSeconds(reader.GetUInt(6, true));
                {
                    Transitions = new LoadControlEventTransition[reader.GetStruct(7)!.Length];
                    for (int n = 0; n < Transitions.Length; n++) {
                        Transitions[n] = new LoadControlEventTransition((object[])((object[])fields[7])[n]);
                    }
                }
            }
            public required byte[] EventID { get; set; }
            public required byte[]? ProgramID { get; set; }
            public required EventControl Control { get; set; }
            public required DeviceClass DeviceClass { get; set; }
            public byte? EnrollmentGroup { get; set; }
            public required CriticalityLevel Criticality { get; set; }
            public required DateTime? StartTime { get; set; }
            public required LoadControlEventTransition[] Transitions { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, EventID, 16);
                writer.WriteBytes(1, ProgramID, 16);
                writer.WriteUInt(2, (uint)Control);
                writer.WriteUInt(3, (uint)DeviceClass);
                if (EnrollmentGroup != null)
                    writer.WriteByte(4, EnrollmentGroup);
                writer.WriteUShort(5, (ushort)Criticality);
                if (!StartTime.HasValue)
                    writer.WriteNull(6);
                else
                    writer.WriteUInt(6, TimeUtil.ToEpochSeconds(StartTime!.Value));
                {
                    writer.StartArray(7);
                    foreach (var item in Transitions) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Load Control Program
        /// </summary>
        public record LoadControlProgram : TLVPayload {
            /// <summary>
            /// Load Control Program
            /// </summary>
            public LoadControlProgram() { }

            /// <summary>
            /// Load Control Program
            /// </summary>
            [SetsRequiredMembers]
            public LoadControlProgram(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ProgramID = reader.GetBytes(0, false, 16)!;
                Name = reader.GetString(1, false, 32)!;
                EnrollmentGroup = reader.GetByte(2, true);
                RandomStartMinutes = reader.GetByte(3, true);
                RandomDurationMinutes = reader.GetByte(4, true);
            }
            public required byte[] ProgramID { get; set; }
            public required string Name { get; set; }
            public required byte? EnrollmentGroup { get; set; }
            public required byte? RandomStartMinutes { get; set; }
            public required byte? RandomDurationMinutes { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, ProgramID, 16);
                writer.WriteString(1, Name, 32);
                writer.WriteByte(2, EnrollmentGroup, byte.MaxValue, 1);
                writer.WriteByte(3, RandomStartMinutes);
                writer.WriteByte(4, RandomDurationMinutes);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record RegisterLoadControlProgramRequestPayload : TLVPayload {
            public required LoadControlProgram LoadControlProgram { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                LoadControlProgram.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record UnregisterLoadControlProgramRequestPayload : TLVPayload {
            public required byte[] LoadControlProgramID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, LoadControlProgramID, 16);
                writer.EndContainer();
            }
        }

        private record AddLoadControlEventRequestPayload : TLVPayload {
            public required LoadControlEvent @Event { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                @Event.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record RemoveLoadControlEventRequestPayload : TLVPayload {
            public required byte[] EventID { get; set; }
            public required CancelControl CancelControl { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, EventID, 16);
                writer.WriteUInt(1, (uint)CancelControl);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Register Load Control Program Request
        /// </summary>
        public async Task<bool> RegisterLoadControlProgramRequest(SecureSession session, LoadControlProgram loadControlProgram, CancellationToken token = default) {
            RegisterLoadControlProgramRequestPayload requestFields = new RegisterLoadControlProgramRequestPayload() {
                LoadControlProgram = loadControlProgram,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Unregister Load Control Program Request
        /// </summary>
        public async Task<bool> UnregisterLoadControlProgramRequest(SecureSession session, byte[] loadControlProgramID, CancellationToken token = default) {
            UnregisterLoadControlProgramRequestPayload requestFields = new UnregisterLoadControlProgramRequestPayload() {
                LoadControlProgramID = loadControlProgramID,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Add Load Control Event Request
        /// </summary>
        public async Task<bool> AddLoadControlEventRequest(SecureSession session, LoadControlEvent @Event, CancellationToken token = default) {
            AddLoadControlEventRequestPayload requestFields = new AddLoadControlEventRequestPayload() {
                @Event = @Event,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Remove Load Control Event Request
        /// </summary>
        public async Task<bool> RemoveLoadControlEventRequest(SecureSession session, byte[] eventID, CancelControl cancelControl, CancellationToken token = default) {
            RemoveLoadControlEventRequestPayload requestFields = new RemoveLoadControlEventRequestPayload() {
                EventID = eventID,
                CancelControl = cancelControl,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Clear Load Control Events Request
        /// </summary>
        public async Task<bool> ClearLoadControlEventsRequest(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, null, token);
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
        /// Load Control Programs Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<LoadControlProgram[]> LoadControlPrograms { get; init; }

        /// <summary>
        /// Number Of Load Control Programs Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfLoadControlPrograms { get; init; }

        /// <summary>
        /// Events Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<LoadControlEvent[]> Events { get; init; }

        /// <summary>
        /// Active Events Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<LoadControlEvent[]> ActiveEvents { get; init; }

        /// <summary>
        /// Number Of Events Per Program Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfEventsPerProgram { get; init; }

        /// <summary>
        /// Number Of Transitions Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> NumberOfTransitions { get; init; }

        /// <summary>
        /// Default Random Start Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> DefaultRandomStart { get; init; }

        /// <summary>
        /// Default Random Duration Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte> DefaultRandomDuration { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Demand Response Load Control";
        }
    }
}