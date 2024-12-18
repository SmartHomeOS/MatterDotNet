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

namespace MatterDotNet
{
    public class Constants
    {
        public const int MATTER_10_REVISION = 10;
        public const int MATTER_12_REVISION = 11;
        public const int MATTER_13_REVISION = 12;

        /// <summary>
        /// The current limit of 900 bytes was chosen to accommodate the maximum size of IPv6 frames, transport headers, 
        /// message layer headers and integrity protection and Interaction Model protocol encoding, 
        /// while accounting for sufficient remaining space for signatures and to allow extensions to larger key and digest sizes in the future.
        /// </summary>
        public const int RESP_MAX = 900;
    }
}
