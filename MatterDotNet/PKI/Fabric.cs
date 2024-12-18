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

using MatterDotNet.Protocol.Cryptography;
using System.Buffers.Binary;
using System.Formats.Asn1;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace MatterDotNet.PKI
{
    public class Fabric : OperationalCertificate
    {
        private static readonly byte[] COMPRESSED_FABRIC_INFO = new byte[] {0x43, 0x6f, 0x6d, 0x70, 0x72, 0x65, 0x73, 0x73, 0x65, 0x64, 0x46, 0x61, 0x62, 0x72, 0x69, 0x63};
        Dictionary<ulong, OperationalCertificate> nodes = new Dictionary<ulong, OperationalCertificate>();
        
        public Fabric(ulong rcac, ulong fabricId) : base()
        {
            if (fabricId == 0)
                throw new ArgumentException("Invalid Fabric ID");
            this.RCAC = rcac;
            this.FabricID = fabricId;
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_RCAC.Substring(4), $"{RCAC:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID.Substring(4), $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            PrivateKey = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            CertificateRequest req = new CertificateRequest(builder.Build(), PrivateKey, HashAlgorithmName.SHA256);
            req.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 0, true));
            req.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, true));
            X509SubjectKeyIdentifierExtension subjectKeyIdentifier = new X509SubjectKeyIdentifierExtension(SHA1.HashData(new BigIntegerPoint(PrivateKey.ExportParameters(false).Q).ToBytes(false)), false);
            req.CertificateExtensions.Add(subjectKeyIdentifier);
            req.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromSubjectKeyIdentifier(subjectKeyIdentifier));
            this.cert = req.CreateSelfSigned(DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(10));
            byte[] fabricBytes = new byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(fabricBytes, FabricID);
            CompressedFabricID = Crypto.KDF(PublicKey.AsSpan(1), fabricBytes, COMPRESSED_FABRIC_INFO, 64);
        }

        protected Fabric(X509Certificate2 cert, ECDsa key)
        {
            this.cert = cert;
            string[] oids = this.cert.Subject.Split(',', StringSplitOptions.TrimEntries);
            foreach (string kvp in oids)
            {
                string[] parts = kvp.Split('=', 2);
                if (parts.Length == 2)
                {
                    switch (parts[0].ToUpper())
                    {
                        case OID_RCAC:
                            if (ulong.TryParse(parts[1], NumberStyles.HexNumber, null, out ulong rcac))
                                RCAC = rcac;
                            break;
                        case OID_FabricID:
                            if (ulong.TryParse(parts[1], NumberStyles.HexNumber, null, out ulong fabric))
                                FabricID = fabric;
                            break;
                    }
                }
            }
            PrivateKey = key;
            byte[] fabricBytes = new byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(fabricBytes, FabricID);
            CompressedFabricID = Crypto.KDF(PublicKey.AsSpan(1), fabricBytes, COMPRESSED_FABRIC_INFO, 64);
        }

        public OperationalCertificate Sign(CertificateRequest nocsr)
        {
            ulong nodeId = (ulong)(0xbaddeed2 + nodes.Count);
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_NodeId.Substring(4), $"{nodeId:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID.Substring(4), $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            CertificateRequest signingCSR = new CertificateRequest(builder.Build(), nocsr.PublicKey, HashAlgorithmName.SHA256);
            signingCSR.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
            signingCSR.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));
            OidCollection collection = new OidCollection();
            collection.Add(new Oid("1.3.6.1.5.5.7.3.1"));
            collection.Add(new Oid("1.3.6.1.5.5.7.3.2"));
            signingCSR.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(collection, true));
            signingCSR.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(nocsr.PublicKey, false));
            signingCSR.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromCertificate(cert, true, false));
            byte[] serial = new byte[20];
            Random.Shared.NextBytes(serial);
            OperationalCertificate ret = new OperationalCertificate(signingCSR.Create(cert, DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(1), serial));
            nodes.Add(ret.NodeID, ret);
            return ret;
        }

        public OperationalCertificate Sign(byte[] publicKey, byte[] privateKey)
        {
            ulong nodeId = (ulong)(0xbaddeed2 + nodes.Count);
            ECDsa key = ECDsa.Create(new ECParameters() { Curve = ECCurve.NamedCurves.nistP256, D = privateKey, Q = new BigIntegerPoint(publicKey).ToECPoint()});
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_NodeId.Substring(4), $"{nodeId:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID.Substring(4), $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            CertificateRequest signingCSR = new CertificateRequest(builder.Build(), key, HashAlgorithmName.SHA256);
            signingCSR.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
            signingCSR.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));
            OidCollection collection = new OidCollection();
            collection.Add(new Oid("1.3.6.1.5.5.7.3.1"));
            collection.Add(new Oid("1.3.6.1.5.5.7.3.2"));
            signingCSR.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(collection, true));
            signingCSR.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(key.ExportSubjectPublicKeyInfo(), false));
            signingCSR.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromCertificate(cert, true, false));
            byte[] serial = new byte[20];
            Random.Shared.NextBytes(serial);
            OperationalCertificate ret = new OperationalCertificate(signingCSR.Create(cert, DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(1), serial));
            nodes.Add(ret.NodeID, ret);
            return ret;
        }

        public X509Certificate2Collection Export()
        {
            X509Certificate2Collection collection = new X509Certificate2Collection();
            collection.Add(cert);
            foreach (var node in nodes)
                collection.Add(node.Value.GetRaw());
            return collection;
        }

        public void Export(string certPath, string keyPath)
        {
            X509Certificate2Collection collection = Export();
            string export = collection.ExportCertificatePems();
            File.WriteAllText(certPath, export);
            File.WriteAllBytes(keyPath, PrivateKey.ExportPkcs8PrivateKey());
        }

        public static Fabric Import(string certPath, string keyPath)
        {
            X509Certificate2Collection collection = new X509Certificate2Collection();
            collection.ImportFromPemFile(certPath);
            ECDsa key = ECDsa.Create();
            key.ImportPkcs8PrivateKey(File.ReadAllBytes(keyPath), out _);
            Fabric fabric = new Fabric(collection[0], key);
            for (int i = 1; i <  collection.Count; i++)
            {
                OperationalCertificate noc = new OperationalCertificate(collection[i]);
                fabric.nodes.TryAdd(noc.NodeID, noc);
            }
            return fabric;
        }

        public ECDsa PrivateKey { get; init; }
        public byte[] CompressedFabricID { get; init; }
    }
}
