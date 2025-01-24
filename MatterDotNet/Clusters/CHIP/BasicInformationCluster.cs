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

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// This cluster provides attributes and events for determining basic information about Nodes, which supports both Commissioning and operational determination of Node characteristics, such as Vendor ID, Product ID and serial number, which apply to the whole Node. Also allows setting user device information such as location.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class BasicInformation : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0028;

        /// <summary>
        /// This cluster provides attributes and events for determining basic information about Nodes, which supports both Commissioning and operational determination of Node characteristics, such as Vendor ID, Product ID and serial number, which apply to the whole Node. Also allows setting user device information such as location.
        /// </summary>
        public BasicInformation(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected BasicInformation(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Product Finish
        /// </summary>
        public enum ProductFinish : byte {
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

        /// <summary>
        /// Color
        /// </summary>
        public enum Color : byte {
            /// <summary>
            /// Approximately RGB #000000.
            /// </summary>
            Black = 0x0,
            /// <summary>
            /// Approximately RGB #000080.
            /// </summary>
            Navy = 0x1,
            /// <summary>
            /// Approximately RGB #008000.
            /// </summary>
            Green = 0x2,
            /// <summary>
            /// Approximately RGB #008080.
            /// </summary>
            Teal = 0x3,
            /// <summary>
            /// Approximately RGB #800080.
            /// </summary>
            Maroon = 0x4,
            /// <summary>
            /// Approximately RGB #800080.
            /// </summary>
            Purple = 0x5,
            /// <summary>
            /// Approximately RGB #808000.
            /// </summary>
            Olive = 0x6,
            /// <summary>
            /// Approximately RGB #808080.
            /// </summary>
            Gray = 0x7,
            /// <summary>
            /// Approximately RGB #0000FF.
            /// </summary>
            Blue = 0x8,
            /// <summary>
            /// Approximately RGB #00FF00.
            /// </summary>
            Lime = 0x9,
            /// <summary>
            /// Approximately RGB #00FFFF.
            /// </summary>
            Aqua = 0xA,
            /// <summary>
            /// Approximately RGB #FF0000.
            /// </summary>
            Red = 0xB,
            /// <summary>
            /// Approximately RGB #FF00FF.
            /// </summary>
            Fuchsia = 0xC,
            /// <summary>
            /// Approximately RGB #FFFF00.
            /// </summary>
            Yellow = 0xD,
            /// <summary>
            /// Approximately RGB #FFFFFF.
            /// </summary>
            White = 0xE,
            /// <summary>
            /// Typical hardware "Nickel" color.
            /// </summary>
            Nickel = 0xF,
            /// <summary>
            /// Typical hardware "Chrome" color.
            /// </summary>
            Chrome = 0x10,
            /// <summary>
            /// Typical hardware "Brass" color.
            /// </summary>
            Brass = 0x11,
            /// <summary>
            /// Typical hardware "Copper" color.
            /// </summary>
            Copper = 0x12,
            /// <summary>
            /// Typical hardware "Silver" color.
            /// </summary>
            Silver = 0x13,
            /// <summary>
            /// Typical hardware "Gold" color.
            /// </summary>
            Gold = 0x14,
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

            /// <summary>
            /// Capability Minima
            /// </summary>
            [SetsRequiredMembers]
            public CapabilityMinima(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CaseSessionsPerFabric = reader.GetUShort(0)!.Value;
                SubscriptionsPerFabric = reader.GetUShort(1)!.Value;
            }
            public required ushort CaseSessionsPerFabric { get; set; }
            public required ushort SubscriptionsPerFabric { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, CaseSessionsPerFabric);
                writer.WriteUShort(1, SubscriptionsPerFabric);
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

            /// <summary>
            /// Product Appearance
            /// </summary>
            [SetsRequiredMembers]
            public ProductAppearance(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Finish = (ProductFinish)reader.GetUShort(0)!.Value;
                PrimaryColor = (Color)reader.GetUShort(1)!.Value;
            }
            public required ProductFinish Finish { get; set; }
            public required Color? PrimaryColor { get; set; }
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
            return (string)(dynamic?)(await GetAttribute(session, 5))!;
        }

        /// <summary>
        /// Set the Node Label attribute
        /// </summary>
        public async Task SetNodeLabel (SecureSession session, string value) {
            await SetAttribute(session, 5, value);
        }

        /// <summary>
        /// Get the Location attribute
        /// </summary>
        public async Task<string> GetLocation(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 6) ?? "xx";
        }

        /// <summary>
        /// Set the Location attribute
        /// </summary>
        public async Task SetLocation (SecureSession session, string? value = "xx") {
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
            return (string)(dynamic?)(await GetAttribute(session, 16))!;
        }

        /// <summary>
        /// Get the Manufacturing Date attribute
        /// </summary>
        public async Task<string> GetManufacturingDate(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 17))!;
        }

        /// <summary>
        /// Get the Part Number attribute
        /// </summary>
        public async Task<string> GetPartNumber(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 18))!;
        }

        /// <summary>
        /// Get the Product URL attribute
        /// </summary>
        public async Task<string> GetProductURL(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 19))!;
        }

        /// <summary>
        /// Get the Product Label attribute
        /// </summary>
        public async Task<string> GetProductLabel(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 20))!;
        }

        /// <summary>
        /// Get the Serial Number attribute
        /// </summary>
        public async Task<string> GetSerialNumber(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 21))!;
        }

        /// <summary>
        /// Get the Local Config Disabled attribute
        /// </summary>
        public async Task<bool> GetLocalConfigDisabled(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 22) ?? false;
        }

        /// <summary>
        /// Set the Local Config Disabled attribute
        /// </summary>
        public async Task SetLocalConfigDisabled (SecureSession session, bool? value = false) {
            await SetAttribute(session, 22, value);
        }

        /// <summary>
        /// Get the Reachable attribute
        /// </summary>
        public async Task<bool> GetReachable(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 23) ?? true;
        }

        /// <summary>
        /// Get the Unique ID attribute
        /// </summary>
        public async Task<string> GetUniqueID(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 24))!;
        }

        /// <summary>
        /// Get the Capability Minima attribute
        /// </summary>
        public async Task<CapabilityMinima> GetCapabilityMinima(SecureSession session) {
            return new CapabilityMinima((object[])(await GetAttribute(session, 25))!);
        }

        /// <summary>
        /// Get the Product Appearance attribute
        /// </summary>
        public async Task<ProductAppearance> GetProductAppearance(SecureSession session) {
            return new ProductAppearance((object[])(await GetAttribute(session, 32))!);
        }

        /// <summary>
        /// Get the Specification Version attribute
        /// </summary>
        public async Task<uint> GetSpecificationVersion(SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 33))!;
        }

        /// <summary>
        /// Get the Max Paths Per Invoke attribute
        /// </summary>
        public async Task<ushort> GetMaxPathsPerInvoke(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 34))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Basic Information";
        }
    }
}