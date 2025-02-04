﻿// MatterDotNet Copyright (C) 2025
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

using MatterDotNet.Protocol.Payloads.Flags;
using MatterDotNet.Protocol.Payloads.OpCodes;
using MatterDotNet.Protocol.Sessions;
using System.Buffers.Binary;

namespace MatterDotNet.Protocol.Payloads
{
    internal class BTPFrame
    {
        public const byte MATTER_BT_VERSION1 = 0x04;

        public BTPFlags Flags {  get; set; }
        public BTPManagementOpcode OpCode { get; set; }
        public byte Acknowledge { get; set; }
        public byte Sequence { get; set; }
        public ushort Length { get; set; }
        public Memory<byte> Payload { get; set; }
        public ushort ATT_MTU { get; set; }
        public byte WindowSize { get; set; }
        public byte Version { get; private set; }

        private BTPFrame() { }

        public BTPFrame(BTPFlags flags)
        {
            Flags = flags;
        }

        public BTPFrame(Memory<byte> payload)
        {
            int pos = 0;
            Span<byte> span = payload.Span;
            Flags = (BTPFlags)span[pos++];
            if ((Flags & BTPFlags.Management) != 0)
            {
                OpCode = (BTPManagementOpcode)span[pos++];
                Version = (byte)(span[pos++] & 0xF);
            }
            if ((Flags & BTPFlags.Acknowledgement) != 0)
                Acknowledge = span[pos++];
            if ((Flags & BTPFlags.Handshake) == 0)
            {
                Sequence = span[pos++];
                if ((Flags & BTPFlags.Beginning) != 0)
                {
                    Length = BinaryPrimitives.ReadUInt16LittleEndian(span.Slice(pos, 2));
                    pos += 2;
                }
                Payload = payload.Slice(pos);
            }
            if ((Flags & BTPFlags.Handshake) != 0)
            {
                ATT_MTU = BinaryPrimitives.ReadUInt16LittleEndian(span.Slice(pos, 2));
                WindowSize = span[pos + 2];
            } 
        }

        public byte[] Serialize(int mtu)
        {
            PayloadWriter stream = new PayloadWriter(mtu);
            stream.Write((byte)Flags);
            if ((Flags & BTPFlags.Management) != 0)
                stream.Write((byte)OpCode);
            if ((Flags & BTPFlags.Acknowledgement) != 0)
                stream.Write(Acknowledge);
            if ((Flags & BTPFlags.Handshake) == 0)
            {
                stream.Write(Sequence);
                if ((Flags & BTPFlags.Beginning) != 0)
                    stream.Write(Length);
                stream.Write(Payload.Span);
            }
            else
            {
                stream.Write([MATTER_BT_VERSION1, 0x0, 0x0, 0x0]);
                stream.Write(ATT_MTU);
                stream.Write(WindowSize);
            }
            return stream.GetPayload().ToArray();
        }

        public override string ToString()
        {
            return $"Flags: {Flags}, Seq: {Sequence}, Ack: {Acknowledge}, Len: {Length}, Payload: {Payload.Length}";
        }

        internal static BTPFrame[] CreateSegments(Frame frame, SessionContext session, ushort maxSegment, byte? ack = null)
        {
            PayloadWriter writer = new PayloadWriter(Frame.MAX_SIZE);
            frame.Serialize(writer, session);
            List<BTPFrame> segments = new List<BTPFrame>();
            ushort bytesPacked = 0;
            do
            {
                ushort header = 2; //Flags + sequence
                BTPFrame segment = new BTPFrame();
                if (segments.Count == 0)
                {
                    segment.Flags = BTPFlags.Beginning;
                    segment.Length = (ushort)writer.Length;
                    header += 2; //Length field
                    if (ack.HasValue)
                    {
                        header += 1;
                        segment.Acknowledge = ack.Value;
                        segment.Flags |= BTPFlags.Acknowledgement;
                    }
                }
                else
                    segment.Flags = BTPFlags.Continuing;
                
                ushort segmentSize = (ushort)Math.Min(writer.Length - bytesPacked, maxSegment - header); 
                if (segmentSize + bytesPacked == writer.Length)
                    segment.Flags |= BTPFlags.Ending;
                segment.Payload = writer.GetPayload().Slice(bytesPacked, segmentSize);

                segments.Add(segment);
                bytesPacked += segmentSize;
            } 
            while (bytesPacked < writer.Length);
            return segments.ToArray();
        }
    }
}
