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

using MatterDotNet.Messages;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.OpCodes;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Cryptography
{
    public class PASE
    {
        SPAKE2Plus spake = new SPAKE2Plus();
        (byte[] cA, byte[] cB, byte[] I2RKey, byte[] R2IKey, byte[] AttestationChallenge) SessionKeys;

        public Frame GeneratePake1(PBKDFParamResp paramResp)
        {
            if (paramResp.Pbkdf_parameters == null)
                throw new InvalidDataException("Missing PBKDF Parameters");
            ushort session = paramResp.ResponderSessionId;
            Console.WriteLine("Iterations: " + (int)paramResp.Pbkdf_parameters!.Iterations);
            BigIntegerPoint pA = spake.PAKEValues_Initiator(36331256, (int)paramResp.Pbkdf_parameters!.Iterations, paramResp.Pbkdf_parameters!.Salt);
            Pake1 pk1 = new Pake1() { PA = pA.ToBytes(false) };
            Frame frame = new Frame(pk1) { Flags = MessageFlags.SourceNodeID };
            frame.Message.OpCode = (byte)SecureOpCodes.PASEPake1;
            return frame;
        }

        public Frame GeneratePake3(Pake1 pake1, Pake2 pake2, PBKDFParamReq req, PBKDFParamResp resp)
        {
            var iv = spake.InitiatorValidate(new BigIntegerPoint(pake2.PB));
            SessionKeys = spake.Finish(req, resp, pake1.PA, pake2.PB);
            if (!SessionKeys.cB.SequenceEqual(pake2.CB))
                throw new CryptographicException("Validators do not match");
            Pake3 pake3 = new Pake3() {
                CA = SessionKeys.cA
            };
            Frame frame = new Frame(pake3) { Flags = MessageFlags.SourceNodeID };
            frame.Message.OpCode = (byte)SecureOpCodes.PASEPake3;
            return frame;
        }

        public Frame GenerateParamRequest(bool hasOnboardingPayload = false)
        {
            PBKDFParamReq req = new PBKDFParamReq()
            {
                InitiatorRandom = RandomNumberGenerator.GetBytes(32),
                InitiatorSessionId = (ushort)Random.Shared.Next(0, ushort.MaxValue),
                PasscodeId = 0,
                HasPBKDFParameters = hasOnboardingPayload
            };

            Frame frame = new Frame(req);
            frame.Message.OpCode = (byte)SecureOpCodes.PBKDFParamRequest;
            frame.Flags |= MessageFlags.SourceNodeID;
            return frame;
        }

        public (byte[] I2RKey, byte[] R2IKey) GetKeys()
        {
            return (SessionKeys.I2RKey, SessionKeys.R2IKey);
        }
    }
}
