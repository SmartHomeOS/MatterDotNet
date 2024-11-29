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
    public class Crypto_PBKDFParameterSet : TLVPayload
    {
        /// <inheritdoc />
        public Crypto_PBKDFParameterSet() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Crypto_PBKDFParameterSet(Memory<byte> data) : this(new TLVReader(data)) {}

        public required uint Iterations { get; set; } 
        public required byte[] Salt { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Crypto_PBKDFParameterSet(TLVReader reader) {
            reader.StartStructure();
            Iterations = reader.GetUInt(1).Value;
            Salt = reader.GetBytes(2)!;
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer) {
            writer.StartStructure();
            writer.WriteUInt(1, Iterations);
            writer.WriteBytes(2, Salt, 1);
            writer.EndContainer();
        }
    }
}