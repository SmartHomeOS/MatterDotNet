using MatterDotNet.Messages;
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
        public const int MSG_COUNTER_WINDOW_SIZE = 32;

        private static uint globalCtr;
        private static ConcurrentDictionary<IPEndPoint, IConnection> connections = new ConcurrentDictionary<IPEndPoint, IConnection>();
        private static ConcurrentDictionary<ushort, SessionContext> sessions = new ConcurrentDictionary<ushort, SessionContext>();

        public static SessionContext GetUnsecureSession(IPEndPoint ep, bool initiator, uint initiatorNodeId)
        {
            SessionContext ctx = new SessionContext(GetConnection(ep), initiator, initiatorNodeId, 0);
            sessions.TryAdd(0, ctx);
            return ctx;
        }

        public static SecureSession? GetSession(IPEndPoint ep, bool initiator, ushort sessionId, byte[] i2r, byte[] r2i, bool group)
        {
            if (group == false && sessionId == 0)
                return null; //Unsecured session
            Console.WriteLine("Secure Session Created: " + sessionId);
            SecureSession ctx = new SecureSession(GetConnection(ep), false, initiator, sessionId, sessionId, i2r, r2i, [], 0, 0, 0);
            sessions.TryAdd(sessionId, ctx);
            return ctx;
        }

        internal static SessionContext? GetSession(ushort sessionId, bool initiator)
        {
            if (sessions.TryGetValue(sessionId, out SessionContext ctx))
                return ctx;
            return null;
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
            IConnection con = new MRPConnetion(endPoint);
            connections.TryAdd(endPoint, con);
            return con;
        }

        public static SessionParameter GetDefaultSessionParams()
        {
            SessionParameter param = new SessionParameter();
            param.SESSION_ACTIVE_THRESHOLD = 4000;
            param.SESSION_ACTIVE_INTERVAL = 500;
            param.SESSION_IDLE_INTERVAL = 300;
            param.MAX_PATHS_PER_INVOKE = 1;
            param.DATA_MODEL_REVISION = 17;
            param.INTERACTION_MODEL_REVISION = 11;
            param.SPECIFICATION_VERSION = 0;
            return param;
        }
    }
}
