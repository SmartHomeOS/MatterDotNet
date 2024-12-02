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
    public class PBKDFParamResp : TLVPayload
    {
        /// <inheritdoc />
        public PBKDFParamResp() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public PBKDFParamResp(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] InitiatorRandom { get; set; } 
        public required byte[] ResponderRandom { get; set; } 
        public required ushort ResponderSessionId { get; set; } 
        public Crypto_PBKDFParameterSet? Pbkdf_parameters { get; set; } 
        public SessionParameter? ResponderSessionParams { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public PBKDFParamResp(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            InitiatorRandom = reader.GetBytes(1)!;
            ResponderRandom = reader.GetBytes(2)!;
            ResponderSessionId = reader.GetUShort(3)!.Value;
            if (reader.IsTag(4))
                Pbkdf_parameters = new Crypto_PBKDFParameterSet(reader, 4);
            if (reader.IsTag(5))
                ResponderSessionParams = new SessionParameter(reader, 5);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, InitiatorRandom, 1);
            writer.WriteBytes(2, ResponderRandom, 1);
            writer.WriteUShort(3, ResponderSessionId);
            if (Pbkdf_parameters != null)
                Pbkdf_parameters.Serialize(writer, 4);
            if (ResponderSessionParams != null)
                ResponderSessionParams.Serialize(writer, 5);
            writer.EndContainer();
        }
    }
}