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

using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Basic Information Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class BasicInformationCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0028;

        /// <summary>
        /// Basic Information Cluster
        /// </summary>
        public BasicInformationCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected BasicInformationCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Color
        /// </summary>
        public enum ColorEnum {
            /// <summary>
            /// Approximately RGB #000000.
            /// </summary>
            Black = 0,
            /// <summary>
            /// Approximately RGB #000080.
            /// </summary>
            Navy = 1,
            /// <summary>
            /// Approximately RGB #008000.
            /// </summary>
            Green = 2,
            /// <summary>
            /// Approximately RGB #008080.
            /// </summary>
            Teal = 3,
            /// <summary>
            /// Approximately RGB #800080.
            /// </summary>
            Maroon = 4,
            /// <summary>
            /// Approximately RGB #800080.
            /// </summary>
            Purple = 5,
            /// <summary>
            /// Approximately RGB #808000.
            /// </summary>
            Olive = 6,
            /// <summary>
            /// Approximately RGB #808080.
            /// </summary>
            Gray = 7,
            /// <summary>
            /// Approximately RGB #0000FF.
            /// </summary>
            Blue = 8,
            /// <summary>
            /// Approximately RGB #00FF00.
            /// </summary>
            Lime = 9,
            /// <summary>
            /// Approximately RGB #00FFFF.
            /// </summary>
            Aqua = 10,
            /// <summary>
            /// Approximately RGB #FF0000.
            /// </summary>
            Red = 11,
            /// <summary>
            /// Approximately RGB #FF00FF.
            /// </summary>
            Fuchsia = 12,
            /// <summary>
            /// Approximately RGB #FFFF00.
            /// </summary>
            Yellow = 13,
            /// <summary>
            /// Approximately RGB #FFFFFF.
            /// </summary>
            White = 14,
            /// <summary>
            /// Typical hardware &quot;Nickel&quot; color.
            /// </summary>
            Nickel = 15,
            /// <summary>
            /// Typical hardware &quot;Chrome&quot; color.
            /// </summary>
            Chrome = 16,
            /// <summary>
            /// Typical hardware &quot;Brass&quot; color.
            /// </summary>
            Brass = 17,
            /// <summary>
            /// Typical hardware &quot;Copper&quot; color.
            /// </summary>
            Copper = 18,
            /// <summary>
            /// Typical hardware &quot;Silver&quot; color.
            /// </summary>
            Silver = 19,
            /// <summary>
            /// Typical hardware &quot;Gold&quot; color.
            /// </summary>
            Gold = 20,
        }

        /// <summary>
        /// Product Finish
        /// </summary>
        public enum ProductFinishEnum {
            /// <summary>
            /// Product has some other finish not listed below.
            /// </summary>
            Other = 0,
            /// <summary>
            /// Product has a matte finish.
            /// </summary>
            Matte = 1,
            /// <summary>
            /// Product has a satin finish.
            /// </summary>
            Satin = 2,
            /// <summary>
            /// Product has a polished or shiny finish.
            /// </summary>
            Polished = 3,
            /// <summary>
            /// Product has a rugged finish.
            /// </summary>
            Rugged = 4,
            /// <summary>
            /// Product has a fabric finish.
            /// </summary>
            Fabric = 5,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Capability Minima
        /// </summary>
        public record CapabilityMinima : TLVPayload {
            /// <summary>
            /// Capability Minima
            /// </summary>
            public CapabilityMinima() { }

            [SetsRequiredMembers]
            internal CapabilityMinima(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CaseSessionsPerFabric = reader.GetUShort(0)!.Value;
                SubscriptionsPerFabric = reader.GetUShort(1)!.Value;
            }
            public required ushort CaseSessionsPerFabric { get; set; } = 3;
            public required ushort SubscriptionsPerFabric { get; set; } = 3;
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, CaseSessionsPerFabric, ushort.MaxValue, 3);
                writer.WriteUShort(1, SubscriptionsPerFabric, ushort.MaxValue, 3);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Product Appearance
        /// </summary>
        public record ProductAppearance : TLVPayload {
            /// <summary>
            /// Product Appearance
            /// </summary>
            public ProductAppearance() { }

            [SetsRequiredMembers]
            internal ProductAppearance(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Finish = (ProductFinishEnum)reader.GetUShort(0)!.Value;
                PrimaryColor = (ColorEnum)reader.GetUShort(1)!.Value;
            }
            public required ProductFinishEnum Finish { get; set; }
            public required ColorEnum? PrimaryColor { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Finish);
                writer.WriteUShort(1, (ushort?)PrimaryColor);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        /// <summary>
        /// Get the Data Model Revision attribute
        /// </summary>
        public async Task<ushort> GetDataModelRevision(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Vendor Name attribute
        /// </summary>
        public async Task<string> GetVendorName(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Vendor ID attribute
        /// </summary>
        public async Task<ushort> GetVendorID(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Product Name attribute
        /// </summary>
        public async Task<string> GetProductName(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Product ID attribute
        /// </summary>
        public async Task<ushort> GetProductID(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 4))!;
        }

        /// <summary>
        /// Get the Node Label attribute
        /// </summary>
        public async Task<string> GetNodeLabel(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 5) ?? "";
        }

        /// <summary>
        /// Set the Node Label attribute
        /// </summary>
        public async Task SetNodeLabel (SecureSession session, string? value = "") {
            await SetAttribute(session, 5, value);
        }

        /// <summary>
        /// Get the Location attribute
        /// </summary>
        public async Task<string> GetLocation(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 6) ?? "XX";
        }

        /// <summary>
        /// Set the Location attribute
        /// </summary>
        public async Task SetLocation (SecureSession session, string? value = "XX") {
            await SetAttribute(session, 6, value);
        }

        /// <summary>
        /// Get the Hardware Version attribute
        /// </summary>
        public async Task<ushort> GetHardwareVersion(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 7) ?? 0;
        }

        /// <summary>
        /// Get the Hardware Version String attribute
        /// </summary>
        public async Task<string> GetHardwareVersionString(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 8))!;
        }

        /// <summary>
        /// Get the Software Version attribute
        /// </summary>
        public async Task<uint> GetSoftwareVersion(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 9) ?? 0;
        }

        /// <summary>
        /// Get the Software Version String attribute
        /// </summary>
        public async Task<string> GetSoftwareVersionString(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 10))!;
        }

        /// <summary>
        /// Get the Manufacturing Date attribute
        /// </summary>
        public async Task<string> GetManufacturingDate(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 11))!;
        }

        /// <summary>
        /// Get the Part Number attribute
        /// </summary>
        public async Task<string> GetPartNumber(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 12))!;
        }

        /// <summary>
        /// Get the Product URL attribute
        /// </summary>
        public async Task<string> GetProductURL(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 13))!;
        }

        /// <summary>
        /// Get the Product Label attribute
        /// </summary>
        public async Task<string> GetProductLabel(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 14))!;
        }

        /// <summary>
        /// Get the Serial Number attribute
        /// </summary>
        public async Task<string> GetSerialNumber(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 15))!;
        }

        /// <summary>
        /// Get the Local Config Disabled attribute
        /// </summary>
        public async Task<bool> GetLocalConfigDisabled(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 16) ?? false;
        }

        /// <summary>
        /// Set the Local Config Disabled attribute
        /// </summary>
        public async Task SetLocalConfigDisabled (SecureSession session, bool? value = false) {
            await SetAttribute(session, 16, value);
        }

        /// <summary>
        /// Get the Reachable attribute
        /// </summary>
        public async Task<bool> GetReachable(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 17) ?? true;
        }

        /// <summary>
        /// Get the Unique ID attribute
        /// </summary>
        public async Task<string> GetUniqueID(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 18))!;
        }

        /// <summary>
        /// Get the Capability Minima attribute
        /// </summary>
        public async Task<CapabilityMinima> GetCapabilityMinima(SecureSession session) {
            return new CapabilityMinima((object[])(await GetAttribute(session, 19))!);
        }

        /// <summary>
        /// Get the Product Appearance attribute
        /// </summary>
        public async Task<ProductAppearance> GetProductAppearance(SecureSession session) {
            return new ProductAppearance((object[])(await GetAttribute(session, 20))!);
        }

        /// <summary>
        /// Get the Specification Version attribute
        /// </summary>
        public async Task<uint> GetSpecificationVersion(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 21) ?? 0;
        }

        /// <summary>
        /// Get the Max Paths Per Invoke attribute
        /// </summary>
        public async Task<ushort> GetMaxPathsPerInvoke(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 22) ?? 1;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Basic Information Cluster";
        }
    }
}