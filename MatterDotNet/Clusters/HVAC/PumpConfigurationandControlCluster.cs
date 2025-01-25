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

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.HVAC
{
    /// <summary>
    /// An interface for configuring and controlling pumps.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class PumpConfigurationandControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0200;

        /// <summary>
        /// An interface for configuring and controlling pumps.
        /// </summary>
        [SetsRequiredMembers]
        public PumpConfigurationandControl(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected PumpConfigurationandControl(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            MaxPressure = new ReadAttribute<short?>(cluster, endPoint, 0, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            MaxSpeed = new ReadAttribute<ushort?>(cluster, endPoint, 1, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MaxFlow = new ReadAttribute<ushort?>(cluster, endPoint, 2, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MinConstPressure = new ReadAttribute<short?>(cluster, endPoint, 3, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            MaxConstPressure = new ReadAttribute<short?>(cluster, endPoint, 4, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            MinCompPressure = new ReadAttribute<short?>(cluster, endPoint, 5, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            MaxCompPressure = new ReadAttribute<short?>(cluster, endPoint, 6, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            MinConstSpeed = new ReadAttribute<ushort?>(cluster, endPoint, 7, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MaxConstSpeed = new ReadAttribute<ushort?>(cluster, endPoint, 8, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MinConstFlow = new ReadAttribute<ushort?>(cluster, endPoint, 9, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MaxConstFlow = new ReadAttribute<ushort?>(cluster, endPoint, 10, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MinConstTemp = new ReadAttribute<short?>(cluster, endPoint, 11, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            MaxConstTemp = new ReadAttribute<short?>(cluster, endPoint, 12, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            PumpStatus = new ReadAttribute<PumpStatusBitmap>(cluster, endPoint, 16) {
                Deserialize = x => (PumpStatusBitmap)DeserializeEnum(x)!
            };
            EffectiveOperationMode = new ReadAttribute<OperationModeEnum>(cluster, endPoint, 17) {
                Deserialize = x => (OperationModeEnum)DeserializeEnum(x)!
            };
            EffectiveControlMode = new ReadAttribute<ControlModeEnum>(cluster, endPoint, 18) {
                Deserialize = x => (ControlModeEnum)DeserializeEnum(x)!
            };
            Capacity = new ReadAttribute<short?>(cluster, endPoint, 19, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            Speed = new ReadAttribute<ushort?>(cluster, endPoint, 20, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            LifetimeRunningHours = new ReadWriteAttribute<uint?>(cluster, endPoint, 21, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x000000

            };
            Power = new ReadAttribute<uint?>(cluster, endPoint, 22, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            LifetimeEnergyConsumed = new ReadWriteAttribute<uint?>(cluster, endPoint, 23, true) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x00000000

            };
            OperationMode = new ReadWriteAttribute<OperationModeEnum>(cluster, endPoint, 32) {
                Deserialize = x => (OperationModeEnum)DeserializeEnum(x)!
            };
            ControlMode = new ReadWriteAttribute<ControlModeEnum>(cluster, endPoint, 33) {
                Deserialize = x => (ControlModeEnum)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports operating in constant pressure mode
            /// </summary>
            ConstantPressure = 1,
            /// <summary>
            /// Supports operating in compensated pressure mode
            /// </summary>
            CompensatedPressure = 2,
            /// <summary>
            /// Supports operating in constant flow mode
            /// </summary>
            ConstantFlow = 4,
            /// <summary>
            /// Supports operating in constant speed mode
            /// </summary>
            ConstantSpeed = 8,
            /// <summary>
            /// Supports operating in constant temperature mode
            /// </summary>
            ConstantTemperature = 16,
            /// <summary>
            /// Supports operating in automatic mode
            /// </summary>
            Automatic = 32,
            /// <summary>
            /// Supports operating using local settings
            /// </summary>
            LocalOperation = 64,
        }

        /// <summary>
        /// Operation Mode
        /// </summary>
        public enum OperationModeEnum : byte {
            /// <summary>
            /// The pump is controlled by a setpoint, as defined by a connected remote sensor or by the ControlMode attribute.
            /// </summary>
            Normal = 0,
            /// <summary>
            /// This value sets the pump to run at the minimum possible speed it can without being stopped.
            /// </summary>
            Minimum = 1,
            /// <summary>
            /// This value sets the pump to run at its maximum possible speed.
            /// </summary>
            Maximum = 2,
            /// <summary>
            /// This value sets the pump to run with the local settings of the pump, regardless of what these are.
            /// </summary>
            Local = 3,
        }

        /// <summary>
        /// Control Mode
        /// </summary>
        public enum ControlModeEnum : byte {
            /// <summary>
            /// The pump is running at a constant speed.
            /// </summary>
            ConstantSpeed = 0,
            /// <summary>
            /// The pump will regulate its speed to maintain a constant differential pressure over its flanges.
            /// </summary>
            ConstantPressure = 1,
            /// <summary>
            /// The pump will regulate its speed to maintain a constant differential pressure over its flanges.
            /// </summary>
            ProportionalPressure = 2,
            /// <summary>
            /// The pump will regulate its speed to maintain a constant flow through the pump.
            /// </summary>
            ConstantFlow = 3,
            /// <summary>
            /// The pump will regulate its speed to maintain a constant temperature.
            /// </summary>
            ConstantTemperature = 5,
            /// <summary>
            /// The operation of the pump is automatically optimized to provide the most suitable performance with respect to comfort and energy savings.
            /// </summary>
            Automatic = 7,
        }

        /// <summary>
        /// Pump Status
        /// </summary>
        [Flags]
        public enum PumpStatusBitmap : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0,
            /// <summary>
            /// A fault related to the system or pump device is detected.
            /// </summary>
            DeviceFault = 0x0001,
            /// <summary>
            /// A fault related to the supply to the pump is detected.
            /// </summary>
            SupplyFault = 0x0002,
            /// <summary>
            /// Setpoint is too low to achieve.
            /// </summary>
            SpeedLow = 0x0004,
            /// <summary>
            /// Setpoint is too high to achieve.
            /// </summary>
            SpeedHigh = 0x0008,
            /// <summary>
            /// Device control is overridden by hardware, such as an external STOP button or via a local HMI.
            /// </summary>
            LocalOverride = 0x0010,
            /// <summary>
            /// Pump is currently running
            /// </summary>
            Running = 0x0020,
            /// <summary>
            /// A remote pressure sensor is used as the sensor for the regulation of the pump.
            /// </summary>
            RemotePressure = 0x0040,
            /// <summary>
            /// A remote flow sensor is used as the sensor for the regulation of the pump.
            /// </summary>
            RemoteFlow = 0x0080,
            RemoteTemperature = 0x00100,
        }
        #endregion Enums

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
        /// Max Pressure Attribute
        /// </summary>
        public required ReadAttribute<short?> MaxPressure { get; init; }

        /// <summary>
        /// Max Speed Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MaxSpeed { get; init; }

        /// <summary>
        /// Max Flow Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MaxFlow { get; init; }

        /// <summary>
        /// Min Const Pressure Attribute
        /// </summary>
        public required ReadAttribute<short?> MinConstPressure { get; init; }

        /// <summary>
        /// Max Const Pressure Attribute
        /// </summary>
        public required ReadAttribute<short?> MaxConstPressure { get; init; }

        /// <summary>
        /// Min Comp Pressure Attribute
        /// </summary>
        public required ReadAttribute<short?> MinCompPressure { get; init; }

        /// <summary>
        /// Max Comp Pressure Attribute
        /// </summary>
        public required ReadAttribute<short?> MaxCompPressure { get; init; }

        /// <summary>
        /// Min Const Speed Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MinConstSpeed { get; init; }

        /// <summary>
        /// Max Const Speed Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MaxConstSpeed { get; init; }

        /// <summary>
        /// Min Const Flow Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MinConstFlow { get; init; }

        /// <summary>
        /// Max Const Flow Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MaxConstFlow { get; init; }

        /// <summary>
        /// Min Const Temp Attribute
        /// </summary>
        public required ReadAttribute<short?> MinConstTemp { get; init; }

        /// <summary>
        /// Max Const Temp Attribute
        /// </summary>
        public required ReadAttribute<short?> MaxConstTemp { get; init; }

        /// <summary>
        /// Pump Status Attribute
        /// </summary>
        public required ReadAttribute<PumpStatusBitmap> PumpStatus { get; init; }

        /// <summary>
        /// Effective Operation Mode Attribute
        /// </summary>
        public required ReadAttribute<OperationModeEnum> EffectiveOperationMode { get; init; }

        /// <summary>
        /// Effective Control Mode Attribute
        /// </summary>
        public required ReadAttribute<ControlModeEnum> EffectiveControlMode { get; init; }

        /// <summary>
        /// Capacity Attribute
        /// </summary>
        public required ReadAttribute<short?> Capacity { get; init; }

        /// <summary>
        /// Speed Attribute
        /// </summary>
        public required ReadAttribute<ushort?> Speed { get; init; }

        /// <summary>
        /// Lifetime Running Hours Attribute
        /// </summary>
        public required ReadWriteAttribute<uint?> LifetimeRunningHours { get; init; }

        /// <summary>
        /// Power Attribute
        /// </summary>
        public required ReadAttribute<uint?> Power { get; init; }

        /// <summary>
        /// Lifetime Energy Consumed Attribute
        /// </summary>
        public required ReadWriteAttribute<uint?> LifetimeEnergyConsumed { get; init; }

        /// <summary>
        /// Operation Mode Attribute
        /// </summary>
        public required ReadWriteAttribute<OperationModeEnum> OperationMode { get; init; }

        /// <summary>
        /// Control Mode Attribute
        /// </summary>
        public required ReadWriteAttribute<ControlModeEnum> ControlMode { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Pump Configuration and Control";
        }
    }
}