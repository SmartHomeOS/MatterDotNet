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

using MatterDotNet.Protocol.Connection;
using MatterDotNet.Protocol.Cryptography;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Sessions
{
    public class SecureSession : SessionContext
    {
        private uint localMessageCtr;
        public bool PASE {  get; init; }
        public byte[] I2RKey { get; init; }
        public byte[] R2IKey { get; init; }
        public byte[] SharedSecret { get; init; }
        public uint LocalMessageCtr { get { return Interlocked.Increment(ref localMessageCtr); } }
        public DateTime LastActive { get; set; }
        public uint ActiveInterval { get; init; }
        public uint IdleInterval { get; init; }
        public uint ActiveThreshold { get; init; }
        public bool PeerActive { get { return (DateTime.Now - LastActive).TotalMilliseconds < SessionManager.GetDefaultSessionParams().SessionActiveThreshold; } }

        internal SecureSession(IConnection connection, bool PASE, bool initiator, ushort localSessionID, ushort remoteSessionID, byte[] i2rKey, byte[] r2iKey, byte[] sharedSecret, uint localMessageCounter, MessageState remoteMessageCounter, ulong initiatorNodeId, ulong peerNodeId, uint sessionIdleInterval, uint sessionActiveInterval, uint sessionActiveThreshold) : base(connection, initiator, initiatorNodeId, peerNodeId, localSessionID, remoteSessionID, remoteMessageCounter)
        {
            this.PASE = PASE;
            I2RKey = i2rKey;
            R2IKey = r2iKey;
            SharedSecret = sharedSecret;
            ActiveInterval = sessionActiveInterval;
            IdleInterval = sessionIdleInterval;
            ActiveThreshold = sessionActiveThreshold;
            if (localMessageCounter == 0)
            {
                Span<byte> working_state = CTR_DRBG.Instantiate(RandomNumberGenerator.GetBytes(32), []);
                Span<byte> rnd = CTR_DRBG.Generate(ref working_state, 28);
                localMessageCtr = BinaryPrimitives.ReadUInt32LittleEndian(rnd) + 1;
            }
        }

        internal override uint GetSessionCounter()
        {
            return LocalMessageCtr;
        }

        /*
         * 
            10. Local Fabric Index: Records the local Index for the session’s Fabric, which MAY be used to look
            up Fabric metadata related to the Fabric for which this session context applies.
            ◦ This field SHALL contain the "no Fabric" value of 0 when the SessionType is PASE and successful
            invocation of the AddNOC command has not yet occurred during commissioning.
            11. Peer Node ID: Records the authenticated node ID of the remote peer, when available.
            ◦ This field SHALL contain the "Unspecified Node ID" value of 0 when the SessionType is PASE.
            12. Resumption ID: The ID used when resuming a session between the local and remote peer.
            13. SessionTimestamp: A timestamp indicating the time at which the last message was sent or
            received. This timestamp SHALL be initialized with the time the session was created. See Section
            4.11.1.1, “Session Establishment - Out of Resources” for more information.
        */
    }
}
