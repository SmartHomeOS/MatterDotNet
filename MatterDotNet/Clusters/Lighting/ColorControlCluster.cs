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

using MatterDotNet.Attributes;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

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
        [SetsRequiredMembers]
        public ColorControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ColorControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            CurrentHue = new ReportAttribute<byte>(cluster, endPoint, 0) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x00

            };
            CurrentSaturation = new ReportAttribute<byte>(cluster, endPoint, 1) {
                Deserialize = x => (byte?)(dynamic?)x ?? 0x00

            };
            RemainingTime = new ReadAttribute<ushort>(cluster, endPoint, 2) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            CurrentX = new ReportAttribute<ushort>(cluster, endPoint, 3) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x616B

            };
            CurrentY = new ReportAttribute<ushort>(cluster, endPoint, 4) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x607D

            };
            DriftCompensation = new ReadAttribute<DriftCompensationEnum>(cluster, endPoint, 5) {
                Deserialize = x => (DriftCompensationEnum)DeserializeEnum(x)!
            };
            CompensationText = new ReadAttribute<string>(cluster, endPoint, 6) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ColorTemperatureMireds = new ReportAttribute<ushort>(cluster, endPoint, 7) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x00FA

            };
            ColorMode = new ReadAttribute<ColorModeEnum>(cluster, endPoint, 8) {
                Deserialize = x => (ColorModeEnum)DeserializeEnum(x)!
            };
            Options = new ReadWriteAttribute<OptionsBitmap>(cluster, endPoint, 15) {
                Deserialize = x => (OptionsBitmap)DeserializeEnum(x)!
            };
            NumberOfPrimaries = new ReadAttribute<byte?>(cluster, endPoint, 16, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            Primary1X = new ReadAttribute<ushort>(cluster, endPoint, 17) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary1Y = new ReadAttribute<ushort>(cluster, endPoint, 18) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary1Intensity = new ReadAttribute<byte?>(cluster, endPoint, 19, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            Primary2X = new ReadAttribute<ushort>(cluster, endPoint, 21) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary2Y = new ReadAttribute<ushort>(cluster, endPoint, 22) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary2Intensity = new ReadAttribute<byte?>(cluster, endPoint, 23, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            Primary3X = new ReadAttribute<ushort>(cluster, endPoint, 25) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary3Y = new ReadAttribute<ushort>(cluster, endPoint, 26) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary3Intensity = new ReadAttribute<byte?>(cluster, endPoint, 27, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            Primary4X = new ReadAttribute<ushort>(cluster, endPoint, 32) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary4Y = new ReadAttribute<ushort>(cluster, endPoint, 33) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary4Intensity = new ReadAttribute<byte?>(cluster, endPoint, 34, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            Primary5X = new ReadAttribute<ushort>(cluster, endPoint, 36) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary5Y = new ReadAttribute<ushort>(cluster, endPoint, 37) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary5Intensity = new ReadAttribute<byte?>(cluster, endPoint, 38, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            Primary6X = new ReadAttribute<ushort>(cluster, endPoint, 40) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary6Y = new ReadAttribute<ushort>(cluster, endPoint, 41) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            Primary6Intensity = new ReadAttribute<byte?>(cluster, endPoint, 42, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            WhitePointX = new ReadWriteAttribute<ushort>(cluster, endPoint, 48) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            WhitePointY = new ReadWriteAttribute<ushort>(cluster, endPoint, 49) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            ColorPointRX = new ReadWriteAttribute<ushort>(cluster, endPoint, 50) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            ColorPointRY = new ReadWriteAttribute<ushort>(cluster, endPoint, 51) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            ColorPointRIntensity = new ReadWriteAttribute<byte?>(cluster, endPoint, 52, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            ColorPointGX = new ReadWriteAttribute<ushort>(cluster, endPoint, 54) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            ColorPointGY = new ReadWriteAttribute<ushort>(cluster, endPoint, 55) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            ColorPointGIntensity = new ReadWriteAttribute<byte?>(cluster, endPoint, 56, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            ColorPointBX = new ReadWriteAttribute<ushort>(cluster, endPoint, 58) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            ColorPointBY = new ReadWriteAttribute<ushort>(cluster, endPoint, 59) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            ColorPointBIntensity = new ReadWriteAttribute<byte?>(cluster, endPoint, 60, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            CoupleColorTempToLevelMinMireds = new ReadAttribute<ushort>(cluster, endPoint, 16397) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            StartUpColorTemperatureMireds = new ReadWriteAttribute<ushort?>(cluster, endPoint, 16400, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
        }

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
        public enum ColorModeEnum : byte {
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
        public enum DriftCompensationEnum : byte {
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
            None = 0x0,
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
            None = 0x0,
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
        public enum OptionsBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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

        private record StepHuePayload : TLVPayload {
            public required StepMode StepMode { get; set; }
            public required byte StepSize { get; set; }
            public required byte TransitionTime { get; set; }
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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

        private record StepSaturationPayload : TLVPayload {
            public required StepMode StepMode { get; set; }
            public required byte StepSize { get; set; }
            public required byte TransitionTime { get; set; }
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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
            public required OptionsBitmap OptionsMask { get; set; }
            public required OptionsBitmap OptionsOverride { get; set; }
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
        public async Task<bool> MoveToHue(SecureSession session, byte hue, Direction direction, ushort transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            MoveToHuePayload requestFields = new MoveToHuePayload() {
                Hue = hue,
                Direction = direction,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Hue
        /// </summary>
        public async Task<bool> MoveHue(SecureSession session, MoveMode moveMode, byte rate, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            MoveHuePayload requestFields = new MoveHuePayload() {
                MoveMode = moveMode,
                Rate = rate,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Hue
        /// </summary>
        public async Task<bool> StepHue(SecureSession session, StepMode stepMode, byte stepSize, byte transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            StepHuePayload requestFields = new StepHuePayload() {
                StepMode = stepMode,
                StepSize = stepSize,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Saturation
        /// </summary>
        public async Task<bool> MoveToSaturation(SecureSession session, byte saturation, ushort transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            MoveToSaturationPayload requestFields = new MoveToSaturationPayload() {
                Saturation = saturation,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Saturation
        /// </summary>
        public async Task<bool> MoveSaturation(SecureSession session, MoveMode moveMode, byte rate, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            MoveSaturationPayload requestFields = new MoveSaturationPayload() {
                MoveMode = moveMode,
                Rate = rate,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Saturation
        /// </summary>
        public async Task<bool> StepSaturation(SecureSession session, StepMode stepMode, byte stepSize, byte transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            StepSaturationPayload requestFields = new StepSaturationPayload() {
                StepMode = stepMode,
                StepSize = stepSize,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Hue And Saturation
        /// </summary>
        public async Task<bool> MoveToHueAndSaturation(SecureSession session, byte hue, byte saturation, ushort transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            MoveToHueAndSaturationPayload requestFields = new MoveToHueAndSaturationPayload() {
                Hue = hue,
                Saturation = saturation,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Color
        /// </summary>
        public async Task<bool> MoveToColor(SecureSession session, ushort colorX, ushort colorY, ushort transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            MoveToColorPayload requestFields = new MoveToColorPayload() {
                ColorX = colorX,
                ColorY = colorY,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Color
        /// </summary>
        public async Task<bool> MoveColor(SecureSession session, short rateX, short rateY, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            MoveColorPayload requestFields = new MoveColorPayload() {
                RateX = rateX,
                RateY = rateY,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x08, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Color
        /// </summary>
        public async Task<bool> StepColor(SecureSession session, short stepX, short stepY, ushort transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            StepColorPayload requestFields = new StepColorPayload() {
                StepX = stepX,
                StepY = stepY,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields, token);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Color Temperature
        /// </summary>
        public async Task<bool> MoveToColorTemperature(SecureSession session, ushort colorTemperatureMireds, ushort transitionTime, OptionsBitmap optionsMask, OptionsBitmap optionsOverride, CancellationToken token = default) {
            MoveToColorTemperaturePayload requestFields = new MoveToColorTemperaturePayload() {
                ColorTemperatureMireds = colorTemperatureMireds,
                TransitionTime = transitionTime,
                OptionsMask = optionsMask,
                OptionsOverride = optionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0A, requestFields, token);
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
        /// Current Hue Attribute [Read/Event]
        /// </summary>
        public required ReportAttribute<byte> CurrentHue { get; init; }

        /// <summary>
        /// Current Saturation Attribute [Read/Event]
        /// </summary>
        public required ReportAttribute<byte> CurrentSaturation { get; init; }

        /// <summary>
        /// Remaining Time Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> RemainingTime { get; init; }

        /// <summary>
        /// CurrentX Attribute [Read/Event]
        /// </summary>
        public required ReportAttribute<ushort> CurrentX { get; init; }

        /// <summary>
        /// CurrentY Attribute [Read/Event]
        /// </summary>
        public required ReportAttribute<ushort> CurrentY { get; init; }

        /// <summary>
        /// Drift Compensation Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<DriftCompensationEnum> DriftCompensation { get; init; }

        /// <summary>
        /// Compensation Text Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<string> CompensationText { get; init; }

        /// <summary>
        /// Color Temperature Mireds Attribute [Read/Event]
        /// </summary>
        public required ReportAttribute<ushort> ColorTemperatureMireds { get; init; }

        /// <summary>
        /// Color Mode Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ColorModeEnum> ColorMode { get; init; }

        /// <summary>
        /// Options Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<OptionsBitmap> Options { get; init; }

        /// <summary>
        /// Number Of Primaries Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> NumberOfPrimaries { get; init; }

        /// <summary>
        /// Primary1X Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary1X { get; init; }

        /// <summary>
        /// Primary1Y Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary1Y { get; init; }

        /// <summary>
        /// Primary1 Intensity Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> Primary1Intensity { get; init; }

        /// <summary>
        /// Primary2X Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary2X { get; init; }

        /// <summary>
        /// Primary2Y Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary2Y { get; init; }

        /// <summary>
        /// Primary2 Intensity Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> Primary2Intensity { get; init; }

        /// <summary>
        /// Primary3X Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary3X { get; init; }

        /// <summary>
        /// Primary3Y Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary3Y { get; init; }

        /// <summary>
        /// Primary3 Intensity Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> Primary3Intensity { get; init; }

        /// <summary>
        /// Primary4X Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary4X { get; init; }

        /// <summary>
        /// Primary4Y Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary4Y { get; init; }

        /// <summary>
        /// Primary4 Intensity Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> Primary4Intensity { get; init; }

        /// <summary>
        /// Primary5X Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary5X { get; init; }

        /// <summary>
        /// Primary5Y Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary5Y { get; init; }

        /// <summary>
        /// Primary5 Intensity Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> Primary5Intensity { get; init; }

        /// <summary>
        /// Primary6X Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary6X { get; init; }

        /// <summary>
        /// Primary6Y Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Primary6Y { get; init; }

        /// <summary>
        /// Primary6 Intensity Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> Primary6Intensity { get; init; }

        /// <summary>
        /// White PointX Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> WhitePointX { get; init; }

        /// <summary>
        /// White PointY Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> WhitePointY { get; init; }

        /// <summary>
        /// Color Point RX Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> ColorPointRX { get; init; }

        /// <summary>
        /// Color Point RY Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> ColorPointRY { get; init; }

        /// <summary>
        /// Color Point R Intensity Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> ColorPointRIntensity { get; init; }

        /// <summary>
        /// Color Point GX Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> ColorPointGX { get; init; }

        /// <summary>
        /// Color Point GY Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> ColorPointGY { get; init; }

        /// <summary>
        /// Color Point G Intensity Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> ColorPointGIntensity { get; init; }

        /// <summary>
        /// Color Point BX Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> ColorPointBX { get; init; }

        /// <summary>
        /// Color Point BY Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort> ColorPointBY { get; init; }

        /// <summary>
        /// Color Point B Intensity Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<byte?> ColorPointBIntensity { get; init; }

        /// <summary>
        /// Couple Color Temp To Level Min Mireds Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> CoupleColorTempToLevelMinMireds { get; init; }

        /// <summary>
        /// Start Up Color Temperature Mireds Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ushort?> StartUpColorTemperatureMireds { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Color Control";
        }
    }
}