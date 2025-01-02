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
    public record ODNode
    {
        public IPAddress? Address { get; set; }
        public ushort Port { get; set; }
        public int IdleInterval { get; set; }
        public int ActiveInterval { get; set; }
        public int ActiveThreshold { get; set; }
        public SupportedTransportMode Transports { get; set; }
        public bool LongIdleTime { get; set; }
        public string? DeviceName { get; set; }
        public DeviceTypeEnum Type { get; set; }
        public uint Vendor { get; set; }
        public uint Product {  get; set; }
        public ushort Descriminator { get; set; }
        public bool Commissionable { get; set; }
    }
}
