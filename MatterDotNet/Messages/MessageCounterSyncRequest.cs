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
    public class MessageCounterSyncRequest : IPayload
    {
        ulong Challenge {  get; set; }

        public MessageCounterSyncRequest(Memory<byte> payload)
        {
            Challenge = BinaryPrimitives.ReadUInt64LittleEndian(payload.Span);
        }
        public bool Serialize(PayloadWriter stream)
        {
            stream.Write(Challenge);
            return true;
        }
    }
}
