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
    public record TimedRequestMessage : TLVPayload
    {
        /// <inheritdoc />
        public TimedRequestMessage() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public TimedRequestMessage(Memory<byte> data) : this(new TLVReader(data)) {}

        public required ushort Timeout { get; set; } 
        public required byte InteractionModelRevision { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public TimedRequestMessage(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            Timeout = reader.GetUShort(0)!.Value;
            InteractionModelRevision = reader.GetByte(255)!.Value;
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteUShort(0, Timeout);
            writer.WriteByte(255, InteractionModelRevision);
            writer.EndContainer();
        }
    }
}