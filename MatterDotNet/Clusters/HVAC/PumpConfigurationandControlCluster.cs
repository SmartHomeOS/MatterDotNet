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
        public PumpConfigurationandControl(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected PumpConfigurationandControl(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum OperationMode : byte {
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
        public enum ControlMode : byte {
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
        public enum PumpStatus : ushort {
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
        /// Get the Max Pressure attribute
        /// </summary>
        public async Task<short?> GetMaxPressure(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Max Speed attribute
        /// </summary>
        public async Task<ushort?> GetMaxSpeed(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1, true);
        }

        /// <summary>
        /// Get the Max Flow attribute
        /// </summary>
        public async Task<ushort?> GetMaxFlow(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2, true);
        }

        /// <summary>
        /// Get the Min Const Pressure attribute
        /// </summary>
        public async Task<short?> GetMinConstPressure(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 3, true);
        }

        /// <summary>
        /// Get the Max Const Pressure attribute
        /// </summary>
        public async Task<short?> GetMaxConstPressure(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 4, true);
        }

        /// <summary>
        /// Get the Min Comp Pressure attribute
        /// </summary>
        public async Task<short?> GetMinCompPressure(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 5, true);
        }

        /// <summary>
        /// Get the Max Comp Pressure attribute
        /// </summary>
        public async Task<short?> GetMaxCompPressure(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 6, true);
        }

        /// <summary>
        /// Get the Min Const Speed attribute
        /// </summary>
        public async Task<ushort?> GetMinConstSpeed(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 7, true);
        }

        /// <summary>
        /// Get the Max Const Speed attribute
        /// </summary>
        public async Task<ushort?> GetMaxConstSpeed(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 8, true);
        }

        /// <summary>
        /// Get the Min Const Flow attribute
        /// </summary>
        public async Task<ushort?> GetMinConstFlow(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 9, true);
        }

        /// <summary>
        /// Get the Max Const Flow attribute
        /// </summary>
        public async Task<ushort?> GetMaxConstFlow(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 10, true);
        }

        /// <summary>
        /// Get the Min Const Temp attribute
        /// </summary>
        public async Task<short?> GetMinConstTemp(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 11, true);
        }

        /// <summary>
        /// Get the Max Const Temp attribute
        /// </summary>
        public async Task<short?> GetMaxConstTemp(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 12, true);
        }

        /// <summary>
        /// Get the Pump Status attribute
        /// </summary>
        public async Task<PumpStatus> GetPumpStatus(SecureSession session) {
            return (PumpStatus)await GetEnumAttribute(session, 16);
        }

        /// <summary>
        /// Get the Effective Operation Mode attribute
        /// </summary>
        public async Task<OperationMode> GetEffectiveOperationMode(SecureSession session) {
            return (OperationMode)await GetEnumAttribute(session, 17);
        }

        /// <summary>
        /// Get the Effective Control Mode attribute
        /// </summary>
        public async Task<ControlMode> GetEffectiveControlMode(SecureSession session) {
            return (ControlMode)await GetEnumAttribute(session, 18);
        }

        /// <summary>
        /// Get the Capacity attribute
        /// </summary>
        public async Task<short?> GetCapacity(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 19, true);
        }

        /// <summary>
        /// Get the Speed attribute
        /// </summary>
        public async Task<ushort?> GetSpeed(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 20, true);
        }

        /// <summary>
        /// Get the Lifetime Running Hours attribute
        /// </summary>
        public async Task<uint?> GetLifetimeRunningHours(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 21, true) ?? 0x000000;
        }

        /// <summary>
        /// Set the Lifetime Running Hours attribute
        /// </summary>
        public async Task SetLifetimeRunningHours (SecureSession session, uint? value = 0x000000) {
            await SetAttribute(session, 21, value, true);
        }

        /// <summary>
        /// Get the Power attribute
        /// </summary>
        public async Task<uint?> GetPower(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 22, true);
        }

        /// <summary>
        /// Get the Lifetime Energy Consumed attribute
        /// </summary>
        public async Task<uint?> GetLifetimeEnergyConsumed(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 23, true) ?? 0x00000000;
        }

        /// <summary>
        /// Set the Lifetime Energy Consumed attribute
        /// </summary>
        public async Task SetLifetimeEnergyConsumed (SecureSession session, uint? value = 0x00000000) {
            await SetAttribute(session, 23, value, true);
        }

        /// <summary>
        /// Get the Operation Mode attribute
        /// </summary>
        public async Task<OperationMode> GetOperationMode(SecureSession session) {
            return (OperationMode)await GetEnumAttribute(session, 32);
        }

        /// <summary>
        /// Set the Operation Mode attribute
        /// </summary>
        public async Task SetOperationMode (SecureSession session, OperationMode value) {
            await SetAttribute(session, 32, value);
        }

        /// <summary>
        /// Get the Control Mode attribute
        /// </summary>
        public async Task<ControlMode> GetControlMode(SecureSession session) {
            return (ControlMode)await GetEnumAttribute(session, 33);
        }

        /// <summary>
        /// Set the Control Mode attribute
        /// </summary>
        public async Task SetControlMode (SecureSession session, ControlMode value) {
            await SetAttribute(session, 33, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Pump Configuration and Control";
        }
    }
}