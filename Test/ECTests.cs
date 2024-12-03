

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
        public void TestDecompression()
        {
            Assert.That(Convert.ToHexString(SPAKE2Plus.M.ToBytes(false)), Is.EqualTo("04886e2f97ace46e55ba9dd7242579f2993b64e16ef3dcab95afd497333d8fa12f5ff355163e43ce224e0b0e65ff02ac8e5c7be09419c785e0ca547d55a12e2d20".ToUpper()));
            Assert.That(Convert.ToHexString(SPAKE2Plus.N.ToBytes(false)), Is.EqualTo("04d8bbd6c639c62937b04d997f38c3770719c629d7014d49a24b4f98baa1292b4907d60aa6bfade45008a636337f5168c64d9bd36034808cd564490b1e656edbe7".ToUpper()));

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
