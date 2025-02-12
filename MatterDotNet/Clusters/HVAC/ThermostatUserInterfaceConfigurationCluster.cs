﻿// MatterDotNet Copyright (C) 2025 
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
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

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
        [SetsRequiredMembers]
        public ThermostatUserInterfaceConfiguration(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ThermostatUserInterfaceConfiguration(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            TemperatureDisplayMode = new ReadWriteAttribute<TemperatureDisplayModeEnum>(cluster, endPoint, 0) {
                Deserialize = x => (TemperatureDisplayModeEnum)DeserializeEnum(x)!
            };
            KeypadLockout = new ReadWriteAttribute<KeypadLockoutEnum>(cluster, endPoint, 1) {
                Deserialize = x => (KeypadLockoutEnum)DeserializeEnum(x)!
            };
            ScheduleProgrammingVisibility = new ReadWriteAttribute<ScheduleProgrammingVisibilityEnum>(cluster, endPoint, 2) {
                Deserialize = x => (ScheduleProgrammingVisibilityEnum)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Keypad Lockout
        /// </summary>
        public enum KeypadLockoutEnum : byte {
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
        public enum ScheduleProgrammingVisibilityEnum : byte {
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
        public enum TemperatureDisplayModeEnum : byte {
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
        /// Temperature Display Mode Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<TemperatureDisplayModeEnum> TemperatureDisplayMode { get; init; }

        /// <summary>
        /// Keypad Lockout Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<KeypadLockoutEnum> KeypadLockout { get; init; }

        /// <summary>
        /// Schedule Programming Visibility Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<ScheduleProgrammingVisibilityEnum> ScheduleProgrammingVisibility { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Thermostat User Interface Configuration";
        }
    }
}