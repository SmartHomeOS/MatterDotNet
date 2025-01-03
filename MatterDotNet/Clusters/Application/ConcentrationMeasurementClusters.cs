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

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Concentration Measurement Clusters
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class ConcentrationMeasurementClusters : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x040C;

        /// <summary>
        /// Concentration Measurement Clusters
        /// </summary>
        public ConcentrationMeasurementClusters(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ConcentrationMeasurementClusters(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum LevelValueEnum {
            /// <summary>
            /// The level is Unknown
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// The level is considered Low
            /// </summary>
            Low = 1,
            /// <summary>
            /// The level is considered Medium
            /// </summary>
            Medium = 2,
            /// <summary>
            /// The level is considered High
            /// </summary>
            High = 3,
            /// <summary>
            /// The level is considered Critical
            /// </summary>
            Critical = 4,
        }

        /// <summary>
        /// Measurement Medium
        /// </summary>
        public enum MeasurementMediumEnum {
            /// <summary>
            /// The measurement is being made in Air
            /// </summary>
            Air = 0,
            /// <summary>
            /// The measurement is being made in Water
            /// </summary>
            Water = 1,
            /// <summary>
            /// The measurement is being made in Soil
            /// </summary>
            Soil = 2,
        }

        /// <summary>
        /// Measurement Unit
        /// </summary>
        public enum MeasurementUnitEnum {
            /// <summary>
            /// Parts per Million (10)
            /// </summary>
            PPM = 0,
            /// <summary>
            /// Parts per Billion (10)
            /// </summary>
            PPB = 1,
            /// <summary>
            /// Parts per Trillion (10)
            /// </summary>
            PPT = 2,
            /// <summary>
            /// Milligram per m
            /// </summary>
            MGM3 = 3,
            /// <summary>
            /// Microgram per m
            /// </summary>
            UGM3 = 4,
            /// <summary>
            /// Nanogram per m
            /// </summary>
            NGM3 = 5,
            /// <summary>
            /// Particles per m
            /// </summary>
            PM3 = 6,
            /// <summary>
            /// Becquerel per m
            /// </summary>
            BQM3 = 7,
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
        /// Get the Measured Value attribute
        /// </summary>
        public async Task<float?> GetMeasuredValue(SecureSession session) {
            return (float?)(dynamic?)await GetAttribute(session, 0, true) ?? null;
        }

        /// <summary>
        /// Get the Min Measured Value attribute
        /// </summary>
        public async Task<float?> GetMinMeasuredValue(SecureSession session) {
            return (float?)(dynamic?)await GetAttribute(session, 1, true) ?? null;
        }

        /// <summary>
        /// Get the Max Measured Value attribute
        /// </summary>
        public async Task<float?> GetMaxMeasuredValue(SecureSession session) {
            return (float?)(dynamic?)await GetAttribute(session, 2, true) ?? null;
        }

        /// <summary>
        /// Get the Peak Measured Value attribute
        /// </summary>
        public async Task<float?> GetPeakMeasuredValue(SecureSession session) {
            return (float?)(dynamic?)await GetAttribute(session, 3, true) ?? null;
        }

        /// <summary>
        /// Get the Peak Measured Value Window attribute
        /// </summary>
        public async Task<TimeSpan> GetPeakMeasuredValueWindow(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 4) ?? TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Get the Average Measured Value attribute
        /// </summary>
        public async Task<float?> GetAverageMeasuredValue(SecureSession session) {
            return (float?)(dynamic?)await GetAttribute(session, 5, true) ?? null;
        }

        /// <summary>
        /// Get the Average Measured Value Window attribute
        /// </summary>
        public async Task<TimeSpan> GetAverageMeasuredValueWindow(SecureSession session) {
            return (TimeSpan?)(dynamic?)await GetAttribute(session, 6) ?? TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Get the Uncertainty attribute
        /// </summary>
        public async Task<float> GetUncertainty(SecureSession session) {
            return (float)(dynamic?)(await GetAttribute(session, 7))!;
        }

        /// <summary>
        /// Get the Measurement Unit attribute
        /// </summary>
        public async Task<MeasurementUnitEnum> GetMeasurementUnit(SecureSession session) {
            return (MeasurementUnitEnum)await GetEnumAttribute(session, 8);
        }

        /// <summary>
        /// Get the Measurement Medium attribute
        /// </summary>
        public async Task<MeasurementMediumEnum> GetMeasurementMedium(SecureSession session) {
            return (MeasurementMediumEnum)await GetEnumAttribute(session, 9);
        }

        /// <summary>
        /// Get the Level Value attribute
        /// </summary>
        public async Task<LevelValueEnum> GetLevelValue(SecureSession session) {
            return (LevelValueEnum)await GetEnumAttribute(session, 10);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Concentration Measurement Clusters";
        }
    }
}