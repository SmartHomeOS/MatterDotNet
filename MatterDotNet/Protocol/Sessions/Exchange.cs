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

using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.Flags;

namespace MatterDotNet.Protocol.Sessions
{
    public class Exchange : IDisposable
    {
        private const int MSG_COUNTER_WINDOW_SIZE = 32;

        public ushort ID { get; init; }
        public SessionContext Session {get; init;}
        
        internal Exchange(SessionContext session, ushort id)
        {
            Session = session;
            ID = id;
        }

        public async Task SendFrame(Frame frame, bool reliable = true)
        {
            frame.SessionID = Session.RemoteSessionID;
            if (Session.Initiator)
                frame.Message.Flags |= ExchangeFlags.Initiator;
            frame.Message.ExchangeID = ID;
            frame.Counter = SessionManager.GlobalUnencryptedCounter;
            await Session.Connection.SendFrame(this, frame, reliable);
        }

        public async Task<Frame> Read()
        {
            Frame? frame = null;
            while (frame == null)
            {
                frame = await Session.Connection.Read();
                MessageState state = Session.PeerMessageCtr;
                if (frame.Counter > state.MaxMessageCounter)
                {
                    int offset = (int)Math.Min(frame.Counter - state.MaxMessageCounter, MSG_COUNTER_WINDOW_SIZE);
                    state.MaxMessageCounter = frame.Counter;
                    state.CounterWindow <<= offset;
                    if (offset < MSG_COUNTER_WINDOW_SIZE)
                        state.CounterWindow |= (uint)(1 << (int)offset - 1);
                }
                else if (frame.Counter == state.MaxMessageCounter)
                {
                    Console.WriteLine("DROPPED DUPLICATE: " + frame);
                    frame = null;
                }
                else
                {
                    uint offset = (state.MaxMessageCounter - frame.Counter);
                    if (offset > MSG_COUNTER_WINDOW_SIZE)
                    {
                        if (Session is SecureSession)
                        {
                            Console.WriteLine("DROPPED DUPLICATE: " + frame);
                            frame = null;
                        }
                        else
                        {
                            state.MaxMessageCounter = frame.Counter;
                            state.CounterWindow = uint.MaxValue;
                        }
                    }
                    else
                    {
                        if ((state.CounterWindow & (uint)(1 << (int)offset - 1)) != 0x0)
                        {
                            Console.WriteLine("DROPPED DUPLICATE: " + frame);
                            frame = null;
                        }
                        else
                            state.CounterWindow |= (uint)(1 << (int)offset - 1);
                    }
                }
                Session.PeerMessageCtr = state;
            }
            return frame;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Console.WriteLine("Closing Exchange: " + ID);
            Session.DeleteExchange(this).Wait();
            GC.SuppressFinalize(this);
        }
    }
}
