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
    /// This cluster is used to describe the configuration and capabilities of a Device's power system.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class PowerSourceConfiguration : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x002E;

        /// <summary>
        /// This cluster is used to describe the configuration and capabilities of a Device's power system.
        /// </summary>
        [SetsRequiredMembers]
        public PowerSourceConfiguration(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected PowerSourceConfiguration(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            Sources = new ReadAttribute<ushort[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ushort[] list = new ushort[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetUShort(i)!.Value;
                    return list;
                }
            };
        }

        #region Attributes
        /// <summary>
        /// Sources Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort[]> Sources { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Power Source Configuration";
        }
    }
}