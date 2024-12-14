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

namespace MatterDotNet.Protocol.Sessions
{
    public class SecureSession : SessionContext
    {
        public bool PASE {  get; init; }
        public byte[] I2RKey { get; init; }
        public byte[] R2IKey { get; init; }
        public byte[] SharedSecret { get; init; }
        public uint LocalMessageCtr { get; init; }

        public SecureSession(IConnection connection, bool PASE, bool initiator, ushort localSessionID, ushort remoteSessionID, byte[] i2rKey, byte[] r2iKey, byte[] sharedSecret, uint localMessageCounter, MessageState remoteMessageCounter, ulong initiatorNodeId, ulong peerNodeId) : base(connection, initiator, initiatorNodeId, peerNodeId, localSessionID, remoteSessionID, remoteMessageCounter)
        {
            this.PASE = PASE;
            I2RKey = i2rKey;
            R2IKey = r2iKey;
            SharedSecret = sharedSecret;
            LocalMessageCtr = localMessageCounter;
        }

        internal override uint GetSessionCounter()
        {
            return LocalMessageCtr;
        }

        /*
         * Local Message Counter: Secure Session Message Counter for outbound messages.
            ◦ At successful session establishment, the Local Message Counter SHALL be initialized per Section
            4.6.1.1, “Message Counter Initialization”.
            9. Message Reception State: Provides tracking for the Secure Session Message Counter of the
            remote peer.
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

                    ActiveTimestamp: A timestamp indicating the time at which the last message was received. This
            timestamp SHALL be initialized with the time the session was created.
            15. The following Session parameters (see Table 22, “Glossary of Session parameters”):
            a. SESSION_IDLE_INTERVAL
            b. SESSION_ACTIVE_INTERVAL
            c. SESSION_ACTIVE_THRESHOLD
            d. PeerActiveMode: A boolean that tracks whether the peer node is in Active or Idle mode. PeerActiveMode
            is set as follows:
        PeerActiveMode = (now() - ActiveTimestamp) &lt; "SESSION_ACTIVE_THRESHOLD"
        */
    }
}
