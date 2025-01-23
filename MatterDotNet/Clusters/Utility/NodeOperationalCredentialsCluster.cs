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
//
// WARNING: This file was auto-generated. Do not edit.

using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// This cluster is used to add or remove Operational Credentials on a Commissionee or Node, as well as manage the associated Fabrics.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class OperationalCredentials : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x003E;

        /// <summary>
        /// This cluster is used to add or remove Operational Credentials on a Commissionee or Node, as well as manage the associated Fabrics.
        /// </summary>
        public OperationalCredentials(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected OperationalCredentials(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Node Operational Cert Status
        /// </summary>
        public enum NodeOperationalCertStatus : byte {
            /// <summary>
            /// OK, no error
            /// </summary>
            OK = 0x00,
            /// <summary>
            /// <see cref="InvalidPublicKey"/> Public Key in the NOC does not match the public key in the NOCSR
            /// </summary>
            InvalidPublicKey = 0x01,
            /// <summary>
            /// <see cref="InvalidOperationalId"/> The Node Operational ID in the NOC is not formatted correctly.
            /// </summary>
            InvalidNodeOpId = 0x02,
            /// <summary>
            /// <see cref="InvalidNoc"/> Any other validation error in NOC chain
            /// </summary>
            InvalidNOC = 0x03,
            /// <summary>
            /// <see cref="MissingCsr"/> No record of prior CSR for which this NOC could match
            /// </summary>
            MissingCsr = 0x04,
            /// <summary>
            /// <see cref="TableFull"/> NOCs table full, cannot add another one
            /// </summary>
            TableFull = 0x05,
            /// <summary>
            /// <see cref="InvalidAdminSubject"/> Invalid CaseAdminSubject field for an AddNOC command.
            /// </summary>
            InvalidAdminSubject = 0x06,
            /// <summary>
            /// <see cref="FabricConflict"/> Trying to AddNOC instead of UpdateNOC against an existing Fabric.
            /// </summary>
            FabricConflict = 0x09,
            /// <summary>
            /// <see cref="LabelConflict"/> Label already exists on another Fabric.
            /// </summary>
            LabelConflict = 0x0a,
            /// <summary>
            /// <see cref="InvalidFabricIndex"/> FabricIndex argument is invalid.
            /// </summary>
            InvalidFabricIndex = 0x0b,
        }

        /// <summary>
        /// Certificate Chain Type
        /// </summary>
        public enum CertificateChainType : byte {
            /// <summary>
            /// Request the DER-encoded DAC certificate
            /// </summary>
            DACCertificate = 0x01,
            /// <summary>
            /// Request the DER-encoded PAI certificate
            /// </summary>
            PAICertificate = 0x02,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Fabric Descriptor
        /// </summary>
        public record FabricDescriptor : TLVPayload {
            /// <summary>
            /// Fabric Descriptor
            /// </summary>
            public FabricDescriptor() { }

            /// <summary>
            /// Fabric Descriptor
            /// </summary>
            [SetsRequiredMembers]
            public FabricDescriptor(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                RootPublicKey = reader.GetBytes(1, false, 65)!;
                VendorID = reader.GetUShort(2)!.Value;
                FabricID = reader.GetULong(3)!.Value;
                NodeID = reader.GetULong(4)!.Value;
                Label = reader.GetString(5, false, 32)!;
            }
            public required byte[] RootPublicKey { get; set; }
            public required ushort VendorID { get; set; }
            public required ulong FabricID { get; set; }
            public required ulong NodeID { get; set; }
            public required string Label { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(1, RootPublicKey, 65);
                writer.WriteUShort(2, VendorID);
                writer.WriteULong(3, FabricID);
                writer.WriteULong(4, NodeID);
                writer.WriteString(5, Label, 32);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// NOC
        /// </summary>
        public record NOC : TLVPayload {
            /// <summary>
            /// NOC
            /// </summary>
            public NOC() { }

            /// <summary>
            /// NOC
            /// </summary>
            [SetsRequiredMembers]
            public NOC(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                NOCField = reader.GetBytes(1, false)!;
                ICAC = reader.GetBytes(2, false)!;
            }
            public required byte[] NOCField { get; set; }
            public required byte[]? ICAC { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(1, NOCField);
                writer.WriteBytes(2, ICAC);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record AttestationRequestPayload : TLVPayload {
            public required byte[] AttestationNonce { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, AttestationNonce, 32);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Attestation Response - Reply from server
        /// </summary>
        public struct AttestationResponse() {
            public required byte[] AttestationElements { get; set; }
            public required byte[] AttestationSignature { get; set; }
        }

        private record CertificateChainRequestPayload : TLVPayload {
            public required CertificateChainType CertificateType { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)CertificateType);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Certificate Chain Response - Reply from server
        /// </summary>
        public struct CertificateChainResponse() {
            public required byte[] Certificate { get; set; }
        }

        private record CSRRequestPayload : TLVPayload {
            public required byte[] CSRNonce { get; set; }
            public bool? IsForUpdateNOC { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, CSRNonce, 32);
                if (IsForUpdateNOC != null)
                    writer.WriteBool(1, IsForUpdateNOC);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// CSR Response - Reply from server
        /// </summary>
        public struct CSRResponse() {
            public required byte[] NOCSRElements { get; set; }
            public required byte[] AttestationSignature { get; set; }
        }

        private record AddNOCPayload : TLVPayload {
            public required byte[] NOCValue { get; set; }
            public byte[]? ICACValue { get; set; }
            public required byte[] IPKValue { get; set; }
            public required ulong CaseAdminSubject { get; set; }
            public required ushort AdminVendorId { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NOCValue, 400);
                if (ICACValue != null)
                    writer.WriteBytes(1, ICACValue, 400);
                writer.WriteBytes(2, IPKValue, 16);
                writer.WriteULong(3, CaseAdminSubject);
                writer.WriteUShort(4, AdminVendorId);
                writer.EndContainer();
            }
        }

        private record UpdateNOCPayload : TLVPayload {
            public required byte[] NOCValue { get; set; }
            public byte[]? ICACValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NOCValue);
                if (ICACValue != null)
                    writer.WriteBytes(1, ICACValue);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// NOC Response - Reply from server
        /// </summary>
        public struct NOCResponse() {
            public required NodeOperationalCertStatus StatusCode { get; set; }
            public byte? FabricIndex { get; set; }
            public string? DebugText { get; set; }
        }

        private record UpdateFabricLabelPayload : TLVPayload {
            public required string Label { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Label, 32);
                writer.EndContainer();
            }
        }

        private record RemoveFabricPayload : TLVPayload {
            public required byte FabricIndex { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, FabricIndex);
                writer.EndContainer();
            }
        }

        private record AddTrustedRootCertificatePayload : TLVPayload {
            public required byte[] RootCACertificate { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, RootCACertificate);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Attestation Request
        /// </summary>
        public async Task<AttestationResponse?> AttestationRequest(SecureSession session, byte[] attestationNonce) {
            AttestationRequestPayload requestFields = new AttestationRequestPayload() {
                AttestationNonce = attestationNonce,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new AttestationResponse() {
                AttestationElements = (byte[])GetField(resp, 0),
                AttestationSignature = (byte[])GetField(resp, 1),
            };
        }

        /// <summary>
        /// Certificate Chain Request
        /// </summary>
        public async Task<CertificateChainResponse?> CertificateChainRequest(SecureSession session, CertificateChainType certificateType) {
            CertificateChainRequestPayload requestFields = new CertificateChainRequestPayload() {
                CertificateType = certificateType,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new CertificateChainResponse() {
                Certificate = (byte[])GetField(resp, 0),
            };
        }

        /// <summary>
        /// CSR Request
        /// </summary>
        public async Task<CSRResponse?> CSRRequest(SecureSession session, byte[] cSRNonce, bool? isForUpdateNOC) {
            CSRRequestPayload requestFields = new CSRRequestPayload() {
                CSRNonce = cSRNonce,
                IsForUpdateNOC = isForUpdateNOC,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new CSRResponse() {
                NOCSRElements = (byte[])GetField(resp, 0),
                AttestationSignature = (byte[])GetField(resp, 1),
            };
        }

        /// <summary>
        /// Add NOC
        /// </summary>
        public async Task<NOCResponse?> AddNOC(SecureSession session, byte[] nOCValue, byte[]? iCACValue, byte[] iPKValue, ulong caseAdminSubject, ushort adminVendorId) {
            AddNOCPayload requestFields = new AddNOCPayload() {
                NOCValue = nOCValue,
                ICACValue = iCACValue,
                IPKValue = iPKValue,
                CaseAdminSubject = caseAdminSubject,
                AdminVendorId = adminVendorId,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NOCResponse() {
                StatusCode = (NodeOperationalCertStatus)(byte)GetField(resp, 0),
                FabricIndex = (byte?)GetOptionalField(resp, 1),
                DebugText = (string?)GetOptionalField(resp, 2),
            };
        }

        /// <summary>
        /// Update NOC
        /// </summary>
        public async Task<NOCResponse?> UpdateNOC(SecureSession session, byte[] nOCValue, byte[]? iCACValue) {
            UpdateNOCPayload requestFields = new UpdateNOCPayload() {
                NOCValue = nOCValue,
                ICACValue = iCACValue,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x07, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NOCResponse() {
                StatusCode = (NodeOperationalCertStatus)(byte)GetField(resp, 0),
                FabricIndex = (byte?)GetOptionalField(resp, 1),
                DebugText = (string?)GetOptionalField(resp, 2),
            };
        }

        /// <summary>
        /// Update Fabric Label
        /// </summary>
        public async Task<NOCResponse?> UpdateFabricLabel(SecureSession session, string label) {
            UpdateFabricLabelPayload requestFields = new UpdateFabricLabelPayload() {
                Label = label,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NOCResponse() {
                StatusCode = (NodeOperationalCertStatus)(byte)GetField(resp, 0),
                FabricIndex = (byte?)GetOptionalField(resp, 1),
                DebugText = (string?)GetOptionalField(resp, 2),
            };
        }

        /// <summary>
        /// Remove Fabric
        /// </summary>
        public async Task<NOCResponse?> RemoveFabric(SecureSession session, byte fabricIndex) {
            RemoveFabricPayload requestFields = new RemoveFabricPayload() {
                FabricIndex = fabricIndex,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0a, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NOCResponse() {
                StatusCode = (NodeOperationalCertStatus)(byte)GetField(resp, 0),
                FabricIndex = (byte?)GetOptionalField(resp, 1),
                DebugText = (string?)GetOptionalField(resp, 2),
            };
        }

        /// <summary>
        /// Add Trusted Root Certificate
        /// </summary>
        public async Task<bool> AddTrustedRootCertificate(SecureSession session, byte[] rootCACertificate) {
            AddTrustedRootCertificatePayload requestFields = new AddTrustedRootCertificatePayload() {
                RootCACertificate = rootCACertificate,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x0b, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Get the NO Cs attribute
        /// </summary>
        public async Task<NOC[]> GetNOCs(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            NOC[] list = new NOC[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new NOC(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Fabrics attribute
        /// </summary>
        public async Task<FabricDescriptor[]> GetFabrics(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            FabricDescriptor[] list = new FabricDescriptor[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new FabricDescriptor(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Supported Fabrics attribute
        /// </summary>
        public async Task<byte> GetSupportedFabrics(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Commissioned Fabrics attribute
        /// </summary>
        public async Task<byte> GetCommissionedFabrics(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Trusted Root Certificates attribute
        /// </summary>
        public async Task<byte[][]> GetTrustedRootCertificates(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 4))!);
            byte[][] list = new byte[reader.Count][];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetBytes(i, false)!;
            return list;
        }

        /// <summary>
        /// Get the Current Fabric Index attribute
        /// </summary>
        public async Task<byte> GetCurrentFabricIndex(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 5))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Operational Credentials";
        }
    }
}