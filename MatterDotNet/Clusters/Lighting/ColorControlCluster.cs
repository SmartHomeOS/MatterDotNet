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

namespace MatterDotNet.Clusters.Lighting
{
    /// <summary>
    /// Attributes and commands for controlling the color properties of a color-capable light.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 7)]
    public class ColorControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0300;

        /// <summary>
        /// Attributes and commands for controlling the color properties of a color-capable light.
        /// </summary>
        public ColorControl(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ColorControl(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports color specification via hue/saturation.
            /// </summary>
            HueAndSaturation = 1,
            /// <summary>
            /// Enhanced hue is supported.
            /// </summary>
            EnhancedHue = 2,
            /// <summary>
            /// Color loop is supported.
            /// </summary>
            ColorLoop = 4,
            /// <summary>
            /// Supports color specification via XY.
            /// </summary>
            XY = 8,
            /// <summary>
            /// Supports specification of color temperature.
            /// </summary>
            ColorTemperature = 16,
        }

        /// <summary>
        /// Direction
        /// </summary>
        public enum Direction : byte {
            /// <summary>
            /// Shortest distance
            /// </summary>
            Shortest = 0,
            /// <summary>
            /// Longest distance
            /// </summary>
            Longest = 1,
            /// <summary>
            /// Up
            /// </summary>
            Up = 2,
            /// <summary>
            /// Down
            /// </summary>
            Down = 3,
        }

        /// <summary>
        /// Move Mode
        /// </summary>
        public enum MoveMode : byte {
            /// <summary>
            /// Stop the movement
            /// </summary>
            Stop = 0,
            /// <summary>
            /// Move in an upwards direction
            /// </summary>
            Up = 1,
            /// <summary>
            /// Move in a downwards direction
            /// </summary>
            Down = 3,
        }

        /// <summary>
        /// Step Mode
        /// </summary>
        public enum StepMode : byte {
            /// <summary>
            /// Step in an upwards direction
            /// </summary>
            Up = 1,
            /// <summary>
            /// Step in a downwards direction
            /// </summary>
            Down = 3,
        }

        /// <summary>
        /// Color Mode
        /// </summary>
        public enum ColorMode : byte {
            /// <summary>
            /// The current hue and saturation attributes determine the color.
            /// </summary>
            CurrentHueAndCurrentSaturation = 0,
            /// <summary>
            /// The current X and Y attributes determine the color.
            /// </summary>
            CurrentXAndCurrentY = 1,
            /// <summary>
            /// The color temperature attribute determines the color.
            /// </summary>
            ColorTemperatureMireds = 2,
        }

        /// <summary>
        /// Color Loop Action
        /// </summary>
        public enum ColorLoopAction : byte {
            /// <summary>
            /// De-activate the color loop.
            /// </summary>
            Deactivate = 0,
            /// <summary>
            /// Activate the color loop from the value in the ColorLoopStartEnhancedHue field.
            /// </summary>
            ActivateFromColorLoopStartEnhancedHue = 1,
            /// <summary>
            /// Activate the color loop from the value of the EnhancedCurrentHue attribute.
            /// </summary>
            ActivateFromEnhancedCurrentHue = 2,
        }

        /// <summary>
        /// Enhanced Color Mode
        /// </summary>
        public enum EnhancedColorMode : byte {
            /// <summary>
            /// The current hue and saturation attributes determine the color.
            /// </summary>
            CurrentHueAndCurrentSaturation = 0,
            /// <summary>
            /// The current X and Y attributes determine the color.
            /// </summary>
            CurrentXAndCurrentY = 1,
            /// <summary>
            /// The color temperature attribute determines the color.
            /// </summary>
            ColorTemperatureMireds = 2,
            /// <summary>
            /// The enhanced current hue and saturation attributes determine the color.
            /// </summary>
            EnhancedCurrentHueAndCurrentSaturation = 3,
        }

        /// <summary>
        /// Drift Compensation
        /// </summary>
        public enum DriftCompensation : byte {
            /// <summary>
            /// There is no compensation.
            /// </summary>
            None = 0,
            /// <summary>
            /// The compensation is based on other or unknown mechanism.
            /// </summary>
            OtherOrUnknown = 1,
            /// <summary>
            /// The compensation is based on temperature monitoring.
            /// </summary>
            TemperatureMonitoring = 2,
            /// <summary>
            /// The compensation is based on optical luminance monitoring and feedback.
            /// </summary>
            OpticalLuminanceMonitoringAndFeedback = 3,
            /// <summary>
            /// The compensation is based on optical color monitoring and feedback.
            /// </summary>
            OpticalColorMonitoringAndFeedback = 4,
        }

        /// <summary>
        /// Color Loop Direction
        /// </summary>
        public enum ColorLoopDirection : byte {
            /// <summary>
            /// Decrement the hue in the color loop.
            /// </summary>
            Decrement = 0,
            /// <summary>
            /// Increment the hue in the color loop.
            /// </summary>
            Increment = 1,
        }

        /// <summary>
        /// Color Capabilities
        /// </summary>
        [Flags]
        public enum ColorCapabilities : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Supports color specification via hue/saturation.
            /// </summary>
            HueSaturation = 0x0001,
            /// <summary>
            /// Enhanced hue is supported.
            /// </summary>
            EnhancedHue = 0x0002,
            /// <summary>
            /// Color loop is supported.
            /// </summary>
            ColorLoop = 0x0004,
            /// <summary>
            /// Supports color specification via XY.
            /// </summary>
            XY = 0x0008,
            /// <summary>
            /// Supports color specification via color temperature.
            /// </summary>
            ColorTemperature = 0x0010,
        }

        /// <summary>
        /// Update Flags
        /// </summary>
        [Flags]
        public enum UpdateFlags : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Device adheres to the associated action field.
            /// </summary>
            UpdateAction = 0x01,
            /// <summary>
            /// Device updates the associated direction attribute.
            /// </summary>
            UpdateDirection = 0x02,
            /// <summary>
            /// Device updates the associated time attribute.
            /// </summary>
            UpdateTime = 0x04,
            /// <summary>
            /// Device updates the associated start hue attribute.
            /// </summary>
            UpdateStartHue = 0x08,
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
        }
        #endregion Enums

        #region Payloads
        private record MoveToHuePayload : TLVPayload {
            public required byte Hue { get; set; }
            public required Direction Direction { get; set; }
            public required ushort TransitionTime { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Hue);
                writer.WriteUShort(1, (ushort)Direction);
                writer.WriteUShort(2, TransitionTime);
                writer.WriteUInt(3, (uint)OptionsMask);
                writer.WriteUInt(4, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveHuePayload : TLVPayload {
            public required MoveMode MoveMode { get; set; }
            public required byte Rate { get; set; }
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

        private record StepHuePayload : TLVPayload {
            public required StepMode StepMode { get; set; }
            public required byte StepSize { get; set; }
            public required byte TransitionTime { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)StepMode);
                writer.WriteByte(1, StepSize);
                writer.WriteByte(2, TransitionTime);
                writer.WriteUInt(3, (uint)OptionsMask);
                writer.WriteUInt(4, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToSaturationPayload : TLVPayload {
            public required byte Saturation { get; set; }
            public required ushort TransitionTime { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Saturation);
                writer.WriteUShort(1, TransitionTime);
                writer.WriteUInt(2, (uint)OptionsMask);
                writer.WriteUInt(3, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveSaturationPayload : TLVPayload {
            public required MoveMode MoveMode { get; set; }
            public required byte Rate { get; set; }
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

        private record StepSaturationPayload : TLVPayload {
            public required StepMode StepMode { get; set; }
            public required byte StepSize { get; set; }
            public required byte TransitionTime { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)StepMode);
                writer.WriteByte(1, StepSize);
                writer.WriteByte(2, TransitionTime);
                writer.WriteUInt(3, (uint)OptionsMask);
                writer.WriteUInt(4, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToHueAndSaturationPayload : TLVPayload {
            public required byte Hue { get; set; }
            public required byte Saturation { get; set; }
            public required ushort TransitionTime { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Hue);
                writer.WriteByte(1, Saturation);
                writer.WriteUShort(2, TransitionTime);
                writer.WriteUInt(3, (uint)OptionsMask);
                writer.WriteUInt(4, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToColorPayload : TLVPayload {
            public required ushort ColorX { get; set; }
            public required ushort ColorY { get; set; }
            public required ushort TransitionTime { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ColorX);
                writer.WriteUShort(1, ColorY);
                writer.WriteUShort(2, TransitionTime);
                writer.WriteUInt(3, (uint)OptionsMask);
                writer.WriteUInt(4, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveColorPayload : TLVPayload {
            public required short RateX { get; set; }
            public required short RateY { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteShort(0, RateX);
                writer.WriteShort(1, RateY);
                writer.WriteUInt(2, (uint)OptionsMask);
                writer.WriteUInt(3, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StepColorPayload : TLVPayload {
            public required short StepX { get; set; }
            public required short StepY { get; set; }
            public required ushort TransitionTime { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteShort(0, StepX);
                writer.WriteShort(1, StepY);
                writer.WriteUShort(2, TransitionTime);
                writer.WriteUInt(3, (uint)OptionsMask);
                writer.WriteUInt(4, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToColorTemperaturePayload : TLVPayload {
            public required ushort ColorTemperatureMireds { get; set; }
            public required ushort TransitionTime { get; set; }
            public required Options OptionsMask { get; set; }
            public required Options OptionsOverride { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ColorTemperatureMireds);
                writer.WriteUShort(1, TransitionTime);
                writer.WriteUInt(2, (uint)OptionsMask);
                writer.WriteUInt(3, (uint)OptionsOverride);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Move To Hue
        /// </summary>
        public async Task<bool> MoveToHue(SecureSession session, byte hue, Direction direction, ushort transitionTime, Options optionsMask, Options optionsOverride) {
            MoveToHuePayload requestFields = new MoveToHuePayload() {
                Hue = hue,
                Direction = direction,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Hue
        /// </summary>
        public async Task<bool> MoveHue(SecureSession session, MoveMode moveMode, byte rate, Options optionsMask, Options optionsOverride) {
            MoveHuePayload requestFields = new MoveHuePayload() {
                MoveMode = moveMode,
                Rate = rate,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Hue
        /// </summary>
        public async Task<bool> StepHue(SecureSession session, StepMode stepMode, byte stepSize, byte transitionTime, Options optionsMask, Options optionsOverride) {
            StepHuePayload requestFields = new StepHuePayload() {
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
        /// Move To Saturation
        /// </summary>
        public async Task<bool> MoveToSaturation(SecureSession session, byte saturation, ushort transitionTime, Options optionsMask, Options optionsOverride) {
            MoveToSaturationPayload requestFields = new MoveToSaturationPayload() {
                Saturation = saturation,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Saturation
        /// </summary>
        public async Task<bool> MoveSaturation(SecureSession session, MoveMode moveMode, byte rate, Options optionsMask, Options optionsOverride) {
            MoveSaturationPayload requestFields = new MoveSaturationPayload() {
                MoveMode = moveMode,
                Rate = rate,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Saturation
        /// </summary>
        public async Task<bool> StepSaturation(SecureSession session, StepMode stepMode, byte stepSize, byte transitionTime, Options optionsMask, Options optionsOverride) {
            StepSaturationPayload requestFields = new StepSaturationPayload() {
                StepMode = stepMode,
                StepSize = stepSize,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Hue And Saturation
        /// </summary>
        public async Task<bool> MoveToHueAndSaturation(SecureSession session, byte hue, byte saturation, ushort transitionTime, Options optionsMask, Options optionsOverride) {
            MoveToHueAndSaturationPayload requestFields = new MoveToHueAndSaturationPayload() {
                Hue = hue,
                Saturation = saturation,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Color
        /// </summary>
        public async Task<bool> MoveToColor(SecureSession session, ushort colorX, ushort colorY, ushort transitionTime, Options optionsMask, Options optionsOverride) {
            MoveToColorPayload requestFields = new MoveToColorPayload() {
                ColorX = colorX,
                ColorY = colorY,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Color
        /// </summary>
        public async Task<bool> MoveColor(SecureSession session, short rateX, short rateY, Options optionsMask, Options optionsOverride) {
            MoveColorPayload requestFields = new MoveColorPayload() {
                RateX = rateX,
                RateY = rateY,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x08, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Color
        /// </summary>
        public async Task<bool> StepColor(SecureSession session, short stepX, short stepY, ushort transitionTime, Options optionsMask, Options optionsOverride) {
            StepColorPayload requestFields = new StepColorPayload() {
                StepX = stepX,
                StepY = stepY,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Color Temperature
        /// </summary>
        public async Task<bool> MoveToColorTemperature(SecureSession session, ushort colorTemperatureMireds, ushort transitionTime, Options optionsMask, Options optionsOverride) {
            MoveToColorTemperaturePayload requestFields = new MoveToColorTemperaturePayload() {
                ColorTemperatureMireds = colorTemperatureMireds,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0A, requestFields);
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
        /// Get the Current Hue attribute
        /// </summary>
        public async Task<byte> GetCurrentHue(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 0) ?? 0x00;
        }

        /// <summary>
        /// Get the Current Saturation attribute
        /// </summary>
        public async Task<byte> GetCurrentSaturation(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1) ?? 0x00;
        }

        /// <summary>
        /// Get the Remaining Time attribute
        /// </summary>
        public async Task<ushort> GetRemainingTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2) ?? 0x0000;
        }

        /// <summary>
        /// Get the CurrentX attribute
        /// </summary>
        public async Task<ushort> GetCurrentX(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3) ?? 0x616B;
        }

        /// <summary>
        /// Get the CurrentY attribute
        /// </summary>
        public async Task<ushort> GetCurrentY(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 4) ?? 0x607D;
        }

        /// <summary>
        /// Get the Drift Compensation attribute
        /// </summary>
        public async Task<DriftCompensation> GetDriftCompensation(SecureSession session) {
            return (DriftCompensation)await GetEnumAttribute(session, 5);
        }

        /// <summary>
        /// Get the Compensation Text attribute
        /// </summary>
        public async Task<string> GetCompensationText(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 6))!;
        }

        /// <summary>
        /// Get the Color Temperature Mireds attribute
        /// </summary>
        public async Task<ushort> GetColorTemperatureMireds(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 7) ?? 0x00FA;
        }

        /// <summary>
        /// Get the Color Mode attribute
        /// </summary>
        public async Task<ColorMode> GetColorMode(SecureSession session) {
            return (ColorMode)await GetEnumAttribute(session, 8);
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
        /// Get the Number Of Primaries attribute
        /// </summary>
        public async Task<byte?> GetNumberOfPrimaries(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 16, true);
        }

        /// <summary>
        /// Get the Primary1X attribute
        /// </summary>
        public async Task<ushort> GetPrimary1X(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 17))!;
        }

        /// <summary>
        /// Get the Primary1Y attribute
        /// </summary>
        public async Task<ushort> GetPrimary1Y(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 18))!;
        }

        /// <summary>
        /// Get the Primary1 Intensity attribute
        /// </summary>
        public async Task<byte?> GetPrimary1Intensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 19, true);
        }

        /// <summary>
        /// Get the Primary2X attribute
        /// </summary>
        public async Task<ushort> GetPrimary2X(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 21))!;
        }

        /// <summary>
        /// Get the Primary2Y attribute
        /// </summary>
        public async Task<ushort> GetPrimary2Y(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 22))!;
        }

        /// <summary>
        /// Get the Primary2 Intensity attribute
        /// </summary>
        public async Task<byte?> GetPrimary2Intensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 23, true);
        }

        /// <summary>
        /// Get the Primary3X attribute
        /// </summary>
        public async Task<ushort> GetPrimary3X(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 25))!;
        }

        /// <summary>
        /// Get the Primary3Y attribute
        /// </summary>
        public async Task<ushort> GetPrimary3Y(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 26))!;
        }

        /// <summary>
        /// Get the Primary3 Intensity attribute
        /// </summary>
        public async Task<byte?> GetPrimary3Intensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 27, true);
        }

        /// <summary>
        /// Get the Primary4X attribute
        /// </summary>
        public async Task<ushort> GetPrimary4X(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 32))!;
        }

        /// <summary>
        /// Get the Primary4Y attribute
        /// </summary>
        public async Task<ushort> GetPrimary4Y(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 33))!;
        }

        /// <summary>
        /// Get the Primary4 Intensity attribute
        /// </summary>
        public async Task<byte?> GetPrimary4Intensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 34, true);
        }

        /// <summary>
        /// Get the Primary5X attribute
        /// </summary>
        public async Task<ushort> GetPrimary5X(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 36))!;
        }

        /// <summary>
        /// Get the Primary5Y attribute
        /// </summary>
        public async Task<ushort> GetPrimary5Y(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 37))!;
        }

        /// <summary>
        /// Get the Primary5 Intensity attribute
        /// </summary>
        public async Task<byte?> GetPrimary5Intensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 38, true);
        }

        /// <summary>
        /// Get the Primary6X attribute
        /// </summary>
        public async Task<ushort> GetPrimary6X(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 40))!;
        }

        /// <summary>
        /// Get the Primary6Y attribute
        /// </summary>
        public async Task<ushort> GetPrimary6Y(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 41))!;
        }

        /// <summary>
        /// Get the Primary6 Intensity attribute
        /// </summary>
        public async Task<byte?> GetPrimary6Intensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 42, true);
        }

        /// <summary>
        /// Get the White PointX attribute
        /// </summary>
        public async Task<ushort> GetWhitePointX(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 48))!;
        }

        /// <summary>
        /// Set the White PointX attribute
        /// </summary>
        public async Task SetWhitePointX (SecureSession session, ushort value) {
            await SetAttribute(session, 48, value);
        }

        /// <summary>
        /// Get the White PointY attribute
        /// </summary>
        public async Task<ushort> GetWhitePointY(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 49))!;
        }

        /// <summary>
        /// Set the White PointY attribute
        /// </summary>
        public async Task SetWhitePointY (SecureSession session, ushort value) {
            await SetAttribute(session, 49, value);
        }

        /// <summary>
        /// Get the Color Point RX attribute
        /// </summary>
        public async Task<ushort> GetColorPointRX(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 50))!;
        }

        /// <summary>
        /// Set the Color Point RX attribute
        /// </summary>
        public async Task SetColorPointRX (SecureSession session, ushort value) {
            await SetAttribute(session, 50, value);
        }

        /// <summary>
        /// Get the Color Point RY attribute
        /// </summary>
        public async Task<ushort> GetColorPointRY(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 51))!;
        }

        /// <summary>
        /// Set the Color Point RY attribute
        /// </summary>
        public async Task SetColorPointRY (SecureSession session, ushort value) {
            await SetAttribute(session, 51, value);
        }

        /// <summary>
        /// Get the Color Point R Intensity attribute
        /// </summary>
        public async Task<byte?> GetColorPointRIntensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 52, true);
        }

        /// <summary>
        /// Set the Color Point R Intensity attribute
        /// </summary>
        public async Task SetColorPointRIntensity (SecureSession session, byte? value) {
            await SetAttribute(session, 52, value, true);
        }

        /// <summary>
        /// Get the Color Point GX attribute
        /// </summary>
        public async Task<ushort> GetColorPointGX(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 54))!;
        }

        /// <summary>
        /// Set the Color Point GX attribute
        /// </summary>
        public async Task SetColorPointGX (SecureSession session, ushort value) {
            await SetAttribute(session, 54, value);
        }

        /// <summary>
        /// Get the Color Point GY attribute
        /// </summary>
        public async Task<ushort> GetColorPointGY(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 55))!;
        }

        /// <summary>
        /// Set the Color Point GY attribute
        /// </summary>
        public async Task SetColorPointGY (SecureSession session, ushort value) {
            await SetAttribute(session, 55, value);
        }

        /// <summary>
        /// Get the Color Point G Intensity attribute
        /// </summary>
        public async Task<byte?> GetColorPointGIntensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 56, true);
        }

        /// <summary>
        /// Set the Color Point G Intensity attribute
        /// </summary>
        public async Task SetColorPointGIntensity (SecureSession session, byte? value) {
            await SetAttribute(session, 56, value, true);
        }

        /// <summary>
        /// Get the Color Point BX attribute
        /// </summary>
        public async Task<ushort> GetColorPointBX(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 58))!;
        }

        /// <summary>
        /// Set the Color Point BX attribute
        /// </summary>
        public async Task SetColorPointBX (SecureSession session, ushort value) {
            await SetAttribute(session, 58, value);
        }

        /// <summary>
        /// Get the Color Point BY attribute
        /// </summary>
        public async Task<ushort> GetColorPointBY(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 59))!;
        }

        /// <summary>
        /// Set the Color Point BY attribute
        /// </summary>
        public async Task SetColorPointBY (SecureSession session, ushort value) {
            await SetAttribute(session, 59, value);
        }

        /// <summary>
        /// Get the Color Point B Intensity attribute
        /// </summary>
        public async Task<byte?> GetColorPointBIntensity(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 60, true);
        }

        /// <summary>
        /// Set the Color Point B Intensity attribute
        /// </summary>
        public async Task SetColorPointBIntensity (SecureSession session, byte? value) {
            await SetAttribute(session, 60, value, true);
        }

        /// <summary>
        /// Get the Couple Color Temp To Level Min Mireds attribute
        /// </summary>
        public async Task<ushort> GetCoupleColorTempToLevelMinMireds(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 16397))!;
        }

        /// <summary>
        /// Get the Start Up Color Temperature Mireds attribute
        /// </summary>
        public async Task<ushort?> GetStartUpColorTemperatureMireds(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16400, true);
        }

        /// <summary>
        /// Set the Start Up Color Temperature Mireds attribute
        /// </summary>
        public async Task SetStartUpColorTemperatureMireds (SecureSession session, ushort? value) {
            await SetAttribute(session, 16400, value, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Color Control";
        }
    }
}