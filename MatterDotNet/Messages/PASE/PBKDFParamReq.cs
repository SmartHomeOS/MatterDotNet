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

namespace MatterDotNet.Messages.PASE
{
    public record PBKDFParamReq : TLVPayload
    {
        /// <inheritdoc />
        public PBKDFParamReq() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public PBKDFParamReq(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] InitiatorRandom { get; set; } 
        public required ushort InitiatorSessionId { get; set; } 
        public required ushort PasscodeId { get; set; } 
        public required bool HasPBKDFParameters { get; set; } 
        public SessionParameter? InitiatorSessionParams { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public PBKDFParamReq(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            InitiatorRandom = reader.GetBytes(1)!;
            InitiatorSessionId = reader.GetUShort(2)!.Value;
            PasscodeId = reader.GetUShort(3)!.Value;
            HasPBKDFParameters = reader.GetBool(4)!.Value;
            if (reader.IsTag(5))
                InitiatorSessionParams = new SessionParameter(reader, 5);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, InitiatorRandom, 1);
            writer.WriteUShort(2, InitiatorSessionId);
            writer.WriteUShort(3, PasscodeId);
            writer.WriteBool(4, HasPBKDFParameters);
            if (InitiatorSessionParams != null)
                InitiatorSessionParams.Serialize(writer, 5);
            writer.EndContainer();
        }
    }
}