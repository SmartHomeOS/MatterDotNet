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

using MatterDotNet.Attributes;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Fixed Label Cluster provides a feature for the device to tag an endpoint with zero or more read only labels.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class FixedLabel : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0040;

        /// <summary>
        /// The Fixed Label Cluster provides a feature for the device to tag an endpoint with zero or more read only labels.
        /// </summary>
        [SetsRequiredMembers]
        public FixedLabel(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected FixedLabel(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            LabelList = new ReadAttribute<Label[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Label[] list = new Label[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Label(reader.GetStruct(i)!);
                    return list;
                }
            };
        }

        #region Records
        /// <summary>
        /// Label
        /// </summary>
        public record Label : TLVPayload {
            /// <summary>
            /// Label
            /// </summary>
            public Label() { }

            /// <summary>
            /// Label
            /// </summary>
            [SetsRequiredMembers]
            public Label(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LabelField = reader.GetString(0, false, 16)!;
                Value = reader.GetString(1, false, 16)!;
            }
            public required string LabelField { get; set; }
            public required string Value { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, LabelField, 16);
                writer.WriteString(1, Value, 16);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        /// <summary>
        /// Label List Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Label[]> LabelList { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Fixed Label";
        }
    }
}