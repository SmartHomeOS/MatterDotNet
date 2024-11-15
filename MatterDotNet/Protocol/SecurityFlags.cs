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

namespace MatterDotNet.Protocol
{
    [Flags]
    internal enum SecurityFlags : byte
    {
        UnicastSession = 0x0,
        GroupSession = 0x1,
        SessionMask = 0x3,
        MessageExtensions = 0x20,
        ControlMessage = 0x40,
        Privacy = 0x80,
    }
}