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

using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Cryptography
{
    internal class AesCtr : IDisposable
    {
        private const int BLOCK_SIZE = 16;
        private readonly byte[] counter = new byte[BLOCK_SIZE];

        private readonly ICryptoTransform counterEncryptor;
        private bool isDisposed;

        public AesCtr(byte[] key, byte[] initialCounter)
        {
            isDisposed = false;

            SymmetricAlgorithm aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;

            Buffer.BlockCopy(initialCounter, 0, counter, 0, BLOCK_SIZE);

            var zeroIv = new byte[BLOCK_SIZE];
            counterEncryptor = aes.CreateEncryptor(key, zeroIv);
        }

        public Span<byte> EncryptDecrypt(Span<byte> input)
        {
            if (isDisposed)
                throw new ObjectDisposedException("AES CTR disposed");

            Span<byte> output = new byte[input.Length];
            int offset = 0, numBytes = input.Length;
            byte[] block = new byte[BLOCK_SIZE];

            while (numBytes > 0)
            {
                counterEncryptor.TransformBlock(counter, 0, BLOCK_SIZE, block, 0);

                for (int i = 0; i < BLOCK_SIZE; i++)
                {
                    if (++counter[i] != 0)
                        break;
                }

                if (numBytes <= BLOCK_SIZE)
                {
                    for (int i = 0; i < numBytes; i++)
                        output[i + offset] = (byte)(input[i + offset] ^ block[i]);
                    return output;
                }

                for (int i = 0; i < BLOCK_SIZE; i++)
                    output[i + offset] = (byte)(input[i + offset] ^ block[i]);

                numBytes -= BLOCK_SIZE;
                offset += BLOCK_SIZE;
            }
            return output;
        }

        public void Dispose()
        {
            if (!isDisposed)
                counterEncryptor?.Dispose();

            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
