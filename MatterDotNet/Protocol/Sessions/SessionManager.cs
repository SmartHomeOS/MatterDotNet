using MatterDotNet.Protocol.Cryptography;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace MatterDotNet.Protocol.Sessions
{
    public class SessionManager
    {
        public const int MSG_COUNTER_WINDOW_SIZE = 32;

        private uint globalCtr;
        public SessionManager()
        {
            Span<byte> working_state = CTR_DRBG.Instantiate(RandomNumberGenerator.GetBytes(32), []);
            Span<byte> rnd = CTR_DRBG.Generate(ref working_state, 28);
            globalCtr = BinaryPrimitives.ReadUInt32LittleEndian(rnd) + 1;
        }

        public static SecureSession GetSession(ushort sessionId)
        {
            return new SecureSession(false, false, sessionId, sessionId, [], [], []);
        }

        public static ushort GetAvailableSessionID()
        {
            return (ushort)Random.Shared.Next(0, ushort.MaxValue);
        }

        public uint GlobalUnencryptedCounter
        {
            get
            {
                return Interlocked.Increment(ref globalCtr);
            }
        }
    }
}
