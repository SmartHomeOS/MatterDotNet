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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Color Control Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 6)]
    public class ColorControlCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0300;

        /// <summary>
        /// Color Control Cluster
        /// </summary>
        public ColorControlCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ColorControlCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports color specification via hue/saturation.
            /// </summary>
            HueSaturation = 1,
            /// <summary>
            /// Enhanced hue is supported.
            /// </summary>
            Enhanced = 2,
            /// <summary>
            /// Color loop is supported.
            /// </summary>
            Color = 4,
            /// <summary>
            /// Supports color specification via XY.
            /// </summary>
            XY = 8,
            /// <summary>
            /// Supports specification of color temperature.
            /// </summary>
            Color2 = 16,
        }
        #endregion Enums

        #region Payloads
        private record MoveToHuePayload : TLVPayload {
            public required byte Hue { get; set; }
            public required byte Direction { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Hue, 254);
                writer.WriteByte(1, Direction);
                writer.WriteUShort(2, TransitionTime, 65534);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveHuePayload : TLVPayload {
            public required byte MoveMode { get; set; }
            public required byte Rate { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, MoveMode);
                writer.WriteByte(1, Rate);
                writer.WriteByte(2, OptionsMask);
                writer.WriteByte(3, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StepHuePayload : TLVPayload {
            public required byte StepMode { get; set; }
            public required byte StepSize { get; set; }
            public required byte TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, StepMode);
                writer.WriteByte(1, StepSize);
                writer.WriteByte(2, TransitionTime);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToSaturationPayload : TLVPayload {
            public required byte Saturation { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Saturation, 254);
                writer.WriteUShort(1, TransitionTime, 65534);
                writer.WriteByte(2, OptionsMask);
                writer.WriteByte(3, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveSaturationPayload : TLVPayload {
            public required byte MoveMode { get; set; }
            public required byte Rate { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, MoveMode);
                writer.WriteByte(1, Rate);
                writer.WriteByte(2, OptionsMask);
                writer.WriteByte(3, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StepSaturationPayload : TLVPayload {
            public required byte StepMode { get; set; }
            public required byte StepSize { get; set; }
            public required byte TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, StepMode);
                writer.WriteByte(1, StepSize);
                writer.WriteByte(2, TransitionTime);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToHueAndSaturationPayload : TLVPayload {
            public required byte Hue { get; set; }
            public required byte Saturation { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, Hue, 254);
                writer.WriteByte(1, Saturation, 254);
                writer.WriteUShort(2, TransitionTime, 65534);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToColorPayload : TLVPayload {
            public required ushort ColorX { get; set; }
            public required ushort ColorY { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ColorX);
                writer.WriteUShort(1, ColorY);
                writer.WriteUShort(2, TransitionTime, 65534);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveColorPayload : TLVPayload {
            public required short RateX { get; set; }
            public required short RateY { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteShort(0, RateX);
                writer.WriteShort(1, RateY);
                writer.WriteByte(2, OptionsMask);
                writer.WriteByte(3, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StepColorPayload : TLVPayload {
            public required short StepX { get; set; }
            public required short StepY { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteShort(0, StepX);
                writer.WriteShort(1, StepY);
                writer.WriteUShort(2, TransitionTime, 65534);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveToColorTemperaturePayload : TLVPayload {
            public required ushort ColorTemperatureMireds { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, ColorTemperatureMireds);
                writer.WriteUShort(1, TransitionTime, 65534);
                writer.WriteByte(2, OptionsMask);
                writer.WriteByte(3, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record EnhancedMoveToHuePayload : TLVPayload {
            public required ushort EnhancedHue { get; set; }
            public required byte Direction { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, EnhancedHue);
                writer.WriteByte(1, Direction);
                writer.WriteUShort(2, TransitionTime, 65534);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record EnhancedMoveHuePayload : TLVPayload {
            public required byte MoveMode { get; set; }
            public required ushort Rate { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, MoveMode);
                writer.WriteUShort(1, Rate);
                writer.WriteByte(2, OptionsMask);
                writer.WriteByte(3, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record EnhancedStepHuePayload : TLVPayload {
            public required byte StepMode { get; set; }
            public required ushort StepSize { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, StepMode);
                writer.WriteUShort(1, StepSize);
                writer.WriteUShort(2, TransitionTime, 65534);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record EnhancedMoveToHueAndSaturationPayload : TLVPayload {
            public required ushort EnhancedHue { get; set; }
            public required byte Saturation { get; set; }
            public required ushort TransitionTime { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, EnhancedHue);
                writer.WriteByte(1, Saturation, 254);
                writer.WriteUShort(2, TransitionTime, 65534);
                writer.WriteByte(3, OptionsMask);
                writer.WriteByte(4, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record ColorLoopSetPayload : TLVPayload {
            public required byte UpdateFlags { get; set; }
            public required byte Action { get; set; }
            public required byte Direction { get; set; }
            public required ushort Time { get; set; }
            public required ushort StartHue { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, UpdateFlags);
                writer.WriteByte(1, Action);
                writer.WriteByte(2, Direction);
                writer.WriteUShort(3, Time);
                writer.WriteUShort(4, StartHue);
                writer.WriteByte(5, OptionsMask);
                writer.WriteByte(6, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StopMoveStepPayload : TLVPayload {
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, OptionsMask);
                writer.WriteByte(1, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record MoveColorTemperaturePayload : TLVPayload {
            public required byte MoveMode { get; set; }
            public required ushort Rate { get; set; }
            public required ushort ColorTemperatureMinimumMireds { get; set; }
            public required ushort ColorTemperatureMaximumMireds { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, MoveMode);
                writer.WriteUShort(1, Rate);
                writer.WriteUShort(2, ColorTemperatureMinimumMireds);
                writer.WriteUShort(3, ColorTemperatureMaximumMireds);
                writer.WriteByte(4, OptionsMask);
                writer.WriteByte(5, OptionsOverride);
                writer.EndContainer();
            }
        }

        private record StepColorTemperaturePayload : TLVPayload {
            public required byte StepMode { get; set; }
            public required ushort StepSize { get; set; }
            public required ushort TransitionTime { get; set; }
            public required ushort ColorTemperatureMinimumMireds { get; set; }
            public required ushort ColorTemperatureMaximumMireds { get; set; }
            public required byte OptionsMask { get; set; } = 0;
            public required byte OptionsOverride { get; set; } = 0;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, StepMode);
                writer.WriteUShort(1, StepSize);
                writer.WriteUShort(2, TransitionTime, 65534);
                writer.WriteUShort(3, ColorTemperatureMinimumMireds);
                writer.WriteUShort(4, ColorTemperatureMaximumMireds);
                writer.WriteByte(5, OptionsMask);
                writer.WriteByte(6, OptionsOverride);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Move To Hue
        /// </summary>
        public async Task<bool> MoveToHue(SecureSession session, byte Hue, byte Direction, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            MoveToHuePayload requestFields = new MoveToHuePayload() {
                Hue = Hue,
                Direction = Direction,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Hue
        /// </summary>
        public async Task<bool> MoveHue(SecureSession session, byte MoveMode, byte Rate, byte OptionsMask, byte OptionsOverride) {
            MoveHuePayload requestFields = new MoveHuePayload() {
                MoveMode = MoveMode,
                Rate = Rate,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x01, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Hue
        /// </summary>
        public async Task<bool> StepHue(SecureSession session, byte StepMode, byte StepSize, byte TransitionTime, byte OptionsMask, byte OptionsOverride) {
            StepHuePayload requestFields = new StepHuePayload() {
                StepMode = StepMode,
                StepSize = StepSize,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x02, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Saturation
        /// </summary>
        public async Task<bool> MoveToSaturation(SecureSession session, byte Saturation, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            MoveToSaturationPayload requestFields = new MoveToSaturationPayload() {
                Saturation = Saturation,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x03, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Saturation
        /// </summary>
        public async Task<bool> MoveSaturation(SecureSession session, byte MoveMode, byte Rate, byte OptionsMask, byte OptionsOverride) {
            MoveSaturationPayload requestFields = new MoveSaturationPayload() {
                MoveMode = MoveMode,
                Rate = Rate,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x04, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Saturation
        /// </summary>
        public async Task<bool> StepSaturation(SecureSession session, byte StepMode, byte StepSize, byte TransitionTime, byte OptionsMask, byte OptionsOverride) {
            StepSaturationPayload requestFields = new StepSaturationPayload() {
                StepMode = StepMode,
                StepSize = StepSize,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Hue And Saturation
        /// </summary>
        public async Task<bool> MoveToHueAndSaturation(SecureSession session, byte Hue, byte Saturation, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            MoveToHueAndSaturationPayload requestFields = new MoveToHueAndSaturationPayload() {
                Hue = Hue,
                Saturation = Saturation,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x06, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Color
        /// </summary>
        public async Task<bool> MoveToColor(SecureSession session, ushort ColorX, ushort ColorY, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            MoveToColorPayload requestFields = new MoveToColorPayload() {
                ColorX = ColorX,
                ColorY = ColorY,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x07, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Color
        /// </summary>
        public async Task<bool> MoveColor(SecureSession session, short RateX, short RateY, byte OptionsMask, byte OptionsOverride) {
            MoveColorPayload requestFields = new MoveColorPayload() {
                RateX = RateX,
                RateY = RateY,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x08, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Color
        /// </summary>
        public async Task<bool> StepColor(SecureSession session, short StepX, short StepY, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            StepColorPayload requestFields = new StepColorPayload() {
                StepX = StepX,
                StepY = StepY,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x09, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move To Color Temperature
        /// </summary>
        public async Task<bool> MoveToColorTemperature(SecureSession session, ushort ColorTemperatureMireds, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            MoveToColorTemperaturePayload requestFields = new MoveToColorTemperaturePayload() {
                ColorTemperatureMireds = ColorTemperatureMireds,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x0A, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enhanced Move To Hue
        /// </summary>
        public async Task<bool> EnhancedMoveToHue(SecureSession session, ushort EnhancedHue, byte Direction, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            EnhancedMoveToHuePayload requestFields = new EnhancedMoveToHuePayload() {
                EnhancedHue = EnhancedHue,
                Direction = Direction,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x40, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enhanced Move Hue
        /// </summary>
        public async Task<bool> EnhancedMoveHue(SecureSession session, byte MoveMode, ushort Rate, byte OptionsMask, byte OptionsOverride) {
            EnhancedMoveHuePayload requestFields = new EnhancedMoveHuePayload() {
                MoveMode = MoveMode,
                Rate = Rate,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x41, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enhanced Step Hue
        /// </summary>
        public async Task<bool> EnhancedStepHue(SecureSession session, byte StepMode, ushort StepSize, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            EnhancedStepHuePayload requestFields = new EnhancedStepHuePayload() {
                StepMode = StepMode,
                StepSize = StepSize,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x42, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Enhanced Move To Hue And Saturation
        /// </summary>
        public async Task<bool> EnhancedMoveToHueAndSaturation(SecureSession session, ushort EnhancedHue, byte Saturation, ushort TransitionTime, byte OptionsMask, byte OptionsOverride) {
            EnhancedMoveToHueAndSaturationPayload requestFields = new EnhancedMoveToHueAndSaturationPayload() {
                EnhancedHue = EnhancedHue,
                Saturation = Saturation,
                TransitionTime = TransitionTime,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x43, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Color Loop Set
        /// </summary>
        public async Task<bool> ColorLoopSet(SecureSession session, byte UpdateFlags, byte Action, byte Direction, ushort Time, ushort StartHue, byte OptionsMask, byte OptionsOverride) {
            ColorLoopSetPayload requestFields = new ColorLoopSetPayload() {
                UpdateFlags = UpdateFlags,
                Action = Action,
                Direction = Direction,
                Time = Time,
                StartHue = StartHue,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x44, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Stop Move Step
        /// </summary>
        public async Task<bool> StopMoveStep(SecureSession session, byte OptionsMask, byte OptionsOverride) {
            StopMoveStepPayload requestFields = new StopMoveStepPayload() {
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x47, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Move Color Temperature
        /// </summary>
        public async Task<bool> MoveColorTemperature(SecureSession session, byte MoveMode, ushort Rate, ushort ColorTemperatureMinimumMireds, ushort ColorTemperatureMaximumMireds, byte OptionsMask, byte OptionsOverride) {
            MoveColorTemperaturePayload requestFields = new MoveColorTemperaturePayload() {
                MoveMode = MoveMode,
                Rate = Rate,
                ColorTemperatureMinimumMireds = ColorTemperatureMinimumMireds,
                ColorTemperatureMaximumMireds = ColorTemperatureMaximumMireds,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x4B, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Step Color Temperature
        /// </summary>
        public async Task<bool> StepColorTemperature(SecureSession session, byte StepMode, ushort StepSize, ushort TransitionTime, ushort ColorTemperatureMinimumMireds, ushort ColorTemperatureMaximumMireds, byte OptionsMask, byte OptionsOverride) {
            StepColorTemperaturePayload requestFields = new StepColorTemperaturePayload() {
                StepMode = StepMode,
                StepSize = StepSize,
                TransitionTime = TransitionTime,
                ColorTemperatureMinimumMireds = ColorTemperatureMinimumMireds,
                ColorTemperatureMaximumMireds = ColorTemperatureMaximumMireds,
                OptionsMask = OptionsMask,
                OptionsOverride = OptionsOverride,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x4C, requestFields);
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
            return (byte?)(dynamic?)await GetAttribute(session, 0) ?? 0;
        }

        /// <summary>
        /// Get the Current Saturation attribute
        /// </summary>
        public async Task<byte> GetCurrentSaturation(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 1) ?? 0;
        }

        /// <summary>
        /// Get the Remaining Time attribute
        /// </summary>
        public async Task<ushort> GetRemainingTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2) ?? 0;
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
        public async Task<byte> GetDriftCompensation(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 5))!;
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
        public async Task<byte> GetColorMode(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 8) ?? 1;
        }

        /// <summary>
        /// Get the Options attribute
        /// </summary>
        public async Task<byte> GetOptions(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 15) ?? 0;
        }

        /// <summary>
        /// Set the Options attribute
        /// </summary>
        public async Task SetOptions (SecureSession session, byte? value = 0) {
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
        /// Get the Enhanced Current Hue attribute
        /// </summary>
        public async Task<ushort> GetEnhancedCurrentHue(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16384) ?? 0;
        }

        /// <summary>
        /// Get the Enhanced Color Mode attribute
        /// </summary>
        public async Task<byte> GetEnhancedColorMode(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 16385) ?? 1;
        }

        /// <summary>
        /// Get the Color Loop Active attribute
        /// </summary>
        public async Task<byte> GetColorLoopActive(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 16386) ?? 0;
        }

        /// <summary>
        /// Get the Color Loop Direction attribute
        /// </summary>
        public async Task<byte> GetColorLoopDirection(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 16387) ?? 0;
        }

        /// <summary>
        /// Get the Color Loop Time attribute
        /// </summary>
        public async Task<ushort> GetColorLoopTime(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16388) ?? 0x0019;
        }

        /// <summary>
        /// Get the Color Loop Start Enhanced Hue attribute
        /// </summary>
        public async Task<ushort> GetColorLoopStartEnhancedHue(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16389) ?? 0x2300;
        }

        /// <summary>
        /// Get the Color Loop Stored Enhanced Hue attribute
        /// </summary>
        public async Task<ushort> GetColorLoopStoredEnhancedHue(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16390) ?? 0;
        }

        /// <summary>
        /// Get the Color Capabilities attribute
        /// </summary>
        public async Task<ushort> GetColorCapabilities(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16394) ?? 0;
        }

        /// <summary>
        /// Get the Color Temp Physical Min Mireds attribute
        /// </summary>
        public async Task<ushort> GetColorTempPhysicalMinMireds(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16395) ?? 0;
        }

        /// <summary>
        /// Get the Color Temp Physical Max Mireds attribute
        /// </summary>
        public async Task<ushort> GetColorTempPhysicalMaxMireds(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16396) ?? 0xFEFF;
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
            return "Color Control Cluster";
        }
    }
}