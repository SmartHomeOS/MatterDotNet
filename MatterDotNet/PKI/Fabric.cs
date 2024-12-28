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
using System.Text;

namespace MatterDotNet.PKI
{
    public class Fabric : OperationalCertificate
    {
        private static readonly byte[] COMPRESSED_FABRIC_INFO = new byte[] {0x43, 0x6f, 0x6d, 0x70, 0x72, 0x65, 0x73, 0x73, 0x65, 0x64, 0x46, 0x61, 0x62, 0x72, 0x69, 0x63};
        
        private Dictionary<ulong, OperationalCertificate> nodes = new Dictionary<ulong, OperationalCertificate>();
        private byte[] epochIPK;

        public Fabric(ulong rcac, ulong fabricId, byte[] ipk) : base()
        {
            if (fabricId == 0)
                throw new ArgumentException("Invalid Fabric ID");
            this.RCAC = rcac;
            this.FabricID = fabricId;
            epochIPK = ipk;
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_RCAC, $"{RCAC:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID, $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            PrivateKey = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            CertificateRequest req = new CertificateRequest(builder.Build(), PrivateKey, HashAlgorithmName.SHA256);
            req.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 0, true));
            req.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, true));
            X509SubjectKeyIdentifierExtension subjectKeyIdentifier = new X509SubjectKeyIdentifierExtension(SHA1.HashData(new BigIntegerPoint(PrivateKey.ExportParameters(false).Q).ToBytes(false)), false);
            req.CertificateExtensions.Add(subjectKeyIdentifier);
            req.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromSubjectKeyIdentifier(subjectKeyIdentifier));
            this.cert = req.CreateSelfSigned(DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(10));
            byte[] fabricIDBytes = new byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(fabricIDBytes, FabricID);
            CompressedFabricID = Crypto.KDF(PublicKey.AsSpan(1), fabricIDBytes, COMPRESSED_FABRIC_INFO, 64);
            OperationalIdentityProtectionKey = Crypto.KDF(ipk, CompressedFabricID, Encoding.ASCII.GetBytes("GroupKey v1.0"), Crypto.SYMMETRIC_KEY_LENGTH_BITS);
        }

        protected Fabric(X509Certificate2 cert, ECDsa key)
        {
            this.cert = cert;
            epochIPK = []; //TODO
            foreach (X500RelativeDistinguishedName dn in cert.SubjectName.EnumerateRelativeDistinguishedNames(false))
            {
                switch (dn.GetSingleElementType().Value)
                {
                    case OID_RCAC:
                            if (ulong.TryParse(dn.GetSingleElementValue()!, NumberStyles.HexNumber, null, out ulong rcac))
                                RCAC = rcac;
                            break;
                        case OID_FabricID:
                            if (ulong.TryParse(dn.GetSingleElementValue()!, NumberStyles.HexNumber, null, out ulong fabric))
                                FabricID = fabric;
                            break;
                }
            }
            PrivateKey = key;
            byte[] fabricIDBytes = new byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(fabricIDBytes, FabricID);
            CompressedFabricID = Crypto.KDF(PublicKey.AsSpan(1), fabricIDBytes, COMPRESSED_FABRIC_INFO, 64);
        }

        public OperationalCertificate Sign(CertificateRequest nocsr)
        {
            ulong nodeId = (ulong)(0xbaddeed2 + nodes.Count);
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_NodeId, $"{nodeId:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID, $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            CertificateRequest signingCSR = new CertificateRequest(builder.Build(), nocsr.PublicKey, HashAlgorithmName.SHA256);
            signingCSR.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
            signingCSR.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));
            OidCollection collection = new OidCollection();
            collection.Add(new Oid(OID_ServerAuth));
            collection.Add(new Oid(OID_ClientAuth));
            signingCSR.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(collection, true));
            signingCSR.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(nocsr.PublicKey, false));
            signingCSR.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromCertificate(cert, true, false));
            byte[] serial = new byte[19];
            Random.Shared.NextBytes(serial);
            OperationalCertificate ret = new OperationalCertificate(signingCSR.Create(cert, DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(1), serial));
            nodes.Add(ret.NodeID, ret);
            return ret;
        }

        public OperationalCertificate CreateCommissioner(byte[] publicKey, byte[] privateKey)
        {
            ulong nodeId = (ulong)(0xbaddeed2 + nodes.Count);
            ECDsa key = ECDsa.Create(new ECParameters() { Curve = ECCurve.NamedCurves.nistP256, D = privateKey, Q = new BigIntegerPoint(publicKey).ToECPoint()});
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_NodeId, $"{nodeId:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID, $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            CertificateRequest signingCSR = new CertificateRequest(builder.Build(), key, HashAlgorithmName.SHA256);
            signingCSR.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
            signingCSR.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));
            OidCollection collection = new OidCollection();
            collection.Add(new Oid(OID_ServerAuth));
            collection.Add(new Oid(OID_ClientAuth));
            signingCSR.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(collection, true));
            signingCSR.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(SHA1.HashData(publicKey), false));
            signingCSR.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromCertificate(cert, true, false));
            byte[] serial = new byte[19];
            Random.Shared.NextBytes(serial);
            OperationalCertificate ret = new OperationalCertificate(signingCSR.Create(cert, DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(1), serial).CopyWithPrivateKey(key));
            nodes.Add(ret.NodeID, ret);
            Commissioner = ret;
            return ret;
        }

        public X509Certificate2Collection Export()
        {
            X509Certificate2Collection collection = new X509Certificate2Collection();
            collection.Add(cert);
            if (Commissioner != null)
                collection.Add(Commissioner.GetRaw());
            foreach (var node in nodes)
            {
                if (node.Value != Commissioner)
                    collection.Add(node.Value.GetRaw());
            }
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
            for (int i = 1; i < collection.Count; i++)
            {
                OperationalCertificate noc = new OperationalCertificate(collection[i]);
                if (i == 1)
                    fabric.Commissioner = noc;
                fabric.nodes.TryAdd(noc.NodeID, noc);
            }
            return fabric;
        }

        public bool ContainsNOC(ulong nodeId)
        {
            return nodes.ContainsKey(nodeId);
        }

        public OperationalCertificate? GetNOC(ulong nodeId)
        {
            if (nodes.ContainsKey(nodeId))
                return nodes[nodeId];
            return null;
        }

        public byte[] ComputeDestinationID(byte[] random, ulong nodeId)
        {
            byte[] message = new byte[113];
            Array.Copy(random, message, 32);
            Array.Copy(PublicKey, 0, message, 32, Crypto.PUBLIC_KEY_SIZE_BYTES);
            BinaryPrimitives.WriteUInt64LittleEndian(message.AsSpan(Crypto.PUBLIC_KEY_SIZE_BYTES + 32), FabricID);
            BinaryPrimitives.WriteUInt64LittleEndian(message.AsSpan(Crypto.PUBLIC_KEY_SIZE_BYTES + 32 + 8), nodeId);
            return Crypto.HMAC(OperationalIdentityProtectionKey, message);
        }

        public ECDsa PrivateKey { get; init; }
        public byte[] CompressedFabricID { get; init; }
        public OperationalCertificate? Commissioner { get; private set; }
        public byte[] OperationalIdentityProtectionKey { get; init; }
    }
}
