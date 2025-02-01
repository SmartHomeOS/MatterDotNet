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

using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.Flags;
using System.Threading.Channels;

namespace MatterDotNet.Protocol.Sessions
{
    internal class Exchange : IDisposable
    {
        public ushort ID { get; init; }
        public SessionContext Session {get; init;}
        internal Channel<Frame> Messages { get; init;}
        
        internal Exchange(SessionContext session, ushort id)
        {
            Session = session;
            ID = id;
            Messages = Channel.CreateBounded<Frame>(10);
        }

        public async Task SendFrame(Frame frame, bool reliable = true, CancellationToken token = default)
        {
            try
            {
                frame.SessionID = Session.RemoteSessionID;
                if (Session.Initiator)
                    frame.Message.Flags |= ExchangeFlags.Initiator;
                frame.Message.ExchangeID = ID;
                frame.Counter = Session.GetSessionCounter();
                await Session.Connection.SendFrame(this, frame, reliable, token);
            }
            catch(OperationCanceledException e)
            {
                Console.WriteLine("Failed to send frame: " + e.ToString());
            }
        }

        public async Task<Frame> Read(CancellationToken token = default)
        {
            return await Messages.Reader.ReadAsync(token);
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
