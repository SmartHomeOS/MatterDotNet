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

namespace MatterDotNet.Clusters.Appliances
{
    /// <summary>
    /// This cluster provides a way to access options associated with the operation of a laundry dryer device type.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class LaundryDryerControls : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x004A;

        /// <summary>
        /// This cluster provides a way to access options associated with the operation of a laundry dryer device type.
        /// </summary>
        [SetsRequiredMembers]
        public LaundryDryerControls(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected LaundryDryerControls(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            SupportedDrynessLevels = new ReadAttribute<DrynessLevel[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    DrynessLevel[] list = new DrynessLevel[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (DrynessLevel)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            SelectedDrynessLevel = new ReadWriteAttribute<DrynessLevel?>(cluster, endPoint, 1, true) {
                Deserialize = x => (DrynessLevel?)DeserializeEnum(x)
            };
        }

        #region Enums
        /// <summary>
        /// Dryness Level
        /// </summary>
        public enum DrynessLevel : byte {
            /// <summary>
            /// Provides a low dryness level for the selected mode
            /// </summary>
            Low = 0,
            /// <summary>
            /// Provides the normal level of dryness for the selected mode
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Provides an extra dryness level for the selected mode
            /// </summary>
            Extra = 2,
            /// <summary>
            /// Provides the max dryness level for the selected mode
            /// </summary>
            Max = 3,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Supported Dryness Levels Attribute
        /// </summary>
        public required ReadAttribute<DrynessLevel[]> SupportedDrynessLevels { get; init; }

        /// <summary>
        /// Selected Dryness Level Attribute
        /// </summary>
        public required ReadWriteAttribute<DrynessLevel?> SelectedDrynessLevel { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Laundry Dryer Controls";
        }
    }
}