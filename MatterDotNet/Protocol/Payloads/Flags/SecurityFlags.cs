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

namespace MatterDotNet.Protocol.Payloads.Flags
{
    /// <summary>
    /// Frame Security Flags present
    /// </summary>
    [Flags]
    public enum SecurityFlags : byte
    {
        /// <summary>
        /// Session is unicast
        /// </summary>
        UnicastSession = 0x0,
        /// <summary>
        /// Session is multicast
        /// </summary>
        GroupSession = 0x1,
        /// <summary>
        /// Mask session type
        /// </summary>
        SessionMask = 0x3,
        /// <summary>
        /// Message extensions present
        /// </summary>
        MessageExtensions = 0x20,
        /// <summary>
        /// Message is a control message
        /// </summary>
        ControlMessage = 0x40,
        /// <summary>
        /// Privacy encryption enabled
        /// </summary>
        Privacy = 0x80,
    }
}