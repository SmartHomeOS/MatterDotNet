using MatterDotNet.Protocol.Payloads;
using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace MatterDotNet.Protocol.Cryptography
{
    public class SPAKE2Plus
    {
        static readonly byte[] ContextPrefixValue = new byte[] { 0x43, 0x48, 0x49, 0x50, 0x20, 0x50, 0x41, 0x4b, 0x45, 0x20, 0x56, 0x31, 0x20, 0x43,
                                                                 0x6f, 0x6d, 0x6d, 0x69, 0x73, 0x73, 0x69, 0x6f, 0x6e, 0x69, 0x6e, 0x67
                                                                }; // "CHIP PAKE V1 Commissioning"
        static readonly byte[] SEKeys_Info = new byte[] { 0x53, 0x65, 0x73, 0x73, 0x69, 0x6f, 0x6e, 0x4b, 0x65, 0x79, 0x73 }; /* "SessionKeys" */
        static readonly BigIntegerPoint M = new BigIntegerPoint(Convert.FromHexString("02886E2F97ACE46E55BA9DD7242579F2993B64E16EF3DCAB95AFD497333D8FA12F"));
        static readonly BigIntegerPoint N = new BigIntegerPoint(Convert.FromHexString("03D8BBD6C639C62937B04D997F38C3770719C629D7014D49A24B4F98BAA1292B49"));

        //Party A
        public static (BigInteger w0, BigInteger w1, BigIntegerPoint X, BigInteger x) PAKEValues_Initiator(uint passcode, int iterations, byte[] salt)
        {
            byte[] pinBytes = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(pinBytes, passcode);
            byte[] w = Crypto.PBKDF(pinBytes, salt, iterations, 80 * 8);
            BigInteger w0s = new BigInteger(w.AsSpan().Slice(0, 40), true, true);
            BigInteger w1s = new BigInteger(w.AsSpan().Slice(40, 40), true, true);
            BigInteger w0 = w0s % SecP256.n;
            BigInteger w1 = w1s % SecP256.n;
            BigInteger x = new BigInteger(RandomNumberGenerator.GetBytes(Crypto.GROUP_SIZE_BYTES)) % SecP256.p;
            BigIntegerPoint X = SecP256.Add(SecP256.Multiply(x, SecP256.GeneratorP), SecP256.Multiply(w0, M)); //X = pA
            return (w0, w1, X, x);
        }

        // Party B
        public static (BigInteger w0, BigIntegerPoint L, BigIntegerPoint Y, BigInteger y) PAKEValues_Responder(uint passcode, int iterations, byte[] salt)
        {
            byte[] pinBytes = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(pinBytes, passcode);
            byte[] w = Crypto.PBKDF(pinBytes, salt, iterations, 80 * 8);
            BigInteger w0s = new BigInteger(w.AsSpan().Slice(0, 40), true, true);
            BigInteger w1s = new BigInteger(w.AsSpan().Slice(40, 40), true, true);
            BigInteger w0 = w0s % SecP256.n;
            BigInteger w1 = w1s % SecP256.n;
            BigIntegerPoint L = SecP256.Multiply(w1, SecP256.GeneratorP);
            BigInteger y = new BigInteger(RandomNumberGenerator.GetBytes(Crypto.GROUP_SIZE_BYTES)) % SecP256.p;
            BigIntegerPoint Y = SecP256.Add(SecP256.Multiply(y, SecP256.GeneratorP), SecP256.Multiply(w0, N)); //Y = pB
            return (w0, L, Y, y);
        }

        public static (BigIntegerPoint Z, BigIntegerPoint V) InitiatorValidate(BigInteger w0, BigInteger w1, BigInteger x, BigIntegerPoint Y)
        {
            BigIntegerPoint TMP = SecP256.Multiply(w0, N);
            TMP.Negate();
            TMP = SecP256.Add(Y, TMP);
            BigIntegerPoint Z = SecP256.Multiply(x, TMP);
            BigIntegerPoint V = SecP256.Multiply(w1, TMP);
            return (Z, V);
        }

        public static (BigIntegerPoint Z, BigIntegerPoint V) ResponderValidate(BigInteger w0, BigIntegerPoint L, BigInteger y, BigIntegerPoint X)
        {
            BigIntegerPoint TMP = SecP256.Multiply(w0, M);
            TMP.Negate();
            BigIntegerPoint Z = SecP256.Multiply(y, SecP256.Add(X, TMP));
            BigIntegerPoint V = SecP256.Multiply(y, L);
            return (Z, V);
        }

        public static Span<byte> ComputeTranscript(Span<byte> PBKDFParamRequest, Span<byte> PBKDFParamResponse, BigIntegerPoint pA, BigIntegerPoint pB, BigIntegerPoint Z, BigIntegerPoint V, BigInteger w0)
        {
            Span<byte> context = new byte[ContextPrefixValue.Length +  PBKDFParamRequest.Length + PBKDFParamResponse.Length];
            ContextPrefixValue.CopyTo(context);
            PBKDFParamRequest.CopyTo(context.Slice(ContextPrefixValue.Length));
            PBKDFParamResponse.CopyTo(context.Slice(context.Length - PBKDFParamResponse.Length));
            context = Crypto.Hash(context);

            PayloadWriter TT = new PayloadWriter(context.Length + 302);
            TT.Write((ulong)context.Length);
            TT.Write(context);
            TT.Seek(16); //Write 2 zeros
            TT.Write((ulong)33);
            TT.Write(M.ToBytes(true));
            TT.Write((ulong)33);
            TT.Write(N.ToBytes(true));
            TT.Write((ulong)33);
            TT.Write(pA.ToBytes(true));
            TT.Write((ulong)33);
            TT.Write(pB.ToBytes(true));
            TT.Write((ulong)33);
            TT.Write(Z.ToBytes(true));
            TT.Write((ulong)33);
            TT.Write(V.ToBytes(true));
            TT.Write((ulong)32);
            TT.Write(w0.ToByteArray(true, true));

            return TT.GetPayload().Span;
        }

        public (byte[] cA, byte[] cB, byte[] I2RKey, byte[] R2IKey, byte[] AttestationChallenge) Finish(Span<byte> TT, BigIntegerPoint pA, BigIntegerPoint pB)
        {
            Span<byte> KaKe = Crypto.Hash(TT);
            Span<byte> kcAkcB = Crypto.KDF([], KaKe.Slice(0, Crypto.SYMMETRIC_KEY_LENGTH_BYTES), Encoding.UTF8.GetBytes("ConfirmationKeys"), Crypto.HASH_LEN_BITS);
            byte[] cA = Crypto.HMAC(kcAkcB.Slice(0, Crypto.HASH_LEN_BYTES / 2), pB.ToBytes(true));
            byte[] cB = Crypto.HMAC(kcAkcB.Slice(Crypto.HASH_LEN_BYTES / 2), pA.ToBytes(true));

            Span<byte> pake3 = Crypto.KDF(KaKe.Slice(Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES), [], SEKeys_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS * 3);
            return (cA, cB, 
                pake3.Slice(0, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(), 
                pake3.Slice(Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(), 
                pake3.Slice(2 * Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray());
        }
    }
}
