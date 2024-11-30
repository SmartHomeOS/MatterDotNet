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
    public class Sigma2 : TLVPayload
    {
        /// <inheritdoc />
        public Sigma2() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma2(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] ResponderRandom { get; set; } 
        public required ushort ResponderSessionId { get; set; } 
        public required byte[] ResponderEphPubKey { get; set; } 
        public required byte[] Encrypted2 { get; set; } 
        public SessionParameter? ResponderSessionParams { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma2(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            ResponderRandom = reader.GetBytes(1)!;
            ResponderSessionId = reader.GetUShort(2)!.Value;
            ResponderEphPubKey = reader.GetBytes(3)!;
            Encrypted2 = reader.GetBytes(4)!;
            if (reader.IsTag(5))
                ResponderSessionParams = new SessionParameter(reader);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, ResponderRandom, 1);
            writer.WriteUShort(2, ResponderSessionId);
            writer.WriteBytes(3, ResponderEphPubKey, 1);
            writer.WriteBytes(4, Encrypted2, 0);
            if (ResponderSessionParams != null)
                ResponderSessionParams.Serialize(writer, 5);
            writer.EndContainer();
        }
    }
}