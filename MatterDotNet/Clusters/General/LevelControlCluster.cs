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
        public LevelControl(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected LevelControl(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum Options : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
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
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
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
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
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
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
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
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
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
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
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
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
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
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
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
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
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
        public async Task<bool> MoveToLevel(SecureSession session, byte level, ushort? transitionTime, Options optionsMask, Options optionsOverride) {
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
        public async Task<bool> Move(SecureSession session, MoveMode moveMode, byte? rate, Options optionsMask, Options optionsOverride) {
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
        public async Task<bool> Step(SecureSession session, StepMode stepMode, byte stepSize, ushort? transitionTime, Options optionsMask, Options optionsOverride) {
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
        public async Task<bool> Stop(SecureSession session, Options optionsMask, Options optionsOverride) {
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
        public async Task<bool> MoveToLevelWithOnOff(SecureSession session, byte level, ushort? transitionTime, Options optionsMask, Options optionsOverride) {
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
        public async Task<bool> MoveWithOnOff(SecureSession session, MoveMode moveMode, byte? rate, Options optionsMask, Options optionsOverride) {
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
        public async Task<bool> StepWithOnOff(SecureSession session, StepMode stepMode, byte stepSize, ushort? transitionTime, Options optionsMask, Options optionsOverride) {
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
        public async Task<bool> StopWithOnOff(SecureSession session, Options optionsMask, Options optionsOverride) {
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
        /// Get the Current Level attribute
        /// </summary>
        public async Task<byte?> GetCurrentLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 0, true) ?? 0x00;
        }

        /// <summary>
        /// Get the Remaining Time attribute
        /// </summary>
        public async Task<ushort> GetRemainingTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1) ?? 0x0000;
        }

        /// <summary>
        /// Get the Min Level attribute
        /// </summary>
        public async Task<byte> GetMinLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 2) ?? 0x00;
        }

        /// <summary>
        /// Get the Max Level attribute
        /// </summary>
        public async Task<byte> GetMaxLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 3) ?? 0xFE;
        }

        /// <summary>
        /// Get the Current Frequency attribute
        /// </summary>
        public async Task<ushort> GetCurrentFrequency(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 4) ?? 0x0000;
        }

        /// <summary>
        /// Get the Min Frequency attribute
        /// </summary>
        public async Task<ushort> GetMinFrequency(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 5) ?? 0x0000;
        }

        /// <summary>
        /// Get the Max Frequency attribute
        /// </summary>
        public async Task<ushort> GetMaxFrequency(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 6) ?? 0x0000;
        }

        /// <summary>
        /// Get the On Off Transition Time attribute
        /// </summary>
        public async Task<ushort> GetOnOffTransitionTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16) ?? 0x0000;
        }

        /// <summary>
        /// Set the On Off Transition Time attribute
        /// </summary>
        public async Task SetOnOffTransitionTime (SecureSession session, ushort? value = 0x0000) {
            await SetAttribute(session, 16, value);
        }

        /// <summary>
        /// Get the On Level attribute
        /// </summary>
        public async Task<byte?> GetOnLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 17, true);
        }

        /// <summary>
        /// Set the On Level attribute
        /// </summary>
        public async Task SetOnLevel (SecureSession session, byte? value) {
            await SetAttribute(session, 17, value, true);
        }

        /// <summary>
        /// Get the On Transition Time attribute
        /// </summary>
        public async Task<ushort?> GetOnTransitionTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 18, true);
        }

        /// <summary>
        /// Set the On Transition Time attribute
        /// </summary>
        public async Task SetOnTransitionTime (SecureSession session, ushort? value) {
            await SetAttribute(session, 18, value, true);
        }

        /// <summary>
        /// Get the Off Transition Time attribute
        /// </summary>
        public async Task<ushort?> GetOffTransitionTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 19, true);
        }

        /// <summary>
        /// Set the Off Transition Time attribute
        /// </summary>
        public async Task SetOffTransitionTime (SecureSession session, ushort? value) {
            await SetAttribute(session, 19, value, true);
        }

        /// <summary>
        /// Get the Default Move Rate attribute
        /// </summary>
        public async Task<byte?> GetDefaultMoveRate(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 20, true);
        }

        /// <summary>
        /// Set the Default Move Rate attribute
        /// </summary>
        public async Task SetDefaultMoveRate (SecureSession session, byte? value) {
            await SetAttribute(session, 20, value, true);
        }

        /// <summary>
        /// Get the Options attribute
        /// </summary>
        public async Task<Options> GetOptions(SecureSession session) {
            return (Options)await GetEnumAttribute(session, 15);
        }

        /// <summary>
        /// Set the Options attribute
        /// </summary>
        public async Task SetOptions (SecureSession session, Options value) {
            await SetAttribute(session, 15, value);
        }

        /// <summary>
        /// Get the Start Up Current Level attribute
        /// </summary>
        public async Task<byte?> GetStartUpCurrentLevel(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 16384, true);
        }

        /// <summary>
        /// Set the Start Up Current Level attribute
        /// </summary>
        public async Task SetStartUpCurrentLevel (SecureSession session, byte? value) {
            await SetAttribute(session, 16384, value, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Level Control";
        }
    }
}