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

using MatterDotNet.Protocol.Connection;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Payloads.Status;
using MatterDotNet.Protocol.Subprotocols;
using System.Collections.Concurrent;

namespace MatterDotNet.Protocol.Sessions
{
    public class SessionContext : IDisposable
    {
        private const int MSG_COUNTER_WINDOW_SIZE = 32;

        public bool Initiator { get; init; }
        public ulong InitiatorNodeID { get; init; }
        public ushort LocalSessionID { get; init; }
        public ushort RemoteSessionID { get; init; }
        public ulong ResponderNodeID { get; init; }
        public MessageState PeerMessageCtr { get; set; }
        internal IConnection Connection { get; init; }
        public DateTime LastActive { get; set; }
        public DateTime Timestamp { get; set; }

        private ConcurrentDictionary<ushort, Exchange> exchanges = new ConcurrentDictionary<ushort, Exchange>();

        internal SessionContext(IConnection connection, bool initiator, ulong localNodeId, ulong peerNodeId, ushort localSessionId, ushort remoteSessionId, MessageState remoteCtr)
        {
            Connection = connection;
            Initiator = initiator;
            InitiatorNodeID = initiator ? localNodeId : peerNodeId;
            ResponderNodeID = initiator ? peerNodeId : localNodeId;
            LocalSessionID = localSessionId;
            RemoteSessionID = remoteSessionId;
            PeerMessageCtr = remoteCtr;
            Timestamp = DateTime.Now;
            LastActive = Timestamp;
        }

        internal virtual uint GetSessionCounter()
        {
            return SessionManager.GlobalUnencryptedCounter;
        }

        internal Exchange CreateExchange(ushort id)
        {
            if (exchanges.TryGetValue(id, out Exchange? exchange))
                return exchange;
            Exchange newExchange = new Exchange(this, id);
            if (!exchanges.TryAdd(id, newExchange))
                return CreateExchange(id);
            return newExchange;
        }

        internal Exchange CreateExchange()
        {
            if (exchanges.Count >= 5)
                throw new InvalidOperationException("5 Exchanges are already open");
            ushort exchangeId;
            do
            {
                exchangeId = (ushort)Random.Shared.Next(1, ushort.MaxValue);
            }
            while (exchanges.ContainsKey(exchangeId));
            Exchange ret = new Exchange(this, exchangeId);
            if (!exchanges.TryAdd(exchangeId, ret))
                return CreateExchange(); //Handle rare race condition
            return ret;
        }

        internal bool ProcessFrame(Frame frame)
        {
            MessageState state = PeerMessageCtr;
            if (!state.Initialized)
            {
                state.Initialized = true;
                state.CounterWindow = uint.MaxValue;
                state.MaxMessageCounter = frame.Counter;
            }
            else if (frame.Counter > state.MaxMessageCounter)
            {
                int offset = (int)Math.Min(frame.Counter - state.MaxMessageCounter, MSG_COUNTER_WINDOW_SIZE);
                state.MaxMessageCounter = frame.Counter;
                state.CounterWindow <<= offset;
                if (offset < MSG_COUNTER_WINDOW_SIZE)
                    state.CounterWindow |= (uint)(1 << (int)offset - 1);
            }
            else if (frame.Counter == state.MaxMessageCounter)
            {
                Console.WriteLine("DROPPED DUPLICATE <repeated last>: " + frame);
                return false;
            }
            else
            {
                uint offset = (state.MaxMessageCounter - frame.Counter);
                if (offset > MSG_COUNTER_WINDOW_SIZE)
                {
                    if (HandleBehindWindow(ref state, frame))
                        return false;
                }
                else
                {
                    if ((state.CounterWindow & (uint)(1 << (int)offset - 1)) != 0x0)
                    {
                        Console.WriteLine("DROPPED DUPLICATE <within window>: " + frame);
                        return false;
                    }
                    else
                        state.CounterWindow |= (uint)(1 << (int)offset - 1);
                }
            }
            PeerMessageCtr = state;
            if (frame.Message.Protocol == ProtocolType.SecureChannel && (SecureOpCodes?)frame.Message.OpCode == SecureOpCodes.MRPStandaloneAcknowledgement)
                return true; //Standalone Ack
            if (exchanges.TryGetValue(frame.Message.ExchangeID, out Exchange? exchange))
                exchange.Messages.Writer.TryWrite(frame);
            else
                Task.Factory.StartNew(async() => { await InteractionManager.HandleUnsolicited(this, frame); });
            return true;
        }

        internal virtual bool HandleBehindWindow(ref MessageState state, Frame frame)
        {
            state.MaxMessageCounter = frame.Counter;
            state.CounterWindow = uint.MaxValue;
            return false;
        }

        internal async Task DeleteExchange(Exchange exchange)
        {
            await Connection.CloseExchange(exchange);
            exchanges.TryRemove(exchange.ID, out _);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Console.WriteLine("Closing Session " + LocalSessionID);
            if (this is SecureSession secure && secure.Connection.Connected)
                CreateExchange().SendFrame(new Frame(new StatusPayload(GeneralCode.SUCCESS, 0, ProtocolType.SecureChannel, (ushort)SecureStatusCodes.CLOSE_SESSION), (byte)SecureOpCodes.StatusReport), false).Wait();
            var keys = exchanges.Keys;
            foreach (var key in keys)
                exchanges[key].Dispose();
            SessionManager.RemoveSession(LocalSessionID, Connection.EndPoint);
            GC.SuppressFinalize(this);
        }
    }
}
