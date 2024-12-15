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
//
// WARNING: This file was auto-generated. Do not edit.

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters
{
    public class NodeOperationalCredentialsCluster : ClusterBase
    {
        private const uint CLUSTER_ID = 0x003E;

        public NodeOperationalCredentialsCluster(ushort endPoint) : base(endPoint) { }

        public enum CertificateChainTypeEnum {
            /// <summary>
            /// Request the DER-encoded DAC certificate
            /// </summary>
            DACCertificate = 1,
            /// <summary>
            /// Request the DER-encoded PAI certificate
            /// </summary>
            PAICertificate = 2,
        }

        public enum NodeOperationalCertStatusEnum {
            /// <summary>
            /// OK, no error
            /// </summary>
            OK = 0,
            /// <summary>
            /// <see cref="InvalidPublicKey"/> Public Key in the NOC does not match the public key in the NOCSR
            /// </summary>
            InvalidPublicKey = 1,
            /// <summary>
            /// <see cref="InvalidOperationalId"/> The Node Operational ID in the NOC is not formatted correctly.
            /// </summary>
            InvalidNodeOpId = 2,
            /// <summary>
            /// <see cref="InvalidNoc"/> Any other validation error in NOC chain
            /// </summary>
            InvalidNOC = 3,
            /// <summary>
            /// <see cref="MissingCsr"/> No record of prior CSR for which this NOC could match
            /// </summary>
            MissingCsr = 4,
            /// <summary>
            /// <see cref="TableFull"/> NOCs table full, cannot add another one
            /// </summary>
            TableFull = 5,
            /// <summary>
            /// <see cref="InvalidAdminSubject"/> Invalid CaseAdminSubject field for an AddNOC command.
            /// </summary>
            InvalidAdminSubject = 6,
            /// <summary>
            /// Reserved for future use
            /// </summary>
            Item7 = 7,
            /// <summary>
            /// Reserved for future use
            /// </summary>
            Item8 = 8,
            /// <summary>
            /// <see cref="FabricConflict"/> Trying to AddNOC instead of UpdateNOC against an existing Fabric.
            /// </summary>
            FabricConflict = 9,
            /// <summary>
            /// <see cref="LabelConflict"/> Label already exists on another Fabric.
            /// </summary>
            LabelConflict = 10,
            /// <summary>
            /// <see cref="InvalidFabricIndex"/> FabricIndex argument is invalid.
            /// </summary>
            InvalidFabricIndex = 11,
        }

        public record FabricDescriptor : TLVPayload {
            public required byte[] RootPublicKey { get; set; }
            public required ushort VendorID { get; set; }
            public required ulong FabricID { get; set; }
            public required ulong NodeID { get; set; }
            public string? Label { get; set; } = "";
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(1, RootPublicKey);
                writer.WriteUShort(2, VendorID);
                writer.WriteULong(3, FabricID);
                writer.WriteULong(4, NodeID);
                if (Label != null)
                    writer.WriteString(5, Label);
                writer.EndContainer();
            }
        }

        public record NOC : TLVPayload {
            public required byte[] NOCField { get; set; }
            public required byte[] ICAC { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(1, NOCField);
                writer.WriteBytes(2, ICAC);
                writer.EndContainer();
            }
        }

        private record AttestationRequestPayload : TLVPayload {
            public required byte[] AttestationNonce { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, AttestationNonce);
                writer.EndContainer();
            }
        }

        public struct AttestationResponse {
            public required byte[] AttestationElements { get; set; }
            public required byte[] AttestationSignature { get; set; }
        }

        private record CertificateChainRequestPayload : TLVPayload {
            public required CertificateChainTypeEnum CertificateType { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)CertificateType);
                writer.EndContainer();
            }
        }

        public struct CertificateChainResponse {
            public required byte[] Certificate { get; set; }
        }

        private record CSRRequestPayload : TLVPayload {
            public required byte[] CSRNonce { get; set; }
            public bool? IsForUpdateNOC { get; set; } = false;
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, CSRNonce);
                if (IsForUpdateNOC != null)
                    writer.WriteBool(1, IsForUpdateNOC);
                writer.EndContainer();
            }
        }

        public struct CSRResponse {
            public required byte[] NOCSRElements { get; set; }
            public required byte[] AttestationSignature { get; set; }
        }

        private record AddNOCPayload : TLVPayload {
            public required byte[] NOCValue { get; set; }
            public byte[]? ICACValue { get; set; }
            public required byte[] IPKValue { get; set; }
            public required ulong CaseAdminSubject { get; set; }
            public required ushort AdminVendorId { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NOCValue);
                if (ICACValue != null)
                    writer.WriteBytes(1, ICACValue);
                writer.WriteBytes(2, IPKValue);
                writer.WriteULong(3, CaseAdminSubject);
                writer.WriteUShort(4, AdminVendorId);
                writer.EndContainer();
            }
        }

        private record UpdateNOCPayload : TLVPayload {
            public required byte[] NOCValue { get; set; }
            public byte[]? ICACValue { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NOCValue);
                if (ICACValue != null)
                    writer.WriteBytes(1, ICACValue);
                writer.EndContainer();
            }
        }

        public struct NOCResponse {
            public required NodeOperationalCertStatusEnum StatusCode { get; set; }
            public byte? FabricIndex { get; set; }
            public string? DebugText { get; set; }
        }

        private record UpdateFabricLabelPayload : TLVPayload {
            public required string Label { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Label);
                writer.EndContainer();
            }
        }

        private record RemoveFabricPayload : TLVPayload {
            public required byte FabricIndex { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, FabricIndex);
                writer.EndContainer();
            }
        }

        private record AddTrustedRootCertificatePayload : TLVPayload {
            public required byte[] RootCACertificate { get; set; }
            public override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, RootCACertificate);
                writer.EndContainer();
            }
        }

        // Commands
        public async Task<AttestationResponse?> AttestationRequest (Exchange exchange, byte[] AttestationNonce) {
            AttestationRequestPayload requestFields = new AttestationRequestPayload() {
                AttestationNonce = AttestationNonce,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, 0x00, requestFields);
            if (!validateResponse(resp))
                return null;
            return new AttestationResponse() {
                AttestationElements = (byte[])GetField(resp, 0),
                AttestationSignature = (byte[])GetField(resp, 1),
            };
        }

        public async Task<CertificateChainResponse?> CertificateChainRequest (Exchange exchange, CertificateChainTypeEnum CertificateType) {
            CertificateChainRequestPayload requestFields = new CertificateChainRequestPayload() {
                CertificateType = CertificateType,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, 0x02, requestFields);
            if (!validateResponse(resp))
                return null;
            return new CertificateChainResponse() {
                Certificate = (byte[])GetField(resp, 0),
            };
        }

        public async Task<CSRResponse?> CSRRequest (Exchange exchange, byte[] CSRNonce, bool IsForUpdateNOC) {
            CSRRequestPayload requestFields = new CSRRequestPayload() {
                CSRNonce = CSRNonce,
                IsForUpdateNOC = IsForUpdateNOC,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, 0x04, requestFields);
            if (!validateResponse(resp))
                return null;
            return new CSRResponse() {
                NOCSRElements = (byte[])GetField(resp, 0),
                AttestationSignature = (byte[])GetField(resp, 1),
            };
        }

        public async Task<NOCResponse?> AddNOC (Exchange exchange, byte[] NOCValue, byte[] ICACValue, byte[] IPKValue, ulong CaseAdminSubject, ushort AdminVendorId) {
            AddNOCPayload requestFields = new AddNOCPayload() {
                NOCValue = NOCValue,
                ICACValue = ICACValue,
                IPKValue = IPKValue,
                CaseAdminSubject = CaseAdminSubject,
                AdminVendorId = AdminVendorId,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, 0x06, requestFields);
            if (!validateResponse(resp))
                return null;
            return new NOCResponse() {
                StatusCode = (NodeOperationalCertStatusEnum)GetField(resp, 0),
                FabricIndex = (byte?)GetOptionalField(resp, 1),
                DebugText = (string?)GetOptionalField(resp, 2),
            };
        }

        public async Task<NOCResponse?> UpdateNOC (Exchange exchange, byte[] NOCValue, byte[] ICACValue) {
            UpdateNOCPayload requestFields = new UpdateNOCPayload() {
                NOCValue = NOCValue,
                ICACValue = ICACValue,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, 0x07, requestFields);
            if (!validateResponse(resp))
                return null;
            return new NOCResponse() {
                StatusCode = (NodeOperationalCertStatusEnum)GetField(resp, 0),
                FabricIndex = (byte?)GetOptionalField(resp, 1),
                DebugText = (string?)GetOptionalField(resp, 2),
            };
        }

        public async Task<NOCResponse?> UpdateFabricLabel (Exchange exchange, string Label) {
            UpdateFabricLabelPayload requestFields = new UpdateFabricLabelPayload() {
                Label = Label,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, 0x09, requestFields);
            if (!validateResponse(resp))
                return null;
            return new NOCResponse() {
                StatusCode = (NodeOperationalCertStatusEnum)GetField(resp, 0),
                FabricIndex = (byte?)GetOptionalField(resp, 1),
                DebugText = (string?)GetOptionalField(resp, 2),
            };
        }

        public async Task<NOCResponse?> RemoveFabric (Exchange exchange, byte FabricIndex) {
            RemoveFabricPayload requestFields = new RemoveFabricPayload() {
                FabricIndex = FabricIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, 0x0A, requestFields);
            if (!validateResponse(resp))
                return null;
            return new NOCResponse() {
                StatusCode = (NodeOperationalCertStatusEnum)GetField(resp, 0),
                FabricIndex = (byte?)GetOptionalField(resp, 1),
                DebugText = (string?)GetOptionalField(resp, 2),
            };
        }

        public async Task<bool> AddTrustedRootCertificate (Exchange exchange, byte[] RootCACertificate) {
            AddTrustedRootCertificatePayload requestFields = new AddTrustedRootCertificatePayload() {
                RootCACertificate = RootCACertificate,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(exchange, endPoint, CLUSTER_ID, 0x0B, requestFields);
            return validateResponse(resp);
        }

        // Attributes
        public List<NOC> NOCs { get; }
        public List<FabricDescriptor> Fabrics { get; }
        public byte SupportedFabrics { get; }
        public byte CommissionedFabrics { get; }
        public List<byte[]> TrustedRootCertificates { get; }
        public byte CurrentFabricIndex { get; } = 0;
    }
}