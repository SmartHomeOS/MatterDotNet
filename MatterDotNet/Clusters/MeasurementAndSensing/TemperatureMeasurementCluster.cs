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

using MatterDotNet.Attributes;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

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
        [SetsRequiredMembers]
        public TemperatureMeasurement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected TemperatureMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            MeasuredValue = new ReadAttribute<decimal?>(cluster, endPoint, 0, true) {
                Deserialize = x => (decimal?)(dynamic?)x
            };
            MinMeasuredValue = new ReadAttribute<decimal?>(cluster, endPoint, 1, true) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 0x8000

            };
            MaxMeasuredValue = new ReadAttribute<decimal?>(cluster, endPoint, 2, true) {
                Deserialize = x => (decimal?)(dynamic?)x ?? 0x8000

            };
            Tolerance = new ReadAttribute<ushort>(cluster, endPoint, 3) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
        }

        #region Attributes
        /// <summary>
        /// Measured Value [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> MeasuredValue { get; init; }

        /// <summary>
        /// Min Measured Value [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> MinMeasuredValue { get; init; }

        /// <summary>
        /// Max Measured Value [°C] Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<decimal?> MaxMeasuredValue { get; init; }

        /// <summary>
        /// Tolerance Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Tolerance { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Temperature Measurement";
        }
    }
}