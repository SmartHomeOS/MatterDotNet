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
    public record Sigma2Tbedata : TLVPayload
    {
        /// <inheritdoc />
        public Sigma2Tbedata() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma2Tbedata(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] ResponderNOC { get; set; } 
        public byte[]? ResponderICAC { get; set; } 
        public required byte[] Signature { get; set; } 
        public required byte[] ResumptionID { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        internal Sigma2Tbedata(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            ResponderNOC = reader.GetBytes(1)!;
            if (reader.IsTag(2))
                ResponderICAC = reader.GetBytes(2);
            Signature = reader.GetBytes(3, false, 64, 64)!;
            ResumptionID = reader.GetBytes(4, false, 16, 16)!;
            reader.EndContainer();
        }

        /// <inheritdoc />
        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, ResponderNOC);
            if (ResponderICAC != null)
                writer.WriteBytes(2, ResponderICAC);
            writer.WriteBytes(3, Signature, 64, 64);
            writer.WriteBytes(4, ResumptionID, 16, 16);
            writer.EndContainer();
        }
    }
}