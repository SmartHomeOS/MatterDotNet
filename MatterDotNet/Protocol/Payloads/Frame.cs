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

using MatterDotNet.Protocol.Cryptography;
using MatterDotNet.Protocol.Payloads.Flags;
using MatterDotNet.Protocol.Sessions;
using System.Buffers;
using System.Buffers.Binary;
using System.Text;

namespace MatterDotNet.Protocol.Payloads
{
    /// <summary>
    /// Protocol Frame
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// Frame MTU Size
        /// </summary>
        public const int MAX_SIZE = 1280;
        internal static readonly byte[] PRIVACY_INFO = Encoding.UTF8.GetBytes("PrivacyKey");

        /// <summary>
        /// Message Flags
        /// </summary>
        public MessageFlags Flags { get; set; }
        /// <summary>
        /// Frame Session Number
        /// </summary>
        public ushort SessionID { get; set; }
        /// <summary>
        /// Security Flags
        /// </summary>
        public SecurityFlags Security { get; set; }
        /// <summary>
        /// Transmission Counter
        /// </summary>
        public uint Counter { get; set; }
        /// <summary>
        /// Source Node ID
        /// </summary>
        public ulong SourceNodeID { get; set; }
        /// <summary>
        /// Destination NodeID or GroupID
        /// </summary>
        public ulong DestinationID { get; set; }
        /// <summary>
        /// Version1 Message
        /// </summary>
        public Version1Payload Message { get; set; }
        /// <summary>
        /// Message is valid
        /// </summary>
        public bool Valid { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!Valid)
                return $"Invalid Frame: [F:{Flags}, Session: {SessionID}, S:{Security}, From: {SourceNodeID}, To: {DestinationID}, Ctr: {Counter}]";
            return $"Frame [F:{Flags}, Session: {SessionID}, S:{Security}, From: {SourceNodeID}, To: {DestinationID}, Ctr: {Counter}, Message: {Message}";
        }

        public void Serialize(PayloadWriter stream, SessionContext session)
        {
            stream.Write((byte)Flags);
            stream.Write(SessionID);
            stream.Write((byte)Security);
            stream.Write(Counter);
            if ((Flags & MessageFlags.SourceNodeID) == MessageFlags.SourceNodeID)
                stream.Write(SourceNodeID);
            if ((Flags & MessageFlags.DestinationNodeID) == MessageFlags.DestinationNodeID)
                stream.Write(DestinationID);
            else if ((Flags & MessageFlags.DestinationGroupID) == MessageFlags.DestinationGroupID)
                stream.Write(DestinationID);

            //Extensions not supported
            if (SessionID == 0)
            {
                Message.Serialize(stream);
            }
            else
            {
                byte[] temp = ArrayPool<byte>.Shared.Rent(MAX_SIZE);
                try
                {
                    PayloadWriter secureStream = new PayloadWriter(temp);
                    Message.Serialize(secureStream);
                    SecureSession secureContext = (SecureSession)session;
                    Span<byte> nonce = new byte[Crypto.NONCE_LENGTH_BYTES];
                    stream.GetPayload().Span.Slice(3, 5).CopyTo(nonce);
                    if ((Security & SecurityFlags.GroupSession) == SecurityFlags.GroupSession)
                        BinaryPrimitives.WriteUInt64LittleEndian(nonce.Slice(5, 8), SourceNodeID);
                    else if (!secureContext.PASE)
                        BinaryPrimitives.WriteUInt64LittleEndian(nonce.Slice(5, 8), session.Initiator ? session.InitiatorNodeID : session.ResponderNodeID);

                    ReadOnlySpan<byte> mic = Crypto.AEAD_GenerateEncrypt(secureContext.Initiator ? secureContext.I2RKey : secureContext.R2IKey, secureStream.GetPayload().Span, stream.GetPayload().Span, nonce);
                    stream.Write(secureStream);
                    stream.Write(mic);
                    if ((Security & SecurityFlags.Privacy) == SecurityFlags.Privacy)
                    {
                        byte[] privacyKey = Crypto.KDF(secureContext.Initiator ? secureContext.I2RKey : secureContext.R2IKey, [], PRIVACY_INFO, Crypto.SYMMETRIC_KEY_LENGTH_BITS);
                        Span<byte> ptr = stream.GetPayload().Span;
                        byte[] privacyNonce = new byte[Crypto.NONCE_LENGTH_BYTES];
                        BinaryPrimitives.WriteUInt16BigEndian(privacyNonce, SessionID);
                        mic.Slice(5, Crypto.AEAD_MIC_LENGTH_BYTES - 5).CopyTo(privacyNonce.AsSpan().Slice(2));
                        Crypto.Privacy_Encrypt(privacyKey, ptr.Slice(4, PrivacyBlockSize()), privacyNonce);
                    }
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(temp);
                }
            }
        }

        public Frame(IPayload? payload, byte opCode)
        {
            Valid = true;
            Message = new Version1Payload(payload, opCode);
        }

        public Frame(Span<byte> payload)
        {
            Valid = true;
            Flags = (MessageFlags)payload[0];
            SessionID = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(1, 2));
            Security = (SecurityFlags)payload[3];

            SecureSession? session = SessionManager.GetSession(SessionID) as SecureSession;

            if ((Security & SecurityFlags.Privacy) == SecurityFlags.Privacy)
            {
                if (session == null)
                    throw new InvalidDataException("Privacy requested in unsecured session");
                // Remove Privacy Encryption
                byte[] privacyKey = Crypto.KDF(session.Initiator ? session.R2IKey : session.I2RKey, [], PRIVACY_INFO, Crypto.SYMMETRIC_KEY_LENGTH_BITS);
                byte[] privacyNonce = new byte[Crypto.NONCE_LENGTH_BYTES];
                BinaryPrimitives.WriteUInt16BigEndian(privacyNonce, SessionID);
                payload.Slice(payload.Length - Crypto.AEAD_MIC_LENGTH_BYTES + 5).CopyTo(privacyNonce.AsSpan().Slice(2));
                Crypto.Privacy_Decrypt(privacyKey, payload.Slice(4, PrivacyBlockSize()), privacyNonce).CopyTo(payload.Slice(4, PrivacyBlockSize()));
            }
            Counter = BinaryPrimitives.ReadUInt32LittleEndian(payload.Slice(4, 4));
            Span<byte> slice = payload.Slice(8);
            if ((Flags & MessageFlags.SourceNodeID) == MessageFlags.SourceNodeID)
            {
                SourceNodeID = BinaryPrimitives.ReadUInt64LittleEndian(slice.Slice(0, 8));
                slice = slice.Slice(8);
            }
            if ((Flags & MessageFlags.DestinationNodeID) == MessageFlags.DestinationNodeID)
            {
                DestinationID = BinaryPrimitives.ReadUInt64LittleEndian(slice.Slice(0, 8));
                slice = slice.Slice(8);
            }
            else if ((Flags & MessageFlags.DestinationGroupID) == MessageFlags.DestinationGroupID)
            {
                DestinationID = BinaryPrimitives.ReadUInt16LittleEndian(slice.Slice(0, 2));
                slice = slice.Slice(2);
            }
            if ((Security & SecurityFlags.MessageExtensions) == SecurityFlags.MessageExtensions)
            {
                ushort len = BinaryPrimitives.ReadUInt16LittleEndian(slice.Slice(0, 2));
                slice = slice.Slice(2 + len);
            }
            if (SessionID == 0)
            {
                Message = new Version1Payload(slice);
            }
            else
            {
                if (session == null)
                    throw new InvalidDataException("Security context missing for session " + SessionID);
                Span<byte> nonce = new byte[Crypto.NONCE_LENGTH_BYTES];
                nonce[0] = (byte)Security;
                BinaryPrimitives.WriteUInt32LittleEndian(nonce.Slice(1, 4), Counter);
                if ((Security & SecurityFlags.GroupSession) == SecurityFlags.GroupSession)
                    BinaryPrimitives.WriteUInt64LittleEndian(nonce.Slice(5, 8), SourceNodeID);
                else if (!session.PASE)
                    BinaryPrimitives.WriteUInt64LittleEndian(nonce.Slice(5, 8), session.Initiator ? session.ResponderNodeID : session.InitiatorNodeID);

                if (Crypto.AEAD_DecryptVerify(session.Initiator ? session.R2IKey : session.I2RKey,
                                          slice,
                                          payload.Slice(0, payload.Length - slice.Length),
                                          nonce))
                    Message = new Version1Payload(slice.Slice(0, slice.Length - Crypto.AEAD_MIC_LENGTH_BYTES));
                else
                {
                    Message = null!;
                    Valid = false;
                }
            }
        }

        private int PrivacyBlockSize()
        {
            int ret = 4;
            if ((Flags & MessageFlags.SourceNodeID) == MessageFlags.SourceNodeID)
                ret += 8;
            if ((Flags & MessageFlags.DestinationNodeID) == MessageFlags.DestinationNodeID)
                ret += 8;
            else if ((Flags & MessageFlags.DestinationGroupID) == MessageFlags.DestinationGroupID)
                ret += 2;
            return ret;
        }
    }
}
