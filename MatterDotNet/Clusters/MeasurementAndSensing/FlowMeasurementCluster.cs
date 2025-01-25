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

namespace MatterDotNet.Clusters.MeasurementAndSensing
{
    /// <summary>
    /// Attributes and commands for configuring the measurement of flow, and reporting flow measurements.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class FlowMeasurement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0404;

        /// <summary>
        /// Attributes and commands for configuring the measurement of flow, and reporting flow measurements.
        /// </summary>
        [SetsRequiredMembers]
        public FlowMeasurement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected FlowMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            MeasuredValue = new ReadAttribute<ushort?>(cluster, endPoint, 0, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MinMeasuredValue = new ReadAttribute<ushort?>(cluster, endPoint, 1, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MaxMeasuredValue = new ReadAttribute<ushort?>(cluster, endPoint, 2, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            Tolerance = new ReadAttribute<ushort>(cluster, endPoint, 3) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
        }

        #region Attributes
        /// <summary>
        /// Measured Value Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MeasuredValue { get; init; }

        /// <summary>
        /// Min Measured Value Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MinMeasuredValue { get; init; }

        /// <summary>
        /// Max Measured Value Attribute
        /// </summary>
        public required ReadAttribute<ushort?> MaxMeasuredValue { get; init; }

        /// <summary>
        /// Tolerance Attribute
        /// </summary>
        public required ReadAttribute<ushort> Tolerance { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Flow Measurement";
        }
    }
}