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
    internal class PayloadWriter
    {
        private readonly Memory<byte> data;
        private int pos;

        public PayloadWriter(Memory<byte> data, int pos = 0)
        {
            this.data = data;
            this.pos = pos;
        }

        public PayloadWriter(int capacity)
        {
            this.data = new byte[capacity];
            pos = 0;
        }

        public void Write(byte value)
        {
            data.Span[pos++] = value;
        }

        public void Write(byte[] bytes)
        {
            bytes.CopyTo(data.Slice(pos).Span);
            pos += bytes.Length;
        }

        public void Write(ReadOnlySpan<byte> bytes)
        {
            bytes.CopyTo(data.Slice(pos).Span);
            pos += bytes.Length;
        }

        public void Write(PayloadWriter payload)
        {
            payload.CopyTo(data.Slice(pos));
            pos += payload.Length;
        }

        public void Write(Memory<byte> bytes)
        {
            bytes.CopyTo(data.Slice(pos));
            pos += bytes.Length;
        }

        public void Write(int value)
        {
            BinaryPrimitives.WriteInt32LittleEndian(data.Span.Slice(pos, 4), value);
            pos += 4;
        }

        public void Write(uint value)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(data.Span.Slice(pos, 4), value);
            pos += 4;
        }

        public void Write(ulong value)
        {
            BinaryPrimitives.WriteUInt64LittleEndian(data.Span.Slice(pos, 8), value);
            pos += 8;
        }

        public void Write(short value)
        {
            BinaryPrimitives.WriteInt16LittleEndian(data.Span.Slice(pos, 2), value);
            pos += 2;
        }

        public void Write(ushort value)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(data.Span.Slice(pos, 2), value);
            pos += 2;
        }

        public void Seek(int offset)
        {
            pos += offset;
        }

        public int Length { get { return pos; } }

        public Memory<byte> GetPayload()
        {
            return data.Slice(0, pos);
        }

        private void CopyTo(Memory<byte> slice)
        {
            data.CopyTo(slice);
        }
    }
}
