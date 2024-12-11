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
    public record CommandPathIB : TLVPayload
    {
        /// <inheritdoc />
        public CommandPathIB() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public CommandPathIB(Memory<byte> data) : this(new TLVReader(data)) {}

        public required ushort Endpoint { get; set; } 
        public required uint Cluster { get; set; } 
        public required uint Command { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public CommandPathIB(TLVReader reader, uint structNumber = 0) {
            reader.StartList();
            Endpoint = reader.GetUShort(0)!.Value;
            Cluster = reader.GetUInt(1)!.Value;
            Command = reader.GetUInt(2)!.Value;
            reader.EndContainer();
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer, uint structNumber = 0) {
            writer.StartList();
            writer.WriteUShort(0, Endpoint);
            writer.WriteUInt(1, Cluster);
            writer.WriteUInt(2, Command);
            writer.EndContainer();
        }
    }
}