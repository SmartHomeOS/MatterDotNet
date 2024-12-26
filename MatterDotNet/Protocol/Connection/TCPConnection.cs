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
using MatterDotNet.Protocol.Sessions;
using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;

namespace MatterDotNet.Protocol.Connection
{
    internal class TCPConnection : IConnection
    {
        TcpClient client;
        NetworkStream stream;
        CancellationTokenSource cts = new CancellationTokenSource();
        Channel<Frame> channel = Channel.CreateUnbounded<Frame>();
        public TCPConnection(IPEndPoint destination)
        {
            client = new TcpClient();
            client.Connect(destination);
            stream = client.GetStream();
            Task.Factory.StartNew(Run);
        }

        public async Task SendFrame(Exchange exchange, Frame frame, bool reliable)
        {
            PayloadWriter writer = new PayloadWriter(Frame.MAX_SIZE + 4);
            writer.Seek(4);
            frame.Serialize(writer, exchange.Session);
            BinaryPrimitives.WriteUInt32LittleEndian(writer.GetPayload().Slice(0, 4).Span, (uint)writer.Length);
            await stream.WriteAsync(writer.GetPayload());
        }

        public async Task<Frame> Read()
        {
            return await channel.Reader.ReadAsync();
        }

        public async Task Run()
        { 
            byte[] len = new byte[4];
            Memory<byte> data = new byte[Frame.MAX_SIZE];
            int frameLen;
            while (!cts.IsCancellationRequested)
            {
                await stream.ReadExactlyAsync(len);
                frameLen = BinaryPrimitives.ReadInt32LittleEndian(len);
                await stream.ReadExactlyAsync(data.Slice(0, frameLen));
                Console.WriteLine("READ: " + Convert.ToHexString(data.Slice(0, frameLen).Span));
                channel.Writer.TryWrite(new Frame(data.Slice(0, frameLen).Span));
            }
            channel.Writer.Complete();
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
            client.Dispose();
        }

        public Task Close(Exchange exchange)
        {
            //Do Nothing
            return Task.CompletedTask;
        }
    }
}
