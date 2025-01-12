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

namespace MatterDotNet.Protocol.Payloads.Flags
{
    [Flags]
    internal enum BTPFlags : byte
    {
        Beginning = 0x1,
        Continuing = 0x2,
        Ending = 0x4,
        Acknowledgement = 0x8,
        Reserved = 0x10,
        Management = 0x20,
        Handshake = 0x40,
    }
}
