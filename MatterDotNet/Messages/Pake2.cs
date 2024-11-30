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
    public class Pake2 : TLVPayload
    {
        /// <inheritdoc />
        public Pake2() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Pake2(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] PB { get; set; } 
        public required byte[] CB { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Pake2(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            PB = reader.GetBytes(1)!;
            CB = reader.GetBytes(2)!;
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, PB, 1);
            writer.WriteBytes(2, CB, 1);
            writer.EndContainer();
        }
    }
}