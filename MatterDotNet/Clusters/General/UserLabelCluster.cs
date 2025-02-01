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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The User Label Cluster provides a feature to tag an endpoint with zero or more labels.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class UserLabel : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0041;

        /// <summary>
        /// The User Label Cluster provides a feature to tag an endpoint with zero or more labels.
        /// </summary>
        [SetsRequiredMembers]
        public UserLabel(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected UserLabel(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            LabelList = new ReadWriteAttribute<FixedLabel.Label[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    FixedLabel.Label[] list = new FixedLabel.Label[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new FixedLabel.Label(reader.GetStruct(i)!);
                    return list;
                }
            };
        }

        #region Attributes
        /// <summary>
        /// Label List Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<FixedLabel.Label[]> LabelList { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "User Label";
        }
    }
}