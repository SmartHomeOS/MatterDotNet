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
        [SetsRequiredMembers]
        public WindowCovering(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected WindowCovering(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Type = new ReadAttribute<TypeEnum>(cluster, endPoint, 0) {
                Deserialize = x => (TypeEnum)DeserializeEnum(x)!
            };
            PhysicalClosedLimitLift = new ReadAttribute<ushort>(cluster, endPoint, 1) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            PhysicalClosedLimitTilt = new ReadAttribute<ushort>(cluster, endPoint, 2) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            CurrentPositionLift = new ReadAttribute<ushort?>(cluster, endPoint, 3, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            CurrentPositionTilt = new ReadAttribute<ushort?>(cluster, endPoint, 4, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            NumberOfActuationsLift = new ReadAttribute<ushort>(cluster, endPoint, 5) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            NumberOfActuationsTilt = new ReadAttribute<ushort>(cluster, endPoint, 6) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            ConfigStatus = new ReadAttribute<ConfigStatusBitmap>(cluster, endPoint, 7) {
                Deserialize = x => (ConfigStatusBitmap)DeserializeEnum(x)!
            };
            CurrentPositionLiftPercentage = new ReadAttribute<byte?>(cluster, endPoint, 8, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            CurrentPositionTiltPercentage = new ReadAttribute<byte?>(cluster, endPoint, 9, true) {
                Deserialize = x => (byte?)(dynamic?)x
            };
            OperationalStatus = new ReadAttribute<OperationalStatusBitmap>(cluster, endPoint, 10) {
                Deserialize = x => (OperationalStatusBitmap)DeserializeEnum(x)!
            };
            TargetPositionLiftPercent = new ReadAttribute<decimal?>(cluster, endPoint, 11, true) {
                Deserialize = x => (decimal?)(dynamic?)x
            };
            TargetPositionTiltPercent = new ReadAttribute<decimal?>(cluster, endPoint, 12, true) {
                Deserialize = x => (decimal?)(dynamic?)x
            };
            EndProductType = new ReadAttribute<EndProductTypeEnum>(cluster, endPoint, 13) {
                Deserialize = x => (EndProductTypeEnum)DeserializeEnum(x)!
            };
            CurrentPositionLiftPercent = new ReadAttribute<decimal?>(cluster, endPoint, 14, true) {
                Deserialize = x => (decimal?)(dynamic?)x
            };
            CurrentPositionTiltPercent = new ReadAttribute<decimal?>(cluster, endPoint, 15, true) {
                Deserialize = x => (decimal?)(dynamic?)x
            };
            InstalledOpenLimitLift = new ReadAttribute<ushort>(cluster, endPoint, 16) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            InstalledClosedLimitLift = new ReadAttribute<ushort>(cluster, endPoint, 17) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0xFFFF

            };
            InstalledOpenLimitTilt = new ReadAttribute<ushort>(cluster, endPoint, 18) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            InstalledClosedLimitTilt = new ReadAttribute<ushort>(cluster, endPoint, 19) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0xFFFF

            };
            Mode = new ReadWriteAttribute<ModeBitmap>(cluster, endPoint, 23) {
                Deserialize = x => (ModeBitmap)DeserializeEnum(x)!
            };
            SafetyStatus = new ReadAttribute<SafetyStatusBitmap>(cluster, endPoint, 26) {
                Deserialize = x => (SafetyStatusBitmap)DeserializeEnum(x)!
            };
        }

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
        public enum TypeEnum : byte {
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
            /// Tilt Blind - Lift & Tilt
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
        public enum EndProductTypeEnum : byte {
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
        public enum ModeBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
        public enum OperationalStatusBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
        public enum ConfigStatusBitmap : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
        public enum SafetyStatusBitmap : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
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
                writer.WriteUDecimal(0, LiftPercentValue);
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
                writer.WriteUDecimal(0, TiltPercentValue);
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
        /// Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<TypeEnum> Type { get; init; }

        /// <summary>
        /// Physical Closed Limit Lift Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> PhysicalClosedLimitLift { get; init; }

        /// <summary>
        /// Physical Closed Limit Tilt Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> PhysicalClosedLimitTilt { get; init; }

        /// <summary>
        /// Current Position Lift Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> CurrentPositionLift { get; init; }

        /// <summary>
        /// Current Position Tilt Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> CurrentPositionTilt { get; init; }

        /// <summary>
        /// Number Of Actuations Lift Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> NumberOfActuationsLift { get; init; }

        /// <summary>
        /// Number Of Actuations Tilt Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> NumberOfActuationsTilt { get; init; }

        /// <summary>
        /// Config Status Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ConfigStatusBitmap> ConfigStatus { get; init; }

        /// <summary>
        /// Current Position Lift Percentage [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> CurrentPositionLiftPercentage { get; init; }

        /// <summary>
        /// Current Position Tilt Percentage [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte?> CurrentPositionTiltPercentage { get; init; }

        /// <summary>
        /// Operational Status Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<OperationalStatusBitmap> OperationalStatus { get; init; }

        /// <summary>
        /// Target Position Lift Percent100ths [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> TargetPositionLiftPercent { get; init; }

        /// <summary>
        /// Target Position Tilt Percent100ths [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> TargetPositionTiltPercent { get; init; }

        /// <summary>
        /// End Product Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<EndProductTypeEnum> EndProductType { get; init; }

        /// <summary>
        /// Current Position Lift Percent100ths [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> CurrentPositionLiftPercent { get; init; }

        /// <summary>
        /// Current Position Tilt Percent100ths [%] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> CurrentPositionTiltPercent { get; init; }

        /// <summary>
        /// Installed Open Limit Lift Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> InstalledOpenLimitLift { get; init; }

        /// <summary>
        /// Installed Closed Limit Lift Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> InstalledClosedLimitLift { get; init; }

        /// <summary>
        /// Installed Open Limit Tilt Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> InstalledOpenLimitTilt { get; init; }

        /// <summary>
        /// Installed Closed Limit Tilt Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> InstalledClosedLimitTilt { get; init; }

        /// <summary>
        /// Mode Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ModeBitmap> Mode { get; init; }

        /// <summary>
        /// Safety Status Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<SafetyStatusBitmap> SafetyStatus { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Window Covering";
        }
    }
}