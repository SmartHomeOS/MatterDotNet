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

using MatterDotNet.Messages.PASE;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Messages.CASE
{
    /// <summary>
    /// Sigma1 Payload
    /// </summary>
    public record Sigma1 : TLVPayload
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
        public byte[]? ResumptionId { get; set; }
        public byte[]? InitiatorResumeMIC { get; set; }

        [SetsRequiredMembers]
        internal Sigma1(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            InitiatorRandom = reader.GetBytes(1, false, 32, 32)!;
            InitiatorSessionId = reader.GetUShort(2)!.Value;
            DestinationId = reader.GetBytes(3, false, 32, 32)!;
            InitiatorEphPubKey = reader.GetBytes(4, false, 65, 65)!;
            if (reader.IsTag(5))
                InitiatorSessionParams = new SessionParameter(reader, 5);
            if (reader.IsTag(6))
                ResumptionId = reader.GetBytes(6, false, 16, 16);
            if (reader.IsTag(7))
                InitiatorResumeMIC = reader.GetBytes(7, false, 16, 16);
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteBytes(1, InitiatorRandom, 32, 32);
            writer.WriteUShort(2, InitiatorSessionId);
            writer.WriteBytes(3, DestinationId, 32, 32);
            writer.WriteBytes(4, InitiatorEphPubKey, 65, 65);
            if (InitiatorSessionParams != null)
                InitiatorSessionParams.Serialize(writer, 5);
            if (ResumptionId != null)
                writer.WriteBytes(6, ResumptionId, 16, 16);
            if (InitiatorResumeMIC != null)
                writer.WriteBytes(7, InitiatorResumeMIC, 16, 16);
            writer.EndContainer();
        }
    }
}