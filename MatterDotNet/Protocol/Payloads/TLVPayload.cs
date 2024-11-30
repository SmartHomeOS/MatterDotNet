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

using MatterDotNet.Protocol.Parsers;

namespace MatterDotNet.Protocol.Payloads
{
    /// <summary>
    /// An application payload
    /// </summary>
    public abstract class TLVPayload : IPayload
    {
        /// <summary>
        /// An empty application payload
        /// </summary>
        public TLVPayload() { }
        /// <summary>
        /// Parse the TLVs from a frame into this message
        /// </summary>
        /// <param name="data"></param>
        public TLVPayload(Memory<byte> data) : this(new TLVReader(data)) {}

        /// <summary>
        /// Parse the TLVs from a frame into this message
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="structureNumber"></param>
        public TLVPayload(TLVReader reader, uint structureNumber = 0) { }

        /// <summary>
        /// Write the TLVs to an application payload
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="structureNumber"></param>
        public abstract void Serialize(TLVWriter writer, uint structureNumber = 0);

        /// <summary>
        /// Write the TLVs to an application payload
        /// </summary>
        /// <param name="writer"></param>
        /// <returns></returns>
        public bool Serialize(PayloadWriter writer)
        {
            Serialize(new TLVWriter(writer));
            return true;
        }
    }
}
