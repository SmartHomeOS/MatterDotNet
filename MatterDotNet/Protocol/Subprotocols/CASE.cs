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

using MatterDotNet.Messages.CASE;
using MatterDotNet.Messages.Certificates;
using MatterDotNet.PKI;
using MatterDotNet.Protocol.Cryptography;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Security;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Subprotocols
{
    /// <summary>
    /// Create CASE (certificate based) sessions
    /// </summary>
    /// <param name="unsecureSession"></param>
    public class CASE(SessionContext unsecureSession)
    {
        private static readonly byte[] Resume1MIC_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x53, 0x31]; /* "NCASE_SigmaS1" */
        private static readonly byte[] TBEData2_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x32, 0x4e]; /* "NCASE_Sigma2N" */
        private static readonly byte[] TBEData3_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x33, 0x4e]; /* "NCASE_Sigma3N" */
        private static readonly byte[] Resume2MIC_Nonce = [0x4e, 0x43, 0x41, 0x53, 0x45, 0x5f, 0x53, 0x69, 0x67, 0x6d, 0x61, 0x53, 0x32]; /* "NCASE_SigmaS2" */
        private static readonly byte[] S2K_Info = [0x53, 0x69, 0x67, 0x6d, 0x61, 0x32]; /* "Sigma2" */
        private static readonly byte[] S3K_Info = [0x53, 0x69, 0x67, 0x6d, 0x61, 0x33]; /* "Sigma3" */
        private static readonly byte[] SEKeys_Info = [0x53, 0x65, 0x73, 0x73, 0x69, 0x6f, 0x6e, 0x4b, 0x65, 0x79, 0x73]; /* "SessionKeys" */
        private static readonly byte[] RSEKeys_Info = [0x53, 0x65, 0x73, 0x73, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73, 0x75, 0x6d, 0x70, 0x74, 0x69, 0x6f, 0x6e, 0x4b, 0x65, 0x79, 0x73]; /* "SessionResumptionKeys" */
        private static readonly byte[] S1RK_Info = [0x53, 0x69, 0x67, 0x6d, 0x61, 0x31, 0x5f, 0x52, 0x65, 0x73, 0x75, 0x6d, 0x65]; /* "Sigma1_Resume" */
        private static readonly byte[] S2RK_Info = [0x53, 0x69, 0x67, 0x6d, 0x61, 0x32, 0x5f, 0x52, 0x65, 0x73, 0x75, 0x6d, 0x65]; /* "Sigma2_Resume" */

        private byte[]? attestationChallenge;

        /// <summary>
        /// Establish a new session
        /// </summary>
        /// <param name="fabric"></param>
        /// <param name="nodeId"></param>
        /// <param name="IPKEpoch"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        public async Task<SecureSession?> EstablishSecureSession(Fabric fabric, ulong nodeId, byte[] IPKEpoch)
        {
            Frame? resp = null;
            using (Exchange exchange = unsecureSession.CreateExchange())
            {
                byte[] initiatorRandom = RandomNumberGenerator.GetBytes(32);
                var ephKeys = Crypto.GenerateKeypair();
                Sigma1 Msg1 = new Sigma1()
                {
                    InitiatorRandom = initiatorRandom,
                    InitiatorSessionId = SessionManager.GetAvailableSessionID(),
                    DestinationId = fabric.ComputeDestinationID(initiatorRandom, nodeId),
                    InitiatorEphPubKey = ephKeys.Public,
                    InitiatorSessionParams = SessionManager.GetDefaultSessionParams()
                };

                Frame sigma1 = new Frame(Msg1, (byte)SecureOpCodes.CASESigma1);
                await exchange.SendFrame(sigma1);
                resp = await exchange.Read();
                if (resp.Message.Payload is StatusPayload error)
                {
                    throw new IOException("Failed to establish CASE session. Remote Node returned " + error.GeneralCode + ": " + (SecureStatusCodes)error.ProtocolCode);
                }
                return await ProcessSigma2(Msg1, (Sigma2)resp.Message.Payload!, fabric, nodeId, ephKeys, exchange);
            }
        }

        private async Task<SecureSession?> ProcessSigma2(Sigma1 Msg1, Sigma2 Msg2, Fabric fabric, ulong nodeId, (byte[] Public, byte[] Private) ephKeys, Exchange exchange)
        {
            PayloadWriter Msg2Bytes = new PayloadWriter(1024);
            Msg2.Serialize(Msg2Bytes);
            byte[] sharedSecret = Crypto.ECDH(ephKeys.Private, Msg2.ResponderEphPubKey);
            PayloadWriter Msg1Bytes = new PayloadWriter(512);
            Msg1.Serialize(Msg1Bytes);

            byte[] transcriptHash2 = Crypto.Hash(Msg1Bytes.GetPayload().Span);
            byte[] salt2 = SpanUtil.Combine(fabric.OperationalIdentityProtectionKey, Msg2.ResponderRandom, Msg2.ResponderEphPubKey, transcriptHash2);
            byte[] S2K = Crypto.KDF(sharedSecret, salt2, S2K_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS);
            if (!Crypto.AEAD_DecryptVerify(S2K, Msg2.Encrypted2, [], TBEData2_Nonce))
            {
                Console.WriteLine("Sigma2 decryption failed");
                StatusPayload status = new StatusPayload(GeneralCode.FAILURE, 0, ProtocolType.SecureChannel, (ushort)SecureStatusCodes.INVALID_PARAMETER);
                await exchange.SendFrame(new Frame(status, (byte)SecureOpCodes.StatusReport));
                return null;
            }
            Memory<byte> decrypted = Msg2.Encrypted2.AsMemory(0, Msg2.Encrypted2.Length - Crypto.AEAD_MIC_LENGTH_BYTES);
            Sigma2Tbedata tbeData2 = new Sigma2Tbedata(decrypted);
            MatterCertificate responderNOC = new MatterCertificate(tbeData2.ResponderNOC);
            if (!responderNOC.Subject.Exists(a => a.MatterNodeId == nodeId) || !responderNOC.Subject.Exists(a => a.MatterFabricId == fabric.FabricID))
            {
                Console.WriteLine("Sigma2 wrong cert");
                StatusPayload status = new StatusPayload(GeneralCode.FAILURE, 0, ProtocolType.SecureChannel, (ushort)SecureStatusCodes.INVALID_PARAMETER);
                await exchange.SendFrame(new Frame(status, (byte)SecureOpCodes.StatusReport));
                return null;
            }
            OperationalCertificate responderCert = fabric.GetNOC(nodeId)!;
            if (!responderNOC.EcPubKey.SequenceEqual(responderCert.PublicKey))
            {
                Console.WriteLine("Sigma2 invalid certificate");
                StatusPayload status = new StatusPayload(GeneralCode.FAILURE, 0, ProtocolType.SecureChannel, (ushort)SecureStatusCodes.INVALID_PARAMETER);
                await exchange.SendFrame(new Frame(status, (byte)SecureOpCodes.StatusReport));
                return null;
            }
            Sigma2Tbsdata signature = new Sigma2Tbsdata()
            {
                InitiatorEphPubKey = ephKeys.Public,
                ResponderEphPubKey = Msg2.ResponderEphPubKey,
                ResponderNOC = tbeData2.ResponderNOC
            };
            PayloadWriter signatureBytes = new PayloadWriter(512);
            signature.Serialize(signatureBytes);
            if (!responderCert.VerifyData(signatureBytes.GetPayload().ToArray(), tbeData2.Signature))
            {
                Console.WriteLine("Sigma2 invalid signature");
                StatusPayload status = new StatusPayload(GeneralCode.FAILURE, 0, ProtocolType.SecureChannel, (ushort)SecureStatusCodes.INVALID_PARAMETER);
                await exchange.SendFrame(new Frame(status, (byte)SecureOpCodes.StatusReport));
                return null;
            }

            Sigma3Tbsdata s3sig = new Sigma3Tbsdata()
            {
                InitiatorNOC = fabric.Commissioner!.GetMatterCertBytes(),
                InitiatorEphPubKey = ephKeys.Public,
                ResponderEphPubKey = Msg2.ResponderEphPubKey
            };
            PayloadWriter s3sigBytes = new PayloadWriter(512);
            s3sig.Serialize(s3sigBytes);

            Sigma3Tbedata s3tbe = new Sigma3Tbedata()
            {
                InitiatorNOC = s3sig.InitiatorNOC,
                Signature = fabric.Commissioner.SignData(s3sigBytes.GetPayload().ToArray())!
            };
            PayloadWriter s3tbeBytes = new PayloadWriter(512);
            s3tbe.Serialize(s3tbeBytes);

            byte[] Msg1Msg2Bytes = SpanUtil.Combine(Msg1Bytes.GetPayload().Span, Msg2Bytes.GetPayload().Span);
            byte[] salt3 = SpanUtil.Combine(fabric.OperationalIdentityProtectionKey, Crypto.Hash(Msg1Msg2Bytes));
            byte[] S3K = Crypto.KDF(sharedSecret, salt3, S3K_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS);

            Memory<byte> mic = Crypto.AEAD_GenerateEncrypt(S3K, s3tbeBytes.GetPayload().Span, [], TBEData3_Nonce).ToArray();
            s3tbeBytes.Write(mic);
            Sigma3 Msg3 = new Sigma3()
            {
                Encrypted3 = s3tbeBytes.GetPayload().ToArray()
            };
            PayloadWriter Msg3Bytes = new PayloadWriter(1024);
            Msg3.Serialize(Msg3Bytes);

            await exchange.SendFrame(new Frame(Msg3, (byte)SecureOpCodes.CASESigma3));
            Frame? resp = await exchange.Read();

            StatusPayload s3resp = (StatusPayload)resp.Message.Payload!;
            if (s3resp.GeneralCode != GeneralCode.SUCCESS)
                throw new IOException("CASE step 3 failed with status: " + (SecureStatusCodes)s3resp.ProtocolCode); ;

            byte[] transcriptSession = SpanUtil.Combine(Msg1Msg2Bytes, Msg3Bytes.GetPayload().Span);
            byte[] saltSession = SpanUtil.Combine(fabric.OperationalIdentityProtectionKey, Crypto.Hash(transcriptSession));
            byte[] sessionKeys = Crypto.KDF(sharedSecret, saltSession, SEKeys_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS * 3);
            attestationChallenge = sessionKeys.AsSpan(2 * Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray();

            uint activeInterval = Msg2.ResponderSessionParams?.SessionActiveInterval ?? SessionManager.GetDefaultSessionParams().SessionActiveInterval!.Value;
            uint activeThreshold = Msg2.ResponderSessionParams?.SessionActiveThreshold ?? SessionManager.GetDefaultSessionParams().SessionActiveThreshold!.Value;
            uint idleInterval = Msg2.ResponderSessionParams?.SessionIdleInterval ?? SessionManager.GetDefaultSessionParams().SessionIdleInterval!.Value;

            Console.WriteLine("Created CASE session");
            return SessionManager.CreateSession(unsecureSession.Connection, false, true, Msg1.InitiatorSessionId, Msg2.ResponderSessionId,
                                                sessionKeys.AsSpan(0, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(),
                                                sessionKeys.AsSpan(Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(),
                                                fabric.Commissioner.NodeID, nodeId, sharedSecret, tbeData2.ResumptionId, false, idleInterval, activeInterval, activeThreshold);
        }

        /// <summary>
        /// Resume an existing session
        /// </summary>
        /// <param name="fabric"></param>
        /// <param name="nodeId"></param>
        /// <param name="resumptionId"></param>
        /// <param name="sharedSecret"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        public async Task<SecureSession?> ResumeSecureSession(Fabric fabric, ulong nodeId, byte[] resumptionId, byte[] sharedSecret)
        {
            Frame? resp = null;
            using (Exchange exchange = unsecureSession.CreateExchange())
            {
                byte[] initiatorRandom = RandomNumberGenerator.GetBytes(32);
                byte[] S1RK = Crypto.KDF(sharedSecret, SpanUtil.Combine(initiatorRandom, resumptionId), S1RK_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS);
                var ephKeys = Crypto.GenerateKeypair();
                Sigma1 Msg1 = new Sigma1()
                {
                    InitiatorRandom = initiatorRandom,
                    InitiatorSessionId = SessionManager.GetAvailableSessionID(),
                    DestinationId = fabric.ComputeDestinationID(initiatorRandom, nodeId),
                    InitiatorEphPubKey = ephKeys.Public,
                    InitiatorSessionParams = SessionManager.GetDefaultSessionParams(),
                    ResumptionId = resumptionId,
                    InitiatorResumeMIC = Crypto.AEAD_GenerateEncrypt(S1RK, [], [], Resume1MIC_Nonce).ToArray()
                };

                Frame sigma1 = new Frame(Msg1, (byte)SecureOpCodes.CASESigma1);
                await exchange.SendFrame(sigma1);
                resp = await exchange.Read();

                if (resp.Message.Payload is StatusPayload error)
                    throw new IOException("Failed to establish CASE session. Remote Node returned " + error.GeneralCode + ": " + (SecureStatusCodes)error.ProtocolCode);
                if (resp.Message.Payload is Sigma2 sigma2)
                {
                    Console.WriteLine("Failed to establish CASE session. Peer falling back to full establishment");
                    return await ProcessSigma2(Msg1, sigma2, fabric, nodeId, ephKeys, exchange);
                }
                Sigma2Resume Resume2Msg = (Sigma2Resume)resp.Message.Payload!;
                byte[] S2RK = Crypto.KDF(sharedSecret, SpanUtil.Combine(initiatorRandom, Resume2Msg.ResumptionId), S2RK_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS);
                if (!Crypto.AEAD_DecryptVerify(S2RK, Resume2Msg.Sigma2ResumeMIC, [], Resume2MIC_Nonce))
                {
                    Console.WriteLine("Sigma2 resume invalid signature");
                    StatusPayload status = new StatusPayload(GeneralCode.FAILURE, 0, ProtocolType.SecureChannel, (ushort)SecureStatusCodes.INVALID_PARAMETER);
                    await exchange.SendFrame(new Frame(status, (byte)SecureOpCodes.StatusReport));
                    return null;
                }
                StatusPayload sigmaFinished = new StatusPayload(GeneralCode.SUCCESS, 0, ProtocolType.SecureChannel, (ushort)SecureStatusCodes.SESSION_ESTABLISHMENT_SUCCESS);
                await exchange.SendFrame(new Frame(sigmaFinished, (byte)SecureOpCodes.StatusReport));

                byte[] sessionKeys = Crypto.KDF(sharedSecret, SpanUtil.Combine(initiatorRandom, Resume2Msg.ResumptionId), RSEKeys_Info, 3 * Crypto.SYMMETRIC_KEY_LENGTH_BITS);
                uint activeInterval = Resume2Msg.ResponderSessionParams?.SessionActiveInterval ?? SessionManager.GetDefaultSessionParams().SessionActiveInterval!.Value;
                uint activeThreshold = Resume2Msg.ResponderSessionParams?.SessionActiveThreshold ?? SessionManager.GetDefaultSessionParams().SessionActiveThreshold!.Value;
                uint idleInterval = Resume2Msg.ResponderSessionParams?.SessionIdleInterval ?? SessionManager.GetDefaultSessionParams().SessionIdleInterval!.Value;
                attestationChallenge = sessionKeys.AsSpan(2 * Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray();

                Console.WriteLine("Resumed CASE session");
                return SessionManager.CreateSession(unsecureSession.Connection, false, true, Msg1.InitiatorSessionId, Resume2Msg.ResponderSessionId,
                                                    sessionKeys.AsSpan(0, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(),
                                                    sessionKeys.AsSpan(Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(),
                                                    fabric.Commissioner!.NodeID, nodeId, sharedSecret, Resume2Msg.ResumptionId, false, idleInterval, activeInterval, activeThreshold);
            }
        }

        /// <summary>
        /// Get the attestation challenge negotiated during session establishment
        /// </summary>
        /// <returns></returns>
        public byte[] GetAttestationChallenge()
        {
            return attestationChallenge!;
        }
    }
}
