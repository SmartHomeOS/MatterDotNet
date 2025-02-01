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
    /// Attributes and commands for configuring the measurement of illuminance, and reporting illuminance measurements.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class IlluminanceMeasurement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0400;

        /// <summary>
        /// Attributes and commands for configuring the measurement of illuminance, and reporting illuminance measurements.
        /// </summary>
        [SetsRequiredMembers]
        public IlluminanceMeasurement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected IlluminanceMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            MeasuredValue = new ReportAttribute<ushort?>(cluster, endPoint, 0, true) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            MinMeasuredValue = new ReadAttribute<ushort?>(cluster, endPoint, 1, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            MaxMeasuredValue = new ReadAttribute<ushort?>(cluster, endPoint, 2, true) {
                Deserialize = x => (ushort?)(dynamic?)x
            };
            Tolerance = new ReadAttribute<ushort>(cluster, endPoint, 3) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
            LightSensorType = new ReadAttribute<LightSensorTypeEnum?>(cluster, endPoint, 4, true) {
                Deserialize = x => (LightSensorTypeEnum?)DeserializeEnum(x)
            };
        }

        #region Enums
        /// <summary>
        /// Light Sensor Type
        /// </summary>
        public enum LightSensorTypeEnum : byte {
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
        /// Measured Value Attribute [Read/Event]
        /// </summary>
        public required ReportAttribute<ushort?> MeasuredValue { get; init; }

        /// <summary>
        /// Min Measured Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> MinMeasuredValue { get; init; }

        /// <summary>
        /// Max Measured Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort?> MaxMeasuredValue { get; init; }

        /// <summary>
        /// Tolerance Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> Tolerance { get; init; }

        /// <summary>
        /// Light Sensor Type Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<LightSensorTypeEnum?> LightSensorType { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Illuminance Measurement";
        }
    }
}