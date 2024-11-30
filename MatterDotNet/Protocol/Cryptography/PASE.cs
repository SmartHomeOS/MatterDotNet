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
    public static class PASE
    {
        public static Frame GeneratePake1(PBKDFParamResp paramResp)
        {
            ushort session = paramResp.ResponderSessionId;
            var init = SPAKE2Plus.PAKEValues_Initiator(36331256, (int)paramResp.Pbkdf_parameters.Iterations, paramResp.Pbkdf_parameters.Salt);
            Pake1 pk1 = new Pake1() { PA = init.X.ToBytes(true) };
            Frame frame = new Frame(pk1) { Flags = MessageFlags.SourceNodeID, SourceNodeID = 1, SessionID = paramResp.ResponderSessionId };
            frame.Message.Flags |= ExchangeFlags.Initiator;
            frame.Message.OpCode = (byte)SecureOpCodes.PASEPake1;
            return frame;
        }

        public static Frame GenerateParamRequest(bool hasOnboardingPayload = false)
        {
            PBKDFParamReq req = new PBKDFParamReq()
            {
                InitiatorRandom = RandomNumberGenerator.GetBytes(32),
                InitiatorSessionId = (ushort)Random.Shared.Next(0, ushort.MaxValue),
                PasscodeId = 0,
                HasPBKDFParameters = hasOnboardingPayload
            };

            Frame frame = new Frame(req);
            frame.Message.Flags |= ExchangeFlags.Initiator;
            frame.Message.OpCode = (byte)SecureOpCodes.PBKDFParamRequest;
            frame.Flags |= MessageFlags.SourceNodeID;
            frame.SourceNodeID = 1;
            return frame;
        }
    }
}
