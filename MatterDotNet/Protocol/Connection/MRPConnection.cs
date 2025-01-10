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
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Sessions;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;

namespace MatterDotNet.Protocol.Connection
{
    internal class MRPConnection : IConnection
    {
        private static readonly TimeSpan MRP_STANDALONE_ACK_TIMEOUT = TimeSpan.FromMilliseconds(200);
        private const int MRP_BACKOFF_THRESHOLD = 1;
        private const float MRP_BACKOFF_MARGIN = 1.1F;
        private const float MRP_BACKOFF_JITTER = 0.25F;
        private const float MRP_BACKOFF_BASE = 1.6F;
        private const int MRP_MAX_TRANSMISSIONS = 5;

        ConcurrentDictionary<(ushort, ushort), Retransmission> Retransmissions = new ConcurrentDictionary<(ushort, ushort), Retransmission>();
        ConcurrentDictionary<ushort, uint> AckTable = new ConcurrentDictionary<ushort, uint>();
        CancellationTokenSource cts = new CancellationTokenSource();

        UdpClient client;

        public MRPConnection(IPEndPoint ep)
        {
            client = new UdpClient(ep.AddressFamily);
            client.Connect(ep);
            Task.Factory.StartNew(Run);
        }

        public async Task SendFrame(Exchange exchange, Frame frame, bool reliable)
        {
            if (reliable)
                frame.Message!.Flags |= ExchangeFlags.Reliability;
            if (AckTable.TryRemove(frame.Message.ExchangeID, out uint ackCtr))
            {
                frame.Message.Flags |= ExchangeFlags.Acknowledgement;
                frame.Message.AckCounter = ackCtr;
            }
            PayloadWriter writer = new PayloadWriter(Frame.MAX_SIZE + 4);
            frame.Serialize(writer, exchange.Session);
            Retransmission? rt = null;
            if (reliable)
            {
                while (rt == null)
                {
                    rt = new Retransmission(exchange, frame.Counter, writer);
                    if (!Retransmissions.TryAdd((exchange.Session.LocalSessionID, frame.Message.ExchangeID), rt))
                    {
                        if (!Retransmissions.TryGetValue((exchange.Session.LocalSessionID, frame.Message.ExchangeID), out rt))
                            continue;
                        rt.Ack.Wait();
                        rt = null;
                    }
                }
            }
            Console.WriteLine(DateTime.Now.ToString("h:mm:ss") + " SENT: " + frame.ToString());
            await client.SendAsync(writer.GetPayload());
            exchange.Session.Timestamp = DateTime.Now;
            while (reliable)
            {
                try
                {
                    rt!.SendCount++;
                    if (rt.SendCount == MRP_MAX_TRANSMISSIONS)
                    {
                        rt.Ack.Release();
                        throw new IOException("Message retransmission timed out");
                    }
                    uint retryInterval = SessionManager.GetDefaultSessionParams().SessionActiveInterval!.Value;
                    if (exchange.Session is SecureSession secureSession)
                        retryInterval = secureSession.PeerActive ? secureSession.ActiveInterval : secureSession.IdleInterval;
                    double mrpBackoffTime = (retryInterval * MRP_BACKOFF_MARGIN) * Math.Pow(MRP_BACKOFF_BASE, (Math.Max(0, rt.SendCount - MRP_BACKOFF_THRESHOLD))) * (1.0 + Random.Shared.NextDouble() * MRP_BACKOFF_JITTER);
                    if (await rt.Ack.WaitAsync((int)mrpBackoffTime))
                        return;
                    else
                    {
                        await client.SendAsync(rt.data.GetPayload());
                        Console.WriteLine("RT #" + rt.SendCount);
                    }
                }
                catch (OperationCanceledException)
                {
                    await client.SendAsync(rt!.data.GetPayload());
                    Console.WriteLine("RT #" + rt.SendCount);
                }
            }
        }

        public async Task SendAck(SessionContext? session, ushort exchange, uint counter, bool initiator)
        {
            Frame ack = new Frame(null, (byte)SecureOpCodes.MRPStandaloneAcknowledgement);
            ack.SessionID = session!.RemoteSessionID;
            ack.Counter = session.GetSessionCounter();
            ack.Message.ExchangeID = exchange;
            ack.Message.Flags = ExchangeFlags.Acknowledgement;
            if (initiator)
            {
                ack.Flags |= MessageFlags.SourceNodeID;
                ack.Message.Flags |= ExchangeFlags.Initiator;
            }
            else
                ack.Flags |= MessageFlags.DestinationNodeID;
            ack.Message.AckCounter = counter;
            ack.Message.Protocol = Payloads.ProtocolType.SecureChannel;
            PayloadWriter writer = new PayloadWriter(Frame.MAX_SIZE);
            ack.Serialize(writer, session!);
            if (AckTable.TryGetValue(exchange, out uint ctr) && ctr == counter)
                AckTable.TryRemove(exchange, out _);
            Console.WriteLine(DateTime.Now.ToString("h:mm:ss") + " Sent standalone ack: " + ack.ToString());
            await client.SendAsync(writer.GetPayload());
        }

        public async Task Run()
        {
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    UdpReceiveResult result = await client.ReceiveAsync();
                    Frame frame = new Frame(result.Buffer);
                    if (!frame.Valid)
                    {
                        Console.WriteLine("Invalid frame received");
                        continue;
                    }
                    SessionContext? session = SessionManager.GetSession(frame.SessionID);
                    bool ack = false;
                    if ((frame.Message.Flags & ExchangeFlags.Reliability) == ExchangeFlags.Reliability)
                    {
                        if (!AckTable.TryAdd(frame.Message.ExchangeID, frame.Counter))
                        {
                            ack = true;
                            await SendAck(session, frame.Message.ExchangeID, frame.Counter, (frame.Message.Flags & ExchangeFlags.Initiator) == 0);
                        }
                    }
                    if ((frame.Message.Flags & ExchangeFlags.Acknowledgement) == ExchangeFlags.Acknowledgement)
                    {
                        if (Retransmissions.TryGetValue((frame.SessionID, frame.Message.ExchangeID), out Retransmission? transmission) && transmission.Counter == frame.Message.AckCounter)
                        {
                            Retransmissions.TryRemove((frame.SessionID, frame.Message.ExchangeID), out _);
                            transmission.Ack.Release();
                        }
                    }
                    Console.WriteLine(DateTime.Now.ToString("h:mm:ss") + " Received: " + frame.ToString());
                    if (session == null)
                    {
                        Console.WriteLine("Unknown Session: " + frame.SessionID);
                        continue;
                    }
                    if (!session.ProcessFrame(frame) && !ack)
                        await SendAck(session, frame.Message.ExchangeID, frame.Counter, (frame.Message.Flags & ExchangeFlags.Initiator) == 0);

                    session.Timestamp = DateTime.Now;
                    session.LastActive = DateTime.Now;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public async Task CloseExchange(Exchange exchange)
        {
            if (AckTable.TryGetValue(exchange.ID, out uint ctr))
                await SendAck(exchange.Session, exchange.ID, ctr, exchange.Session.Initiator);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
            client.Dispose();
        }
    }
}
