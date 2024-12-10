// MatterDotNet Copyright (C) 2024 
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

namespace MatterDotNet.Messages.InteractionModel
{
    public class EventStatusIB : TLVPayload
    {
        /// <inheritdoc />
        public EventStatusIB() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public EventStatusIB(Memory<byte> data) : this(new TLVReader(data)) {}

        public required EventPathIB Path { get; set; } 
        public required StatusIB Status { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public EventStatusIB(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            Path = new EventPathIB(reader, 0);
            Status = new StatusIB(reader, 1);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            Path.Serialize(writer, 0);
            Status.Serialize(writer, 1);
            writer.EndContainer();
        }
    }
}