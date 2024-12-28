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

using MatterDotNet.Messages.PASE;
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
            byte[] w = Crypto.PBKDF(pinBytes, Encoding.UTF8.GetBytes("SPAKE2P Key Salt"), 100, Crypto.W_SIZE_BITS * 2);
            BigInteger w0s = new BigInteger(w.AsSpan().Slice(0, Crypto.W_SIZE_BYTES), true, true);
            BigInteger w1s = new BigInteger(w.AsSpan().Slice(Crypto.W_SIZE_BYTES, Crypto.W_SIZE_BYTES), true, true);
            BigInteger w0 = w0s % SecP256.n;
            BigInteger w1 = w1s % SecP256.n;
            Assert.That(Convert.ToHexString(w0.ToByteArray(true, true)), Is.EqualTo("0AFF2FAB0980E98D9D6D33A17AC2F15886CD87F6CDCB34200A072F5F6129F8AD"));
            BigIntegerPoint L = SecP256.Multiply(w1, SecP256.GeneratorP);
            Assert.That(Convert.ToHexString(L.ToBytes(true)), Is.EqualTo("03EAE21D4B206F567BF357E91DF2DA29D1A2B75A9E07519CAB893B97E29A4BF43D"));
        }

        [Test]
        public void TestExchange()
        {
            uint pin = 34567890;
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            SPAKE2Plus initiator = new SPAKE2Plus();
            SPAKE2Plus responder = new SPAKE2Plus();

            var pA = initiator.PAKEValues_Initiator(pin, 10000, salt);
            var pB = responder.PAKEValues_Responder(pin, 10000, salt);
            bool pAValid = SecP256.IsOnCurve(pA);
            bool pBValid = SecP256.IsOnCurve(pB);
            Assert.That(pAValid);
            Assert.That(pBValid);
            (BigIntegerPoint Z, BigIntegerPoint V) ininiatorValidation = responder.ResponderValidate(pA);
            (BigIntegerPoint Z, BigIntegerPoint V) responderValidation = initiator.InitiatorValidate(pB);
            Assert.That(ininiatorValidation.V, Is.EqualTo(responderValidation.V));
            Assert.That(ininiatorValidation.Z, Is.EqualTo(responderValidation.Z));
            Assert.That(responderValidation.V.ToBytes(true).ToArray(), Is.EqualTo(ininiatorValidation.V.ToBytes(true).ToArray()).AsCollection);
            Assert.That(responderValidation.Z.ToBytes(true).ToArray(), Is.EqualTo(ininiatorValidation.Z.ToBytes(true).ToArray()).AsCollection);

            byte[] random = RandomNumberGenerator.GetBytes(32);
            PBKDFParamReq req = new PBKDFParamReq() { HasPBKDFParameters = false, InitiatorRandom = random, InitiatorSessionId = 23, PasscodeId = 0 };
            PBKDFParamResp resp = new PBKDFParamResp() { InitiatorRandom = random, ResponderRandom = random, ResponderSessionId = 23 };
            responder.Finish(req, resp, pA.ToBytes(false), pB.ToBytes(false));
        }
    }
}
