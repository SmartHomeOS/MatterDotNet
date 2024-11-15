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

        public static ReadOnlySpan<byte> Crypto_Privacy_Encrypt(byte[] key, Span<byte> message, byte[] nonce)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
