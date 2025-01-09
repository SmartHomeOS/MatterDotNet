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

using MatterDotNet.Security;

namespace MatterDotNet.OperationalDiscovery
{
    /// <summary>
    /// Parse Commissioning Payloads
    /// </summary>
    public class CommissioningPayload
    {
        /// <summary>
        /// Commissioning Transports Supported
        /// </summary>
        [Flags]
        public enum DiscoveryCapabilities
        {
            /// <summary>
            /// Unknown
            /// </summary>
            UNKNOWN = 0x0,
            /// <summary>
            /// Reserved (ignore)
            /// </summary>
            RESERVED = 0x1,
            /// <summary>
            /// Bluetooth LE
            /// </summary>
            BLE = 0x2,
            /// <summary>
            /// IP
            /// </summary>
            IP = 0x4,
        }
        /// <summary>
        /// Commissioning Flow
        /// </summary>
        public enum FlowType
        {
            /// <summary>
            /// Standard Flow
            /// </summary>
            STANDARD = 0,
            /// <summary>
            /// User Intent
            /// </summary>
            USER_INTENT = 1,
            /// <summary>
            /// Custom Flow (See instructions)
            /// </summary>
            CUSTOM = 2,
            /// <summary>
            /// Reserved for later use
            /// </summary>
            RESERVED = 3
        }

        /// <summary>
        /// Commissioning Transports Supported
        /// </summary>
        public DiscoveryCapabilities Capabilities { get; set; }
        /// <summary>
        /// Commissioning Flow
        /// </summary>
        public FlowType Flow { get; set; }
        /// <summary>
        /// Vendor ID Number (0 if unset)
        /// </summary>
        public ushort VendorID { get; set; }
        /// <summary>
        /// Product ID Number (0 if unset)
        /// </summary>
        public ushort ProductID { get; set; }
        /// <summary>
        /// Node Descriminator
        /// </summary>
        public ushort Discriminator { get; set; }
        /// <summary>
        /// Commissioning Passcode
        /// </summary>
        public uint Passcode { get; set; }
        /// <summary>
        /// Length of Discriminator (bits)
        /// </summary>
        public byte DiscriminatorLength { get; set; }
        /// <summary>
        /// Device Type
        /// </summary>
        public DeviceTypeEnum DeviceType { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Vendor: {VendorID}, Product: {ProductID}, Passcode: {Passcode}, Discriminator: {Discriminator:X}, Flow: {Flow}, Caps: {Capabilities}";
        }

        private CommissioningPayload() { }

        private CommissioningPayload(string QRCode)
        {
            byte[] data = Decode(QRCode.Substring(3));
            uint version = readBits(data, 0, 3);

            VendorID = (ushort)readBits(data, 3, 16);
            ProductID = (ushort)readBits(data, 19, 16);

            Flow = (FlowType)readBits(data, 35, 2);
            Capabilities = (DiscoveryCapabilities)readBits(data, 37, 8);
            DiscriminatorLength = 12;
            Discriminator = (ushort)readBits(data, 45, DiscriminatorLength);
            Passcode = readBits(data, 57, 27);
            uint padding = readBits(data, 84, 4);
            if (padding != 0)
                throw new ArgumentException("Invalid QR Code");
        }

        /// <summary>
        /// Create a payload from a QR code starting with "MT:"
        /// </summary>
        /// <param name="QRCode"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static CommissioningPayload FromQR(string QRCode)
        {
            if (!QRCode.StartsWith("MT:"))
                throw new ArgumentException("Invalid QR Code");
            return new CommissioningPayload(QRCode);
        }

        /// <summary>
        /// Create a payload from a Pairing PIN
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static CommissioningPayload FromPIN(string pin)
        {
            pin = pin.Replace("-", "");
            CommissioningPayload ret = new CommissioningPayload();
            if (pin.Length != 11 && pin.Length != 21)
                throw new ArgumentException("Invalid PIN");
            int actualChecksum = int.Parse(pin.Substring(pin.Length == 11 ? 10 : 20, 1));
            int computedChecksum = Checksum.GenerateVerhoeff(pin.Substring(0, 10));
            if (actualChecksum != computedChecksum)
                throw new ArgumentException("Pin Checksum Invalid: Should be " + computedChecksum);

            byte leading = byte.Parse(pin.Substring(0, 1));
            int version = (leading & 0x8) == 0 ? 0 : 1;
            bool vidpid = (leading & 0x4) == 0x4;
            ret.Discriminator = (ushort)((leading & 0x3) << 2);
            ushort group1 = ushort.Parse(pin.Substring(1, 5));
            ret.Discriminator |= (ushort)(group1 >> 14);
            ret.Passcode = (uint)(group1 & 0x3FFF);
            ushort group2 = ushort.Parse(pin.Substring(6, 4));
            ret.Passcode |= (uint)(group2 << 14);
            ret.DiscriminatorLength = 4;
            if (vidpid)
            {
                if (pin.Length != 21)
                    throw new ArgumentException("Truncated PIN code");
                ret.VendorID = ushort.Parse(pin.Substring(10, 5));
                ret.ProductID = ushort.Parse(pin.Substring(15, 5));
                ret.Flow = FlowType.CUSTOM;
            }
            else
                ret.Flow = FlowType.STANDARD;
            return ret;
        }

        private static byte[] Decode(string str)
        {
            List<byte> data = new List<byte>();
            for (int i = 0; i < str.Length; i += 5)
                data.AddRange(Unpack(str.Substring(i, Math.Min(5, str.Length - i))));
            return data.ToArray();
        }

        private static byte[] Unpack(string str)
        {
            uint digit = DecodeBase38(str);
            if (str.Length == 5)
            {
                byte[] result = new byte[3];
                result[0] = (byte)digit;
                result[1] = (byte)(digit >> 8);
                result[2] = (byte)(digit >> 16);
                return result;
            }
            else if (str.Length == 4)
            {
                return [(byte)digit, (byte)(digit >> 8)];
            }
            else if (str.Length == 2)
            {
                return [(byte)(digit & 0xFF)];
            }
            else
                throw new ArgumentException("Invalid QR String");
        }


        private static uint readBits(byte[] buf, int index, int numberOfBitsToRead)
        {
            uint dest = 0;

            int currentIndex = index;
            for (int bitsRead = 0; bitsRead < numberOfBitsToRead; bitsRead++)
            {
                if ((buf[currentIndex / 8] & 1 << currentIndex % 8) != 0)
                    dest |= (uint)(1 << bitsRead);
                currentIndex++;
            }
            return dest;
        }

        private static uint DecodeBase38(string sIn)
        {
            const string map = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-.";
            uint ret = 0;
            for (int i = sIn.Length - 1; i >= 0; i--)
                ret = (uint)(ret * 38 + map.IndexOf(sIn[i]));
            return ret;
        }
    }
}
