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
    /// Window Covering Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 5)]
    public class WindowCoveringCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0102;

        /// <summary>
        /// Window Covering Cluster
        /// </summary>
        public WindowCoveringCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected WindowCoveringCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Lift control and behavior for lifting/sliding window coverings
            /// </summary>
            Lift = 1,
            /// <summary>
            /// Tilt control and behavior for tilting window coverings
            /// </summary>
            Tilt = 2,
            /// <summary>
            /// Position aware lift control is supported.
            /// </summary>
            PositionAwareLift = 4,
            /// <summary>
            /// Absolute positioning is supported.
            /// </summary>
            AbsolutePosition = 8,
            /// <summary>
            /// Position aware tilt control is supported.
            /// </summary>
            PositionAwareTilt = 16,
        }

        /// <summary>
        /// End Product Type
        /// </summary>
        public enum EndProductTypeEnum {
            /// <summary>
            /// Simple Roller Shade
            /// </summary>
            RollerShade = 0,
            /// <summary>
            /// Roman Shade
            /// </summary>
            RomanShade = 1,
            /// <summary>
            /// Balloon Shade
            /// </summary>
            BalloonShade = 2,
            /// <summary>
            /// Woven Wood
            /// </summary>
            WovenWood = 3,
            /// <summary>
            /// Pleated Shade
            /// </summary>
            PleatedShade = 4,
            /// <summary>
            /// Cellular Shade
            /// </summary>
            CellularShade = 5,
            /// <summary>
            /// Layered Shade
            /// </summary>
            LayeredShade = 6,
            /// <summary>
            /// Layered Shade 2D
            /// </summary>
            LayeredShade2D = 7,
            /// <summary>
            /// Sheer Shade
            /// </summary>
            SheerShade = 8,
            /// <summary>
            /// Tilt Only Interior Blind
            /// </summary>
            TiltOnlyInteriorBlind = 9,
            /// <summary>
            /// Interior Blind
            /// </summary>
            InteriorBlind = 10,
            /// <summary>
            /// Vertical Blind, Strip Curtain
            /// </summary>
            VerticalBlindStripCurtain = 11,
            /// <summary>
            /// Interior Venetian Blind
            /// </summary>
            InteriorVenetianBlind = 12,
            /// <summary>
            /// Exterior Venetian Blind
            /// </summary>
            ExteriorVenetianBlind = 13,
            /// <summary>
            /// Lateral Left Curtain
            /// </summary>
            LateralLeftCurtain = 14,
            /// <summary>
            /// Lateral Right Curtain
            /// </summary>
            LateralRightCurtain = 15,
            /// <summary>
            /// Central Curtain
            /// </summary>
            CentralCurtain = 16,
            /// <summary>
            /// Roller Shutter
            /// </summary>
            RollerShutter = 17,
            /// <summary>
            /// Exterior Vertical Screen
            /// </summary>
            ExteriorVerticalScreen = 18,
            /// <summary>
            /// Awning Terrace (Patio)
            /// </summary>
            AwningTerracePatio = 19,
            /// <summary>
            /// Awning Vertical Screen
            /// </summary>
            AwningVerticalScreen = 20,
            /// <summary>
            /// Tilt Only Pergola
            /// </summary>
            TiltOnlyPergola = 21,
            /// <summary>
            /// Swinging Shutter
            /// </summary>
            SwingingShutter = 22,
            /// <summary>
            /// Sliding Shutter
            /// </summary>
            SlidingShutter = 23,
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 255,
        }

        /// <summary>
        /// Type
        /// </summary>
        public enum TypeEnum {
            /// <summary>
            /// RollerShade
            /// </summary>
            RollerShade = 0,
            /// <summary>
            /// RollerShade - 2 Motor
            /// </summary>
            RollerShade2Motor = 1,
            /// <summary>
            /// RollerShade - Exterior
            /// </summary>
            RollerShadeExterior = 2,
            /// <summary>
            /// RollerShade - Exterior - 2 Motor
            /// </summary>
            RollerShadeExterior2Motor = 3,
            /// <summary>
            /// Drapery (curtain)
            /// </summary>
            Drapery = 4,
            /// <summary>
            /// Awning
            /// </summary>
            Awning = 5,
            /// <summary>
            /// Shutter
            /// </summary>
            Shutter = 6,
            /// <summary>
            /// Tilt Blind - Tilt Only
            /// </summary>
            TiltBlindTiltOnly = 7,
            /// <summary>
            /// Tilt Blind - Lift &amp; Tilt
            /// </summary>
            TiltBlindLiftAndTilt = 8,
            /// <summary>
            /// Projector Screen
            /// </summary>
            ProjectorScreen = 9,
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 255,
        }

        /// <summary>
        /// Config Status Bitmap
        /// </summary>
        [Flags]
        public enum ConfigStatusBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Device is operational.
            /// </summary>
            Operational = 1,
            /// <summary>
            /// Deprecated and reserved.
            /// </summary>
            OnlineReserved = 2,
            /// <summary>
            /// The lift movement is reversed.
            /// </summary>
            LiftMovementReversed = 4,
            /// <summary>
            /// Supports the PositionAwareLift feature (PA_LF).
            /// </summary>
            LiftPositionAware = 8,
            /// <summary>
            /// Supports the PositionAwareTilt feature (PA_TL).
            /// </summary>
            TiltPositionAware = 16,
            /// <summary>
            /// Uses an encoder for lift.
            /// </summary>
            LiftEncoderControlled = 32,
            /// <summary>
            /// Uses an encoder for tilt.
            /// </summary>
            TiltEncoderControlled = 64,
        }

        /// <summary>
        /// Mode Bitmap
        /// </summary>
        [Flags]
        public enum ModeBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Reverse the lift direction.
            /// </summary>
            MotorDirectionReversed = 1,
            /// <summary>
            /// Perform a calibration.
            /// </summary>
            CalibrationMode = 2,
            /// <summary>
            /// Freeze all motions for maintenance.
            /// </summary>
            MaintenanceMode = 4,
            /// <summary>
            /// Control the LEDs feedback.
            /// </summary>
            LedFeedback = 8,
        }

        /// <summary>
        /// Operational Status Bitmap
        /// </summary>
        [Flags]
        public enum OperationalStatusBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Global operational state.
            /// </summary>
            Global = 1,
            /// <summary>
            /// Lift operational state.
            /// </summary>
            Lift = 1,
            /// <summary>
            /// Tilt operational state.
            /// </summary>
            Tilt = 1,
        }

        /// <summary>
        /// Safety Status Bitmap
        /// </summary>
        [Flags]
        public enum SafetyStatusBitmap {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Movement commands are ignored (locked out). e.g. not granted authorization, outside some time/date range.
            /// </summary>
            RemoteLockout = 1,
            /// <summary>
            /// Tampering detected on sensors or any other safety equipment. Ex: a device has been forcedly moved without its actuator(s).
            /// </summary>
            TamperDetection = 2,
            /// <summary>
            /// Communication failure to sensors or other safety equipment.
            /// </summary>
            FailedCommunication = 4,
            /// <summary>
            /// Device has failed to reach the desired position. e.g. with position aware device, time expired before TargetPosition is reached.
            /// </summary>
            PositionFailure = 8,
            /// <summary>
            /// Motor(s) and/or electric circuit thermal protection activated.
            /// </summary>
            ThermalProtection = 16,
            /// <summary>
            /// An obstacle is preventing actuator movement.
            /// </summary>
            ObstacleDetected = 32,
            /// <summary>
            /// Device has power related issue or limitation e.g. device is running w/ the help of a backup battery or power might not be fully available at the moment.
            /// </summary>
            Power = 64,
            /// <summary>
            /// Local safety sensor (not a direct obstacle) is preventing movements (e.g. Safety EU Standard EN60335).
            /// </summary>
            StopInput = 128,
            /// <summary>
            /// Mechanical problem related to the motor(s) detected.
            /// </summary>
            MotorJammed = 256,
            /// <summary>
            /// PCB, fuse and other electrics problems.
            /// </summary>
            HardwareFailure = 512,
            /// <summary>
            /// Actuator is manually operated and is preventing actuator movement (e.g. actuator is disengaged/decoupled).
            /// </summary>
            ManualOperation = 1024,
            /// <summary>
            /// Protection is activated.
            /// </summary>
            Protection = 2048,
        }
        #endregion Enums

        #region Payloads
        private record GoToLiftValuePayload : TLVPayload {
            public required ushort LiftValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, LiftValue);
                writer.EndContainer();
            }
        }

        private record GoToLiftPercentagePayload : TLVPayload {
            public byte? LiftPercentageValue { get; set; }
            public ushort? LiftPercent100thsValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (LiftPercentageValue != null)
                    writer.WriteByte(0, LiftPercentageValue);
                if (LiftPercent100thsValue != null)
                    writer.WriteUShort(1, LiftPercent100thsValue);
                writer.EndContainer();
            }
        }

        private record GoToTiltValuePayload : TLVPayload {
            public required ushort TiltValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, TiltValue);
                writer.EndContainer();
            }
        }

        private record GoToTiltPercentagePayload : TLVPayload {
            public byte? TiltPercentageValue { get; set; }
            public ushort? TiltPercent100thsValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (TiltPercentageValue != null)
                    writer.WriteByte(0, TiltPercentageValue);
                if (TiltPercent100thsValue != null)
                    writer.WriteUShort(1, TiltPercent100thsValue);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Up Or Open
        /// </summary>
        public async Task<bool> UpOrOpen(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Down Or Close
        /// </summary>
        public async Task<bool> DownOrClose(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Stop Motion
        /// </summary>
        public async Task<bool> StopMotion(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Go To Lift Value
        /// </summary>
        public async Task<bool> GoToLiftValue(SecureSession session, ushort LiftValue) {
            GoToLiftValuePayload requestFields = new GoToLiftValuePayload() {
                LiftValue = LiftValue,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Go To Lift Percentage
        /// </summary>
        public async Task<bool> GoToLiftPercentage(SecureSession session, byte? LiftPercentageValue, ushort? LiftPercent100thsValue) {
            GoToLiftPercentagePayload requestFields = new GoToLiftPercentagePayload() {
                LiftPercentageValue = LiftPercentageValue,
                LiftPercent100thsValue = LiftPercent100thsValue,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Go To Tilt Value
        /// </summary>
        public async Task<bool> GoToTiltValue(SecureSession session, ushort TiltValue) {
            GoToTiltValuePayload requestFields = new GoToTiltValuePayload() {
                TiltValue = TiltValue,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Go To Tilt Percentage
        /// </summary>
        public async Task<bool> GoToTiltPercentage(SecureSession session, byte? TiltPercentageValue, ushort? TiltPercent100thsValue) {
            GoToTiltPercentagePayload requestFields = new GoToTiltPercentagePayload() {
                TiltPercentageValue = TiltPercentageValue,
                TiltPercent100thsValue = TiltPercent100thsValue,
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
        /// Get the Type attribute
        /// </summary>
        public async Task<TypeEnum> GetType(SecureSession session) {
            return (TypeEnum)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Physical Closed Limit Lift attribute
        /// </summary>
        public async Task<ushort> GetPhysicalClosedLimitLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1) ?? 0;
        }

        /// <summary>
        /// Get the Physical Closed Limit Tilt attribute
        /// </summary>
        public async Task<ushort> GetPhysicalClosedLimitTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2) ?? 0;
        }

        /// <summary>
        /// Get the Current Position Lift attribute
        /// </summary>
        public async Task<ushort?> GetCurrentPositionLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3, true) ?? null;
        }

        /// <summary>
        /// Get the Current Position Tilt attribute
        /// </summary>
        public async Task<ushort?> GetCurrentPositionTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 4, true) ?? null;
        }

        /// <summary>
        /// Get the Number Of Actuations Lift attribute
        /// </summary>
        public async Task<ushort> GetNumberOfActuationsLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 5) ?? 0;
        }

        /// <summary>
        /// Get the Number Of Actuations Tilt attribute
        /// </summary>
        public async Task<ushort> GetNumberOfActuationsTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 6) ?? 0;
        }

        /// <summary>
        /// Get the Config Status attribute
        /// </summary>
        public async Task<ConfigStatusBitmap> GetConfigStatus(SecureSession session) {
            return (ConfigStatusBitmap)await GetEnumAttribute(session, 7);
        }

        /// <summary>
        /// Get the Current Position Lift Percentage attribute
        /// </summary>
        public async Task<byte?> GetCurrentPositionLiftPercentage(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 8, true) ?? null;
        }

        /// <summary>
        /// Get the Current Position Tilt Percentage attribute
        /// </summary>
        public async Task<byte?> GetCurrentPositionTiltPercentage(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 9, true) ?? null;
        }

        /// <summary>
        /// Get the Operational Status attribute
        /// </summary>
        public async Task<OperationalStatusBitmap> GetOperationalStatus(SecureSession session) {
            return (OperationalStatusBitmap)await GetEnumAttribute(session, 10);
        }

        /// <summary>
        /// Get the Target Position Lift Percent100ths attribute
        /// </summary>
        public async Task<ushort?> GetTargetPositionLiftPercent100ths(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 11, true) ?? null;
        }

        /// <summary>
        /// Get the Target Position Tilt Percent100ths attribute
        /// </summary>
        public async Task<ushort?> GetTargetPositionTiltPercent100ths(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 12, true) ?? null;
        }

        /// <summary>
        /// Get the End Product Type attribute
        /// </summary>
        public async Task<EndProductTypeEnum> GetEndProductType(SecureSession session) {
            return (EndProductTypeEnum)await GetEnumAttribute(session, 13);
        }

        /// <summary>
        /// Get the Current Position Lift Percent100ths attribute
        /// </summary>
        public async Task<ushort?> GetCurrentPositionLiftPercent100ths(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 14, true) ?? null;
        }

        /// <summary>
        /// Get the Current Position Tilt Percent100ths attribute
        /// </summary>
        public async Task<ushort?> GetCurrentPositionTiltPercent100ths(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 15, true) ?? null;
        }

        /// <summary>
        /// Get the Installed Open Limit Lift attribute
        /// </summary>
        public async Task<ushort> GetInstalledOpenLimitLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16) ?? 0;
        }

        /// <summary>
        /// Get the Installed Closed Limit Lift attribute
        /// </summary>
        public async Task<ushort> GetInstalledClosedLimitLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 17) ?? 65534;
        }

        /// <summary>
        /// Get the Installed Open Limit Tilt attribute
        /// </summary>
        public async Task<ushort> GetInstalledOpenLimitTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 18) ?? 0;
        }

        /// <summary>
        /// Get the Installed Closed Limit Tilt attribute
        /// </summary>
        public async Task<ushort> GetInstalledClosedLimitTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 19) ?? 65534;
        }

        /// <summary>
        /// Get the Mode attribute
        /// </summary>
        public async Task<ModeBitmap> GetMode(SecureSession session) {
            return (ModeBitmap)await GetEnumAttribute(session, 23);
        }

        /// <summary>
        /// Set the Mode attribute
        /// </summary>
        public async Task SetMode (SecureSession session, ModeBitmap value) {
            await SetAttribute(session, 23, value);
        }

        /// <summary>
        /// Get the Safety Status attribute
        /// </summary>
        public async Task<SafetyStatusBitmap> GetSafetyStatus(SecureSession session) {
            return (SafetyStatusBitmap)await GetEnumAttribute(session, 26);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Window Covering Cluster";
        }
    }
}