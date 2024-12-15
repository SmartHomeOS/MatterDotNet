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

using MatterDotNet.PKI;

namespace Test
{
    public class MatterCertTests
    {
        [Test]
        public void DeviceCert()
        {
            byte[] cert = Convert.FromBase64String("MIIB6TCCAY+gAwIBAgIIDgY7dCvPvl0wCgYIKoZIzj0EAwIwRjEYMBYGA1UEAwwP\r\nTWF0dGVyIFRlc3QgUEFJMRQwEgYKKwYBBAGConwCAQwERkZGMTEUMBIGCisGAQQB\r\ngqJ8AgIMBDgwMDAwIBcNMjEwNjI4MTQyMzQzWhgPOTk5OTEyMzEyMzU5NTlaMEsx\r\nHTAbBgNVBAMMFE1hdHRlciBUZXN0IERBQyAwMDAxMRQwEgYKKwYBBAGConwCAQwE\r\nRkZGMTEUMBIGCisGAQQBgqJ8AgIMBDgwMDAwWTATBgcqhkjOPQIBBggqhkjOPQMB\r\nBwNCAATCJYMix9xyc3wzvu1wczeqJIW8Rnk+TVrJp1rXQ1JmyQoCjuyvJlD+cAnv\r\n/K7L6tHyw9EkNd7C6tPZkpW/ztbDo2AwXjAMBgNVHRMBAf8EAjAAMA4GA1UdDwEB\r\n/wQEAwIHgDAdBgNVHQ4EFgQUlsLZJJTql4XA0WcI44jxwJHqD9UwHwYDVR0jBBgw\r\nFoAUr0K3CU3r1RXsbs8zuBEVIl8yUogwCgYIKoZIzj0EAwIDSAAwRQIgX8sppA08\r\nNabozmBlxtCdphc9xbJF7DIEkePTSTK3PhcCIQC0VpkPUgUQBFo4j3VOdxVAoESXkjGWRV5EDWgl2WEDZA==");

            MatterCertificate matterCertificate = new MatterCertificate(cert);
            Assert.That(matterCertificate.IssuerName, Is.EqualTo("Matter Test PAI"));
            Assert.That(matterCertificate.CommonName, Is.EqualTo("Matter Test DAC 0001"));
            Assert.That(matterCertificate.VendorID, Is.EqualTo(0xFFF1));
            Assert.That(matterCertificate.ProductID, Is.EqualTo(0x8000));
        }
    }
}
