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

namespace MatterDotNet.OperationalDiscovery
{
    /// <summary>
    /// Connectivity method between a node and a controller
    /// </summary>
    [Flags]
    public enum FabricInterface
    {
        /// <summary>
        /// No connectivity Type
        /// </summary>
        None = 0x0,
        /// <summary>
        /// WiFi Connection
        /// </summary>
        WiFi = 0x1,
        /// <summary>
        /// Thread 802.11.4
        /// </summary>
        Thread = 0x2,
        /// <summary>
        /// Ethernet
        /// </summary>
        Ethernet = 0x4,
        /// <summary>
        /// One or more IP protocols (aka anything but bluetooth)
        /// </summary>
        IP = 0x8,
        /// <summary>
        /// Bluetooth LE
        /// </summary>
        Bluetooth = 0x10
    }
}
