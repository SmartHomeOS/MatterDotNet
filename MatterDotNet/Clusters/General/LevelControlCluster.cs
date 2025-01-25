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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// Attributes and commands for controlling devices that can be set to a level between fully 'On' and fully 'Off.'
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 6)]
    public class LevelControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0008;

        /// <summary>
        /// Attributes and commands for controlling devices that can be set to a level between fully 'On' and fully 'Off.'
        /// </summary>
        [SetsRequiredMembers]
        public LevelControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected LevelControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            CurrentLevel = new ReadAttribute<byte?>(cluster, endPoint, 0, true) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x00

            };
            RemainingTime = new ReadAttribute<ushort>(cluster, endPoint, 1) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            MinLevel = new ReadAttribute<byte>(cluster, endPoint, 2) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x00

            };
            MaxLevel = new ReadAttribute<byte>(cluster, endPoint, 3) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0xFE

            };
            CurrentFrequency = new ReadAttribute<ushort>(cluster, endPoint, 4) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            MinFrequency = new ReadAttribute<ushort>(cluster, endPoint, 5) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            MaxFrequency = new ReadAttribute<ushort>(cluster, endPoint, 6) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            OnOffTransitionTime = new ReadWriteAttribute<ushort>(cluster, endPoint, 16) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            OnLevel = new ReadWriteAttribute<byte?>(cluster, endPoint, 17, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            OnTransitionTime = new ReadWriteAttribute<ushort?>(cluster, endPoint, 18, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            OffTransitionTime = new ReadWriteAttribute<ushort?>(cluster, endPoint, 19, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            DefaultMoveRate = new ReadWriteAttribute<byte?>(cluster, endPoint, 20, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            Options = new ReadWriteAttribute<OptionsBitmap>(cluster, endPoint, 15) {
                Deserialize = x => (OptionsBitmap)DeserializeEnum(x)!
            };
            StartUpCurrentLevel = new ReadWriteAttribute<byte?>(cluster, endPoint, 16384, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Dependency with the On/Off cluster
            /// </summary>
            OnOff = 1,
            /// <summary>
            /// Behavior that supports lighting applications
            /// </summary>
            Lighting = 2,
            /// <summary>
            /// Supports frequency attributes and behavior. The Pulse Width Modulation cluster was created for frequency control.
            /// </summary>
            Frequency = 4,
        }

        /// <summary>
        /// Move Mode
        /// </summary>
        public enum MoveMode : byte {
            /// <summary>
            /// Increase the level
            /// </summary>
            Up = 0,
            /// <summary>
            /// Decrease the level
            /// </summary>
            Down = 1,
        }

        /// <summary>
        /// Step Mode
        /// </summary>
        public enum StepMode : byte {
            /// <summary>
            /// Step upwards
            /// </summary>
            Up = 0,
            /// <summary>
            /// Step downwards
            /// </summary>
            Down = 1,
        }

        /// <summary>
        /// Options
        /// </summary>
        [Flags]
        public enum OptionsBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Dependency on On/Off cluster
            /// </summary>
            ExecuteIfOff = 0x01,
            /// <summary>
            /// Dependency on Color Control cluster
            /// </summary>
            CoupleColorTempToLevel = 0x02,
        }
        #endregion Enums

        #region Payloads
        private record MoveToLevelPayload : TLVPayload {
            public required byte Level { get; set; }
            public required ushort? TransitionTime { get; set; }
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Level);
                writer.WriteUShort(1, TransitionTime);
                writer.WriteUInt(2, (uint)OptionsMask);
                writer.WriteUInt(3, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MovePayload : TLVPayload {
            public required MoveMode MoveMode { get; set; }
            public required byte? Rate { get; set; }
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)MoveMode);
                writer.WriteByte(1, Rate);
                writer.WriteUInt(2, (uint)OptionsMask);
                writer.WriteUInt(3, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StepPayload : TLVPayload {
            public required StepMode StepMode { get; set; }
            public required byte StepSize { get; set; }
            public required ushort? TransitionTime { get; set; }
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)StepMode);
                writer.WriteByte(1, StepSize);
                writer.WriteUShort(2, TransitionTime);
                writer.WriteUInt(3, (uint)OptionsMask);
                writer.WriteUInt(4, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StopPayload : TLVPayload {
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)OptionsMask);
                writer.WriteUInt(1, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToLevelWithOnOffPayload : TLVPayload {
            public required byte Level { get; set; }
            public required ushort? TransitionTime { get; set; }
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Level);
                writer.WriteUShort(1, TransitionTime);
                writer.WriteUInt(2, (uint)OptionsMask);
                writer.WriteUInt(3, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveWithOnOffPayload : TLVPayload {
            public required MoveMode MoveMode { get; set; }
            public required byte? Rate { get; set; }
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)MoveMode);
                writer.WriteByte(1, Rate);
                writer.WriteUInt(2, (uint)OptionsMask);
                writer.WriteUInt(3, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StepWithOnOffPayload : TLVPayload {
            public required StepMode StepMode { get; set; }
            public required byte StepSize { get; set; }
            public required ushort? TransitionTime { get; set; }
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)StepMode);
                writer.WriteByte(1, StepSize);
                writer.WriteUShort(2, TransitionTime);
                writer.WriteUInt(3, (uint)OptionsMask);
                writer.WriteUInt(4, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StopWithOnOffPayload : TLVPayload {
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)OptionsMask);
                writer.WriteUInt(1, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToClosestFrequencyPayload : TLVPayload {
            public required ushort Frequency { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, Frequency);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Move To Level
        /// </summary>
        public async Task<bool> MoveToLevel(SecureSession session, byte level, ushort? transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride) {
            MoveToLevelPayload requestFields = new MoveToLevelPayload() {
                Level = level,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move
        /// </summary>
        public async Task<bool> Move(SecureSession session, MoveMode moveMode, byte? rate, OptionsBitmap optionsMask, OptionsBitmap optionsOverride) {
            MovePayload requestFields = new MovePayload() {
                MoveMode = moveMode,
                Rate = rate,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step
        /// </summary>
        public async Task<bool> Step(SecureSession session, StepMode stepMode, byte stepSize, ushort? transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride) {
            StepPayload requestFields = new StepPayload() {
                StepMode = stepMode,
                StepSize = stepSize,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Stop
        /// </summary>
        public async Task<bool> Stop(SecureSession session, OptionsBitmap optionsMask, OptionsBitmap optionsOverride) {
            StopPayload requestFields = new StopPayload() {
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Level With On Off
        /// </summary>
        public async Task<bool> MoveToLevelWithOnOff(SecureSession session, byte level, ushort? transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride) {
            MoveToLevelWithOnOffPayload requestFields = new MoveToLevelWithOnOffPayload() {
                Level = level,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move With On Off
        /// </summary>
        public async Task<bool> MoveWithOnOff(SecureSession session, MoveMode moveMode, byte? rate, OptionsBitmap optionsMask, OptionsBitmap optionsOverride) {
            MoveWithOnOffPayload requestFields = new MoveWithOnOffPayload() {
                MoveMode = moveMode,
                Rate = rate,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step With On Off
        /// </summary>
        public async Task<bool> StepWithOnOff(SecureSession session, StepMode stepMode, byte stepSize, ushort? transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride) {
            StepWithOnOffPayload requestFields = new StepWithOnOffPayload() {
                StepMode = stepMode,
                StepSize = stepSize,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Stop With On Off
        /// </summary>
        public async Task<bool> StopWithOnOff(SecureSession session, OptionsBitmap optionsMask, OptionsBitmap optionsOverride) {
            StopWithOnOffPayload requestFields = new StopWithOnOffPayload() {
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Closest Frequency
        /// </summary>
        public async Task<bool> MoveToClosestFrequency(SecureSession session, ushort frequency) {
            MoveToClosestFrequencyPayload requestFields = new MoveToClosestFrequencyPayload() {
                Frequency = frequency,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x08, requestFields);
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
        /// Current Level Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> CurrentLevel { get; init; }

        /// <summary>
        /// Remaining Time Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> RemainingTime { get; init; }

        /// <summary>
        /// Min Level Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> MinLevel { get; init; }

        /// <summary>
        /// Max Level Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> MaxLevel { get; init; }

        /// <summary>
        /// Current Frequency Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> CurrentFrequency { get; init; }

        /// <summary>
        /// Min Frequency Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> MinFrequency { get; init; }

        /// <summary>
        /// Max Frequency Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> MaxFrequency { get; init; }

        /// <summary>
        /// On Off Transition Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> OnOffTransitionTime { get; init; }

        /// <summary>
        /// On Level Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> OnLevel { get; init; }

        /// <summary>
        /// On Transition Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort?> OnTransitionTime { get; init; }

        /// <summary>
        /// Off Transition Time Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort?> OffTransitionTime { get; init; }

        /// <summary>
        /// Default Move Rate Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> DefaultMoveRate { get; init; }

        /// <summary>
        /// Options Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<OptionsBitmap> Options { get; init; }

        /// <summary>
        /// Start Up Current Level Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> StartUpCurrentLevel { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Level Control";
        }
    }
}