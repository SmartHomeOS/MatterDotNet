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

using MatterDotNet.Protocol.Cryptography;
using MatterDotNet.Util;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MatterDotNet.PKI
{
    /// <summary>
    /// A Matter Fabric (PKI Store)
    /// </summary>
    public class Fabric : OperationalCertificate
    {
        private static readonly byte[] COMPRESSED_FABRIC_INFO = new byte[] {0x43, 0x6f, 0x6d, 0x70, 0x72, 0x65, 0x73, 0x73, 0x65, 0x64, 0x46, 0x61, 0x62, 0x72, 0x69, 0x63};
        
        private Dictionary<ulong, OperationalCertificate> nodes = new Dictionary<ulong, OperationalCertificate>();

        /// <summary>
        /// Create a new Fabric
        /// </summary>
        /// <param name="fabricName"></param>
        /// <param name="fabricId"></param>
        /// <param name="ipk"></param>
        /// <exception cref="ArgumentException"></exception>
        [SetsRequiredMembers]
        public Fabric(string fabricName, ulong fabricId, byte[] ipk) : base()
        {
            if (fabricId == 0)
                throw new ArgumentException("Invalid Fabric ID");
            if (ipk.Length != 16)
                throw new ArgumentException("Epoch Key must be 16 bytes");
            ArgumentNullException.ThrowIfNullOrEmpty(fabricName, nameof(fabricName));
            this.RCAC = Math.Max(1, (ulong)Random.Shared.NextInt64());
            this.FabricID = fabricId;
            this.CommonName = fabricName.Truncate(64);
            this.IssuerCommonName = CommonName;
            EpochKey = ipk;
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_CommonName, CommonName, UniversalTagNumber.UTF8String);
            builder.Add(OID_RCAC, $"{RCAC:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID, $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            ECDsa privateKey = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            CertificateRequest req = new CertificateRequest(builder.Build(), privateKey, HashAlgorithmName.SHA256);
            req.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 0, true));
            req.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, true));
            X509SubjectKeyIdentifierExtension subjectKeyIdentifier = new X509SubjectKeyIdentifierExtension(SHA1.HashData(new BigIntegerPoint(privateKey.ExportParameters(false).Q).ToBytes(false)), false);
            req.CertificateExtensions.Add(subjectKeyIdentifier);
            req.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromSubjectKeyIdentifier(subjectKeyIdentifier));
            this.cert = req.CreateSelfSigned(DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(10));
            GenerateIDs();
        }

        [SetsRequiredMembers]
        internal Fabric(X509Certificate2 cert, byte[] ipk) : base(cert)
        {
            EpochKey = ipk;
            GenerateIDs();
        }

        private void GenerateIDs()
        {
            byte[] fabricIDBytes = new byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(fabricIDBytes, FabricID!.Value);
            CompressedFabricID = Crypto.KDF(PublicKey.AsSpan(1), fabricIDBytes, COMPRESSED_FABRIC_INFO, 64);
            OperationalIdentityProtectionKey = Crypto.KDF(EpochKey, CompressedFabricID, Encoding.ASCII.GetBytes("GroupKey v1.0"), Crypto.SYMMETRIC_KEY_LENGTH_BITS);
        }

        /// <summary>
        /// Sign a Certificate Signing Request
        /// </summary>
        /// <param name="nocsr"></param>
        /// <returns></returns>
        public OperationalCertificate Sign(CertificateRequest nocsr)
        {
            ulong nodeId = (ulong)(0xbaddeed2 + nodes.Count);
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_NodeId, $"{nodeId:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID, $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            CertificateRequest signingCSR = new CertificateRequest(builder.Build(), nocsr.PublicKey, HashAlgorithmName.SHA256);
            signingCSR.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
            signingCSR.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));
            signingCSR.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension([new Oid(OID_ServerAuth), new Oid(OID_ClientAuth)], true));
            signingCSR.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(nocsr.PublicKey, false));
            signingCSR.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromCertificate(cert, true, false));
            byte[] serial = new byte[19];
            Random.Shared.NextBytes(serial);
            OperationalCertificate ret = new OperationalCertificate(signingCSR.Create(cert, DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(1), serial));
            nodes.Add(nodeId, ret);
            return ret;
        }

        /// <summary>
        /// Add a commissioner to the fabric
        /// </summary>
        /// <returns></returns>
        public OperationalCertificate CreateCommissioner()
        {
            var keyPair = Crypto.GenerateKeypair();
            return CreateCommissioner(keyPair.Public, keyPair.Private);
        }

        /// <summary>
        /// Add a commissioner to the fabric
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public OperationalCertificate CreateCommissioner(byte[] publicKey, byte[] privateKey)
        {
            if (Commissioner != null)
                throw new InvalidOperationException("Commissioner already exists");
            ulong nodeId = (ulong)(0xbaddeed2 + nodes.Count);
            ECDsa key = ECDsa.Create(new ECParameters() { Curve = ECCurve.NamedCurves.nistP256, D = privateKey, Q = new BigIntegerPoint(publicKey).ToECPoint()});
            X500DistinguishedNameBuilder builder = new X500DistinguishedNameBuilder();
            builder.Add(OID_NodeId, $"{nodeId:X16}", UniversalTagNumber.UTF8String);
            builder.Add(OID_FabricID, $"{FabricID:X16}", UniversalTagNumber.UTF8String);
            CertificateRequest signingCSR = new CertificateRequest(builder.Build(), key, HashAlgorithmName.SHA256);
            signingCSR.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
            signingCSR.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));
            signingCSR.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension([new Oid(OID_ServerAuth), new Oid(OID_ClientAuth)], true));
            signingCSR.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(SHA1.HashData(publicKey), false));
            signingCSR.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromCertificate(cert, true, false));
            byte[] serial = new byte[19];
            Random.Shared.NextBytes(serial);
            Commissioner = new OperationalCertificate(signingCSR.Create(cert, DateTime.Now.Subtract(TimeSpan.FromSeconds(30)), DateTime.Now.AddYears(1), serial).CopyWithPrivateKey(key));
            nodes.Add(nodeId, Commissioner);
            return Commissioner;
        }

        /// <summary>
        /// Export the Fabric to a certificate collection
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Export the fabric to a fabric and key file
        /// </summary>
        /// <param name="certPath"></param>
        /// <param name="keyPath"></param>
        public void Export(string certPath, string keyPath)
        {
            X509Certificate2Collection collection = Export();
            byte[] export = collection.Export(X509ContentType.Pkcs12)!;
            File.WriteAllBytes(certPath, export);
            File.WriteAllBytes(keyPath, EpochKey);
        }

        /// <summary>
        /// Import a Fabric from a fabric and key file
        /// </summary>
        /// <param name="certPath"></param>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        public static Fabric Import(string certPath, string keyPath)
        {
            #if NET9_0_OR_GREATER
                X509Certificate2Collection collection = X509CertificateLoader.LoadPkcs12CollectionFromFile(certPath, null, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.EphemeralKeySet);
            #else
                X509Certificate2Collection collection = new X509Certificate2Collection();
                collection.Import(certPath, null, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.EphemeralKeySet);
            #endif
            Fabric fabric = new Fabric(collection[0], File.ReadAllBytes(keyPath));
            for (int i = 1; i < collection.Count; i++)
            {
                OperationalCertificate noc = new OperationalCertificate(collection[i]);
                if (i == 1)
                    fabric.Commissioner = noc;
                if (noc.NodeID != null)
                    fabric.nodes.TryAdd(noc.NodeID.Value, noc);
            }
            return fabric;
        }

        /// <summary>
        /// Returns true if the fabric has a NOC for the provided node ID
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool HasNode(ulong nodeId)
        {
            return nodes.ContainsKey(nodeId);
        }

        /// <summary>
        /// Get the operational certificate for the provided node ID
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public OperationalCertificate? GetNOC(ulong nodeId)
        {
            if (nodes.ContainsKey(nodeId))
                return nodes[nodeId];
            return null;
        }

        /// <summary>
        /// Enumerate NOCs within the fabric
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OperationalCertificate> GetNodes()
        {
            foreach (OperationalCertificate node in nodes.Values)
            {
                if (node != Commissioner)
                    yield return node;
            }
        }

        internal byte[] ComputeDestinationID(byte[] random, ulong nodeId)
        {
            byte[] message = new byte[113];
            Array.Copy(random, message, 32);
            Array.Copy(PublicKey, 0, message, 32, Crypto.PUBLIC_KEY_SIZE_BYTES);
            BinaryPrimitives.WriteUInt64LittleEndian(message.AsSpan(Crypto.PUBLIC_KEY_SIZE_BYTES + 32), FabricID!.Value);
            BinaryPrimitives.WriteUInt64LittleEndian(message.AsSpan(Crypto.PUBLIC_KEY_SIZE_BYTES + 32 + 8), nodeId);
            return Crypto.HMAC(OperationalIdentityProtectionKey, message);
        }

        /// <summary>
        /// A compressed version of the fabric identifier
        /// </summary>
        public required byte[] CompressedFabricID { get; set; }
        /// <summary>
        /// This node (the fabric commissioning node)
        /// </summary>
        public OperationalCertificate? Commissioner { get; private set; }
        /// <summary>
        /// Derived IPK
        /// </summary>
        public required byte[] OperationalIdentityProtectionKey { get; set; }
        /// <summary>
        /// Epoch Key (Epoch 0)
        /// </summary>
        public required byte[] EpochKey { get; init; }
    }
}
