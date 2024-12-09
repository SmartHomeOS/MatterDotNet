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

namespace MatterDotNet.Protocol.Sessions
{
    public class Exchange : IDisposable
    {
        public ushort ID { get; init; }
        public SessionContext Session {get; init;}
        
        internal Exchange(SessionContext session, ushort id)
        {
            Session = session;
            ID = id;
        }

        public async Task SendFrame(Frame frame)
        {
            frame.SessionID = Session.LocalSessionID;
            if (Session.Initiator)
                frame.Message.Flags = ExchangeFlags.Initiator;
            frame.Message.ExchangeID = ID;
            frame.Counter = SessionManager.GlobalUnencryptedCounter;
            await Session.Connection.SendFrame(this, frame);
        }

        public async Task<Frame> Read()
        {
            return await Session.Connection.Read();
        }

        public void Dispose()
        {
            Session.DeleteExchange(this);
        }
    }
}
