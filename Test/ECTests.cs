

using MatterDotNet.Protocol.Cryptography;
using System.Security.Cryptography;

namespace Test
{
    public class ECTests
    {
        [Test]
        public void TestCompression()
        {
            string M = "02886E2F97ACE46E55BA9DD7242579F2993B64E16EF3DCAB95AFD497333D8FA12F";
            string N = "03D8BBD6C639C62937B04D997F38C3770719C629D7014D49A24B4F98BAA1292B49";

            BigIntegerPoint pt1 = new BigIntegerPoint(Convert.FromHexString(M));
            BigIntegerPoint pt2 = new BigIntegerPoint(Convert.FromHexString(N));
            Assert.That(Convert.ToHexString(pt1.ToBytes(true)), Is.EqualTo(M));
            Assert.That(Convert.ToHexString(pt2.ToBytes(true)), Is.EqualTo(N));
        }

        [Test]
        public void TestSigning()
        {
            byte[] msg = RandomNumberGenerator.GetBytes(32);
            var keypair = Crypto.GenerateKeypair();
            byte[] signature = Crypto.Sign(keypair.Private, msg);
            Assert.That(Crypto.Verify(keypair.Public, msg, signature), Is.True);
        }

        [Test]
        public void TestECDH()
        {
            var keypairA = Crypto.GenerateKeypair();
            var keypairB = Crypto.GenerateKeypair();

            var sharedA = Crypto.ECDH(keypairA.Private, keypairB.Public);
            var sharedB = Crypto.ECDH(keypairB.Private, keypairA.Public);
            Assert.That(sharedA.SequenceEqual(sharedB), Is.True);
        }
    }
}
