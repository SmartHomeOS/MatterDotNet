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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Thermostat User Interface Configuration Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ThermostatUserInterfaceConfigurationCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0204;

        /// <summary>
        /// Thermostat User Interface Configuration Cluster
        /// </summary>
        public ThermostatUserInterfaceConfigurationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ThermostatUserInterfaceConfigurationCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Keypad Lockout
        /// </summary>
        public enum KeypadLockoutEnum {
            /// <summary>
            /// All functionality available to the user
            /// </summary>
            NoLockout = 0,
            /// <summary>
            /// Level 1 reduced functionality
            /// </summary>
            Lockout1 = 1,
            /// <summary>
            /// Level 2 reduced functionality
            /// </summary>
            Lockout2 = 2,
            /// <summary>
            /// Level 3 reduced functionality
            /// </summary>
            Lockout3 = 3,
            /// <summary>
            /// Level 4 reduced functionality
            /// </summary>
            Lockout4 = 4,
            /// <summary>
            /// Least functionality available to the user
            /// </summary>
            Lockout5 = 5,
        }

        /// <summary>
        /// Schedule Programming Visibility
        /// </summary>
        public enum ScheduleProgrammingVisibilityEnum {
            /// <summary>
            /// Local schedule programming functionality is enabled at the thermostat
            /// </summary>
            ScheduleProgrammingPermitted = 0,
            /// <summary>
            /// Local schedule programming functionality is disabled at the thermostat
            /// </summary>
            ScheduleProgrammingDenied = 1,
        }

        /// <summary>
        /// Temperature Display Mode
        /// </summary>
        public enum TemperatureDisplayModeEnum {
            /// <summary>
            /// Temperature displayed in °C
            /// </summary>
            Celsius = 0,
            /// <summary>
            /// Temperature displayed in °F
            /// </summary>
            Fahrenheit = 1,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Get the Temperature Display Mode attribute
        /// </summary>
        public async Task<TemperatureDisplayModeEnum> GetTemperatureDisplayMode(SecureSession session) {
            return (TemperatureDisplayModeEnum?)await GetEnumAttribute(session, 0) ?? TemperatureDisplayModeEnum.Celsius;
        }

        /// <summary>
        /// Set the Temperature Display Mode attribute
        /// </summary>
        public async Task SetTemperatureDisplayMode (SecureSession session, TemperatureDisplayModeEnum? value = TemperatureDisplayModeEnum.Celsius) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Keypad Lockout attribute
        /// </summary>
        public async Task<KeypadLockoutEnum> GetKeypadLockout(SecureSession session) {
            return (KeypadLockoutEnum?)await GetEnumAttribute(session, 1) ?? KeypadLockoutEnum.NoLockout;
        }

        /// <summary>
        /// Set the Keypad Lockout attribute
        /// </summary>
        public async Task SetKeypadLockout (SecureSession session, KeypadLockoutEnum? value = KeypadLockoutEnum.NoLockout) {
            await SetAttribute(session, 1, value);
        }

        /// <summary>
        /// Get the Schedule Programming Visibility attribute
        /// </summary>
        public async Task<ScheduleProgrammingVisibilityEnum> GetScheduleProgrammingVisibility(SecureSession session) {
            return (ScheduleProgrammingVisibilityEnum?)await GetEnumAttribute(session, 2) ?? ScheduleProgrammingVisibilityEnum.ScheduleProgrammingPermitted;
        }

        /// <summary>
        /// Set the Schedule Programming Visibility attribute
        /// </summary>
        public async Task SetScheduleProgrammingVisibility (SecureSession session, ScheduleProgrammingVisibilityEnum? value = ScheduleProgrammingVisibilityEnum.ScheduleProgrammingPermitted) {
            await SetAttribute(session, 2, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thermostat User Interface Configuration Cluster";
        }
    }
}