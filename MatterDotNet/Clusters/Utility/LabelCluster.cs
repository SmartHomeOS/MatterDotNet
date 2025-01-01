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
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Label Cluster
    /// </summary>
    public class LabelCluster : ClusterBase
    {

        /// <summary>
        /// Label Cluster
        /// </summary>
        public LabelCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Records
        /// <summary>
        /// Label
        /// </summary>
        public record Label : TLVPayload {
            [SetsRequiredMembers]
            internal Label(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LabelField = reader.GetString(0, false)!;
                Value = reader.GetString(1, false)!;
            }
            public required string LabelField { get; set; } = "";
            public required string Value { get; set; } = "";
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, LabelField, 16);
                writer.WriteString(1, Value, 16);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Label Cluster";
        }
    }
}