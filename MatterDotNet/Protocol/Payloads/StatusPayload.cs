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

using MatterDotNet.Protocol.Payloads.Status;
using System.Buffers.Binary;

namespace MatterDotNet.Protocol.Payloads
{
    public class StatusPayload : IPayload
    {
        /// <summary>
        /// General status codes conveyed in the GeneralCode field are uniform codes that convey both success and failures.
        /// </summary>
        public GeneralCode GeneralCode { get; set; }
        public ushort ProtocolVendor { get; set; }
        public ProtocolType ProtocolID { get; set; }
        public ushort ProtocolCode { get; set; }

        public StatusPayload(Memory<byte> data)
        {
            GeneralCode = (GeneralCode)BinaryPrimitives.ReadUInt16LittleEndian(data.Span);
            ProtocolVendor = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(2, 2).Span);
            ProtocolID = (ProtocolType)BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(4, 2).Span);
            ProtocolCode = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(6, 2).Span);
        }

        public StatusPayload(GeneralCode generalCode, ushort protocolVendor, ProtocolType protocolID, ushort protocolCode)
        {
            GeneralCode = generalCode;
            ProtocolVendor = protocolVendor;
            ProtocolID = protocolID;
            ProtocolCode = protocolCode;
        }

        public override string ToString()
        {
            if (ProtocolVendor == 0)
                return $"General Code: {GeneralCode}, Protocol: {ProtocolID}, Protocol Code: {ProtocolCode}";
            else
                return $"General Code: {GeneralCode}, Vendor: {ProtocolVendor:X2}, Protocol: {ProtocolID:X2}, Protocol Code: {ProtocolCode}";
        }

        public void Serialize(PayloadWriter stream)
        {
            stream.Write((ushort)GeneralCode);
            stream.Write(ProtocolVendor);
            stream.Write((ushort)ProtocolID);
            stream.Write(ProtocolCode);
        }
    }
}
