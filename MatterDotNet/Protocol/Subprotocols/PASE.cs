﻿// MatterDotNet Copyright (C) 2025
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
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.Flags;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using System.Net;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Subprotocols
{
    public class PASE(SessionContext unsecureSession)
    {
        SPAKE2Plus spake = new SPAKE2Plus();
        (byte[] cA, byte[] cB, byte[] I2RKey, byte[] R2IKey, byte[] AttestationChallenge) SessionKeys;

        public async Task<SecureSession?> EstablishSecureSession(uint passcode, CancellationToken token = default)
        {
            Frame? resp = null;
            Exchange exchange = unsecureSession.CreateExchange();
            Frame paramReq = GenerateParamRequest(unsecureSession.Connection.EndPoint);
            await exchange.SendFrame(paramReq, true, token);
            resp = await exchange.Read();
            if (resp.Message.Payload is StatusPayload error)
            {
                throw new IOException("Failed to establish PASE session. Remote Node returned " + error.GeneralCode + ": " + (SecureStatusCodes)error.ProtocolCode);
            }
            PBKDFParamResp paramResp = (PBKDFParamResp)resp.Message.Payload!;
            Frame pake1 = GeneratePake1(paramResp, passcode);
            await exchange.SendFrame(pake1, true, token);
            resp = await exchange.Read(token);
            Pake2 pake2 = (Pake2)resp.Message.Payload!;
            Frame pake3 = GeneratePake3((Pake1)pake1.Message.Payload!, pake2, (PBKDFParamReq)paramReq.Message.Payload!, paramResp);
            await exchange.SendFrame(pake3, true, token);
            resp = await exchange.Read(token);
            StatusPayload status = (StatusPayload)resp.Message.Payload!;
            if (status.GeneralCode != GeneralCode.SUCCESS)
                throw new IOException("PASE failed with status: " + (SecureStatusCodes)status.ProtocolCode);
            exchange.Dispose();
            ushort localSessionID = ((PBKDFParamReq)paramReq.Message.Payload!).InitiatorSessionId;

            uint activeInterval = paramResp.ResponderSessionParams?.SessionActiveInterval ?? SessionManager.GetDefaultSessionParams().SessionActiveInterval!.Value;
            uint activeThreshold = paramResp.ResponderSessionParams?.SessionActiveThreshold ?? SessionManager.GetDefaultSessionParams().SessionActiveThreshold!.Value;
            uint idleInterval = paramResp.ResponderSessionParams?.SessionIdleInterval ?? SessionManager.GetDefaultSessionParams().SessionIdleInterval!.Value;

            return SessionManager.CreateSession(unsecureSession.Connection, true, true, localSessionID, paramResp.ResponderSessionId, SessionKeys.I2RKey, SessionKeys.R2IKey, 0, 0, [], [], false, idleInterval, activeInterval, activeThreshold);
        }

        public byte[] GetAttestationChallenge()
        {
            return SessionKeys.AttestationChallenge;
        }

        private Frame GeneratePake1(PBKDFParamResp paramResp, uint passcode)
        {
            if (paramResp.Pbkdf_parameters == null)
                throw new InvalidDataException("Missing PBKDF Parameters");
            ushort session = paramResp.ResponderSessionId;
            Console.WriteLine("Iterations: " + (int)paramResp.Pbkdf_parameters!.Iterations);
            BigIntegerPoint pA = spake.PAKEValues_Initiator(passcode, (int)paramResp.Pbkdf_parameters!.Iterations, paramResp.Pbkdf_parameters!.Salt);
            Pake1 pk1 = new Pake1() { PA = pA.ToBytes(false) };
            Frame frame = new Frame(pk1, (byte)SecureOpCodes.PASEPake1);
            frame.Flags |= MessageFlags.SourceNodeID;
            return frame;
        }

        private Frame GeneratePake3(Pake1 pake1, Pake2 pake2, PBKDFParamReq req, PBKDFParamResp resp)
        {
            var iv = spake.InitiatorValidate(new BigIntegerPoint(pake2.PB));
            SessionKeys = spake.Finish(req, resp, pake1.PA, pake2.PB);
            if (!SessionKeys.cB.SequenceEqual(pake2.CB))
                throw new CryptographicException("Validators do not match");
            Pake3 pake3 = new Pake3()
            {
                CA = SessionKeys.cA
            };
            Frame frame = new Frame(pake3, (byte)SecureOpCodes.PASEPake3);
            frame.Flags |= MessageFlags.SourceNodeID;
            return frame;
        }

        private Frame GenerateParamRequest(EndPoint endPoint, bool hasOnboardingPayload = false)
        {
            PBKDFParamReq req = new PBKDFParamReq()
            {
                InitiatorRandom = RandomNumberGenerator.GetBytes(32),
                InitiatorSessionId = SessionManager.GetAvailableSessionID(endPoint),
                PasscodeId = 0,
                HasPBKDFParameters = hasOnboardingPayload
            };

            Frame frame = new Frame(req, (byte)SecureOpCodes.PBKDFParamRequest);
            frame.Flags |= MessageFlags.SourceNodeID;
            return frame;
        }
    }
}
