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

using MatterDotNet.Messages.Certificates;
using MatterDotNet.PKI;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;

namespace Test
{
    public class MatterCertTests
    {
        [Test]
        public void DeviceCert()
        {
            byte[] cert = Convert.FromBase64String("MIIB6TCCAY+gAwIBAgIIDgY7dCvPvl0wCgYIKoZIzj0EAwIwRjEYMBYGA1UEAwwP\r\nTWF0dGVyIFRlc3QgUEFJMRQwEgYKKwYBBAGConwCAQwERkZGMTEUMBIGCisGAQQB\r\ngqJ8AgIMBDgwMDAwIBcNMjEwNjI4MTQyMzQzWhgPOTk5OTEyMzEyMzU5NTlaMEsx\r\nHTAbBgNVBAMMFE1hdHRlciBUZXN0IERBQyAwMDAxMRQwEgYKKwYBBAGConwCAQwE\r\nRkZGMTEUMBIGCisGAQQBgqJ8AgIMBDgwMDAwWTATBgcqhkjOPQIBBggqhkjOPQMB\r\nBwNCAATCJYMix9xyc3wzvu1wczeqJIW8Rnk+TVrJp1rXQ1JmyQoCjuyvJlD+cAnv\r\n/K7L6tHyw9EkNd7C6tPZkpW/ztbDo2AwXjAMBgNVHRMBAf8EAjAAMA4GA1UdDwEB\r\n/wQEAwIHgDAdBgNVHQ4EFgQUlsLZJJTql4XA0WcI44jxwJHqD9UwHwYDVR0jBBgw\r\nFoAUr0K3CU3r1RXsbs8zuBEVIl8yUogwCgYIKoZIzj0EAwIDSAAwRQIgX8sppA08\r\nNabozmBlxtCdphc9xbJF7DIEkePTSTK3PhcCIQC0VpkPUgUQBFo4j3VOdxVAoESXkjGWRV5EDWgl2WEDZA==");

            OperationalCertificate matterCertificate = new OperationalCertificate(cert);
            Assert.That(matterCertificate.IssuerName, Is.EqualTo("Matter Test PAI"));
            Assert.That(matterCertificate.CommonName, Is.EqualTo("Matter Test DAC 0001"));
            Assert.That(matterCertificate.VendorID, Is.EqualTo(0xFFF1));
            Assert.That(matterCertificate.ProductID, Is.EqualTo(0x8000));
        }

        [Test]
        public void ICACCert()
        {
            byte[] icac = new byte[] { 0x15, 0x30, 0x01, 0x08, 0x2d, 0xb4, 0x44, 0x85, 0x56, 0x41, 0xae, 0xdf, 0x24, 0x02, 0x01, 0x37, 0x03, 0x27, 0x14, 0x01, 0x00, 0x00, 0x00, 0xca, 0xca, 0xca, 0xca, 0x18, 0x26, 0x04, 0xef, 0x17, 0x1b, 0x27, 0x26, 0x05, 0x6e, 0xb5, 0xb9, 0x4c, 0x37, 0x06, 0x27, 0x13, 0x03, 0x00, 0x00, 0x00, 0xca, 0xca, 0xca, 0xca, 0x18, 0x24, 0x07, 0x01, 0x24, 0x08, 0x01, 0x30, 0x09, 0x41, 0x04, 0xc5, 0xd0, 0x86, 0x1b, 0xb8, 0xf9, 0x0c, 0x40, 0x5c, 0x12, 0x31, 0x4e, 0x4c, 0x5e, 0xbe, 0xea, 0x93, 0x9f, 0x72, 0x77, 0x4b, 0xcc, 0x33, 0x23, 0x9e, 0x2f, 0x59, 0xf6, 0xf4, 0x6a, 0xf8, 0xdc, 0x7d, 0x46, 0x82, 0xa0, 0xe3, 0xcc, 0xc6, 0x46, 0xe6, 0xdf, 0x29, 0xea, 0x86, 0xbf, 0x56, 0x2a, 0xe7, 0x20, 0xa8, 0x98, 0x33, 0x7d, 0x38, 0x3f, 0x32, 0xc0, 0xa0, 0x9e, 0x41, 0x60, 0x19, 0xea, 0x37, 0x0a, 0x35, 0x01, 0x29, 0x01, 0x18, 0x24, 0x02, 0x60, 0x30, 0x04, 0x14, 0x53, 0x52, 0xd7, 0x05, 0x9e, 0x9c, 0x15, 0xa5, 0x08, 0x90, 0x68, 0x62, 0x86, 0x48, 0x01, 0xa2, 0x9f, 0x1f, 0x41, 0xd3, 0x30, 0x05, 0x14, 0x13, 0xaf, 0x81, 0xab, 0x37, 0x37, 0x4b, 0x2e, 0xd2, 0xa9, 0x64, 0x9b, 0x12, 0xb7, 0xa3, 0xa4, 0x28, 0x7e, 0x15, 0x1d, 0x18, 0x30, 0x0b, 0x40, 0x84, 0x1a, 0x06, 0xd4, 0x3b, 0x5e, 0x9f, 0xec, 0xd2, 0x4e, 0x87, 0xb1, 0x24, 0x4e, 0xb5, 0x1c, 0x6a, 0x2c, 0xf2, 0x0d, 0x9b, 0x5e, 0x6b, 0xa0, 0x7f, 0x11, 0xe6, 0x00, 0x2f, 0x7e, 0x0c, 0xa3, 0x4e, 0x32, 0xa6, 0x02, 0xc3, 0x60, 0x9d, 0x00, 0x92, 0xd3, 0x48, 0xbd, 0xbd, 0x19, 0x8a, 0x11, 0x46, 0x46, 0xbd, 0x41, 0xcf, 0x10, 0x37, 0x83, 0x64, 0x1a, 0xe2, 0x5e, 0x3f, 0x23, 0xfd, 0x26, 0x18 };
            TLVReader reader = new TLVReader(icac);
            MatterCertificate cert = new MatterCertificate(reader);
            Assert.That(cert.Subject[0].MatterIcacId, Is.EqualTo(0xCACACACA00000003));
            Assert.That(cert.Extensions[3].AuthorityKeyId, Is.EqualTo(Convert.FromHexString("13AF81AB37374B2ED2A9649B12B7A3A4287E151D")));
        }

        [Test]
        public void NOCEncoding()
        {
            byte[] nocDER = Convert.FromBase64String("MIIB4DCCAYagAwIBAgIIPvz/FwK5oXowCgYIKoZIzj0EAwIwIjEgMB4GCisGAQQBgqJ8AQMMEENBQ0FDQUNBMDAwMDAwMDMwHhcNMjAxMDE1MTQyMzQzWhcNNDAxMDE1MTQyMzQyWjBEMSAwHgYKKwYBBAGConwBAQwQREVERURFREUwMDAxMDAwMTEgMB4GCisGAQQBgqJ8AQUMEEZBQjAwMDAwMDAwMDAwMUQwWTATBgcqhkjOPQIBBggqhkjOPQMBBwNCAASaKiFvs53WtvohG4NciePmr7ZsFPdYMZVPn/T3o/ARLIoNjq8pxlMpTUju4HCKAyzKOTk8OntG8YGuoHj+rYODo4GDMIGAMAwGA1UdEwEB/wQCMAAwDgYDVR0PAQH/BAQDAgeAMCAGA1UdJQEB/wQWMBQGCCsGAQUFBwMCBggrBgEFBQcDATAdBgNVHQ4EFgQUn1Wia35DA+YIg+kTv5T0+14qYWEwHwYDVR0jBBgwFoAUU1LXBZ6cFaUIkGhihkgBop8fQdMwCgYIKoZIzj0EAwIDSAAwRQIgeVXCAmMLS6TVkSUmMi/fKPie3+WvnA5XK9ihSqq7TRICIQC4PKF8ewX7Fkt315xSlhMxa8/ReJXksqTyQEuYFzJxWQ==");
            byte[] nocTLV = Convert.FromHexString("153001083efcff1702b9a17a2402013703271303000000cacacaca182604ef171b2726056eb5b94c3706271101000100dededede27151d0000000000b0fa18240701240801300941049a2a216fb39dd6b6fa211b835c89e3e6afb66c14f75831954f9ff4f7a3f0112c8a0d8eaf29c653294d48eee0708a032cca39393c3a7b46f181aea078fead8383370a3501280118240201360304020401183004149f55a26b7e4303e60883e913bf94f4fb5e2a61613005145352d7059e9c15a508906862864801a29f1f41d318300b407955c202630b4ba4d5912526322fdf28f89edfe5af9c0e572bd8a14aaabb4d12b83ca17c7b05fb164b77d79c529613316bcfd17895e4b2a4f2404b981732715918");

            OperationalCertificate cert = new OperationalCertificate(nocDER);
            MatterCertificate control = new MatterCertificate(nocTLV);

            MatterCertificate tlv = cert.ToMatterCertificate();
            PayloadWriter output = new PayloadWriter(512);
            tlv.Serialize(new TLVWriter(output));
            CollectionAssert.AreEqual(output.GetPayload().Span.ToArray(), nocTLV);
        }
    }
}
