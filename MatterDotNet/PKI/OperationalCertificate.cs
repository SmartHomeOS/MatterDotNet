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
using MatterDotNet.Protocol.Cryptography;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace MatterDotNet.PKI
{
    public class OperationalCertificate
    {
        private static readonly TimeSpan EPOCH = TimeSpan.FromSeconds(946684800);
        X509Certificate2 cert;

        public OperationalCertificate(ulong rcac, string commonName)
        {
            this.RCAC = rcac;
            this.CommonName = commonName;
            string subject = $"CN={CommonName},OID.1.3.6.1.4.1.37244.1.4={RCAC}";
            PrivateKey = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            CertificateRequest req = new CertificateRequest(subject, PrivateKey, HashAlgorithmName.SHA256);
            req.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 0, true));
            req.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, true));
            byte[] subjectKeyIdentifier = SHA1.HashData(new BigIntegerPoint(PrivateKey.ExportParameters(false).Q).ToBytes(true));
            req.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(subjectKeyIdentifier, false));
            req.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromSubjectKeyIdentifier(subjectKeyIdentifier));
            this.cert = req.CreateSelfSigned(DateTime.Now, DateTime.Now.AddYears(10));
        }

        public OperationalCertificate(byte[] cert)
        {
            #if NET9_0_OR_GREATER
                this.cert = X509CertificateLoader.LoadCertificate(cert);
            #else
                this.cert = new X509Certificate2(cert);
            #endif
            string[] oids = this.cert.Subject.Split(',', StringSplitOptions.TrimEntries);
            foreach (string kvp in oids)
            {
                string[] parts = kvp.Split('=', 2);
                if (parts.Length == 2)
                {
                    switch (parts[0].ToUpper())
                    {
                        case "CN":
                            CommonName = parts[1];
                            break;
                        case "OID.1.3.6.1.4.1.37244.1.1":
                            if (ulong.TryParse(parts[1], NumberStyles.HexNumber, null, out ulong id))
                                NodeID = id;
                            break;
                        case "OID.1.3.6.1.4.1.37244.1.2":
                            if (ulong.TryParse(parts[1], NumberStyles.HexNumber, null, out ulong firmware))
                                FirmwareSigningID = firmware;
                            break;
                        case "OID.1.3.6.1.4.1.37244.1.3":
                            if (ulong.TryParse(parts[1], NumberStyles.HexNumber, null, out ulong icac))
                                ICAC = icac;
                            break;
                        case "OID.1.3.6.1.4.1.37244.1.4":
                            if (ulong.TryParse(parts[1], NumberStyles.HexNumber, null, out ulong rcac))
                                RCAC = rcac;
                            break;
                        case "OID.1.3.6.1.4.1.37244.1.5":
                            if (ulong.TryParse(parts[1], NumberStyles.HexNumber, null, out ulong fabric))
                                FabricID = fabric;
                            break;
                        case "OID.1.3.6.1.4.1.37244.1.6":
                            if (uint.TryParse(parts[1], NumberStyles.HexNumber, null, out uint noc))
                                NOCCat = noc;
                            break;
                        case "OID.1.3.6.1.4.1.37244.2.1":
                            if (uint.TryParse(parts[1], NumberStyles.HexNumber, null, out uint vid))
                                VendorID = vid;
                            break;
                        case "OID.1.3.6.1.4.1.37244.2.2":
                            if (uint.TryParse(parts[1], NumberStyles.HexNumber, null, out uint pid))
                                ProductID = pid;
                            break;
                    }
                }
            }
            string[] issuerOIDs = this.cert.Issuer.Split(',', StringSplitOptions.TrimEntries);
            foreach (string kvp in issuerOIDs)
            {
                string[] parts = kvp.Split('=', 2);
                if (parts.Length == 2)
                {
                    switch (parts[0].ToUpper())
                    {
                        case "CN":
                            IssuerName = parts[1];
                            break;
                    }
                }
            }
        }

        public bool VerifyChain(byte[] paiCert, X509Certificate2 paaCert)
        {
            X509Chain chain = new X509Chain();
            #if NET9_0_OR_GREATER
                chain.ChainPolicy.ExtraStore.Add(X509CertificateLoader.LoadCertificate(paiCert));
            #else
                chain.ChainPolicy.ExtraStore.Add(new X509Certificate2(paiCert));
            #endif
            chain.ChainPolicy.CustomTrustStore.Add(paaCert);
            chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
            chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreWrongUsage;
            return chain.Build(cert);
        }

        public void Export(string path)
        {
            File.WriteAllBytes(path, cert.Export(X509ContentType.Cert));
        }

        private byte[] GetSignature(AsnEncodingRules encodingRules = AsnEncodingRules.DER)
        {
            var signedData = cert.RawDataMemory;
            AsnDecoder.ReadSequence(signedData.Span, encodingRules, out var offset, out var length, out _);

            var certificateSpan = signedData.Span.Slice(offset, length);
            AsnDecoder.ReadSequence( certificateSpan, encodingRules, out var tbsOffset, out var tbsLength, out _);

            var algorithmSpan = certificateSpan.Slice(tbsOffset + tbsLength);
            AsnDecoder.ReadSequence(algorithmSpan, encodingRules, out var algOffset, out var algLength, out _);

            byte[] signatureBlock = AsnDecoder.ReadBitString(algorithmSpan.Slice(algOffset + algLength), encodingRules, out _, out _ );
            byte[] signature = new byte[64];
            Array.Copy(signatureBlock, 4, signature, 0, 32);
            Array.Copy(signatureBlock, 39, signature, 32, 32);
            return signature;
        }

        private ushort GetKeyUsage(X509KeyUsageFlags keyUsage)
        {
            ushort ret = 0x0;
            if ((keyUsage & X509KeyUsageFlags.EncipherOnly) != 0)
                ret |= 0x80;
            if ((keyUsage & X509KeyUsageFlags.CrlSign) != 0)
                ret |= 0x40;
            if ((keyUsage & X509KeyUsageFlags.KeyCertSign) != 0)
                ret |= 0x20;
            if ((keyUsage & X509KeyUsageFlags.KeyAgreement) != 0)
                ret |= 0x10;
            if ((keyUsage & X509KeyUsageFlags.DataEncipherment) != 0)
                ret |= 0x8;
            if ((keyUsage & X509KeyUsageFlags.KeyEncipherment) != 0)
                ret |= 0x4;
            if ((keyUsage & X509KeyUsageFlags.NonRepudiation) != 0)
                ret |= 0x2;
            if ((keyUsage & X509KeyUsageFlags.DigitalSignature) != 0)
                ret |= 0x1;
            if ((keyUsage & X509KeyUsageFlags.DecipherOnly) != 0)
                ret |= 0x100;
            return ret;
        }

        private uint[] GetExtendedKeyUsage(X509EnhancedKeyUsageExtension extended)
        {
            List<uint> extUsage = new List<uint>();
            foreach (Oid oid in extended.EnhancedKeyUsages)
            {
                if (uint.TryParse(oid.Value!.Split('.').Last(), out uint val))
                {
                    if (val < 5)
                        extUsage.Add(val);
                    else if (val == 8)
                        extUsage.Add(5);
                    else if (val == 9)
                        extUsage.Add(6);
                }
            }
            return extUsage.ToArray();
        }

        public MatterCertificate ToMatterCertificate()
        {
            List<Extension> extensions = new List<Extension>();
            foreach (X509Extension ext in cert.Extensions)
            {
                if (ext is X509BasicConstraintsExtension basic)
                {
                    BasicConstraints constraint = new BasicConstraints() { IsCa = basic.CertificateAuthority };
                    if (basic.HasPathLengthConstraint)
                        constraint.PathLenConstraint = (byte)basic.PathLengthConstraint;
                    extensions.Add(new Extension() { BasicCnstr = constraint });
                }
                else if (ext is X509KeyUsageExtension keyUsage)
                    extensions.Add(new Extension() { KeyUsage = GetKeyUsage(keyUsage.KeyUsages) });
                else if (ext is X509EnhancedKeyUsageExtension extended)
                    extensions.Add(new Extension() { ExtendedKeyUsage = GetExtendedKeyUsage(extended) });
                else if (ext is X509SubjectKeyIdentifierExtension subKey)
                    extensions.Add(new Extension() { SubjectKeyId = subKey.SubjectKeyIdentifierBytes.ToArray() });
                else if (ext is X509AuthorityKeyIdentifierExtension authKey)
                    extensions.Add(new Extension() { AuthorityKeyId = authKey.KeyIdentifier!.Value.ToArray() });
            }
            return new MatterCertificate()
            {
                EcCurveId = 0x1,
                PubKeyAlgo = 0x1,
                SigAlgo = 0x1,
                EcPubKey = cert.GetPublicKey(),
                SerialNum = cert.GetSerialNumber().Reverse().ToArray(),
                NotBefore = (uint)((DateTimeOffset)cert.NotBefore - EPOCH).ToUnixTimeSeconds(),
                NotAfter = (uint)((DateTimeOffset)cert.NotAfter - EPOCH).ToUnixTimeSeconds(),
                Signature = GetSignature(),
                Extensions = extensions,
                Issuer = GetDNs(cert.Issuer),
                Subject = GetDNs(cert.Subject)
            };
        }

        private static List<DnAttribute> GetDNs(string subject)
        {
            List<DnAttribute> attrs = new List<DnAttribute>();
            string[] oids = subject.Split(',', StringSplitOptions.TrimEntries);
            Dictionary<string, string> kvps = new Dictionary<string, string>();
            foreach (string oid in oids)
            {
                string[] parts = oid.Split('=', 2);
                if (parts.Length == 2)
                    kvps.Add(parts[0], parts[1]);
            }
            if (kvps.ContainsKey("CN"))
                attrs.Add(new DnAttribute() { CommonName = kvps["CN"] });
            if (kvps.ContainsKey("OID.1.3.6.1.4.1.37244.1.1"))
            {
                if (ulong.TryParse(kvps["OID.1.3.6.1.4.1.37244.1.1"], NumberStyles.HexNumber, null, out ulong id))
                    attrs.Add(new DnAttribute() { MatterNodeId = id });
            }
            if (kvps.ContainsKey("OID.1.3.6.1.4.1.37244.1.2"))
            {
                if (ulong.TryParse(kvps["OID.1.3.6.1.4.1.37244.1.2"], NumberStyles.HexNumber, null, out ulong firmware))
                    attrs.Add(new DnAttribute() { MatterFirmwareSigningId = firmware });
            }
            if (kvps.ContainsKey("OID.1.3.6.1.4.1.37244.1.3"))
            {
                if (ulong.TryParse(kvps["OID.1.3.6.1.4.1.37244.1.3"], NumberStyles.HexNumber, null, out ulong icac))
                    attrs.Add(new DnAttribute() { MatterIcacId = icac });
            }
            if (kvps.ContainsKey("OID.1.3.6.1.4.1.37244.1.4"))
            {
                if (ulong.TryParse(kvps["OID.1.3.6.1.4.1.37244.1.4"], NumberStyles.HexNumber, null, out ulong rcac))
                    attrs.Add(new DnAttribute() { MatterRcacId = rcac });
            }
            if (kvps.ContainsKey("OID.1.3.6.1.4.1.37244.1.5"))
            {
                if (ulong.TryParse(kvps["OID.1.3.6.1.4.1.37244.1.5"], NumberStyles.HexNumber, null, out ulong fabric))
                    attrs.Add(new DnAttribute() { MatterFabricId = fabric });
            }
            if (kvps.ContainsKey("OID.1.3.6.1.4.1.37244.1.6"))
            {
                if (uint.TryParse(kvps["OID.1.3.6.1.4.1.37244.1.6"], NumberStyles.HexNumber, null, out uint noc))
                    attrs.Add(new DnAttribute() { MatterNocCat = noc });
            }
            return attrs;
        }

        public string IssuerName { get; set; } = string.Empty;

        public string CommonName { get; set; } = string.Empty;

        public ulong NodeID { get; set; }

        public ulong FirmwareSigningID { get; set; }

        public ulong ICAC { get; set; }

        public ulong RCAC { get; set; }

        public ulong FabricID { get; set; }

        public uint NOCCat { get; set; }

        public uint VendorID { get; set; }

        public uint ProductID { get; set; }
        public byte[] PublicKey { get { return cert.GetPublicKey(); } }
        public ECDsa? PrivateKey { get; init; }
    }
}
