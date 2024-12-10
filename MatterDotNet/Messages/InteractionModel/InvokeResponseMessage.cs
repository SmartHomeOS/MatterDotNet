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
    public class InvokeResponseMessage : TLVPayload
    {
        /// <inheritdoc />
        public InvokeResponseMessage() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public InvokeResponseMessage(Memory<byte> data) : this(new TLVReader(data)) {}

        public required bool SuppressResponse { get; set; } 
        public required InvokeResponseIB[] InvokeResponses { get; set; } 
        public required bool MoreChunkedMessages { get; set; } 
        public required byte InteractionModelRevision { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public InvokeResponseMessage(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            SuppressResponse = reader.GetBool(0)!.Value;
            {
                reader.StartArray(1);
                List<InvokeResponseIB> items = new();
                while (!reader.IsEndContainer()) {
                    items.Add(new InvokeResponseIB(reader, 0));
                }
                InvokeResponses = items.ToArray();
            }
            MoreChunkedMessages = reader.GetBool(2)!.Value;
            InteractionModelRevision = reader.GetByte(255)!.Value;
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            writer.WriteBool(0, SuppressResponse);
            {
                writer.StartArray(1);
                foreach (var item in InvokeResponses) {
                    item.Serialize(writer, 0);
                }
                writer.EndContainer();
            }
            writer.WriteBool(2, MoreChunkedMessages);
            writer.WriteByte(255, InteractionModelRevision);
            writer.EndContainer();
        }
    }
}