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

namespace MatterDotNet.Messages.CASE
{
    public record Sigma2Tbsdata : TLVPayload
    {
        /// <inheritdoc />
        public Sigma2Tbsdata() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma2Tbsdata(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] ResponderNOC { get; set; } 
        public byte[]? ResponderICAC { get; set; } 
        public required byte[] ResponderEphPubKey { get; set; } 
        public required byte[] InitiatorEphPubKey { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma2Tbsdata(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            ResponderNOC = reader.GetBytes(1)!;
            if (reader.IsTag(2))
                ResponderICAC = reader.GetBytes(2);
            ResponderEphPubKey = reader.GetBytes(3)!;
            InitiatorEphPubKey = reader.GetBytes(4)!;
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, ResponderNOC, 0);
            if (ResponderICAC != null)
                writer.WriteBytes(2, ResponderICAC, 0);
            writer.WriteBytes(3, ResponderEphPubKey, 1);
            writer.WriteBytes(4, InitiatorEphPubKey, 1);
            writer.EndContainer();
        }
    }
}