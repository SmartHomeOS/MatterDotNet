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

using System.Buffers.Binary;

namespace MatterDotNet.Protocol
{
    internal class Frame
    {
        internal const int MAX_SIZE = 1280;

        public MessageFlags Flags { get; init; }
        public ushort SessionID { get; init; }
        public SecurityFlags Security { get; init; }
        public uint Counter { get; init; }
        public ulong SourceNodeID { get; init; }
        public ulong DestinationNodeID { get; init; }
        public Version1Payload Message { get; init; }
        public bool Valid { get; init; }

        public Frame(ReadOnlySpan<byte> payload)
        {
            Flags = (MessageFlags)payload[0];
            SessionID = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(1, 2));
            Security = (SecurityFlags)payload[3];
            Counter = BinaryPrimitives.ReadUInt32LittleEndian(payload.Slice(4, 4));
            if ((Flags & MessageFlags.SourceNodeID) == MessageFlags.SourceNodeID)
            {
                SourceNodeID = BinaryPrimitives.ReadUInt64LittleEndian(payload.Slice(8, 8));
                payload = payload.Slice(8);
            }
            if ((Flags & MessageFlags.DestinationGroupID) == MessageFlags.DestinationNodeID)
            {
                DestinationNodeID = BinaryPrimitives.ReadUInt64LittleEndian(payload.Slice(8, 8));
                payload = payload.Slice(8);
            }
            else if ((Flags & MessageFlags.DestinationGroupID) == MessageFlags.DestinationGroupID)
            {
                DestinationNodeID = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(8, 2));
                payload = payload.Slice(2);
            }
            if ((Security & SecurityFlags.MessageExtensions) == SecurityFlags.MessageExtensions)
            {
                ushort len = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(8, 2));
                payload = payload.Slice(2 + len);
            }

            //TODO - Decryption
        }
    }
}
