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

namespace MatterDotNet.Clusters.MeasurementAndSensing
{
    /// <summary>
    /// Attributes and commands for configuring the measurement of illuminance, and reporting illuminance measurements.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class IlluminanceMeasurement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0400;

        /// <summary>
        /// Attributes and commands for configuring the measurement of illuminance, and reporting illuminance measurements.
        /// </summary>
        public IlluminanceMeasurement(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected IlluminanceMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Light Sensor Type
        /// </summary>
        public enum LightSensorType : byte {
            /// <summary>
            /// Indicates photodiode sensor type
            /// </summary>
            Photodiode = 0,
            /// <summary>
            /// Indicates CMOS sensor type
            /// </summary>
            CMOS = 1,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Get the Measured Value attribute
        /// </summary>
        public async Task<ushort?> GetMeasuredValue(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 0, true) ?? 0x0000;
        }

        /// <summary>
        /// Get the Min Measured Value attribute
        /// </summary>
        public async Task<ushort?> GetMinMeasuredValue(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1, true);
        }

        /// <summary>
        /// Get the Max Measured Value attribute
        /// </summary>
        public async Task<ushort?> GetMaxMeasuredValue(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2, true);
        }

        /// <summary>
        /// Get the Tolerance attribute
        /// </summary>
        public async Task<ushort> GetTolerance(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Light Sensor Type attribute
        /// </summary>
        public async Task<LightSensorType?> GetLightSensorType(SecureSession session) {
            return (LightSensorType?)await GetEnumAttribute(session, 4, true);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Illuminance Measurement";
        }
    }
}