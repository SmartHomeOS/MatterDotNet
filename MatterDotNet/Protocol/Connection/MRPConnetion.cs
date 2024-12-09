﻿// MatterDotNet Copyright (C) 2024 
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
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Sessions;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;

namespace MatterDotNet.Protocol.Connection
{
    public class MRPConnetion : IConnection
    {
        public int RetryInterval { get; set; } = 500;

        private static readonly TimeSpan MRP_STANDALONE_ACK_TIMEOUT = TimeSpan.FromMilliseconds(200);
        private const int MRP_BACKOFF_THRESHOLD = 1;
        private const float MRP_BACKOFF_MARGIN = 1.1F;
        private const float MRP_BACKOFF_JITTER = 0.25F;
        private const float MRP_BACKOFF_BASE = 1.6F;
        private const int MRP_MAX_TRANSMISSIONS = 5;

        ConcurrentDictionary<(ushort, ushort), Retransmission> Retransmissions = new ConcurrentDictionary<(ushort, ushort), Retransmission>();
        ConcurrentDictionary<ushort, uint> AckTable = new ConcurrentDictionary<ushort, uint>();
        Channel<Frame> channel = Channel.CreateUnbounded<Frame>();
        CancellationTokenSource cts = new CancellationTokenSource();

        UdpClient client;

        public MRPConnetion(IPEndPoint ep)
        {
            client = new UdpClient(AddressFamily.InterNetwork);
            client.Connect(ep);
            Task.Factory.StartNew(Run);
        }

        public async Task SendFrame(Exchange exchange, Frame frame)
        {
            frame.Message!.Flags |= ExchangeFlags.Reliability;
            if (AckTable.TryRemove(frame.Message.ExchangeID, out uint ackCtr))
            {
                frame.Message.Flags |= ExchangeFlags.Acknowledgement;
                frame.Message.AckCounter = ackCtr;
            }
            PayloadWriter writer = new PayloadWriter(Frame.MAX_SIZE + 4);
            frame.Serialize(writer);
            Retransmission? rt = null;
            while (rt == null)
            {
                rt = new Retransmission(exchange, frame.Counter, writer);
                if (!Retransmissions.TryAdd((frame.SessionID, frame.Message.ExchangeID), rt))
                {
                    if (!Retransmissions.TryGetValue((frame.SessionID, frame.Message.ExchangeID), out rt))
                        continue;
                    rt.Ack.Wait();
                    rt = null;
                }
            }
            Console.WriteLine("SENT: " + frame.ToString());
            await client.SendAsync(writer.GetPayload());
            while (true)
            {
                try
                {
                    rt.SendCount++;
                    if (rt.SendCount == MRP_MAX_TRANSMISSIONS)
                    {
                        rt.Ack.Release();
                        throw new IOException("Message retransmission timed out");
                    }
                    double mrpBackoffTime = (RetryInterval * MRP_BACKOFF_MARGIN) * Math.Pow(MRP_BACKOFF_BASE, (Math.Max(0, rt.SendCount - MRP_BACKOFF_THRESHOLD))) * (1.0 + Random.Shared.NextDouble() * MRP_BACKOFF_JITTER);
                    await rt.Ack.WaitAsync((int)mrpBackoffTime);
                    Console.WriteLine("RT success");
                    return;
                }
                catch (OperationCanceledException)
                {
                    await client.SendAsync(rt.data.GetPayload());
                    Console.WriteLine("RT #" + rt.SendCount);
                }
            }
        }

        public async Task SendAck(ushort sessionID, ushort exchange, uint counter, bool initiator)
        {
            Frame ack = new Frame((IPayload?)null);
            ack.SessionID = sessionID;
            ack.Counter = SessionManager.GlobalUnencryptedCounter;
            ack.Message.ExchangeID = exchange;
            ack.Message.Flags = ExchangeFlags.Acknowledgement;
            if (initiator)
                ack.Message.Flags |= ExchangeFlags.Initiator;
            ack.Message.AckCounter = counter;
            ack.Message.Protocol = Payloads.ProtocolType.SecureChannel;
            ack.Message.OpCode = (byte)SecureOpCodes.MRPStandaloneAcknowledgement;
            PayloadWriter writer = new PayloadWriter(Frame.MAX_SIZE + 4);
            ack.Serialize(writer);
            if (AckTable.TryGetValue(exchange, out uint ctr) && ctr == counter)
                AckTable.TryRemove(exchange, out _);
            Console.WriteLine("Sent standalone ack: " + ack.ToString());
            await client.SendAsync(writer.GetPayload());
        }

        public async Task<Frame> Read()
        {
            return await channel.Reader.ReadAsync();
        }

        public async Task Run()
        {
            while (!cts.IsCancellationRequested)
            {
                UdpReceiveResult result = await client.ReceiveAsync();
                Frame frame = new Frame(result.Buffer);
                if ((frame.Message.Flags & ExchangeFlags.Reliability) == ExchangeFlags.Reliability)
                {
                    if (!AckTable.TryAdd(frame.Message.ExchangeID, frame.Counter))
                        await SendAck(frame.SessionID, frame.Message.ExchangeID, frame.Counter, (frame.Message.Flags & ExchangeFlags.Initiator) == 0);
                }
                if ((frame.Message.Flags & ExchangeFlags.Acknowledgement) == ExchangeFlags.Acknowledgement)
                {
                    if (Retransmissions.TryGetValue((frame.SessionID, frame.Message.ExchangeID), out Retransmission? transmission) && transmission.Counter == frame.Message.AckCounter)
                    {
                        Retransmissions.TryRemove((frame.SessionID, frame.Message.ExchangeID), out _);
                        transmission.Ack.Release();
                    }
                }
                Console.WriteLine("Received: " + frame.ToString());
                channel.Writer.TryWrite(frame);
            }
            channel.Writer.Complete();
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
