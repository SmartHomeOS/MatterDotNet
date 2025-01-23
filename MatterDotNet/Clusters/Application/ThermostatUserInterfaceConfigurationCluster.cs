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
    /// An interface for configuring the user interface of a thermostat (which may be remote from the thermostat).
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class ThermostatUserInterfaceConfiguration : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0204;

        /// <summary>
        /// An interface for configuring the user interface of a thermostat (which may be remote from the thermostat).
        /// </summary>
        public ThermostatUserInterfaceConfiguration(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ThermostatUserInterfaceConfiguration(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Keypad Lockout
        /// </summary>
        public enum KeypadLockout : byte {
            /// <summary>
            /// All functionality available to the user
            /// </summary>
            NoLockout = 0x00,
            /// <summary>
            /// Level 1 reduced functionality
            /// </summary>
            Lockout1 = 0x01,
            /// <summary>
            /// Level 2 reduced functionality
            /// </summary>
            Lockout2 = 0x02,
            /// <summary>
            /// Level 3 reduced functionality
            /// </summary>
            Lockout3 = 0x03,
            /// <summary>
            /// Level 4 reduced functionality
            /// </summary>
            Lockout4 = 0x04,
            /// <summary>
            /// Least functionality available to the user
            /// </summary>
            Lockout5 = 0x05,
        }

        /// <summary>
        /// Schedule Programming Visibility
        /// </summary>
        public enum ScheduleProgrammingVisibility : byte {
            /// <summary>
            /// Local schedule programming functionality is enabled at the thermostat
            /// </summary>
            ScheduleProgrammingPermitted = 0x00,
            /// <summary>
            /// Local schedule programming functionality is disabled at the thermostat
            /// </summary>
            ScheduleProgrammingDenied = 0x01,
        }

        /// <summary>
        /// Temperature Display Mode
        /// </summary>
        public enum TemperatureDisplayMode : byte {
            /// <summary>
            /// Temperature displayed in °C
            /// </summary>
            Celsius = 0x00,
            /// <summary>
            /// Temperature displayed in °F
            /// </summary>
            Fahrenheit = 0x01,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Get the Temperature Display Mode attribute
        /// </summary>
        public async Task<TemperatureDisplayMode> GetTemperatureDisplayMode(SecureSession session) {
            return (TemperatureDisplayMode)await GetEnumAttribute(session, 0);
        }

        /// <summary>
        /// Set the Temperature Display Mode attribute
        /// </summary>
        public async Task SetTemperatureDisplayMode (SecureSession session, TemperatureDisplayMode value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Keypad Lockout attribute
        /// </summary>
        public async Task<KeypadLockout> GetKeypadLockout(SecureSession session) {
            return (KeypadLockout)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Set the Keypad Lockout attribute
        /// </summary>
        public async Task SetKeypadLockout (SecureSession session, KeypadLockout value) {
            await SetAttribute(session, 1, value);
        }

        /// <summary>
        /// Get the Schedule Programming Visibility attribute
        /// </summary>
        public async Task<ScheduleProgrammingVisibility> GetScheduleProgrammingVisibility(SecureSession session) {
            return (ScheduleProgrammingVisibility)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Set the Schedule Programming Visibility attribute
        /// </summary>
        public async Task SetScheduleProgrammingVisibility (SecureSession session, ScheduleProgrammingVisibility value) {
            await SetAttribute(session, 2, value);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thermostat User Interface Configuration";
        }
    }
}