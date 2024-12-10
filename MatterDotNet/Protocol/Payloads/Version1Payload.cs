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

using MatterDotNet.Messages.CASE;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Messages.MCSP;
using MatterDotNet.Messages.PASE;
using MatterDotNet.Protocol.Payloads.OpCodes;
using System.Buffers.Binary;

namespace MatterDotNet.Protocol.Payloads
{
    public class Version1Payload : IPayload
    {
        public ExchangeFlags Flags { get; set; }
        public byte OpCode { get; set; } //Depends on Protocol
        public ushort ExchangeID { get; set; }
        public ushort VendorID { get; set; }
        public ProtocolType Protocol { get; set; }
        public uint AckCounter { get; set; }
        public IPayload? Payload { get; set; }

        public override string ToString()
        {
            return $"[Flags: {Flags}, Proto: {Protocol}, Op: {OpCode}, Exchange: {ExchangeID}, Ack: {AckCounter}, Content: {Payload}]";
        }

        public Version1Payload(IPayload? payload)
        {
            this.Payload = payload;
        }

        public Version1Payload(ReadOnlySpan<byte> payload)
        {
            Flags = (ExchangeFlags)payload[0];
            OpCode = payload[1];
            ExchangeID = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(2, 2));
            if ((Flags & ExchangeFlags.VendorPresent) == ExchangeFlags.VendorPresent)
            {
                VendorID = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(4, 2));
                payload = payload.Slice(2);
            }
            Protocol = (ProtocolType)BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(4, 2));
            if ((Flags & ExchangeFlags.Acknowledgement) == ExchangeFlags.Acknowledgement)
            {
                AckCounter = BinaryPrimitives.ReadUInt32LittleEndian(payload.Slice(6, 4));
                payload = payload.Slice(4);
            }
            if ((Flags & ExchangeFlags.SecuredExtensions) == ExchangeFlags.SecuredExtensions)
            {
                ushort len = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(6, 2));
                payload = payload.Slice(2 + len);
            }
            Payload = CreatePayload(payload.Slice(6).ToArray());
        }

        private IPayload? CreatePayload(byte[] bytes)
        {
            switch (Protocol)
            {
                case ProtocolType.SecureChannel:
                    switch ((SecureOpCodes)OpCode)
                    {
                        case SecureOpCodes.MsgCounterSyncReq:
                            return new MessageCounterSyncRequest(bytes);
                        case SecureOpCodes.MsgCounterSyncRsp:
                            return new MessageCounterSyncResponse(bytes);
                        case SecureOpCodes.MRPStandaloneAcknowledgement:
                            return null;
                        case SecureOpCodes.PBKDFParamRequest:
                            return new PBKDFParamReq(bytes);
                        case SecureOpCodes.PBKDFParamResponse:
                            return new PBKDFParamResp(bytes);
                        case SecureOpCodes.PASEPake1:
                            return new Pake1(bytes);
                        case SecureOpCodes.PASEPake2:
                            return new Pake2(bytes);
                        case SecureOpCodes.PASEPake3:
                            return new Pake3(bytes);
                        case SecureOpCodes.CASESigma1:
                            return new Sigma1(bytes);
                        case SecureOpCodes.CASESigma2:
                            return new Sigma2(bytes);
                        case SecureOpCodes.CASESigma3:
                            return new Sigma3(bytes);
                        case SecureOpCodes.CASESigma2_Resume:
                            return new Sigma2Resume(bytes);
                        case SecureOpCodes.StatusReport:
                            return new StatusPayload(bytes);
                        case SecureOpCodes.ICDCheckInMessage:
                            break; //TODO - Return ActiveModeThreshold when clusters are implemented
                    }
                    break;
                case ProtocolType.InteractionModel:
                    switch ((IMOpCodes)OpCode)
                    {
                        case IMOpCodes.StatusResponse:
                            return new StatusResponseMessage(bytes);
                        case IMOpCodes.ReadRequest:
                            return new ReadRequestMessage(bytes);
                        case IMOpCodes.SubscribeRequest:
                            return new SubscribeRequestMessage(bytes);
                        case IMOpCodes.SubscribeResponse:
                            return new SubscribeResponseMessage(bytes);
                        case IMOpCodes.ReportData:
                            return new ReportDataMessage(bytes);
                        case IMOpCodes.WriteRequest:
                            return new WriteRequestMessage(bytes);
                        case IMOpCodes.WriteResponse:
                            return new WriteResponseMessage(bytes);
                        case IMOpCodes.InvokeRequest:
                            return new InvokeRequestMessage(bytes);
                        case IMOpCodes.InvokeResponse:
                            return new InvokeResponseMessage(bytes);
                        case IMOpCodes.TimedRequest:
                            return new TimedRequestMessage(bytes);
                    }
                    break;
            }
            throw new NotImplementedException($"Protocol: {Protocol}, OpCode: {OpCode}");
        }

        public void Serialize(PayloadWriter stream)
        {
            stream.Write((byte)Flags);
            stream.Write(OpCode);
            stream.Write(ExchangeID);
            if ((Flags & ExchangeFlags.VendorPresent) == ExchangeFlags.VendorPresent)
                stream.Write(VendorID);
            stream.Write((ushort)Protocol);
            if ((Flags & ExchangeFlags.Acknowledgement) == ExchangeFlags.Acknowledgement)
                stream.Write(AckCounter);
            if (Payload != null)
                Payload.Serialize(stream);
        }
    }
}