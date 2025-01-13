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
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Messages
{
    public record LocationDescriptor : TLVPayload
    {
        /// <inheritdoc />
        public LocationDescriptor() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public LocationDescriptor(Memory<byte> data) : this(new TLVReader(data)) {}

        public required string LocationName { get; set; }
        public required ushort FloorNumber { get; set; }
        public required SemanticTag AreaType { get; set; }

        [SetsRequiredMembers]
        internal LocationDescriptor(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            LocationName = reader.GetString(0, false, 128, 128)!;
            FloorNumber = reader.GetUShort(1)!.Value;
            AreaType = new SemanticTag(reader, 2);
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteString(0, LocationName, 128, 128);
            writer.WriteUShort(1, FloorNumber);
            AreaType.Serialize(writer, 2);
            writer.EndContainer();
        }
    }
}