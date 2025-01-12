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

using InTheHand.Bluetooth;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Payloads.Flags;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Sessions;
using System.Threading.Channels;

namespace MatterDotNet.Protocol.Connection
{
    internal class BTPConnection : IConnection
    {
        public static readonly BluetoothUuid MATTER_UUID = BluetoothUuid.FromShortId(0xFFF6);
        private static readonly BluetoothUuid C1_UUID = BluetoothUuid.FromGuid(Guid.Parse("18EE2EF5-263D-4559-959F-4F9C429F9D11"));
        private static readonly BluetoothUuid C2_UUID = BluetoothUuid.FromGuid(Guid.Parse("18EE2EF5-263D-4559-959F-4F9C429F9D12"));

        private static readonly TimeSpan CONN_RSP_TIMEOUT = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan ACK_TIME = TimeSpan.FromSeconds(6);
        private static readonly TimeSpan ACK_TIMEOUT = TimeSpan.FromSeconds(15);

        CancellationTokenSource cts = new CancellationTokenSource();
        private GattCharacteristic Read;
        private GattCharacteristic Write;
        Channel<BTPFrame> instream = Channel.CreateBounded<BTPFrame>(10);
        ushort MTU = 0;
        byte ServerWindow = 0;
        byte txCounter = 0; // First is 0
        byte rxCounter = 0;
        byte rxAcknowledged = 255; //Ensures we acknowledge the handshake
        byte txAcknowledged = 0;
        Timer? AckTimer;
        SemaphoreSlim WriteLock = new SemaphoreSlim(1, 1);
        bool connected;
        BluetoothDevice? device;

        public BTPConnection(BLEEndPoint bleDevice)
        {
            Connect(bleDevice.Address).Wait();
            AckTimer = new Timer(SendAck, null, ACK_TIME, ACK_TIME);
            if (Read == null || Write == null)
                throw new InvalidOperationException("Failed to initialize characteristics");
        }

        private async Task Connect(string deviceID)
        {
            device = await BluetoothDevice.FromIdAsync(deviceID);
            device.GattServerDisconnected += Device_GattServerDisconnected;
            await Connect();
        }

        private async Task Connect()
        { 
            if (!device!.Gatt.IsConnected)
                await device.Gatt.ConnectAsync();
            MTU = (ushort)Math.Min(device.Gatt.Mtu, 244);
            GattService service = await device.Gatt.GetPrimaryServiceAsync(MATTER_UUID);
            Write = await service.GetCharacteristicAsync(C1_UUID);
            Read = await service.GetCharacteristicAsync(C2_UUID);
            await Read.StopNotificationsAsync();

            await SendHandshake().WaitAsync(CONN_RSP_TIMEOUT);
            await Task.Factory.StartNew(Run);
        }

        private void Device_GattServerDisconnected(object? sender, EventArgs e)
        {
            connected = false;
            AckTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            rxAcknowledged = 255;
            Console.WriteLine(DateTime.Now + "** Disconnected **");
        }

        private async Task SendHandshake()
        {
            try
            {
                Console.WriteLine("Send Handshake Request");
                BTPFrame handshake = new BTPFrame(BTPFlags.Handshake | BTPFlags.Management | BTPFlags.Beginning | BTPFlags.Ending);
                handshake.OpCode = BTPManagementOpcode.Handshake;
                handshake.WindowSize = 8;
                handshake.ATT_MTU = MTU;
                await Write.WriteValueWithResponseAsync(handshake.Serialize(9));
                Read.CharacteristicValueChanged += Read_CharacteristicValueChanged;
                await Read.StartNotificationsAsync();

                BTPFrame frame = await instream.Reader.ReadAsync();
                MTU = frame.ATT_MTU;
                ServerWindow = frame.WindowSize;
                if (frame.Version != BTPFrame.MATTER_BT_VERSION1)
                    throw new NotSupportedException($"Version {frame.Version} not supported");
                connected = true;
                Console.WriteLine($"MTU: {MTU}, Window: {ServerWindow}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        private async void SendAck(object? state)
        {
            await WriteLock.WaitAsync();
            if (!connected)
                return;
            try
            {
                BTPFrame segment = new BTPFrame(BTPFlags.Acknowledgement);
                segment.Sequence = txCounter++;
                if (rxCounter != rxAcknowledged)
                {
                    segment.Acknowledge = rxCounter;
                    rxAcknowledged = rxCounter;
                }
                Console.WriteLine("[StandaloneAck] Wrote Segment: " + segment);
                await Write.WriteValueWithResponseAsync(segment.Serialize(MTU));
            }
            finally
            {
                WriteLock.Release();
            }
        }

        private void Read_CharacteristicValueChanged(object? sender, GattCharacteristicValueChangedEventArgs e)
        {
            if (e.Value != null)
            {
                BTPFrame frame = new BTPFrame(e.Value!);
                Console.WriteLine("BTP Received: " + frame);
                AckTimer?.Change(ACK_TIME, ACK_TIME);
                if ((frame.Flags & BTPFlags.Acknowledgement) != 0)
                    txAcknowledged = frame.Acknowledge;
                if ((frame.Flags & BTPFlags.Handshake) == 0)
                    rxCounter = frame.Sequence;
                if ((frame.Flags & BTPFlags.Continuing) != 0 || (frame.Flags & BTPFlags.Beginning) != 0)
                    instream.Writer.TryWrite(frame);
            }
        }

        public async Task SendFrame(Exchange exchange, Frame frame, bool reliable)
        {
            PayloadWriter writer = new PayloadWriter(Frame.MAX_SIZE);
            frame.Serialize(writer, exchange.Session);
            if (!connected)
                await Connect();
            await WaitForWindow();
            await WriteLock.WaitAsync();
            try
            {
                byte? ack = null;
                if (rxCounter != rxAcknowledged)
                {
                    ack = rxCounter;
                    rxAcknowledged = rxCounter;
                    AckTimer?.Change(Timeout.Infinite, Timeout.Infinite);
                }
                BTPFrame[] segments = BTPFrame.CreateSegments(frame, exchange.Session, MTU, ack);
                foreach (BTPFrame segment in segments)
                {
                    await WaitForWindow();
                    segment.Sequence = txCounter++;
                    Console.WriteLine("Wrote Segment: " + segment);
                    await Write.WriteValueWithResponseAsync(segment.Serialize(MTU));
                }
            }
            finally
            {
                WriteLock.Release();
            }
        }

        private async Task WaitForWindow()
        {
            while (txCounter - txAcknowledged > ServerWindow)
                await instream.Reader.WaitToReadAsync();
        }

        public async Task Run()
        {
            try
            {
                List<BTPFrame> segments = new List<BTPFrame>();
                while (!cts.IsCancellationRequested)
                {
                    BTPFrame segment = await instream.Reader.ReadAsync();
                    Console.WriteLine("Segment Received: " + segment);
                    segments.Add(segment);
                    if ((segment.Flags & BTPFlags.Ending) == 0x0)
                        continue;
                    PayloadWriter buffer = new PayloadWriter(segments[0].Length);
                    foreach (BTPFrame part in segments)
                        buffer.Write(part.Payload);
                    segments.Clear();
                    Frame frame = new Frame(buffer.GetPayload().Span);
                    if (!frame.Valid)
                    {
                        Console.WriteLine("Invalid frame received");
                        continue;
                    }
                    SessionContext? session = SessionManager.GetSession(frame.SessionID);
                    Console.WriteLine(DateTime.Now.ToString("h:mm:ss") + " Received: " + frame.ToString());
                    if (session == null)
                    {
                        Console.WriteLine("Unknown Session: " + frame.SessionID);
                        continue;
                    }
                    session.ProcessFrame(frame);
                    session.Timestamp = DateTime.Now;
                    session.LastActive = DateTime.Now;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Task CloseExchange(Exchange exchange)
        {
            // Nothing to do
            return Task.CompletedTask;
        }

        public bool Connected { get {  return connected; } }

        /// <inheritdoc />
        public void Dispose()
        {
            AckTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            Read.StopNotificationsAsync().Wait();
            cts.Cancel();
            cts.Dispose();
        }
    }
}
