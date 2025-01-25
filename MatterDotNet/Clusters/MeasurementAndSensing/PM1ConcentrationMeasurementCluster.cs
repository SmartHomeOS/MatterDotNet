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
    /// Attributes for reporting PM1 concentration measurements
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class PM1ConcentrationMeasurement : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x042c;

        /// <summary>
        /// Attributes for reporting PM1 concentration measurements
        /// </summary>
        [SetsRequiredMembers]
        public PM1ConcentrationMeasurement(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected PM1ConcentrationMeasurement(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            MeasuredValue = new ReadAttribute<float?>(cluster, endPoint, 0, true) {
                Deserialize = x => (float?)(dynamic?)x
            };
            MinMeasuredValue = new ReadAttribute<float?>(cluster, endPoint, 1, true) {
                Deserialize = x => (float?)(dynamic?)x
            };
            MaxMeasuredValue = new ReadAttribute<float?>(cluster, endPoint, 2, true) {
                Deserialize = x => (float?)(dynamic?)x
            };
            PeakMeasuredValue = new ReadAttribute<float?>(cluster, endPoint, 3, true) {
                Deserialize = x => (float?)(dynamic?)x
            };
            PeakMeasuredValueWindow = new ReadAttribute<TimeSpan>(cluster, endPoint, 4) {
                Deserialize = x => (TimeSpan?)(dynamic?)x ?? TimeSpan.FromSeconds(1)

            };
            AverageMeasuredValue = new ReadAttribute<float?>(cluster, endPoint, 5, true) {
                Deserialize = x => (float?)(dynamic?)x
            };
            AverageMeasuredValueWindow = new ReadAttribute<TimeSpan>(cluster, endPoint, 6) {
                Deserialize = x => (TimeSpan?)(dynamic?)x ?? TimeSpan.FromSeconds(1)

            };
            Uncertainty = new ReadAttribute<float>(cluster, endPoint, 7) {
                Deserialize = x => (float?)(dynamic?)x ?? 0

            };
            MeasurementUnit = new ReadAttribute<MeasurementUnitEnum>(cluster, endPoint, 8) {
                Deserialize = x => (MeasurementUnitEnum)DeserializeEnum(x)!
            };
            MeasurementMedium = new ReadAttribute<MeasurementMediumEnum>(cluster, endPoint, 9) {
                Deserialize = x => (MeasurementMediumEnum)DeserializeEnum(x)!
            };
            LevelValue = new ReadAttribute<LevelValueEnum>(cluster, endPoint, 10) {
                Deserialize = x => (LevelValueEnum)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Cluster supports numeric measurement of substance
            /// </summary>
            NumericMeasurement = 1,
            /// <summary>
            /// Cluster supports basic level indication for substance using the ConcentrationLevel enum
            /// </summary>
            LevelIndication = 2,
            /// <summary>
            /// Cluster supports the Medium Concentration Level
            /// </summary>
            MediumLevel = 4,
            /// <summary>
            /// Cluster supports the Critical Concentration Level
            /// </summary>
            CriticalLevel = 8,
            /// <summary>
            /// Cluster supports peak numeric measurement of substance
            /// </summary>
            PeakMeasurement = 16,
            /// <summary>
            /// Cluster supports average numeric measurement of substance
            /// </summary>
            AverageMeasurement = 32,
        }

        /// <summary>
        /// Level Value
        /// </summary>
        public enum LevelValueEnum : byte {
            Unknown = 0,
            Low = 1,
            Medium = 2,
            High = 3,
            Critical = 4,
        }

        /// <summary>
        /// Measurement Unit
        /// </summary>
        public enum MeasurementUnitEnum : byte {
            PPM = 0,
            PPB = 1,
            PPT = 2,
            MGM3 = 3,
            UGM3 = 4,
            NGM3 = 5,
            PM3 = 6,
            BQM3 = 7,
        }

        /// <summary>
        /// Measurement Medium
        /// </summary>
        public enum MeasurementMediumEnum : byte {
            Air = 0,
            Water = 1,
            Soil = 2,
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
        /// Measured Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<float?> MeasuredValue { get; init; }

        /// <summary>
        /// Min Measured Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<float?> MinMeasuredValue { get; init; }

        /// <summary>
        /// Max Measured Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<float?> MaxMeasuredValue { get; init; }

        /// <summary>
        /// Peak Measured Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<float?> PeakMeasuredValue { get; init; }

        /// <summary>
        /// Peak Measured Value Window Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<TimeSpan> PeakMeasuredValueWindow { get; init; }

        /// <summary>
        /// Average Measured Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<float?> AverageMeasuredValue { get; init; }

        /// <summary>
        /// Average Measured Value Window Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<TimeSpan> AverageMeasuredValueWindow { get; init; }

        /// <summary>
        /// Uncertainty Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<float> Uncertainty { get; init; }

        /// <summary>
        /// Measurement Unit Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<MeasurementUnitEnum> MeasurementUnit { get; init; }

        /// <summary>
        /// Measurement Medium Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<MeasurementMediumEnum> MeasurementMedium { get; init; }

        /// <summary>
        /// Level Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<LevelValueEnum> LevelValue { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "PM1 Concentration Measurement";
        }
    }
}