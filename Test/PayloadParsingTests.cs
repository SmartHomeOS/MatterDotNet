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

using MatterDotNet.OperationalDiscovery;

namespace Test
{
    public class PayloadParsingTests
    {

        [Test]
        public void PIN_AllOnes()
        {
            string PIN = "765535819165535655359";
            CommissioningPayload parser = CommissioningPayload.FromPIN(PIN);
            Assert.That(parser.Discriminator, Is.EqualTo(0xF));
            Assert.That(parser.VendorID, Is.EqualTo(65535), "Invalid Vendor ID");
            Assert.That(parser.ProductID, Is.EqualTo(65535), "Invalid Product ID");
            Assert.That(parser.Passcode, Is.EqualTo(0x7FFFFFF), "Invalid Passcode");
            Assert.That(parser.LongDiscriminator, Is.EqualTo(false), "Invalid Discriminator Length");
        }

        [Test]
        public void PIN_TestValuesLong()
        {
            string PIN = "641295075300001000018";
            CommissioningPayload parser = CommissioningPayload.FromPIN(PIN);
            Assert.That(parser.Discriminator, Is.EqualTo(0xA));
            Assert.That(parser.VendorID, Is.EqualTo(1), "Invalid Vendor ID");
            Assert.That(parser.ProductID, Is.EqualTo(1), "Invalid Product ID");
            Assert.That(parser.Passcode, Is.EqualTo(12345679), "Invalid Passcode");
            Assert.That(parser.LongDiscriminator, Is.EqualTo(false), "Invalid Discriminator Length");
        }

        [Test]
        public void PIN_TestValuesShort()
        {
            string PIN = "00362159269";
            CommissioningPayload parser = CommissioningPayload.FromPIN(PIN);
            Assert.That(parser.Discriminator, Is.EqualTo(0x0));
            Assert.That(parser.VendorID, Is.EqualTo(0), "Vendor ID should not exist");
            Assert.That(parser.ProductID, Is.EqualTo(0), "Product ID should not exist");
            Assert.That(parser.Passcode, Is.EqualTo(97095205), "Invalid Passcode");
            Assert.That(parser.LongDiscriminator, Is.EqualTo(false), "Invalid Discriminator Length");
        }

        [Test]
        public void QR_Test()
        {
            string QR = "MT:Y.K9042C00KA0648G00";
            CommissioningPayload parser = CommissioningPayload.FromQR(QR);
            Assert.That(parser.Discriminator, Is.EqualTo(3840));
            Assert.That(parser.VendorID, Is.EqualTo(0xfff1), "Invalid Vendor ID");
            Assert.That(parser.ProductID, Is.EqualTo(0x8000), "Invalid Product ID");
            Assert.That(parser.Passcode, Is.EqualTo(20202021), "Invalid Passcode");
            Assert.That(parser.Capabilities, Is.EqualTo(CommissioningPayload.DiscoveryCapabilities.BLE), "Invalid Capabilities");
            Assert.That(parser.Flow, Is.EqualTo(CommissioningPayload.FlowType.STANDARD), "Invalid Capabilities");
            Assert.That(parser.LongDiscriminator, Is.EqualTo(true), "Invalid Discriminator Length");
        }
    }
}