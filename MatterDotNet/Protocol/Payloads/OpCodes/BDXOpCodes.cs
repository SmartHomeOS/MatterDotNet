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

namespace MatterDotNet.Protocol.Payloads.OpCodes
{
    public enum BDXOpCodes
    {
        SendInit = 0x01,
        SendAccept = 0x02,
        Reserved = 0x03,
        ReceiveInit = 0x04,
        ReceiveAccept = 0x05,
        BlockQuery = 0x10,
        Block = 0x11,
        BlockEOF = 0x12,
        BlockAck = 0x13,
        BlockAckEOF = 0x14,
        BlockQueryWithSkip = 0x15,
    }
}
