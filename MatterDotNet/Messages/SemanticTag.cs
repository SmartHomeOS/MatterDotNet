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

namespace MatterDotNet.Messages
{
    public record SemanticTag : TLVPayload
    {
        /// <inheritdoc />
        public SemanticTag() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public SemanticTag(Memory<byte> data) : this(new TLVReader(data)) {}

        public ushort? MfgCode { get; set; } 
        public required byte NamespaceID { get; set; } 
        public required byte Tag { get; set; } 
        public string? Label { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public SemanticTag(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            if (reader.IsTag(0))
                MfgCode = reader.GetUShort(0, true);
            NamespaceID = reader.GetByte(1)!.Value;
            Tag = reader.GetByte(2)!.Value;
            if (reader.IsTag(3))
                Label = reader.GetString(3);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            if (MfgCode != null)
                writer.WriteUShort(0, MfgCode);
            writer.WriteByte(1, NamespaceID);
            writer.WriteByte(2, Tag);
            if (Label != null)
                writer.WriteString(3, Label);
            writer.EndContainer();
        }
    }
}