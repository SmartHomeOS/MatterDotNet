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

namespace MatterDotNet.Messages.InteractionModel
{
    public record WriteResponseMessage : TLVPayload
    {
        /// <inheritdoc />
        public WriteResponseMessage() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public WriteResponseMessage(Memory<byte> data) : this(new TLVReader(data)) {}

        public required AttributeStatusIB[] WriteResponses { get; set; }
        public required byte InteractionModelRevision { get; set; }

        [SetsRequiredMembers]
        internal WriteResponseMessage(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            {
                reader.StartArray(0);
                List<AttributeStatusIB> items = new();
                while (!reader.IsEndContainer()) {
                    items.Add(new AttributeStatusIB(reader, -1));
                }
                reader.EndContainer();
                WriteResponses = items.ToArray();
            }
            InteractionModelRevision = reader.GetByte(255)!.Value;
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            {
                writer.StartArray(0);
                foreach (var item in WriteResponses) {
                    item.Serialize(writer, -1);
                }
                writer.EndContainer();
            }
            writer.WriteByte(255, InteractionModelRevision);
            writer.EndContainer();
        }
    }
}