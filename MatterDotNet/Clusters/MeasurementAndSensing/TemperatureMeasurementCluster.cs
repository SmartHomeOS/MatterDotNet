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
    /// Attributes and commands for configuring the measurement of temperature, and reporting temperature measurements.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class TemperatureMeasurement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0402;

        /// <summary>
        /// Attributes and commands for configuring the measurement of temperature, and reporting temperature measurements.
        /// </summary>
        public TemperatureMeasurement(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected TemperatureMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Attributes
        /// <summary>
        /// Get the Measured Value attribute
        /// </summary>
        public async Task<short?> GetMeasuredValue(SecureSession session) {
            return (short?)(dynamic?)await GetAttribute(session, 0, true);
        }

        /// <summary>
        /// Get the Min Measured Value attribute
        /// </summary>
        public async Task<short?> GetMinMeasuredValue(SecureSession session)
        {
            return (short?)(dynamic?)await GetAttribute(session, 1, true) ?? short.MinValue;
        }

        /// <summary>
        /// Get the Max Measured Value attribute
        /// </summary>
        public async Task<short?> GetMaxMeasuredValue(SecureSession session)
        {
            return (short?)(dynamic?)await GetAttribute(session, 2, true) ?? short.MinValue;
        }

        /// <summary>
        /// Get the Tolerance attribute
        /// </summary>
        public async Task<ushort> GetTolerance(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3) ?? 0;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Temperature Measurement";
        }
    }
}