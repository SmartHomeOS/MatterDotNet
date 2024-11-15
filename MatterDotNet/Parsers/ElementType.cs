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

namespace MatterDotNet.Parsers
{
    internal enum ElementType
    {
        SignedByte = 0,
        SignedShort = 1,
        SignedInt = 2,
        SignedLong = 3,
        UnsignedByte = 4,
        UnsignedShort = 5,
        UnsignedInt = 6,
        UnsignedLong = 7,
        True = 8,
        False = 9,
        Float = 10,
        Double = 11,
        /// <summary>
        /// 1 Byte Length UTF-8 String
        /// </summary>
        ByteString = 12,
        /// <summary>
        /// 2 Byte Length UTF-8 String
        /// </summary>
        ShortString = 13,
        /// <summary>
        /// 4 Byte Length UTF-8 String
        /// </summary>
        IntString = 14,
        /// <summary>
        /// 8 Byte Length UTF-8 String
        /// </summary>
        LongString = 15,
        /// <summary>
        /// 1 Byte Length Octet String
        /// </summary>
        ByteBytes = 16,
        /// <summary>
        /// 2 Byte Length Octet String
        /// </summary>
        ShortBytes = 17,
        /// <summary>
        /// 4 Byte Length Octet String
        /// </summary>
        IntBytes = 18,
        /// <summary>
        /// 8 Byte Length Octet String
        /// </summary>
        LongBytes = 19,
        Null = 20,
        Structure = 21,
        Array = 22,
        List = 23,
        EndOfContainer = 24,
    }
}
