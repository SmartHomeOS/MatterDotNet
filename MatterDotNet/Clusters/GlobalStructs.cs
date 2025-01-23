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
using MatterDotNet.Protocol.Payloads.Status;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters
{
        /// <summary>
        /// Atomic Attribute Status
        /// </summary>
        public record AtomicAttributeStatus : TLVPayload {
            /// <summary>
            /// Atomic Attribute Status
            /// </summary>
            public AtomicAttributeStatus() { }

            /// <summary>
            /// Atomic Attribute Status
            /// </summary>
            [SetsRequiredMembers]
            public AtomicAttributeStatus(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                AttributeID = reader.GetUInt(0)!.Value;
                StatusCode = (IMStatusCode)reader.GetByte(1)!.Value;
            }
            public required uint AttributeID { get; set; }
            public required IMStatusCode StatusCode { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, AttributeID);
                writer.WriteByte(1, (byte)StatusCode);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Location Descriptor
        /// </summary>
        public record LocationDescriptor : TLVPayload {
            /// <summary>
            /// Location Descriptor
            /// </summary>
            public LocationDescriptor() { }

            /// <summary>
            /// Location Descriptor
            /// </summary>
            [SetsRequiredMembers]
            public LocationDescriptor(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LocationName = reader.GetString(0, false, 128)!;
                FloorNumber = reader.GetShort(1, true);
                AreaType = (AreaTypeTag)reader.GetUShort(2)!.Value;
            }
            public required string LocationName { get; set; }
            public required short? FloorNumber { get; set; }
            public required AreaTypeTag? AreaType { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, LocationName, 128);
                writer.WriteShort(1, FloorNumber);
                writer.WriteUShort(2, (ushort?)AreaType);
                writer.EndContainer();
            }
        }
}