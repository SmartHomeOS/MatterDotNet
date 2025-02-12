// MatterDotNet Copyright (C) 2025 
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
    public record Sigma3Tbedata : TLVPayload
    {
        /// <inheritdoc />
        public Sigma3Tbedata() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma3Tbedata(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] InitiatorNOC { get; set; }
        public byte[]? InitiatorICAC { get; set; }
        public required byte[] Signature { get; set; }

        [SetsRequiredMembers]
        internal Sigma3Tbedata(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            InitiatorNOC = reader.GetBytes(1)!;
            if (reader.IsTag(2))
                InitiatorICAC = reader.GetBytes(2);
            Signature = reader.GetBytes(3, false, 64, 64)!;
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, InitiatorNOC);
            if (InitiatorICAC != null)
                writer.WriteBytes(2, InitiatorICAC);
            writer.WriteBytes(3, Signature, 64, 64);
            writer.EndContainer();
        }
    }
}