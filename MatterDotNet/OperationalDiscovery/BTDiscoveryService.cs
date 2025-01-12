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

using InTheHand.Bluetooth;
using MatterDotNet.Protocol.Connection;
using System.Buffers.Binary;

namespace MatterDotNet.OperationalDiscovery
{
    /// <summary>
    /// Bluetooth LE Operational Discovery Service
    /// </summary>
    public static class BTDiscoveryService
    {
        /// <summary>
        /// Generate commissionable node info from BLE advertisement
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="bleAdvertisement"></param>
        /// <returns></returns>
        private static ODNode FromAdvertisement(string id, string name, ReadOnlySpan<byte> bleAdvertisement)
        {
            ODNode ret = new ODNode();
            if (bleAdvertisement[0] == 0x0)
                ret.CommissioningMode = CommissioningMode.Basic;
            ret.Discriminator = (ushort)(BinaryPrimitives.ReadUInt16LittleEndian(bleAdvertisement.Slice(1, 2)) & 0xFFF);
            ret.Vendor = BinaryPrimitives.ReadUInt16LittleEndian(bleAdvertisement.Slice(3, 2));
            ret.Product = BinaryPrimitives.ReadUInt16LittleEndian(bleAdvertisement.Slice(5, 2));
            ret.DeviceName = name;
            ret.BTAddress = id;
            return ret;
        }

        /// <summary>
        /// Discover all matter devices commissionable using Bluetooth LE
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public static async Task<ODNode[]> ScanAll()
        {
            Dictionary<string, ODNode> discoveredDevices = new Dictionary<string, ODNode>();
            void Bluetooth_AdvertisementReceived(object? sender, BluetoothAdvertisingEvent e)
            {
                if (e.ServiceData.ContainsKey(BTPConnection.MATTER_UUID) && e.Device != null && !discoveredDevices.ContainsKey(e.Device.Id))
                    discoveredDevices.Add(e.Device.Id, FromAdvertisement(e.Device.Id, e.Device?.Name ?? e.Name, e.ServiceData[BTPConnection.MATTER_UUID]));
            }
            if (await Bluetooth.GetAvailabilityAsync() == false)
                throw new PlatformNotSupportedException("No Bluetooth Adapter Found");
            BluetoothLEScanOptions opts = new BluetoothLEScanOptions();
            BluetoothLEScanFilter filter = new BluetoothLEScanFilter();
            filter.Services.Add(BTPConnection.MATTER_UUID);
            opts.Filters.Add(filter);
            opts.AcceptAllAdvertisements = false;
            opts.KeepRepeatedDevices = false;
            Bluetooth.AdvertisementReceived += Bluetooth_AdvertisementReceived;
            BluetoothLEScan scan = await Bluetooth.RequestLEScanAsync(opts);
            await Task.Delay(3000);
            scan.Stop();
            Bluetooth.AdvertisementReceived -= Bluetooth_AdvertisementReceived;
            return discoveredDevices.Values.ToArray();
        }

        /// <summary>
        /// Find a Bluetooth device that matches the provided payload
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<ODNode?> Find(CommissioningPayload payload)
        {
            SemaphoreSlim findLock = new SemaphoreSlim(0, 1);
            ODNode? result = null;
            void Bluetooth_AdvertisementReceived(object? sender, BluetoothAdvertisingEvent e)
            {
                if (e.ServiceData.ContainsKey(BTPConnection.MATTER_UUID))
                {
                    ODNode found = FromAdvertisement(e.Device.Id, e.Device?.Name ?? e.Name, e.ServiceData[BTPConnection.MATTER_UUID]);
                    if (found.Vendor != payload.VendorID && payload.VendorID != 0 && found.Vendor != 0)
                        return;
                    if (found.Product != payload.ProductID && payload.ProductID != 0 && found.Product != 0)
                        return;
                    if (payload.LongDiscriminator && found.Discriminator != payload.Discriminator)
                        return;
                    if (!payload.LongDiscriminator && (found.Discriminator >> 8) != payload.Discriminator)
                        return;
                    result = found;
                    findLock.Release();
                }
            }
            if (await Bluetooth.GetAvailabilityAsync() == false)
                throw new InvalidOperationException("No Bluetooth");
            BluetoothLEScanOptions opts = new BluetoothLEScanOptions();
            BluetoothLEScanFilter filter = new BluetoothLEScanFilter();
            filter.Services.Add(BTPConnection.MATTER_UUID);
            opts.Filters.Add(filter);
            opts.AcceptAllAdvertisements = false;
            opts.KeepRepeatedDevices = false;
            Bluetooth.AdvertisementReceived += Bluetooth_AdvertisementReceived;
            BluetoothLEScan scan = await Bluetooth.RequestLEScanAsync(opts);
            await findLock.WaitAsync(10000);
            scan.Stop();
            Bluetooth.AdvertisementReceived -= Bluetooth_AdvertisementReceived;
            return result;
        }
    }
}
