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

using System.Globalization;
using System.Text;

namespace MatterDotNet.Security
{
    internal static class SpanUtil
    {
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        public static Memory<byte> Fill(byte val, int count)
        {
            Memory<byte> ret = new byte[count];
            if (val != 0x0)
                ret.Span.Fill(val);
            return ret;
        }

        public static Span<byte> PadZeros(Span<byte> val, int count)
        {
            if (count <= 0)
                return val;
            Memory<byte> ret = new byte[val.Length + count];
            if (val.Length > 0)
                val.CopyTo(ret.Span);
            return ret.Span;
        }

        public static Span<byte> LeftShift1(Span<byte> array)
        {
            Memory<byte> ret = new byte[array.Length];
            for (int i = 0; i < ret.Length - 1; i++)
                ret.Span[i] = (byte)(array[i] << 1 | (array[i + 1] >> 7));
            ret.Span[ret.Length - 1] = (byte)(array[array.Length - 1] << 1);
            return ret.Span;
        }

        public static Span<byte> XOR(Span<byte> a, Span<byte> b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Invalid Byte Array Sizes");
            Memory<byte> ret = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
                ret.Span[i] = (byte)(a[i] ^ b[i]);
            return ret.Span;
        }

        public static byte XOR(Span<byte> a, byte start)
        {
            foreach (byte b in a)
                start ^= b;
            return start;
        }

        public static void Increment(Span<byte> mem)
        {
            for (int i = mem.Length - 1; i >= 0; i--)
            {
                mem[i] += 1;
                if (mem[i] != 0x0)
                    return;
            }
        }

        public static string Print(ReadOnlySpan<byte> span)
        {
            StringBuilder ret = new StringBuilder(span.Length * 3);
            foreach (byte b in span)
            {
                if (ret.Length > 0)
                    ret.Append(' ');
                ret.Append(b.ToString("X2"));
            }
            return ret.ToString();
        }

        public static Span<byte> From(string hexString)
        {
            if (hexString.Length % 2 != 0)
                throw new ArgumentException("Not a hex string");

            Memory<byte> data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
                data.Span[index] = byte.Parse(hexString.AsSpan(index * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);

            return data.Span;
        }
    }
}
