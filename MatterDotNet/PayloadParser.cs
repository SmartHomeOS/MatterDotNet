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

using MatterDotNet.Security;

namespace MatterDotNet
{
    public class PayloadParser
    {
        [Flags]
        public enum DiscoveryCapabilities
        {
            RESERVED = 0x1,
            BLE = 0x2,
            IP = 0x4,
        }
        public enum FlowType
        {
            STANDARD = 0,
            USER_INTENT = 1,
            CUSTOM = 2,
            RESERVED = 3
        }

        public DiscoveryCapabilities Capabiilities { get; set; }
        public FlowType Flow {  get; set; }
        public ushort VendorID { get; set; }
        public ushort ProductID { get; set; }
        public ushort Discriminator {  get; set; }
        public uint Passcode { get; set; }
        public byte DiscriminatorLength { get; set; }

        public override string ToString()
        {
            return $"Vendor: {VendorID}, Product: {ProductID}, Passcode: {Passcode}, Discriminator: {Discriminator:X}, Flow: {Flow}, Caps: {Capabiilities}";
        }

        private PayloadParser() { }

        private PayloadParser(string QRCode)
        {
            byte[] data = Decode(QRCode.Substring(3));
            uint version = readBits(data, 0, 3);

            VendorID = (ushort)readBits(data, 3, 16);
            ProductID = (ushort)readBits(data, 19, 16);

            Flow = (FlowType)readBits(data, 35, 2);
            Capabiilities = (DiscoveryCapabilities)readBits(data, 37, 8);
            DiscriminatorLength = 12;
            Discriminator = (ushort)readBits(data, 45, DiscriminatorLength);
            Passcode = readBits(data, 57, 27);
            uint padding = readBits(data, 84, 4);
            bool success = padding == 0;
        }

        public static PayloadParser FromQR(string QRCode)
        {
            if (!QRCode.StartsWith("MT:"))
                throw new ArgumentException("Invalid QR Code");
           return new PayloadParser(QRCode);
        }

        public static PayloadParser FromPIN(string pin)
        {
            PayloadParser ret = new PayloadParser();
            if (pin.Length != 11 && pin.Length != 21)
                throw new ArgumentException("Invalid PIN");
            int actualChecksum = int.Parse(pin.Substring(pin.Length == 11 ? 10 : 20, 1));
            int computedChecksum = Checksum.GenerateVerhoeff(pin.Substring(0, 10));
            if (actualChecksum != computedChecksum)
                throw new ArgumentException("Pin Checksum Invalid: Should be " + computedChecksum);

            byte leading = byte.Parse(pin.Substring(0, 1));
            int version = ((leading & 0x8) == 0) ? 0 : 1;
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


        static uint readBits(byte[] buf, int index, int numberOfBitsToRead)
        {
            uint dest = 0;

            int currentIndex = index;
            for (int bitsRead = 0; bitsRead < numberOfBitsToRead; bitsRead++)
            {
                if ((buf[currentIndex / 8] & (1 << (currentIndex % 8))) != 0)
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
