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
    public record EventReportIB : TLVPayload
    {
        /// <inheritdoc />
        public EventReportIB() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public EventReportIB(Memory<byte> data) : this(new TLVReader(data)) {}

        public required EventStatusIB EventStatus { get; set; } 
        public required EventDataIB EventData { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public EventReportIB(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            EventStatus = new EventStatusIB(reader, 0);
            EventData = new EventDataIB(reader, 1);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            EventStatus.Serialize(writer, 0);
            EventData.Serialize(writer, 1);
            writer.EndContainer();
        }
    }
}