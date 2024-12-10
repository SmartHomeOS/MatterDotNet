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
    public class EventDataIB : TLVPayload
    {
        /// <inheritdoc />
        public EventDataIB() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public EventDataIB(Memory<byte> data) : this(new TLVReader(data)) {}

        public required EventPathIB Path { get; set; } 
        public required ulong EventNumber { get; set; } 
        public required byte Priority { get; set; } 
        public required ulong EpochTimestamp { get; set; } 
        public required ulong SystemTimestamp { get; set; } 
        public required ulong DeltaEpochTimestamp { get; set; } 
        public required ulong DeltaSystemTimestamp { get; set; } 
        public required object Data { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public EventDataIB(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            Path = new EventPathIB(reader, 0);
            EventNumber = reader.GetULong(1)!.Value;
            Priority = reader.GetByte(2)!.Value;
            EpochTimestamp = reader.GetULong(3)!.Value;
            SystemTimestamp = reader.GetULong(4)!.Value;
            DeltaEpochTimestamp = reader.GetULong(5)!.Value;
            DeltaSystemTimestamp = reader.GetULong(6)!.Value;
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            Path.Serialize(writer, 0);
            writer.WriteULong(1, EventNumber);
            writer.WriteByte(2, Priority);
            writer.WriteULong(3, EpochTimestamp);
            writer.WriteULong(4, SystemTimestamp);
            writer.WriteULong(5, DeltaEpochTimestamp);
            writer.WriteULong(6, DeltaSystemTimestamp);
            writer.EndContainer();
        }
    }
}