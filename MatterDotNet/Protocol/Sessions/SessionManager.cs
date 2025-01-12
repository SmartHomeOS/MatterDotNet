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

using MatterDotNet.Messages.PASE;
using MatterDotNet.Protocol.Connection;
using MatterDotNet.Protocol.Cryptography;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Sessions
{
    public static class SessionManager
    {
        private static uint globalCtr;
        private static ConcurrentDictionary<EndPoint, IConnection> connections = new ConcurrentDictionary<EndPoint, IConnection>();
        private static ConcurrentDictionary<EndPoint, ConcurrentDictionary<ushort, SessionContext>> sessions = new ConcurrentDictionary<EndPoint, ConcurrentDictionary<ushort, SessionContext>>();

        public static SessionContext GetUnsecureSession(EndPoint ep, bool initiator)
        {
            return GetUnsecureSession(GetConnection(ep), initiator);
        }

        internal static SessionContext GetUnsecureSession(IConnection connection, bool initiator)
        {
            ConcurrentDictionary<ushort, SessionContext>? existing;
            if (!sessions.TryGetValue(connection.EndPoint, out existing))
            {
                existing = new ConcurrentDictionary<ushort, SessionContext>();
                sessions.TryAdd(connection.EndPoint, existing);
            }
            if (existing.TryGetValue(0, out SessionContext? existingSession))
                return existingSession;
            SessionContext ctx = new SessionContext(connection, initiator, 0, 0, 0, 0, new MessageState());
            existing.TryAdd(0, ctx);
            return ctx;
        }

        internal static SecureSession? CreateSession(EndPoint ep, bool PASE, bool initiator, ushort initiatorSessionId, ushort responderSessionId, byte[] i2r, byte[] r2i, ulong localNodeId, ulong peerNodeId, byte[] sharedSecret, byte[] resumptionId, bool group, uint idleInterval, uint activeInterval, uint activeThreshold)
        {
            return CreateSession(GetConnection(ep), PASE, initiator, initiatorSessionId, responderSessionId, i2r, r2i, localNodeId, peerNodeId, sharedSecret, resumptionId, group, idleInterval, activeInterval, activeThreshold);
        }

        internal static SecureSession? CreateSession(IConnection connection, bool PASE, bool initiator, ushort initiatorSessionId, ushort responderSessionId, byte[] i2r, byte[] r2i, ulong localNodeId, ulong peerNodeId, byte[] sharedSecret, byte[] resumptionId, bool group, uint idleInterval, uint activeInterval, uint activeThreshold)
        {
            if (group == false && initiatorSessionId == 0)
                return null; //Unsecured session
            ConcurrentDictionary<ushort, SessionContext>? existing;
            if (!sessions.TryGetValue(connection.EndPoint, out existing))
            {
                existing = new ConcurrentDictionary<ushort, SessionContext>();
                sessions.TryAdd(connection.EndPoint, existing);
            }
            if (existing.TryGetValue(initiator ? initiatorSessionId : responderSessionId, out SessionContext? existingSession) && existingSession is SecureSession secure && secure.PASE == PASE)
                return secure;

            SecureSession ctx = new SecureSession(connection, PASE, initiator, initiator ? initiatorSessionId : responderSessionId, initiator ? responderSessionId : initiatorSessionId, i2r, r2i, sharedSecret, resumptionId, 0, new MessageState(), localNodeId, peerNodeId, idleInterval, activeInterval, activeThreshold);
            Console.WriteLine("Secure Session Created: " + ctx.LocalSessionID);
            existing.TryAdd(ctx.LocalSessionID, ctx);
            return ctx;
        }

        public static SessionContext? GetSession(ushort sessionId, EndPoint endPoint)
        {
            ConcurrentDictionary<ushort, SessionContext>? existing;
            if (!sessions.TryGetValue(endPoint, out existing))
                return null;
            if (existing.TryGetValue(sessionId, out SessionContext? existingSession))
                return existingSession;
            return null;
        }

        internal static void RemoveSession(ushort sessionId, EndPoint endPoint)
        {
            ConcurrentDictionary<ushort, SessionContext>? existing;
            if (!sessions.TryGetValue(endPoint, out existing))
                return;
            existing.TryRemove(sessionId, out _);
        }

        internal static ushort GetAvailableSessionID()
        {
            return (ushort)Random.Shared.Next(1, ushort.MaxValue);
        }

        public static uint GlobalUnencryptedCounter
        {
            get
            {
                if (globalCtr == 0)
                {
                    Span<byte> working_state = CTR_DRBG.Instantiate(RandomNumberGenerator.GetBytes(32), []);
                    Span<byte> rnd = CTR_DRBG.Generate(ref working_state, 28);
                    globalCtr = BinaryPrimitives.ReadUInt32LittleEndian(rnd) + 1;
                }
                return Interlocked.Increment(ref globalCtr);
            }
        }

        private static IConnection GetConnection(EndPoint endPoint)
        {
            if (connections.TryGetValue(endPoint, out IConnection? connection))
                return connection;
            if (endPoint is IPEndPoint ipep)
            {
                IConnection con = new MRPConnection(ipep);
                connections.TryAdd(endPoint, con);
                return con;
            }
            else if (endPoint is BLEEndPoint ble)
            {
                IConnection con = new BTPConnection(ble);
                connections.TryAdd(endPoint, con);
                return con;
            }
            throw new ArgumentException("Invalid EndPoint");
        }

        public static SessionParameter GetDefaultSessionParams()
        {
            SessionParameter param = new SessionParameter();
            param.SessionActiveThreshold = 4000;
            param.SessionActiveInterval = 500;
            param.SessionIdleInterval = 300;
            param.MaxPathsPerInvoke = 1;
            param.DataModelRevision = 17;
            param.InteractionModelRevision = Constants.MATTER_13_REVISION;
            param.SpecificationVersion = 0x01030000;
            return param;
        }
    }
}
