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

namespace MatterDotNet.Messages.PASE
{
    public record SessionParameter : TLVPayload
    {
        /// <inheritdoc />
        public SessionParameter() {}

        /// <inheritdoc />
        [SetsRequiredMembers]
        public SessionParameter(Memory<byte> data) : this(new TLVReader(data)) {}

        public uint? SessionIdleInterval { get; set; } 
        public uint? SessionActiveInterval { get; set; } 
        public ushort? SessionActiveThreshold { get; set; } 
        public ushort? DataModelRevision { get; set; } 
        public ushort? InteractionModelRevision { get; set; } 
        public uint? SpecificationVersion { get; set; } 
        public ushort? MaxPathsPerInvoke { get; set; } 

        [SetsRequiredMembers]
        internal SessionParameter(TLVReader reader, long structNumber = -1) {
            reader.StartStructure(structNumber);
            if (reader.IsTag(1))
                SessionIdleInterval = reader.GetUInt(1);
            if (reader.IsTag(2))
                SessionActiveInterval = reader.GetUInt(2);
            if (reader.IsTag(3))
                SessionActiveThreshold = reader.GetUShort(3);
            if (reader.IsTag(4))
                DataModelRevision = reader.GetUShort(4);
            if (reader.IsTag(5))
                InteractionModelRevision = reader.GetUShort(5);
            if (reader.IsTag(6))
                SpecificationVersion = reader.GetUInt(6);
            if (reader.IsTag(7))
                MaxPathsPerInvoke = reader.GetUShort(7);
            reader.EndContainer();
        }

        internal override void Serialize(TLVWriter writer, long structNumber = -1) {
            writer.StartStructure(structNumber);
            if (SessionIdleInterval != null)
                writer.WriteUInt(1, SessionIdleInterval);
            if (SessionActiveInterval != null)
                writer.WriteUInt(2, SessionActiveInterval);
            if (SessionActiveThreshold != null)
                writer.WriteUShort(3, SessionActiveThreshold);
            if (DataModelRevision != null)
                writer.WriteUShort(4, DataModelRevision);
            if (InteractionModelRevision != null)
                writer.WriteUShort(5, InteractionModelRevision);
            if (SpecificationVersion != null)
                writer.WriteUInt(6, SpecificationVersion);
            if (MaxPathsPerInvoke != null)
                writer.WriteUShort(7, MaxPathsPerInvoke);
            writer.EndContainer();
        }
    }
}