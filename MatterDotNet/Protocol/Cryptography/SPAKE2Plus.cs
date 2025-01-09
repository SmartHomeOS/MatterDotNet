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

using MatterDotNet.Messages.PASE;
using MatterDotNet.Protocol.Payloads;
using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace MatterDotNet.Protocol.Cryptography
{
    internal class SPAKE2Plus
    {
        static readonly byte[] ContextPrefixValue = new byte[] { 0x43, 0x48, 0x49, 0x50, 0x20, 0x50, 0x41, 0x4b, 0x45, 0x20, 0x56, 0x31, 0x20, 0x43,
                                                                 0x6f, 0x6d, 0x6d, 0x69, 0x73, 0x73, 0x69, 0x6f, 0x6e, 0x69, 0x6e, 0x67
                                                                }; // "CHIP PAKE V1 Commissioning"
        static readonly byte[] SEKeys_Info = new byte[] { 0x53, 0x65, 0x73, 0x73, 0x69, 0x6f, 0x6e, 0x4b, 0x65, 0x79, 0x73 }; /* "SessionKeys" */
        public static readonly BigIntegerPoint M = new BigIntegerPoint(Convert.FromHexString("02886E2F97ACE46E55BA9DD7242579F2993B64E16EF3DCAB95AFD497333D8FA12F"));
        public static readonly BigIntegerPoint N = new BigIntegerPoint(Convert.FromHexString("03D8BBD6C639C62937B04D997F38C3770719C629D7014D49A24B4F98BAA1292B49")); //"04d8bbd6c639c62937b04d997f38c3770719c629d7014d49a24b4f98baa1292b4907d60aa6bfade45008a636337f5168c64d9bd36034808cd564490b1e656edbe7"));  

        //Pake Session State
        private BigInteger w0;
        private BigIntegerPoint Z;
        private BigIntegerPoint V;
        private BigInteger w1;
        private BigInteger x;
        private BigInteger y;
        private BigIntegerPoint L;

        //Party A
        public BigIntegerPoint PAKEValues_Initiator(uint passcode, int iterations, byte[] salt)
        {
            byte[] pinBytes = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(pinBytes, passcode);
            byte[] w = Crypto.PBKDF(pinBytes, salt, iterations, Crypto.W_SIZE_BITS * 2);
            BigInteger w0s = new BigInteger(w.AsSpan().Slice(0, Crypto.W_SIZE_BYTES), true, true);
            BigInteger w1s = new BigInteger(w.AsSpan().Slice(Crypto.W_SIZE_BYTES, Crypto.W_SIZE_BYTES), true, true);
            w0 = w0s % SecP256.n;
            w1 = w1s % SecP256.n;
            x = new BigInteger(RandomNumberGenerator.GetBytes(Crypto.GROUP_SIZE_BYTES), true, true) % SecP256.p;
            BigIntegerPoint X = SecP256.Add(SecP256.Multiply(x, SecP256.GeneratorP), SecP256.Multiply(w0, M)); //X = pA
            return X;
        }

        // Party B
        public BigIntegerPoint PAKEValues_Responder(uint passcode, int iterations, byte[] salt)
        {
            byte[] pinBytes = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(pinBytes, passcode);
            byte[] w = Crypto.PBKDF(pinBytes, salt, iterations, Crypto.W_SIZE_BITS * 2);
            BigInteger w0s = new BigInteger(w.AsSpan().Slice(0, Crypto.W_SIZE_BYTES), true, true);
            BigInteger w1s = new BigInteger(w.AsSpan().Slice(Crypto.W_SIZE_BYTES, Crypto.W_SIZE_BYTES), true, true);
            w0 = w0s % SecP256.n;
            w1 = w1s % SecP256.n;
            L = SecP256.Multiply(w1, SecP256.GeneratorP);
            y = new BigInteger(RandomNumberGenerator.GetBytes(Crypto.GROUP_SIZE_BYTES), true, true) % SecP256.p;
            BigIntegerPoint Y = SecP256.Add(SecP256.Multiply(y, SecP256.GeneratorP), SecP256.Multiply(w0, N)); //Y = pB
            return Y;
        }

        public (BigIntegerPoint Z, BigIntegerPoint V) InitiatorValidate(BigIntegerPoint pB)
        {
            if (!SecP256.IsOnCurve(pB))
                throw new ArgumentException("Invalid pB");
            BigIntegerPoint TMP = SecP256.Multiply(w0, N);
            TMP.Negate();
            TMP = SecP256.Add(pB, TMP);
            Z = SecP256.Multiply(x, TMP);
            V = SecP256.Multiply(w1, TMP);
            return (Z, V);
        }

        public (BigIntegerPoint Z, BigIntegerPoint V) ResponderValidate(BigIntegerPoint pA)
        {
            if (!SecP256.IsOnCurve(pA))
                throw new ArgumentException("Invalid pA");
            BigIntegerPoint TMP = SecP256.Multiply(w0, M);
            TMP.Negate();
            Z = SecP256.Multiply(y, SecP256.Add(pA, TMP));
            V = SecP256.Multiply(y, L);
            return (Z, V);
        }

        public (byte[] cA, byte[] cB, byte[] I2RKey, byte[] R2IKey, byte[] AttestationChallenge) Finish(PBKDFParamReq pbkdfReq, PBKDFParamResp pbkdfResp, Span<byte> pA, Span<byte> pB)
        {
            PayloadWriter reqBytes = new PayloadWriter(1024);
            pbkdfReq.Serialize(reqBytes);
            PayloadWriter respBytes = new PayloadWriter(1024);
            pbkdfResp.Serialize(respBytes);

            Span<byte> context = new byte[ContextPrefixValue.Length + reqBytes.Length + respBytes.Length];
            ContextPrefixValue.CopyTo(context);
            reqBytes.GetPayload().Span.CopyTo(context.Slice(ContextPrefixValue.Length));
            respBytes.GetPayload().Span.CopyTo(context.Slice(context.Length - respBytes.Length));
            context = Crypto.Hash(context);

            PayloadWriter TT = new PayloadWriter(context.Length + 502);
            TT.Write((ulong)context.Length);
            TT.Write(context);
            TT.Seek(16); //Write 2 zeros
            TT.Write((ulong)Crypto.PUBLIC_KEY_SIZE_BYTES);
            TT.Write(M.ToBytes(false));
            TT.Write((ulong)Crypto.PUBLIC_KEY_SIZE_BYTES);
            TT.Write(N.ToBytes(false));
            TT.Write((ulong)Crypto.PUBLIC_KEY_SIZE_BYTES);
            TT.Write(pA);
            TT.Write((ulong)Crypto.PUBLIC_KEY_SIZE_BYTES);
            TT.Write(pB);
            TT.Write((ulong)Crypto.PUBLIC_KEY_SIZE_BYTES);
            TT.Write(Z.ToBytes(false));
            TT.Write((ulong)Crypto.PUBLIC_KEY_SIZE_BYTES);
            TT.Write(V.ToBytes(false));
            TT.Write((ulong)w0.GetByteCount(true));
            TT.Write(w0.ToByteArray(true, true));

            Span<byte> KaKe = Crypto.Hash(TT.GetPayload().Span);
            Span<byte> kcAkcB = Crypto.KDF(KaKe.Slice(0, Crypto.SYMMETRIC_KEY_LENGTH_BYTES), [], Encoding.UTF8.GetBytes("ConfirmationKeys"), Crypto.HASH_LEN_BITS);
            byte[] cA = Crypto.HMAC(kcAkcB.Slice(0, Crypto.HASH_LEN_BYTES / 2), pB);
            byte[] cB = Crypto.HMAC(kcAkcB.Slice(Crypto.HASH_LEN_BYTES / 2, Crypto.HASH_LEN_BYTES / 2), pA);

            Span<byte> sessionKeys = Crypto.KDF(KaKe.Slice(Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES), [], SEKeys_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS * 3);
            return (cA, cB, 
                sessionKeys.Slice(0, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(), 
                sessionKeys.Slice(Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(), 
                sessionKeys.Slice(2 * Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray());
        }
    }
}
