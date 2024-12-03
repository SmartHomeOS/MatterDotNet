using MatterDotNet.Messages;
using MatterDotNet.Protocol.Cryptography;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Sessions
{
    public class SessionManager
    {
        public const int MSG_COUNTER_WINDOW_SIZE = 32;

        private static uint globalCtr;
        public SessionManager()
        {
            Span<byte> working_state = CTR_DRBG.Instantiate(RandomNumberGenerator.GetBytes(32), []);
            Span<byte> rnd = CTR_DRBG.Generate(ref working_state, 28);
            globalCtr = BinaryPrimitives.ReadUInt32LittleEndian(rnd) + 1;
        }

        public static SecureSession? GetSession(ushort sessionId, bool group)
        {
            if (group == false && sessionId == 0)
                return null; //Unsecured session
            return new SecureSession(false, false, sessionId, sessionId, [], [], []);
        }

        public static ushort GetAvailableSessionID()
        {
            return (ushort)Random.Shared.Next(0, ushort.MaxValue);
        }

        public static uint GlobalUnencryptedCounter
        {
            get
            {
                return Interlocked.Increment(ref globalCtr);
            }
        }

        public static SessionParameter GetSessionParams()
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
