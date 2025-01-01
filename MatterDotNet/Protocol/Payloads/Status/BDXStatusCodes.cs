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
    /// Status Reports specific to the BDX Protocol are designated by embedding the PROTOCOL_ID_BDX in the ProtocolId field of the StatusReport body
    /// </summary>
    public enum BDXStatusCodes
    {
        /// <summary>
        /// Definite length too large to support. For example, trying to SendInit with too large of a file.
        /// </summary>
        LENGTH_TOO_LARGE = 0x12,
        /// <summary>
        /// Definite length proposed for transfer is too short for the context based on the responder’s knowledge of expected size.
        /// </summary>
        LENGTH_TOO_SHORT = 0x13,
        /// <summary>
        /// Pre-negotiated size of transfer was not fulfilled prior to BlockAckEOF.
        /// </summary>
        LENGTH_MISMATCH = 0x14,
        /// <summary>
        /// Responder can only support proposed transfer if definite length is provided.
        /// </summary>
        LENGTH_REQUIRED = 0x15,
        /// <summary>
        /// Received a malformed protocol message.
        /// </summary>
        BAD_MESSAGE_CONTENTS = 0x16,
        /// <summary>
        /// Received block counter out of order from expectation.
        /// </summary>
        BAD_BLOCK_COUNTER = 0x17,
        /// <summary>
        /// Received a well-formed message that was contextually inappropriate for the current state of the transfer.
        /// </summary>
        UNEXPECTED_MESSAGE = 0x18,
        /// <summary>
        /// Responder is too busy to proceed with a new transfer at this moment.
        /// </summary>
        RESPONDER_BUSY = 0x19,
        /// <summary>
        /// Other error occurred, such as perhaps an input/output error occurring at one of the peers.
        /// </summary>
        TRANSFER_FAILED_UNKNOWN_ERROR = 0x1F,
        /// <summary>
        /// Received a message that mismatches the current transfer mode.
        /// </summary>
        TRANSFER_METHOD_NOT_SUPPORTED = 0x50,
        /// <summary>
        /// Attempted to request a file whose designator is unknown to the responder.
        /// </summary>
        FILE_DESIGNATOR_UNKNOWN = 0x51,
        /// <summary>
        /// Proposed transfer with explicit start offset is not supported in current context.
        /// </summary>
        START_OFFSET_NOT_SUPPORTED = 0x52,
        /// <summary>
        /// Could not find a common supported version between initiator and responder.
        /// </summary>
        VERSION_NOT_SUPPORTED = 0x53,
        /// <summary>
        /// Other unexpected error.
        /// </summary>
        UNKNOWN = 0x5F
    }
}
