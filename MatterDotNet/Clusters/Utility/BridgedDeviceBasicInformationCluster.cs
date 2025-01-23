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

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// This Cluster serves two purposes towards a Node communicating with a Bridge: indicate that the functionality on the Endpoint where it is placed (and its Parts) is bridged from a non-CHIP technology; and provide a centralized collection of attributes that the Node MAY collect to aid in conveying information regarding the Bridged Device to a user, such as the vendor name, the model name, or user-assigned name.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 3)]
    public class BridgedDeviceBasicInformation : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0039;

        /// <summary>
        /// This Cluster serves two purposes towards a Node communicating with a Bridge: indicate that the functionality on the Endpoint where it is placed (and its Parts) is bridged from a non-CHIP technology; and provide a centralized collection of attributes that the Node MAY collect to aid in conveying information regarding the Bridged Device to a user, such as the vendor name, the model name, or user-assigned name.
        /// </summary>
        public BridgedDeviceBasicInformation(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected BridgedDeviceBasicInformation(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Support Bridged ICD Devices.
            /// </summary>
            BridgedICDSupport = 1048576,
        }

        /// <summary>
        /// Product Finish
        /// </summary>
        public enum ProductFinish : byte {
            Other = 0,
            Matte = 1,
            Satin = 2,
            Polished = 3,
            Rugged = 4,
            Fabric = 5,
        }

        /// <summary>
        /// Color
        /// </summary>
        public enum Color : byte {
            Black = 0,
            Navy = 1,
            Green = 2,
            Teal = 3,
            Maroon = 4,
            Purple = 5,
            Olive = 6,
            Gray = 7,
            Blue = 8,
            Lime = 9,
            Aqua = 10,
            Red = 11,
            Fuchsia = 12,
            Yellow = 13,
            White = 14,
            Nickel = 15,
            Chrome = 16,
            Brass = 17,
            Copper = 18,
            Silver = 19,
            Gold = 20,
        }
        #endregion Enums

        #region Records
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

        #region Payloads
        private record KeepActivePayload : TLVPayload {
            public required uint StayActiveDuration { get; set; }
            public required uint TimeoutMs { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, StayActiveDuration);
                writer.WriteUInt(1, TimeoutMs, 3600000, 30000);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Keep Active
        /// </summary>
        public async Task<bool> KeepActive(SecureSession session, uint stayActiveDuration, uint timeoutMs) {
            KeepActivePayload requestFields = new KeepActivePayload() {
                StayActiveDuration = stayActiveDuration,
                TimeoutMs = timeoutMs,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x80, requestFields);
            return ValidateResponse(resp);
        }
        #endregion Commands

        #region Attributes
        /// <summary>
        /// Features supported by this cluster
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<Feature> GetSupportedFeatures(SecureSession session)
        {
            return (Feature)(byte)(await GetAttribute(session, 0xFFFC))!;
        }

        /// <summary>
        /// Returns true when the feature is supported by the cluster
        /// </summary>
        /// <param name="session"></param>
        /// <param name="feature"></param>
        /// <returns></returns>
        public async Task<bool> Supports(SecureSession session, Feature feature)
        {
            return ((feature & await GetSupportedFeatures(session)) != 0);
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
        /// Get the Product Appearance attribute
        /// </summary>
        public async Task<ProductAppearance> GetProductAppearance(SecureSession session) {
            return new ProductAppearance((object[])(await GetAttribute(session, 32))!);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Bridged Device Basic Information";
        }
    }
}