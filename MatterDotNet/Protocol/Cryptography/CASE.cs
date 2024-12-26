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

using MatterDotNet.Messages.CASE;
using MatterDotNet.Messages.Certificates;
using MatterDotNet.PKI;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.Flags;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Sessions;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Cryptography
{
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

        private byte[]? attestationChallenge;

        public async Task<SecureSession?> EstablishSecureSession(Fabric fabric, ulong nodeId, byte[] IPKEpoch)
        {
            Frame? resp = null;
            Exchange exchange = unsecureSession.CreateExchange();
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

            Frame sigma1 = new Frame(Msg1, (byte)SecureOpCodes.CASESigma1) { Flags = MessageFlags.SourceNodeID };
            await exchange.SendFrame(sigma1);
            Console.WriteLine("Sent SIGMA 1");
            resp = await exchange.Read();
            if (resp.Message.Payload is StatusPayload error)
            {
                throw new IOException("Failed to establish PASE session. Remote Node returned " + error.GeneralCode + ": " + (SecureStatusCodes)error.ProtocolCode);
            }
            Sigma2 Msg2 = (Sigma2)resp.Message.Payload!;
            PayloadWriter Msg2Bytes = new PayloadWriter(1024);
            Msg2.Serialize(Msg2Bytes);
            byte[] SharedSecret = Crypto.ECDH(ephKeys.Private, Msg2.ResponderEphPubKey);
            PayloadWriter Msg1Bytes = new PayloadWriter(512);
            Msg1.Serialize(Msg1Bytes);
            byte[] transcript2 = Crypto.Hash(Msg1Bytes.GetPayload().Span);
            byte[] salt2 = new byte[Crypto.SYMMETRIC_KEY_LENGTH_BYTES + 32 + Crypto.PUBLIC_KEY_SIZE_BYTES + Crypto.HASH_LEN_BYTES];
            Array.Copy(fabric.OperationalIdentityProtectionKey, salt2, Crypto.SYMMETRIC_KEY_LENGTH_BYTES);
            Array.Copy(Msg2.ResponderRandom, 0, salt2, Crypto.SYMMETRIC_KEY_LENGTH_BYTES, 32);
            Array.Copy(Msg2.ResponderEphPubKey, 0, salt2, Crypto.SYMMETRIC_KEY_LENGTH_BYTES + 32, Crypto.PUBLIC_KEY_SIZE_BYTES);
            Array.Copy(transcript2, 0, salt2, Crypto.SYMMETRIC_KEY_LENGTH_BYTES + 32 + Crypto.PUBLIC_KEY_SIZE_BYTES, Crypto.HASH_LEN_BYTES);
            byte[] S2K = Crypto.KDF(SharedSecret, salt2, S2K_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS);
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
            if (!responderNOC.EcPubKey.SequenceEqual(fabric.GetNOC(nodeId)!.PublicKey))
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
            if (!Crypto.Verify(responderNOC.EcPubKey, signatureBytes.GetPayload().ToArray(), tbeData2.Signature))
            {
                Console.WriteLine("Sigma2 invalid signature");
                StatusPayload status = new StatusPayload(GeneralCode.FAILURE, 0, ProtocolType.SecureChannel, (ushort)SecureStatusCodes.INVALID_PARAMETER);
                await exchange.SendFrame(new Frame(status, (byte)SecureOpCodes.StatusReport));
                return null;
            }

            PayloadWriter noc = new PayloadWriter(512);
            fabric.Commissioner!.ToMatterCertificate().Serialize(noc);

            Sigma3Tbsdata s3sig = new Sigma3Tbsdata()
            {
                InitiatorNOC = noc.GetPayload().ToArray(),
                InitiatorEphPubKey = ephKeys.Public,
                ResponderEphPubKey = Msg2.ResponderEphPubKey
            };
            PayloadWriter s3sigBytes = new PayloadWriter(512);
            s3sig.Serialize(s3sigBytes);

            Sigma3Tbedata s3tbe = new Sigma3Tbedata()
            {
                InitiatorNOC = s3sig.InitiatorNOC,
                Signature = Crypto.Sign(fabric.Commissioner.GetPrivateKey()!, s3sigBytes.GetPayload().ToArray())
            };
            PayloadWriter s3tbeBytes = new PayloadWriter(512);
            s3sig.Serialize(s3tbeBytes);

            byte[] Msg1Msg2 = new byte[Msg1Bytes.Length + Msg2Bytes.Length];
            Msg1Bytes.GetPayload().CopyTo(Msg1Msg2);
            Msg2Bytes.GetPayload().CopyTo(Msg1Msg2.AsMemory(Msg1Bytes.Length));
            byte[] transcript3 = Crypto.Hash(Msg1Msg2);
            byte[] salt3 = new byte[Crypto.SYMMETRIC_KEY_LENGTH_BYTES + Crypto.HASH_LEN_BYTES];
            Array.Copy(fabric.OperationalIdentityProtectionKey, salt3, Crypto.SYMMETRIC_KEY_LENGTH_BYTES);
            Array.Copy(transcript3, 0, salt3, Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.HASH_LEN_BYTES);
            byte[] S3K = Crypto.KDF(SharedSecret, salt3, S3K_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS);

            Sigma3 Msg3 = new Sigma3()
            {
                Encrypted3 = Crypto.AEAD_GenerateEncrypt(S3K, s3tbeBytes.GetPayload().Span, [], TBEData3_Nonce).ToArray()
            };
            PayloadWriter Msg3Bytes = new PayloadWriter(1024);
            Msg3.Serialize(Msg3Bytes);

            await exchange.SendFrame(new Frame(Msg3, (byte)SecureOpCodes.CASESigma3));
            Console.WriteLine("Sent SIGMA 3");
            resp = await exchange.Read();

            StatusPayload s3resp = (StatusPayload)resp.Message.Payload!;
            if (s3resp.GeneralCode != GeneralCode.SUCCESS)
                throw new IOException("CASE step 3 failed with status: " + (SecureStatusCodes)s3resp.ProtocolCode);

            byte[] salt = new byte[Crypto.SYMMETRIC_KEY_LENGTH_BYTES + Msg1Msg2.Length + Msg3Bytes.Length];
            fabric.OperationalIdentityProtectionKey.CopyTo(salt, 0);
            Msg1Msg2.CopyTo(salt, Crypto.SYMMETRIC_KEY_LENGTH_BYTES);
            Msg3Bytes.GetPayload().CopyTo(salt.AsMemory(Crypto.SYMMETRIC_KEY_LENGTH_BYTES + Msg1Msg2.Length));
            byte[] sessionKeys = Crypto.KDF(SharedSecret, salt, SEKeys_Info, Crypto.SYMMETRIC_KEY_LENGTH_BITS * 3);
            attestationChallenge = sessionKeys.AsSpan(2 * Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray();

            uint activeInterval = Msg2.ResponderSessionParams?.SessionActiveInterval ?? SessionManager.GetDefaultSessionParams().SessionActiveInterval!.Value;
            uint activeThreshold = Msg2.ResponderSessionParams?.SessionActiveThreshold ?? SessionManager.GetDefaultSessionParams().SessionActiveThreshold!.Value;
            uint idleInterval = Msg2.ResponderSessionParams?.SessionIdleInterval ?? SessionManager.GetDefaultSessionParams().SessionIdleInterval!.Value;

            Console.WriteLine("Created CASE session");
            SecureSession? session = SessionManager.CreateSession(unsecureSession.Connection, true, Msg1.InitiatorSessionId, Msg2.ResponderSessionId, 
                                                                  sessionKeys.AsSpan(0, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(), 
                                                                  sessionKeys.AsSpan(Crypto.SYMMETRIC_KEY_LENGTH_BYTES, Crypto.SYMMETRIC_KEY_LENGTH_BYTES).ToArray(), 
                                                                  false, idleInterval, activeInterval, activeThreshold);
            unsecureSession.Dispose();
            return session;
        }

        public byte[] GetAttestationChallenge()
        {
            return attestationChallenge;
        }
    }
}
