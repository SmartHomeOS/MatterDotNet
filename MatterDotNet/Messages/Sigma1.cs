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
    public class Sigma1 : TLVPayload
    {
        /// <inheritdoc />
        public Sigma1() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma1(Memory<byte> data) : this(new TLVReader(data)) {}

        public required byte[] InitiatorRandom { get; set; } 
        public required ushort InitiatorSessionId { get; set; } 
        public required byte[] DestinationId { get; set; } 
        public required byte[] InitiatorEphPubKey { get; set; } 
        public SessionParameter? InitiatorSessionParams { get; set; } 
        public byte[]? ResumptionID { get; set; } 
        public byte[]? InitiatorResumeMIC { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public Sigma1(TLVReader reader, uint structNumber = 0) {
            reader.StartStructure(structNumber);
            InitiatorRandom = reader.GetBytes(1)!;
            InitiatorSessionId = reader.GetUShort(2)!.Value;
            DestinationId = reader.GetBytes(3)!;
            InitiatorEphPubKey = reader.GetBytes(4)!;
            if (reader.IsTag(5))
                InitiatorSessionParams = new SessionParameter(reader);
            if (reader.IsTag(6))
                ResumptionID = reader.GetBytes(6);
            if (reader.IsTag(7))
                InitiatorResumeMIC = reader.GetBytes(7);
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, InitiatorRandom, 1);
            writer.WriteUShort(2, InitiatorSessionId);
            writer.WriteBytes(3, DestinationId, 1);
            writer.WriteBytes(4, InitiatorEphPubKey, 1);
            if (InitiatorSessionParams != null)
                InitiatorSessionParams.Serialize(writer, 5);
            if (ResumptionID != null)
                writer.WriteBytes(6, ResumptionID, 1);
            if (InitiatorResumeMIC != null)
                writer.WriteBytes(7, InitiatorResumeMIC, 1);
            writer.EndContainer();
        }
    }
}