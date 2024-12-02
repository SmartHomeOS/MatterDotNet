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

using MatterDotNet.Protocol.Payloads;
using System.Buffers.Binary;

namespace MatterDotNet.Messages
{
    public class MessageCounterSyncResponse : IPayload
    {
        public uint SynchronizedCounter { get; set; }
        public ulong Response {  get; set; }

        public MessageCounterSyncResponse(uint synchronizedCounter, ulong response)
        {
            SynchronizedCounter = synchronizedCounter;
            Response = response;
        }

        public MessageCounterSyncResponse(Memory<byte> payload)
        {
            SynchronizedCounter = BinaryPrimitives.ReadUInt32LittleEndian(payload.Span);
            Response = BinaryPrimitives.ReadUInt64LittleEndian(payload.Span.Slice(4, 8));
        }
        public bool Serialize(PayloadWriter stream)
        {
            stream.Write(SynchronizedCounter);
            stream.Write(Response);
            return true;
        }
    }
}
