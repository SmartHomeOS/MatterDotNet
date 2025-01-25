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

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// Nodes should be expected to be deployed to any and all regions of the world. These global regions may have differing preferences for the units in which values are conveyed in communication to a user. As such, Nodes that visually or audibly convey measurable values to the user need a mechanism by which they can be configured to use a user’s preferred unit.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class UnitLocalization : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002d;

        /// <summary>
        /// Nodes should be expected to be deployed to any and all regions of the world. These global regions may have differing preferences for the units in which values are conveyed in communication to a user. As such, Nodes that visually or audibly convey measurable values to the user need a mechanism by which they can be configured to use a user’s preferred unit.
        /// </summary>
        [SetsRequiredMembers]
        public UnitLocalization(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected UnitLocalization(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            TemperatureUnit = new ReadWriteAttribute<TempUnit>(cluster, endPoint, 0) {
                Deserialize = x => (TempUnit)DeserializeEnum(x)!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// The Node can be configured to use different units of temperature when conveying values to a user.
            /// </summary>
            TemperatureUnit = 1,
        }

        /// <summary>
        /// Temp Unit
        /// </summary>
        public enum TempUnit : byte {
            /// <summary>
            /// Temperature conveyed in Fahrenheit
            /// </summary>
            Fahrenheit = 0,
            /// <summary>
            /// Temperature conveyed in Celsius
            /// </summary>
            Celsius = 1,
            /// <summary>
            /// Temperature conveyed in Kelvin
            /// </summary>
            Kelvin = 2,
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
        /// Temperature Unit Attribute
        /// </summary>
        public required ReadWriteAttribute<TempUnit> TemperatureUnit { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Unit Localization";
        }
    }
}