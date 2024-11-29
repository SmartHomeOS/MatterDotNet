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
    public class SessionParameter : TLVPayload
    {
        /// <inheritdoc />
        public SessionParameter() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public SessionParameter(Memory<byte> data) : this(new TLVReader(data)) {}

        public uint? SESSION_IDLE_INTERVAL { get; set; } 
        public uint? SESSION_ACTIVE_INTERVAL { get; set; } 
        public ushort? SESSION_ACTIVE_THRESHOLD { get; set; } 
        public required ushort DATA_MODEL_REVISION { get; set; } 
        public required ushort INTERACTION_MODEL_REVISION { get; set; } 
        public required uint SPECIFICATION_VERSION { get; set; } 
        public required ushort MAX_PATHS_PER_INVOKE { get; set; } 

        /// <inheritdoc />
        [SetsRequiredMembers]
        public SessionParameter(TLVReader reader) {
            reader.StartStructure();
            if (reader.IsTag(1))
                SESSION_IDLE_INTERVAL = reader.GetUInt(1);
            if (reader.IsTag(2))
                SESSION_ACTIVE_INTERVAL = reader.GetUInt(2);
            if (reader.IsTag(3))
                SESSION_ACTIVE_THRESHOLD = reader.GetUShort(3);
            DATA_MODEL_REVISION = reader.GetUShort(4).Value;
            INTERACTION_MODEL_REVISION = reader.GetUShort(5).Value;
            SPECIFICATION_VERSION = reader.GetUInt(6).Value;
            MAX_PATHS_PER_INVOKE = reader.GetUShort(7).Value;
        }

        /// <inheritdoc />
        public override void Serialize(TLVWriter writer) {
            writer.StartStructure();
            if (SESSION_IDLE_INTERVAL != null)
                writer.WriteUInt(1, SESSION_IDLE_INTERVAL);
            if (SESSION_ACTIVE_INTERVAL != null)
                writer.WriteUInt(2, SESSION_ACTIVE_INTERVAL);
            if (SESSION_ACTIVE_THRESHOLD != null)
                writer.WriteUShort(3, SESSION_ACTIVE_THRESHOLD);
            writer.WriteUShort(4, DATA_MODEL_REVISION);
            writer.WriteUShort(5, INTERACTION_MODEL_REVISION);
            writer.WriteUInt(6, SPECIFICATION_VERSION);
            writer.WriteUShort(7, MAX_PATHS_PER_INVOKE);
            writer.EndContainer();
        }
    }
}