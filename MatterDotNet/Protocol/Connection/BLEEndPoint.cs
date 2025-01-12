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

using System.Net;

namespace MatterDotNet.Protocol.Connection
{
    /// <summary>
    /// Bluetooth End Point
    /// </summary>
    public class BLEEndPoint : EndPoint
    {
        private string address;

        /// <summary>
        /// Create a Bluetooth End Point
        /// </summary>
        /// <param name="address"></param>
        public BLEEndPoint(string address)
        {
            this.address = address;
        }

        /// <summary>
        /// Get the BT Address associated with this endpoint
        /// </summary>
        public string Address {  get { return address; } }
    }
}
