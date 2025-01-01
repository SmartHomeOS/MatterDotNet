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

namespace MatterDotNet.Protocol.Payloads.Status
{
    /// <summary>
    /// Status Reports specific to the Secure Channel are designated by embedding the PROTOCOL_ID_SECURE_CHANNEL in the ProtocolId field of the StatusReport body
    /// </summary>
    public enum SecureStatusCodes
    {
        /// <summary>
        /// Indication that the last session establishment message was successfully processed.
        /// </summary>
        SESSION_ESTABLISHMENT_SUCCESS = 0x0,
        /// <summary>
        /// Failure to find a common set of shared roots.
        /// </summary>
        NO_SHARED_TRUST_ROOTS = 0x1,
        /// <summary>
        /// Generic failure during session establishment.
        /// </summary>
        INVALID_PARAMETER = 0x2,
        /// <summary>
        /// Indication that the sender will close the current session.
        /// </summary>
        CLOSE_SESSION = 0x3,
        /// <summary>
        /// Indication that the sender cannot currently fulfill the request.
        /// </summary>
        BUSY = 0x4
    }
}
