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

namespace MatterDotNet.Messages.InteractionModel
{
    public record ClusterPathIB : TLVPayload
    {
        /// <inheritdoc />
        public ClusterPathIB() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public ClusterPathIB(Memory<byte> data) : this(new TLVReader(data)) {}

        public ulong? Node { get; set; } 
        public ushort? Endpoint { get; set; } 
        public uint? Cluster { get; set; } 

        [SetsRequiredMembers]
        internal ClusterPathIB(TLVReader reader, long structNumber = -1) {
            reader.StartList(structNumber);
            if (reader.IsTag(0))
                Node = reader.GetULong(0);
            if (reader.IsTag(1))
                Endpoint = reader.GetUShort(1);
            if (reader.IsTag(2))
                Cluster = reader.GetUInt(2);
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartList(structNumber);
            if (Node != null)
                writer.WriteULong(0, Node);
            if (Endpoint != null)
                writer.WriteUShort(1, Endpoint);
            if (Cluster != null)
                writer.WriteUInt(2, Cluster);
            writer.EndContainer();
        }
    }
}