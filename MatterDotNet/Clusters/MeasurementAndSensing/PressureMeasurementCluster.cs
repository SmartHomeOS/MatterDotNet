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
    /// Attributes and commands for configuring the measurement of pressure, and reporting pressure measurements.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class PressureMeasurement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0403;

        /// <summary>
        /// Attributes and commands for configuring the measurement of pressure, and reporting pressure measurements.
        /// </summary>
        [SetsRequiredMembers]
        public PressureMeasurement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected PressureMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            MeasuredValue = new ReadAttribute<short?>(cluster, endPoint, 0, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            MinMeasuredValue = new ReadAttribute<short?>(cluster, endPoint, 1, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            MaxMeasuredValue = new ReadAttribute<short?>(cluster, endPoint, 2, true) {
                Deserialize = x => (short?)(dynamic?)x
            };
            Tolerance = new ReadAttribute<ushort>(cluster, endPoint, 3) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            ScaledValue = new ReadAttribute<short?>(cluster, endPoint, 16, true) {
                Deserialize = x => (short?)(dynamic?)x ?? 0

            };
            MinScaledValue = new ReadAttribute<short?>(cluster, endPoint, 17, true) {
                Deserialize = x => (short?)(dynamic?)x ?? 0

            };
            MaxScaledValue = new ReadAttribute<short?>(cluster, endPoint, 18, true) {
                Deserialize = x => (short?)(dynamic?)x ?? 0

            };
            ScaledTolerance = new ReadAttribute<ushort>(cluster, endPoint, 19) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0

            };
            Scale = new ReadAttribute<sbyte>(cluster, endPoint, 20) {
                Deserialize = x => (sbyte?)(dynamic?)x ?? 0

            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Extended range and resolution
            /// </summary>
            Extended = 1,
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
        /// Measured Value Attribute
        /// </summary>
        public required ReadAttribute<short?> MeasuredValue { get; init; }

        /// <summary>
        /// Min Measured Value Attribute
        /// </summary>
        public required ReadAttribute<short?> MinMeasuredValue { get; init; }

        /// <summary>
        /// Max Measured Value Attribute
        /// </summary>
        public required ReadAttribute<short?> MaxMeasuredValue { get; init; }

        /// <summary>
        /// Tolerance Attribute
        /// </summary>
        public required ReadAttribute<ushort> Tolerance { get; init; }

        /// <summary>
        /// Scaled Value Attribute
        /// </summary>
        public required ReadAttribute<short?> ScaledValue { get; init; }

        /// <summary>
        /// Min Scaled Value Attribute
        /// </summary>
        public required ReadAttribute<short?> MinScaledValue { get; init; }

        /// <summary>
        /// Max Scaled Value Attribute
        /// </summary>
        public required ReadAttribute<short?> MaxScaledValue { get; init; }

        /// <summary>
        /// Scaled Tolerance Attribute
        /// </summary>
        public required ReadAttribute<ushort> ScaledTolerance { get; init; }

        /// <summary>
        /// Scale Attribute
        /// </summary>
        public required ReadAttribute<sbyte> Scale { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Pressure Measurement";
        }
    }
}