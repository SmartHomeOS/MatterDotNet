﻿// MatterDotNet Copyright (C) 2024 
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
using System.Security.Cryptography;

namespace MatterDotNet.Security
{
    internal static class Crypto
    {
        /// <summary>
        /// Encrypts the data and returns the MIC (tag)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="payload"></param>
        /// <param name="additionalData"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static ReadOnlySpan<byte> Crypto_AEAD_GenerateEncrypt(byte[] key, Span<byte> payload, ReadOnlySpan<byte> additionalData, byte[] nonce)
        {
            Span<byte> tag = new byte[16];
            AesCcm aes = new AesCcm(key);
            aes.Encrypt(nonce, payload, payload, tag, additionalData);
            return tag;
        }

        /// <summary>
        /// Decrypts the data and validates the MIC
        /// </summary>
        /// <param name="key"></param>
        /// <param name="payload"></param>
        /// <param name="mic"></param>
        /// <param name="additionalData"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static bool Crypto_AEAD_DecryptVerify(byte[] key, Span<byte> payload, ReadOnlySpan<byte> mic, ReadOnlySpan<byte> additionalData, byte[] nonce)
        {
            try
            {
                AesCcm aes = new AesCcm(key);
                aes.Decrypt(nonce, payload, mic, payload, additionalData);
                return true;
            }
            catch(AuthenticationTagMismatchException)
            {
                return false;
            }
        }

        /// <summary>
        /// Performs the decryption of message using the key and a nonce; the output is the decryption of message
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static Span<byte> Crypto_Privacy_Decrypt(byte[] key, Span<byte> message, byte[] nonce)
        {
            AesCtr ctr = new AesCtr(key, nonce);
            return ctr.EncryptDecrypt(message);
        }

        /// <summary>
        /// Performs the Encryption of message using the key and a nonce; the output is the decryption of message
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static Span<byte> Crypto_Privacy_Encrypt(byte[] key, Span<byte> message, byte[] nonce)
        {
            AesCtr ctr = new AesCtr(key, nonce);
            return ctr.EncryptDecrypt(message);
        }

        /// <summary>
        /// Returns the key of len bits derived from inputKey using the salt and the info; len SHALL be a multiple of 8.
        /// </summary>
        /// <param name="inputKey"></param>
        /// <param name="salt"></param>
        /// <param name="info"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static Span<byte> Crypto_KDF(byte[] inputKey, byte[] salt, byte[] info, int len)
        {
            if (len > 256)
                throw new ArgumentException("This wasn't designed for multiple blocks");
            int L = len / 8;
            const int HashLen = 32;
            byte N = (byte)Math.Ceiling((double)(L / HashLen));
            byte[] PRK = HKDF_Extract(salt, inputKey);
            return HMAC_Hash(N, PRK, info);
        }

        private static byte[] HMAC_Hash(byte T, byte[] PRK, byte[] info)
        {
            if (T == 0)
                return [];
            byte[] bytes = SpanUtil.Combine(HMAC_Hash((byte)(T - 1), PRK, info), info, [T]);
            return HMACSHA256.HashData(PRK, bytes);
        }

        public static byte[] HKDF_Extract(byte[] salt, byte[] IKM)
        {
            return HMACSHA256.HashData(salt, IKM);
        }

        /// <summary>
        /// Password-based key derivation function to compute a derived key from a cryptographically weak password
        /// </summary>
        /// <param name="input"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="len">Length in bits</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] Crypto_PBKDF(byte[] input, byte[] salt, int iterations, int len)
        {
            if (iterations < 1000 || iterations > 100000)
                throw new ArgumentOutOfRangeException(nameof(iterations));
            int numBlocks = (int)Math.Ceiling((double)(len / 256));
            byte[] ret = new byte[len / 8];
            for (int i = 0; i < numBlocks; i++)
            {
                byte[] IntI = new byte[4];
                BinaryPrimitives.WriteUInt32BigEndian(IntI, (uint)i);
                Span<byte> T = new byte[32];
                byte[] U = SpanUtil.Combine(salt, IntI);
                for (int j = 1; j < iterations; j++)
                {
                    U = HMACSHA256.HashData(input, U);
                    T = SpanUtil.XOR(T, U);
                }
                T.CopyTo(ret.AsSpan(i * 32, 32));
            }
            return ret;
        }
    }
}
