
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
            Assert.That(matterCertificate.ValidateChain(), Is.True);
            Assert.That(matterCertificate.IssuerName, Is.EqualTo("Matter Test PAI"));
            Assert.That(matterCertificate.CommonName, Is.EqualTo("Matter Test DAC 0001"));
            Assert.That(matterCertificate.VendorID, Is.EqualTo(0xFFF1));
            Assert.That(matterCertificate.ProductID, Is.EqualTo(0x8000));
        }
    }
}
