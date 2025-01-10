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
//
// WARNING: This file was auto-generated. Do not edit.

using System.Net;

namespace MatterDotNet.OperationalDiscovery
{
    /// <summary>
    /// Operational Discovery Node
    /// </summary>
    public record ODNode
    {
        /// <summary>
        /// Discovered IP Address
        /// </summary>
        public IPAddress? IPAddress { get; set; }
        /// <summary>
        /// Discovered Port
        /// </summary>
        public ushort Port { get; set; }
        /// <summary>
        /// Discovered BT LE Address
        /// </summary>
        public string BTAddress { get; set; }
        /// <summary>
        /// Idle Session Interval
        /// </summary>
        public int IdleInterval { get; set; }
        /// <summary>
        /// Active Session Interval
        /// </summary>
        public int ActiveInterval { get; set; }
        /// <summary>
        /// Active Threshold
        /// </summary>
        public int ActiveThreshold { get; set; }
        /// <summary>
        /// Supported Transports
        /// </summary>
        public SupportedTransportMode Transports { get; set; }
        /// <summary>
        /// Long Idle Time
        /// </summary>
        public bool LongIdleTime { get; set; }
        /// <summary>
        /// Device Name
        /// </summary>
        public string? DeviceName { get; set; }
        /// <summary>
        /// Device Type
        /// </summary>
        public DeviceTypeEnum Type { get; set; }
        /// <summary>
        /// Vendor ID
        /// </summary>
        public uint Vendor { get; set; }
        /// <summary>
        /// Product ID
        /// </summary>
        public uint Product {  get; set; }
        /// <summary>
        /// Discriminator
        /// </summary>
        public ushort Discriminator { get; set; }
        /// <summary>
        /// Current Commissioning Mode
        /// </summary>
        public CommissioningMode CommissioningMode { get; set; }

        public override string ToString()
        {
            return $"Vendor: {Vendor}, Product: {Product}, Discriminator: {Discriminator:X3}, Name: {DeviceName}, Address: {(BTAddress != null ? BTAddress : $"{IPAddress}:{Port}")}, Type: {Type}, Mode: {CommissioningMode}";
        }
    }
}
