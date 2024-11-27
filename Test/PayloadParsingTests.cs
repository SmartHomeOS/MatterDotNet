using MatterDotNet;

namespace Test
{
    public class PayloadParsingTests
    {

        [Test]
        public void PIN_AllOnes()
        {
            string PIN = "765535819165535655359";
            PayloadParser parser = PayloadParser.FromPIN(PIN);
            Assert.That(parser.Discriminator, Is.EqualTo(0xF));
            Assert.That(parser.VendorID, Is.EqualTo(65535), "Invalid Vendor ID");
            Assert.That(parser.ProductID, Is.EqualTo(65535), "Invalid Product ID");
            Assert.That(parser.Passcode, Is.EqualTo(0x7FFFFFF), "Invalid Passcode");
            Assert.That(parser.DiscriminatorLength, Is.EqualTo(4), "Invalid Discriminator Length");
        }

        [Test]
        public void PIN_TestValues()
        {
            string PIN = "641295075300001000018";
            PayloadParser parser = PayloadParser.FromPIN(PIN);
            Assert.That(parser.Discriminator, Is.EqualTo(0xA));
            Assert.That(parser.VendorID, Is.EqualTo(1), "Invalid Vendor ID");
            Assert.That(parser.ProductID, Is.EqualTo(1), "Invalid Product ID");
            Assert.That(parser.Passcode, Is.EqualTo(12345679), "Invalid Passcode");
            Assert.That(parser.DiscriminatorLength, Is.EqualTo(4), "Invalid Discriminator Length");
        }

        [Test]
        public void QR_Test()
        {
            string QR = "MT:Y.K9042C00KA0648G00";
            PayloadParser parser = PayloadParser.FromQR(QR);
            Assert.That(parser.Discriminator, Is.EqualTo(3840));
            Assert.That(parser.VendorID, Is.EqualTo(0xfff1), "Invalid Vendor ID");
            Assert.That(parser.ProductID, Is.EqualTo(0x8000), "Invalid Product ID");
            Assert.That(parser.Passcode, Is.EqualTo(20202021), "Invalid Passcode");
            Assert.That(parser.Capabiilities, Is.EqualTo(PayloadParser.DiscoveryCapabilities.BLE), "Invalid Capabilities");
            Assert.That(parser.Flow, Is.EqualTo(PayloadParser.FlowType.STANDARD), "Invalid Capabilities");
            Assert.That(parser.DiscriminatorLength, Is.EqualTo(12), "Invalid Discriminator Length");
        }
    }
}