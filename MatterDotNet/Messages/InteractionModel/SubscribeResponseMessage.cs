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

namespace MatterDotNet.Messages.InteractionModel
{
    public record SubscribeResponseMessage : TLVPayload
    {
        /// <inheritdoc />
        public SubscribeResponseMessage() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public SubscribeResponseMessage(Memory<byte> data) : this(new TLVReader(data)) {}

        public required uint SubscriptionID { get; set; } 
        public required ushort MaxInterval { get; set; } 
        public required byte InteractionModelRevision { get; set; } 

        [SetsRequiredMembers]
        internal SubscribeResponseMessage(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            SubscriptionID = reader.GetUInt(0)!.Value;
            MaxInterval = reader.GetUShort(2)!.Value;
            InteractionModelRevision = reader.GetByte(255)!.Value;
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            writer.WriteUInt(0, SubscriptionID);
            writer.WriteUShort(2, MaxInterval);
            writer.WriteByte(255, InteractionModelRevision);
            writer.EndContainer();
        }
    }
}