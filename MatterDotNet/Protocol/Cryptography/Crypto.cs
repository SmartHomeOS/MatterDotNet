﻿// MatterDotNet Copyright (C) 2025
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

using MatterDotNet.Security;
using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace MatterDotNet.Protocol.Cryptography
{
    /// <summary>
    /// Crypto functions
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int SYMMETRIC_KEY_LENGTH_BITS = 128;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int SYMMETRIC_KEY_LENGTH_BYTES = SYMMETRIC_KEY_LENGTH_BITS / 8;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int AEAD_MIC_LENGTH_BITS = 128;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int AEAD_MIC_LENGTH_BYTES = AEAD_MIC_LENGTH_BITS / 8;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int NONCE_LENGTH_BYTES = 13;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int HASH_LEN_BYTES = SHA256.HashSizeInBytes;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int HASH_LEN_BITS = SHA256.HashSizeInBits;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int GROUP_SIZE_BYTES = 32;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int GROUP_SIZE_BITS = GROUP_SIZE_BYTES * 8;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int PUBLIC_KEY_SIZE_BYTES = (2 * GROUP_SIZE_BYTES) + 1;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int W_SIZE_BYTES = GROUP_SIZE_BYTES + 8;
        /// <summary>
        /// Specification defined value
        /// </summary>
        public const int W_SIZE_BITS = W_SIZE_BYTES * 8;

        /// <summary>
        /// Encrypts the data and returns the MIC (tag)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="payload"></param>
        /// <param name="additionalData"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static ReadOnlySpan<byte> AEAD_GenerateEncrypt(byte[] key, Span<byte> payload, ReadOnlySpan<byte> additionalData, Span<byte> nonce)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));
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
        /// <param name="additionalData"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static bool AEAD_DecryptVerify(byte[] key, Span<byte> payload, ReadOnlySpan<byte> additionalData, ReadOnlySpan<byte> nonce)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));
            try
            {
                AesCcm aes = new AesCcm(key);
                aes.Decrypt(nonce, payload.Slice(0, payload.Length - AEAD_MIC_LENGTH_BYTES), payload.Slice(payload.Length - AEAD_MIC_LENGTH_BYTES, AEAD_MIC_LENGTH_BYTES), payload.Slice(0, payload.Length - AEAD_MIC_LENGTH_BYTES), additionalData);
                return true;
            }
            catch (CryptographicException)
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
        public static Span<byte> Privacy_Decrypt(byte[] key, Span<byte> message, byte[] nonce)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));
            ArgumentNullException.ThrowIfNull(nonce, nameof(nonce));
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
        public static Span<byte> Privacy_Encrypt(byte[] key, Span<byte> message, byte[] nonce)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));
            ArgumentNullException.ThrowIfNull(nonce, nameof(nonce));
            AesCtr ctr = new AesCtr(key, nonce);
            return ctr.EncryptDecrypt(message);
        }

        /// <summary>
        /// Returns the key of len bits derived from inputKey using the salt and the info; len SHALL be a multiple of 8.
        /// </summary>
        /// <param name="inputKey"></param>
        /// <param name="salt"></param>
        /// <param name="info"></param>
        /// <param name="len">Length in bits</param>
        /// <returns></returns>
        public static byte[] KDF(Span<byte> inputKey, Span<byte> salt, Span<byte> info, int len)
        {
            Span<byte> prk = stackalloc byte[HMACSHA256.HashSizeInBytes];
            HKDF.Extract(HashAlgorithmName.SHA256, inputKey, salt, prk);
            byte[] result = new byte[len / 8];
            HKDF.Expand(HashAlgorithmName.SHA256, prk, result, info);
            return result;
        }

        /// <summary>
        /// Password-based key derivation function to compute a derived key from a cryptographically weak password
        /// </summary>
        /// <param name="input"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="klen">Length in bits</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] PBKDF(byte[] input, byte[] salt, int iterations, int klen)
        {
            //Is this a hard requirement?
            //if (iterations < 1000 || iterations > 100000)
            //    throw new ArgumentOutOfRangeException(nameof(iterations));

            int numBlocks = (int)Math.Ceiling((double)klen / 256);
            byte[] ret = new byte[klen / 8];
            for (int i = 1; i <= numBlocks; i++)
            {
                byte[] IntI = new byte[4];
                BinaryPrimitives.WriteUInt32BigEndian(IntI, (uint)i);
                Span<byte> T = new byte[HASH_LEN_BYTES];
                byte[] U = SpanUtil.Combine(salt, IntI);
                for (int j = 1; j <= iterations; j++)
                {
                    U = HMACSHA256.HashData(input, U);
                    T = SpanUtil.XOR(T, U);
                }
                int pos = (i - 1) * HASH_LEN_BYTES;
                int useableBytes = Math.Min(HASH_LEN_BYTES, ret.Length - pos);
                T.Slice(0, useableBytes).CopyTo(ret.AsSpan(pos, useableBytes));
            }
            return ret;
        }

        public static byte[] Hash(ReadOnlySpan<byte> data)
        {
            return SHA256.HashData(data);
        }

        public static byte[] HMAC(ReadOnlySpan<byte> key, ReadOnlySpan<byte> text)
        {
            return HMACSHA256.HashData(key, text);
        }

        public static (byte[] Public, byte[] Private) GenerateKeypair()
        {
            ECDsa ecc = ECDsa.Create();
            ecc.GenerateKey(ECCurve.NamedCurves.nistP256);
            var p = ecc.ExportParameters(true);
            byte[] pub = new byte[PUBLIC_KEY_SIZE_BYTES];
            pub[0] = 0x4;
            p.Q.X!.CopyTo(pub, 33 - p.Q.X.Length);
            p.Q.Y!.CopyTo(pub, PUBLIC_KEY_SIZE_BYTES - p.Q.Y.Length);
            return (pub, p.D!);
        }

        public static uint GeneratePasscode()
        {
            StringBuilder pin = new StringBuilder(10);
            for (int i = 0; i < 8; i++)
                pin.Append((char)Random.Shared.Next(48, 57));
            string ret = pin.ToString();
            bool allSame = true;
            char check = ret[0];
            foreach (char c in ret)
            {
                if (check != c)
                {
                    allSame = false;
                    break;
                }
            }
            if (allSame || ret == "12345678" || ret == "87654321")
                return GeneratePasscode();
            return uint.Parse(ret);
        }

        public static byte[] ECDH(byte[] myPrivateKey, byte[] theirPublicKey)
        {
            ArgumentNullException.ThrowIfNull(myPrivateKey, nameof(myPrivateKey));
            ArgumentNullException.ThrowIfNull(theirPublicKey, nameof(theirPublicKey));
            ECParameters ec = new ECParameters()
            {
                Curve = ECCurve.NamedCurves.nistP256,
                D = myPrivateKey
            };
            ECDiffieHellman ecdh = ECDiffieHellman.Create(ec);
            return ecdh.DeriveRawSecretAgreement(GetKey(theirPublicKey));
        }

        public static ECDiffieHellmanPublicKey GetKey(byte[] key)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));
            //I hate how clunky this is but there isn't a public key constructor
            ECParameters ec = new ECParameters()
            {
                Curve = ECCurve.NamedCurves.nistP256,
                Q = new BigIntegerPoint(key).ToECPoint()
            };
            return ECDiffieHellman.Create(ec).PublicKey;
        }
    }
}
