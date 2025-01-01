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
// Author jdomnitz (For ZWaveDotNet)

using MatterDotNet.Security;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Cryptography
{
    internal static class CTR_DRBG
    {
        private const int BLOCK_LEN = 16;
        private const int BLOCK_LEN_BITS = BLOCK_LEN * 8;
        private const int SEED_LEN = 32;
        private static readonly Memory<byte> EMPTY_SEED = new byte[SEED_LEN];

        public static Span<byte> Instantiate(Span<byte> entropy, Span<byte> personalization)
        {
            if (personalization.Length < SEED_LEN)
                personalization = SpanUtil.PadZeros(personalization, SEED_LEN - personalization.Length);

            Span<byte> seed = SpanUtil.XOR(entropy, personalization);
            return Update(seed, SpanUtil.Fill(0, BLOCK_LEN), SpanUtil.Fill(0, BLOCK_LEN));
        }

        public static Span<byte> Generate(ref Span<byte> working_state, int requestedBits)
        {
            var K = working_state.Slice(0, BLOCK_LEN);
            var V = working_state.Slice(BLOCK_LEN, BLOCK_LEN);
            int numBlocks = requestedBits / BLOCK_LEN_BITS;
            if (requestedBits % BLOCK_LEN_BITS != 0)
                numBlocks++;
            Span<byte> temp = new byte[numBlocks * BLOCK_LEN];
            for (int i = 0; i < numBlocks; i++)
            {
                SpanUtil.Increment(V);
                BlockEncrypt(K, V, temp.Slice(i * BLOCK_LEN, BLOCK_LEN));
            }

            working_state = Update(EMPTY_SEED.Span, K, V);
            return SpanUtil.Leftmost(temp.Slice(0, (requestedBits + 7) >> 3), requestedBits);
        }

        private static Span<byte> Update(ReadOnlySpan<byte> provided_data, Span<byte> key, Span<byte> v)
        {
            Span<byte> temp = new byte[SEED_LEN];
            for (int i = 0; i < SEED_LEN; i+= BLOCK_LEN)
            {
                SpanUtil.Increment(v);
                BlockEncrypt(key, v, temp.Slice(i, BLOCK_LEN));
            }
            return SpanUtil.XOR(temp, provided_data);
        }

        public static Span<byte> Reseed(Span<byte> working_state, Span<byte> entropy, Span<byte> additional_input)
        {
            additional_input = SpanUtil.PadZeros(additional_input, SEED_LEN - additional_input.Length);
            Span<byte> seed = SpanUtil.XOR(entropy, additional_input);
            var K = working_state.Slice(0, BLOCK_LEN);
            var V = working_state.Slice(BLOCK_LEN, BLOCK_LEN);
            return Update(seed, K, V);
        }

        private static void BlockEncrypt(Span<byte> key, Span<byte> plaintext, Span<byte> output)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key.ToArray();
                aes.EncryptEcb(plaintext, output, PaddingMode.None);
            }
        }
    }
}
