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

namespace MatterDotNet.Messages.Certificates
{
    public record AttestationElements : TLVPayload
    {
        /// <inheritdoc />
        public AttestationElements() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public AttestationElements(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] Certification_declaration { get; set; } 
        public required byte[] Attestation_nonce { get; set; } 
        public required uint Timestamp { get; set; } 
        public byte[]? Firmware_information { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public AttestationElements(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            Certification_declaration = reader.GetBytes(1)!;
            Attestation_nonce = reader.GetBytes(2, false, 32, 32)!;
            Timestamp = reader.GetUInt(3)!.Value;
            if (reader.IsTag(4))
                Firmware_information = reader.GetBytes(4);
            reader.EndContainer();
        }

        /// <inheritdoc />
        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, Certification_declaration);
            writer.WriteBytes(2, Attestation_nonce, 32, 32);
            writer.WriteUInt(3, Timestamp);
            if (Firmware_information != null)
                writer.WriteBytes(4, Firmware_information);
            writer.EndContainer();
        }
    }
}