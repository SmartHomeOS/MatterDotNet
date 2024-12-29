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

using MatterDotNet.Messages.PASE;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Messages.CASE
{
    public record Sigma2Resume : TLVPayload
    {
        /// <inheritdoc />
        public Sigma2Resume() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma2Resume(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] ResumptionID { get; set; } 
        public required byte[] Sigma2ResumeMIC { get; set; } 
        public required ushort ResponderSessionID { get; set; } 
        public SessionParameter? ResponderSessionParams { get; set; } 

        [SetsRequiredMembers]
        internal Sigma2Resume(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            ResumptionID = reader.GetBytes(1, false, 16, 16)!;
            Sigma2ResumeMIC = reader.GetBytes(2, false, 16, 16)!;
            ResponderSessionID = reader.GetUShort(3)!.Value;
            if (reader.IsTag(4))
                ResponderSessionParams = new SessionParameter(reader, 4);
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, ResumptionID, 16, 16);
            writer.WriteBytes(2, Sigma2ResumeMIC, 16, 16);
            writer.WriteUShort(3, ResponderSessionID);
            if (ResponderSessionParams != null)
                ResponderSessionParams.Serialize(writer, 4);
            writer.EndContainer();
        }
    }
}