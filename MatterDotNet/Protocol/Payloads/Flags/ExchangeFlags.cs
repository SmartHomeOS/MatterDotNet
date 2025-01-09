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
    /// <summary>
    /// Frame exchange flags
    /// </summary>
    [Flags]
    public enum ExchangeFlags : byte
    {
        /// <summary>
        /// Sender is initiator
        /// </summary>
        Initiator = 0x1,
        /// <summary>
        /// Acknowledgement counter present
        /// </summary>
        Acknowledgement = 0x2,
        /// <summary>
        /// Reliability required
        /// </summary>
        Reliability = 0x4,
        /// <summary>
        /// Secure extensions present
        /// </summary>
        SecuredExtensions = 0x8,
        /// <summary>
        /// Vendor ID present
        /// </summary>
        VendorPresent = 0x10,
    }
}