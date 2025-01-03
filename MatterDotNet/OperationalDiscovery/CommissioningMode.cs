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
    /// Device Commissioning Mode
    /// </summary>
    public enum CommissioningMode
    {
        /// <summary>
        /// Device cannot be commissioined
        /// </summary>
        None = 0,
        /// <summary>
        /// Device is in Basic Commissioning Mode
        /// </summary>
        Basic = 1,
        /// <summary>
        /// Device is in Dynamic Commissioning Mode (with dynamically generated PIN)
        /// </summary>
        Dynamic = 2,
    }
}
