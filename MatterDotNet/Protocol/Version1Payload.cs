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
    internal class Version1Payload : IPayload
    {
        public ExchangeFlags Flags { get; init; }
        public byte OpCode { get; init; } //Depends on Protocol
        public ushort ExchangeID { get; init; }
        public ushort VendorID { get; init; }
        public ProtocolType Protocol { get; init; }
        public uint AckCounter { get; init; }
        public object Payload { get; init; }

        public Version1Payload(ReadOnlySpan<byte> payload)
        {
            Flags = (ExchangeFlags)payload[0];
            OpCode = payload[1];
            ExchangeID = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(2, 2));
            if ((Flags & ExchangeFlags.VendorPresent) == ExchangeFlags.VendorPresent)
            {
                VendorID = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(4, 2));
                payload = payload.Slice(2);
            }
            Protocol = (ProtocolType)BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(4, 2));
            if ((Flags & ExchangeFlags.Acknowledgement) == ExchangeFlags.Acknowledgement)
            {
                AckCounter = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(6, 2));
                payload = payload.Slice(2);
            }
            if ((Flags & ExchangeFlags.SecuredExtensions) == ExchangeFlags.SecuredExtensions)
            {
                ushort len = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(6, 2));
                payload = payload.Slice(2 + len);
            }
            Payload = payload.Slice(6).ToArray();
        }

        public bool Serialize(PayloadWriter stream)
        {
            throw new NotImplementedException();
        }
    }
}