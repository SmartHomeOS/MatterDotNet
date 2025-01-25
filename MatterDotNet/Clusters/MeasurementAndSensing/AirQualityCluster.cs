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
    /// Attributes for reporting air quality classification
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class AirQuality : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x005B;

        /// <summary>
        /// Attributes for reporting air quality classification
        /// </summary>
        [SetsRequiredMembers]
        public AirQuality(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected AirQuality(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            AirQualityAttribute = new ReadAttribute<AirQualityEnum>(cluster, endPoint, 0) {
                Deserialize = x => (AirQualityEnum)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Cluster supports the Fair air quality level
            /// </summary>
            Fair = 1,
            /// <summary>
            /// Cluster supports the Moderate air quality level
            /// </summary>
            Moderate = 2,
            /// <summary>
            /// Cluster supports the Very poor air quality level
            /// </summary>
            VeryPoor = 4,
            /// <summary>
            /// Cluster supports the Extremely poor air quality level
            /// </summary>
            ExtremelyPoor = 8,
        }

        /// <summary>
        /// Air Quality
        /// </summary>
        public enum AirQualityEnum : byte {
            /// <summary>
            /// The air quality is unknown.
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// The air quality is good.
            /// </summary>
            Good = 1,
            /// <summary>
            /// The air quality is fair.
            /// </summary>
            Fair = 2,
            /// <summary>
            /// The air quality is moderate.
            /// </summary>
            Moderate = 3,
            /// <summary>
            /// The air quality is poor.
            /// </summary>
            Poor = 4,
            /// <summary>
            /// The air quality is very poor.
            /// </summary>
            VeryPoor = 5,
            /// <summary>
            /// The air quality is extremely poor.
            /// </summary>
            ExtremelyPoor = 6,
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
        /// Air Quality Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<AirQualityEnum> AirQualityAttribute { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Air Quality";
        }
    }
}