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
        private static ConcurrentDictionary<IPEndPoint, IConnection> connections = new ConcurrentDictionary<IPEndPoint, IConnection>();
        private static ConcurrentDictionary<ushort, SessionContext> sessions = new ConcurrentDictionary<ushort, SessionContext>();

        public static SessionContext GetUnsecureSession(IPEndPoint ep, bool initiator)
        {
            return GetUnsecureSession(GetConnection(ep), initiator);
        }

        internal static SessionContext GetUnsecureSession(IConnection connection, bool initiator)
        {
            if (sessions.TryGetValue(0, out SessionContext? existing))
                return existing;
            SessionContext ctx = new SessionContext(connection, initiator, 0, 0, 0, 0, new MessageState());
            sessions.TryAdd(0, ctx);
            return ctx;
        }

        public static SecureSession? CreateSession(IPEndPoint ep, bool PASE, bool initiator, ushort initiatorSessionId, ushort responderSessionId, byte[] i2r, byte[] r2i, ulong localNodeId, ulong peerNodeId, byte[] sharedSecret, byte[] resumptionId, bool group, uint idleInterval, uint activeInterval, uint activeThreshold)
        {
            return CreateSession(GetConnection(ep), PASE, initiator, initiatorSessionId, responderSessionId, i2r, r2i, localNodeId, peerNodeId, sharedSecret, resumptionId, group, idleInterval, activeInterval, activeThreshold);
        }

        internal static SecureSession? CreateSession(IConnection connection, bool PASE, bool initiator, ushort initiatorSessionId, ushort responderSessionId, byte[] i2r, byte[] r2i, ulong localNodeId, ulong peerNodeId, byte[] sharedSecret, byte[] resumptionId, bool group, uint idleInterval, uint activeInterval, uint activeThreshold)
        {
            if (group == false && initiatorSessionId == 0)
                return null; //Unsecured session
            SecureSession ctx = new SecureSession(connection, PASE, initiator, initiator ? initiatorSessionId : responderSessionId, initiator ? responderSessionId : initiatorSessionId, i2r, r2i, sharedSecret, resumptionId, 0, new MessageState(), localNodeId, peerNodeId, idleInterval, activeInterval, activeThreshold);
            Console.WriteLine("Secure Session Created: " + ctx.LocalSessionID);
            sessions.TryAdd(ctx.LocalSessionID, ctx);
            return ctx;
        }

        public static SessionContext? GetSession(ushort sessionId)
        {
            if (sessions.TryGetValue(sessionId, out SessionContext? ctx))
                return ctx;
            return null;
        }

        internal static void RemoveSession(ushort sessionId)
        {
            sessions.TryRemove(sessionId, out _);
        }

        internal static ushort GetAvailableSessionID()
        {
            return (ushort)Random.Shared.Next(0, ushort.MaxValue);
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

        private static IConnection GetConnection(IPEndPoint endPoint)
        {
            if (connections.TryGetValue(endPoint, out IConnection? connection))
                return connection;
            IConnection con = new MRPConnection(endPoint);
            connections.TryAdd(endPoint, con);
            return con;
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
            param.SpecificationVersion = 0;
            return param;
        }
    }
}
