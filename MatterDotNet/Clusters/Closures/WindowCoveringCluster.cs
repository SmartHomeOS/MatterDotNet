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

namespace MatterDotNet.Clusters.Closures
{
    /// <summary>
    /// Provides an interface for controlling and adjusting automatic window coverings. 
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 5)]
    public class WindowCovering : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0102;

        /// <summary>
        /// Provides an interface for controlling and adjusting automatic window coverings. 
        /// </summary>
        public WindowCovering(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected WindowCovering(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        /// Type
        /// </summary>
        public enum Type : byte {
            /// <summary>
            /// RollerShade
            /// </summary>
            RollerShade = 0x0,
            /// <summary>
            /// RollerShade - 2 Motor
            /// </summary>
            RollerShade2Motor = 0x1,
            /// <summary>
            /// RollerShade - Exterior
            /// </summary>
            RollerShadeExterior = 0x2,
            /// <summary>
            /// RollerShade - Exterior - 2 Motor
            /// </summary>
            RollerShadeExterior2Motor = 0x3,
            /// <summary>
            /// Drapery (curtain)
            /// </summary>
            Drapery = 0x4,
            /// <summary>
            /// Awning
            /// </summary>
            Awning = 0x5,
            /// <summary>
            /// Shutter
            /// </summary>
            Shutter = 0x6,
            /// <summary>
            /// Tilt Blind - Tilt Only
            /// </summary>
            TiltBlindTiltOnly = 0x7,
            /// <summary>
            /// Tilt Blind - Lift &amp; Tilt
            /// </summary>
            TiltBlindLiftAndTilt = 0x8,
            /// <summary>
            /// Projector Screen
            /// </summary>
            ProjectorScreen = 0x9,
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 0xFF,
        }

        /// <summary>
        /// End Product Type
        /// </summary>
        public enum EndProductType : byte {
            /// <summary>
            /// Simple Roller Shade
            /// </summary>
            RollerShade = 0x0,
            /// <summary>
            /// Roman Shade
            /// </summary>
            RomanShade = 0x1,
            /// <summary>
            /// Balloon Shade
            /// </summary>
            BalloonShade = 0x2,
            /// <summary>
            /// Woven Wood
            /// </summary>
            WovenWood = 0x3,
            /// <summary>
            /// Pleated Shade
            /// </summary>
            PleatedShade = 0x4,
            /// <summary>
            /// Cellular Shade
            /// </summary>
            CellularShade = 0x5,
            /// <summary>
            /// Layered Shade
            /// </summary>
            LayeredShade = 0x6,
            /// <summary>
            /// Layered Shade 2D
            /// </summary>
            LayeredShade2D = 0x7,
            /// <summary>
            /// Sheer Shade
            /// </summary>
            SheerShade = 0x8,
            /// <summary>
            /// Tilt Only Interior Blind
            /// </summary>
            TiltOnlyInteriorBlind = 0x9,
            /// <summary>
            /// Interior Blind
            /// </summary>
            InteriorBlind = 0xA,
            /// <summary>
            /// Vertical Blind, Strip Curtain
            /// </summary>
            VerticalBlindStripCurtain = 0xB,
            /// <summary>
            /// Interior Venetian Blind
            /// </summary>
            InteriorVenetianBlind = 0xC,
            /// <summary>
            /// Exterior Venetian Blind
            /// </summary>
            ExteriorVenetianBlind = 0xD,
            /// <summary>
            /// Lateral Left Curtain
            /// </summary>
            LateralLeftCurtain = 0xE,
            /// <summary>
            /// Lateral Right Curtain
            /// </summary>
            LateralRightCurtain = 0xF,
            /// <summary>
            /// Central Curtain
            /// </summary>
            CentralCurtain = 0x10,
            /// <summary>
            /// Roller Shutter
            /// </summary>
            RollerShutter = 0x11,
            /// <summary>
            /// Exterior Vertical Screen
            /// </summary>
            ExteriorVerticalScreen = 0x12,
            /// <summary>
            /// Awning Terrace (Patio)
            /// </summary>
            AwningTerracePatio = 0x13,
            /// <summary>
            /// Awning Vertical Screen
            /// </summary>
            AwningVerticalScreen = 0x14,
            /// <summary>
            /// Tilt Only Pergola
            /// </summary>
            TiltOnlyPergola = 0x15,
            /// <summary>
            /// Swinging Shutter
            /// </summary>
            SwingingShutter = 0x16,
            /// <summary>
            /// Sliding Shutter
            /// </summary>
            SlidingShutter = 0x17,
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 0xFF,
        }

        /// <summary>
        /// Mode
        /// </summary>
        [Flags]
        public enum Mode : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Reverse the lift direction.
            /// </summary>
            MotorDirectionReversed = 0x01,
            /// <summary>
            /// Perform a calibration.
            /// </summary>
            CalibrationMode = 0x02,
            /// <summary>
            /// Freeze all motions for maintenance.
            /// </summary>
            MaintenanceMode = 0x04,
            /// <summary>
            /// Control the LEDs feedback.
            /// </summary>
            LedFeedback = 0x08,
        }

        /// <summary>
        /// Operational Status
        /// </summary>
        [Flags]
        public enum OperationalStatus : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Global operational state.
            /// </summary>
            Global = 0x03,
            /// <summary>
            /// Lift operational state.
            /// </summary>
            Lift = 0x0C,
            /// <summary>
            /// Tilt operational state.
            /// </summary>
            Tilt = 0x30,
        }

        /// <summary>
        /// Config Status
        /// </summary>
        [Flags]
        public enum ConfigStatus : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Device is operational.
            /// </summary>
            Operational = 0x01,
            /// <summary>
            /// 
            /// </summary>
            OnlineReserved = 0x02,
            /// <summary>
            /// The lift movement is reversed.
            /// </summary>
            LiftMovementReversed = 0x04,
            /// <summary>
            /// Supports the PositionAwareLift feature (PA_LF).
            /// </summary>
            LiftPositionAware = 0x08,
            /// <summary>
            /// Supports the PositionAwareTilt feature (PA_TL).
            /// </summary>
            TiltPositionAware = 0x10,
            /// <summary>
            /// Uses an encoder for lift.
            /// </summary>
            LiftEncoderControlled = 0x20,
            /// <summary>
            /// Uses an encoder for tilt.
            /// </summary>
            TiltEncoderControlled = 0x40,
        }

        /// <summary>
        /// Safety Status
        /// </summary>
        [Flags]
        public enum SafetyStatus : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// Movement commands are ignored (locked out). e.g. not granted authorization, outside some time/date range.
            /// </summary>
            RemoteLockout = 0x0001,
            /// <summary>
            /// Tampering detected on sensors or any other safety equipment. Ex: a device has been forcedly moved without its actuator(s).
            /// </summary>
            TamperDetection = 0x0002,
            /// <summary>
            /// Communication failure to sensors or other safety equipment.
            /// </summary>
            FailedCommunication = 0x0004,
            /// <summary>
            /// Device has failed to reach the desired position. e.g. with position aware device, time expired before TargetPosition is reached.
            /// </summary>
            PositionFailure = 0x0008,
            /// <summary>
            /// Motor(s) and/or electric circuit thermal protection activated.
            /// </summary>
            ThermalProtection = 0x0010,
            /// <summary>
            /// An obstacle is preventing actuator movement.
            /// </summary>
            ObstacleDetected = 0x0020,
            /// <summary>
            /// Device has power related issue or limitation e.g. device is running w/ the help of a backup battery or power might not be fully available at the moment.
            /// </summary>
            Power = 0x0040,
            /// <summary>
            /// Local safety sensor (not a direct obstacle) is preventing movements (e.g. Safety EU Standard EN60335).
            /// </summary>
            StopInput = 0x0080,
            /// <summary>
            /// Mechanical problem related to the motor(s) detected.
            /// </summary>
            MotorJammed = 0x0100,
            /// <summary>
            /// PCB, fuse and other electrics problems.
            /// </summary>
            HardwareFailure = 0x0200,
            /// <summary>
            /// Actuator is manually operated and is preventing actuator movement (e.g. actuator is disengaged/decoupled).
            /// </summary>
            ManualOperation = 0x0400,
            /// <summary>
            /// Protection is activated.
            /// </summary>
            Protection = 0x0800,
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
            public required decimal LiftPercentValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteDecimal(0, LiftPercentValue);
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
            public required decimal TiltPercentValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteDecimal(0, TiltPercentValue);
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
        public async Task<bool> GoToLiftValue(SecureSession session, ushort liftValue) {
            GoToLiftValuePayload requestFields = new GoToLiftValuePayload() {
                LiftValue = liftValue,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Go To Lift Percentage
        /// </summary>
        public async Task<bool> GoToLiftPercentage(SecureSession session, decimal liftPercentValue) {
            GoToLiftPercentagePayload requestFields = new GoToLiftPercentagePayload() {
                LiftPercentValue = liftPercentValue,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x05, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Go To Tilt Value
        /// </summary>
        public async Task<bool> GoToTiltValue(SecureSession session, ushort tiltValue) {
            GoToTiltValuePayload requestFields = new GoToTiltValuePayload() {
                TiltValue = tiltValue,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Go To Tilt Percentage
        /// </summary>
        public async Task<bool> GoToTiltPercentage(SecureSession session, decimal tiltPercentValue) {
            GoToTiltPercentagePayload requestFields = new GoToTiltPercentagePayload() {
                TiltPercentValue = tiltPercentValue,
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
        public async Task<Type> GetType(SecureSession session) {
            return (Type)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Get the Physical Closed Limit Lift attribute
        /// </summary>
        public async Task<ushort> GetPhysicalClosedLimitLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1) ?? 0x0000;
        }

        /// <summary>
        /// Get the Physical Closed Limit Tilt attribute
        /// </summary>
        public async Task<ushort> GetPhysicalClosedLimitTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2) ?? 0x0000;
        }

        /// <summary>
        /// Get the Current Position Lift attribute
        /// </summary>
        public async Task<ushort?> GetCurrentPositionLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3, true);
        }

        /// <summary>
        /// Get the Current Position Tilt attribute
        /// </summary>
        public async Task<ushort?> GetCurrentPositionTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 4, true);
        }

        /// <summary>
        /// Get the Number Of Actuations Lift attribute
        /// </summary>
        public async Task<ushort> GetNumberOfActuationsLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 5) ?? 0x0000;
        }

        /// <summary>
        /// Get the Number Of Actuations Tilt attribute
        /// </summary>
        public async Task<ushort> GetNumberOfActuationsTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 6) ?? 0x0000;
        }

        /// <summary>
        /// Get the Config Status attribute
        /// </summary>
        public async Task<ConfigStatus> GetConfigStatus(SecureSession session) {
            return (ConfigStatus)await GetEnumAttribute(session, 7);
        }

        /// <summary>
        /// Get the Current Position Lift Percentage [%] attribute
        /// </summary>
        public async Task<byte?> GetCurrentPositionLiftPercentage(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 8, true);
        }

        /// <summary>
        /// Get the Current Position Tilt Percentage [%] attribute
        /// </summary>
        public async Task<byte?> GetCurrentPositionTiltPercentage(SecureSession session) {
            return (byte?)(dynamic?)await GetAttribute(session, 9, true);
        }

        /// <summary>
        /// Get the Operational Status attribute
        /// </summary>
        public async Task<OperationalStatus> GetOperationalStatus(SecureSession session) {
            return (OperationalStatus)await GetEnumAttribute(session, 10);
        }

        /// <summary>
        /// Get the Target Position Lift Percent100ths [%] attribute
        /// </summary>
        public async Task<decimal?> GetTargetPositionLiftPercent100ths(SecureSession session) {
            return (decimal?)(dynamic?)await GetAttribute(session, 11, true);
        }

        /// <summary>
        /// Get the Target Position Tilt Percent100ths [%] attribute
        /// </summary>
        public async Task<decimal?> GetTargetPositionTiltPercent100ths(SecureSession session) {
            return (decimal?)(dynamic?)await GetAttribute(session, 12, true);
        }

        /// <summary>
        /// Get the End Product Type attribute
        /// </summary>
        public async Task<EndProductType> GetEndProductType(SecureSession session) {
            return (EndProductType)await GetEnumAttribute(session, 13);
        }

        /// <summary>
        /// Get the Current Position Lift Percent100ths [%] attribute
        /// </summary>
        public async Task<decimal?> GetCurrentPositionLiftPercent100ths(SecureSession session) {
            return (decimal?)(dynamic?)await GetAttribute(session, 14, true);
        }

        /// <summary>
        /// Get the Current Position Tilt Percent100ths [%] attribute
        /// </summary>
        public async Task<decimal?> GetCurrentPositionTiltPercent100ths(SecureSession session) {
            return (decimal?)(dynamic?)await GetAttribute(session, 15, true);
        }

        /// <summary>
        /// Get the Installed Open Limit Lift attribute
        /// </summary>
        public async Task<ushort> GetInstalledOpenLimitLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 16) ?? 0x0000;
        }

        /// <summary>
        /// Get the Installed Closed Limit Lift attribute
        /// </summary>
        public async Task<ushort> GetInstalledClosedLimitLift(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 17) ?? 0xFFFF;
        }

        /// <summary>
        /// Get the Installed Open Limit Tilt attribute
        /// </summary>
        public async Task<ushort> GetInstalledOpenLimitTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 18) ?? 0x0000;
        }

        /// <summary>
        /// Get the Installed Closed Limit Tilt attribute
        /// </summary>
        public async Task<ushort> GetInstalledClosedLimitTilt(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 19) ?? 0xFFFF;
        }

        /// <summary>
        /// Get the Mode attribute
        /// </summary>
        public async Task<Mode> GetMode(SecureSession session) {
            return (Mode)await GetEnumAttribute(session, 23);
        }

        /// <summary>
        /// Set the Mode attribute
        /// </summary>
        public async Task SetMode (SecureSession session, Mode value) {
            await SetAttribute(session, 23, value);
        }

        /// <summary>
        /// Get the Safety Status attribute
        /// </summary>
        public async Task<SafetyStatus> GetSafetyStatus(SecureSession session) {
            return (SafetyStatus)await GetEnumAttribute(session, 26);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Window Covering";
        }
    }
}