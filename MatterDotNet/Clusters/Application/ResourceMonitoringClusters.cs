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
using MatterDotNet.Util;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Resource Monitoring Clusters
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ResourceMonitoringClusters : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0071;

        /// <summary>
        /// Resource Monitoring Clusters
        /// </summary>
        public ResourceMonitoringClusters(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ResourceMonitoringClusters(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Supports monitoring the condition of the resource in percentage
            /// </summary>
            Condition = 1,
            /// <summary>
            /// Supports warning indication
            /// </summary>
            Warning = 2,
            /// <summary>
            /// Supports specifying the list of replacement products
            /// </summary>
            Replacement = 4,
        }

        /// <summary>
        /// Change Indication
        /// </summary>
        public enum ChangeIndicationEnum {
            /// <summary>
            /// Resource is in good condition, no intervention required
            /// </summary>
            OK = 0,
            /// <summary>
            /// Resource will be exhausted soon, intervention will shortly be required
            /// </summary>
            Warning = 1,
            /// <summary>
            /// Resource is exhausted, immediate intervention is required
            /// </summary>
            Critical = 2,
        }

        /// <summary>
        /// Degradation Direction
        /// </summary>
        public enum DegradationDirectionEnum {
            /// <summary>
            /// The degradation of the resource is indicated by an upwards moving/increasing value
            /// </summary>
            Up = 0,
            /// <summary>
            /// The degradation of the resource is indicated by a downwards moving/decreasing value
            /// </summary>
            Down = 1,
        }

        /// <summary>
        /// Product Identifier Type
        /// </summary>
        public enum ProductIdentifierTypeEnum {
            /// <summary>
            /// 12-digit Universal Product Code
            /// </summary>
            UPC = 0,
            /// <summary>
            /// 8-digit Global Trade Item Number
            /// </summary>
            GTIN8 = 1,
            /// <summary>
            /// 13-digit European Article Number
            /// </summary>
            EAN = 2,
            /// <summary>
            /// 14-digit Global Trade Item Number
            /// </summary>
            GTIN14 = 3,
            /// <summary>
            /// Original Equipment Manufacturer part number
            /// </summary>
            OEM = 4,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Replacement Product
        /// </summary>
        public record ReplacementProduct : TLVPayload {
            /// <summary>
            /// Replacement Product
            /// </summary>
            public ReplacementProduct() { }

            [SetsRequiredMembers]
            internal ReplacementProduct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ProductIdentifierType = (ProductIdentifierTypeEnum)reader.GetUShort(0)!.Value;
                ProductIdentifierValue = reader.GetString(1, false)!;
            }
            public required ProductIdentifierTypeEnum ProductIdentifierType { get; set; }
            public required string ProductIdentifierValue { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)ProductIdentifierType);
                writer.WriteString(1, ProductIdentifierValue, 20);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Reset Condition
        /// </summary>
        public async Task<bool> ResetCondition(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00);
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
        /// Get the Condition attribute
        /// </summary>
        public async Task<byte> GetCondition(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Degradation Direction attribute
        /// </summary>
        public async Task<DegradationDirectionEnum> GetDegradationDirection(SecureSession session) {
            return (DegradationDirectionEnum)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Change Indication attribute
        /// </summary>
        public async Task<ChangeIndicationEnum> GetChangeIndication(SecureSession session) {
            return (ChangeIndicationEnum)await GetEnumAttribute(session, 2);
        }

        /// <summary>
        /// Get the In Place Indicator attribute
        /// </summary>
        public async Task<bool> GetInPlaceIndicator(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Last Changed Time attribute
        /// </summary>
        public async Task<DateTime?> GetLastChangedTime(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 4, true) ?? null;
        }

        /// <summary>
        /// Set the Last Changed Time attribute
        /// </summary>
        public async Task SetLastChangedTime (SecureSession session, DateTime? value = null) {
            await SetAttribute(session, 4, value, true);
        }

        /// <summary>
        /// Get the Replacement Product List attribute
        /// </summary>
        public async Task<ReplacementProduct[]> GetReplacementProductList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 5))!);
            ReplacementProduct[] list = new ReplacementProduct[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ReplacementProduct(reader.GetStruct(i)!);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Resource Monitoring Clusters";
        }
    }
}