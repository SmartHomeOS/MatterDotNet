
using MatterDotNet.Protocol.Cryptography;
using System.Buffers.Binary;
using System.Numerics;
using System.Text;

namespace Test
{
    public class CryptoTests
    {

        [Test]
        public void SPAKE()
        {
            uint pin = 34567890;
            byte[] pinBytes = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(pinBytes, pin);
            byte[] w = Crypto.PBKDF(pinBytes, Encoding.UTF8.GetBytes("SPAKE2P Key Salt"), 100, 80 * 8);
            BigInteger w0s = new BigInteger(w.AsSpan().Slice(0, 40), true, true);
            BigInteger w1s = new BigInteger(w.AsSpan().Slice(40, 40), true, true);
            BigInteger p = new BigInteger(Convert.FromHexString("FFFFFFFF00000000FFFFFFFFFFFFFFFFBCE6FAADA7179E84F3B9CAC2FC632551"), true, true);
            BigInteger w0 = w0s % SecP256.n;
            BigInteger w1 = w1s % SecP256.n;
            Assert.That(Convert.ToHexString(w0.ToByteArray(true, true)), Is.EqualTo("0AFF2FAB0980E98D9D6D33A17AC2F15886CD87F6CDCB34200A072F5F6129F8AD"));
            BigIntegerPoint L = SecP256.Multiply(w1, SecP256.G);
            Assert.That(Convert.ToHexString(L.ToBytes(true)), Is.EqualTo("03EAE21D4B206F567BF357E91DF2DA29D1A2B75A9E07519CAB893B97E29A4BF43D"));;
        }
    }
}
