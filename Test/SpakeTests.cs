
using MatterDotNet.Protocol.Cryptography;
using System.Buffers.Binary;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Test
{
    public class SpakeTests
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
            BigInteger w0 = w0s % SecP256.n;
            BigInteger w1 = w1s % SecP256.n;
            Assert.That(Convert.ToHexString(w0.ToByteArray(true, true)), Is.EqualTo("0AFF2FAB0980E98D9D6D33A17AC2F15886CD87F6CDCB34200A072F5F6129F8AD"));
            BigIntegerPoint L = SecP256.Multiply(w1, SecP256.GeneratorP);
            Assert.That(Convert.ToHexString(L.ToBytes(true)), Is.EqualTo("03EAE21D4B206F567BF357E91DF2DA29D1A2B75A9E07519CAB893B97E29A4BF43D"));;
        }

        [Test]
        public void TestExchange()
        {
            uint pin = 34567890;
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var Values_Initiator = SPAKE2Plus.PAKEValues_Initiator(pin, 10000, salt);
            var Values_Responder = SPAKE2Plus.PAKEValues_Responder(pin, 10000, salt);
            var sharedA = SPAKE2Plus.ResponderValidate(Values_Initiator.w0, Values_Responder.L, Values_Responder.y, Values_Initiator.X);
            var sharedB = SPAKE2Plus.InitiatorValidate(Values_Initiator.w0, Values_Initiator.w1, Values_Initiator.x, Values_Responder.Y);
            //if (!sharedA.V.ToBytes(true).SequenceEqual(sharedB.V.ToBytes(true)))
            //{
            //    byte[] sa = sharedA.V.ToBytes(true).ToArray();
            //    byte[] sb = sharedB.V.ToBytes(true).ToArray();
            //    Console.WriteLine();
            //}
            Assert.That(sharedA.V, Is.EqualTo(sharedB.V));
            Assert.That(sharedA.Z, Is.EqualTo(sharedB.Z));
            CollectionAssert.AreEqual(sharedA.V.ToBytes(true).ToArray(), sharedB.V.ToBytes(true).ToArray());
            CollectionAssert.AreEqual(sharedA.Z.ToBytes(true).ToArray(), sharedB.Z.ToBytes(true).ToArray());
        }
    }
}
