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

using System.Text;

namespace MatterDotNet.Security
{
    /// <summary>
    /// Utility functions for byte spans
    /// </summary>
    public static class SpanUtil
    {
        /// <summary>
        /// Combine multiple arrays
        /// </summary>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] ret = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, ret, offset, array.Length);
                offset += array.Length;
            }
            return ret;
        }

        /// <summary>
        /// Combine multiple arrays
        /// </summary>
        /// <param name="span1"></param>
        /// <param name="span2"></param>
        /// <returns></returns>
        public static byte[] Combine(ReadOnlySpan<byte> span1, ReadOnlySpan<byte> span2)
        {
            byte[] ret = new byte[span1.Length + span2.Length];
            span1.CopyTo(ret);
            span2.CopyTo(ret.AsSpan(span1.Length));
            return ret;
        }

        /// <summary>
        /// Fill a span with a count of vals
        /// </summary>
        /// <param name="val"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Span<byte> Fill(byte val, int count)
        {
            Span<byte> ret = new byte[count];
            if (val != 0x0)
                ret.Fill(val);
            return ret;
        }

        /// <summary>
        /// Pad the given number of zeros onto a span
        /// </summary>
        /// <param name="val"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Span<byte> PadZeros(Span<byte> val, int count)
        {
            if (count <= 0)
                return val;
            Memory<byte> ret = new byte[val.Length + count];
            if (val.Length > 0)
                val.CopyTo(ret.Span);
            return ret.Span;
        }

        /// <summary>
        /// Get the leftmost values
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Span<byte> Leftmost(ReadOnlySpan<byte> data, int length)
        {
            Span<byte> ret = new byte[(length + 7) >> 3];
            int bytes = length >> 3;
            data.Slice(0, bytes).CopyTo(ret);
            ret[bytes] = (byte)(data[bytes] & (0xFF00 >> (length % 8)));
            return ret;
        }

        /// <summary>
        /// XOR two spans
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Span<byte> XOR(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Invalid Byte Array Sizes");
            Memory<byte> ret = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
                ret.Span[i] = (byte)(a[i] ^ b[i]);
            return ret.Span;
        }

        /// <summary>
        /// Increment a value stored as a span
        /// </summary>
        /// <param name="mem"></param>
        public static void Increment(Span<byte> mem)
        {
            for (int i = mem.Length - 1; i >= 0; i--)
            {
                mem[i] += 1;
                if (mem[i] != 0x0)
                    return;
            }
        }
    }
}
