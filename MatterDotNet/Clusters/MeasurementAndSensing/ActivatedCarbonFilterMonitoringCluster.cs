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

namespace MatterDotNet.Clusters.MeasurementAndSensing
{
    /// <summary>
    /// Attributes and commands for monitoring activated carbon filters in a device
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ActivatedCarbonFilterMonitoring : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0072;

        /// <summary>
        /// Attributes and commands for monitoring activated carbon filters in a device
        /// </summary>
        public ActivatedCarbonFilterMonitoring(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ActivatedCarbonFilterMonitoring(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
            ReplacementProductList = 4,
        }

        /// <summary>
        /// Degradation Direction
        /// </summary>
        public enum DegradationDirection : byte {
            Up = 0,
            Down = 1,
        }

        /// <summary>
        /// Change Indication
        /// </summary>
        public enum ChangeIndication : byte {
            OK = 0,
            Warning = 1,
            Critical = 2,
        }

        /// <summary>
        /// Product Identifier Type
        /// </summary>
        public enum ProductIdentifierType : byte {
            UPC = 0,
            GTIN8 = 1,
            EAN = 2,
            GTIN14 = 3,
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

            /// <summary>
            /// Replacement Product
            /// </summary>
            [SetsRequiredMembers]
            public ReplacementProduct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ProductIdentifierType = (ProductIdentifierType)reader.GetUShort(0)!.Value;
                ProductIdentifierValue = reader.GetString(1, false, 20)!;
            }
            public required ProductIdentifierType ProductIdentifierType { get; set; }
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
        /// Get the Condition [%] attribute
        /// </summary>
        public async Task<byte> GetCondition(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Degradation Direction attribute
        /// </summary>
        public async Task<DegradationDirection> GetDegradationDirection(SecureSession session) {
            return (DegradationDirection)await GetEnumAttribute(session, 1);
        }

        /// <summary>
        /// Get the Change Indication attribute
        /// </summary>
        public async Task<ChangeIndication> GetChangeIndication(SecureSession session) {
            return (ChangeIndication)await GetEnumAttribute(session, 2);
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
            return TimeUtil.FromEpochSeconds((uint)(dynamic?)await GetAttribute(session, 4));
        }

        /// <summary>
        /// Set the Last Changed Time attribute
        /// </summary>
        public async Task SetLastChangedTime (SecureSession session, DateTime? value) {
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
            return "Activated Carbon Filter Monitoring";
        }
    }
}