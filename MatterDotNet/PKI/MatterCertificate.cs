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

using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace MatterDotNet.PKI
{
    public class MatterCertificate
    {
        X509Certificate2 cert;

        public MatterCertificate(byte[] cert)
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

        public bool ValidateChain() {
            bool valid;
            X509ChainStatus[] status;
            using (X509Chain chain = new X509Chain())
            {
                chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority; //TODO - Remove when valid root
                valid = chain.Build(cert);
                status = chain.ChainStatus;
            }
            return valid;
        }
    }
}
