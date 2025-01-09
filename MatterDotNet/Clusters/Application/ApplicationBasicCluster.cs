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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Application Basic Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ApplicationBasicCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050D;

        /// <summary>
        /// Application Basic Cluster
        /// </summary>
        public ApplicationBasicCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ApplicationBasicCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Application Status
        /// </summary>
        public enum ApplicationStatusEnum {
            /// <summary>
            /// Application is not running.
            /// </summary>
            Stopped = 0,
            /// <summary>
            /// Application is running, is visible to the user, and is the active target for input.
            /// </summary>
            ActiveVisibleFocus = 1,
            /// <summary>
            /// Application is running but not visible to the user.
            /// </summary>
            ActiveHidden = 2,
            /// <summary>
            /// Application is running and visible, but is not the active target for input.
            /// </summary>
            ActiveVisibleNotFocus = 3,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Application
        /// </summary>
        public record Application : TLVPayload {
            /// <summary>
            /// Application
            /// </summary>
            public Application() { }

            [SetsRequiredMembers]
            internal Application(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                CatalogVendorID = reader.GetUShort(0)!.Value;
                ApplicationID = reader.GetString(1, false)!;
            }
            public required ushort CatalogVendorID { get; set; }
            public required string ApplicationID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, CatalogVendorID);
                writer.WriteString(1, ApplicationID);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        /// <summary>
        /// Get the Vendor Name attribute
        /// </summary>
        public async Task<string> GetVendorName(SecureSession session) {
            return (string?)(dynamic?)await GetAttribute(session, 0) ?? "";
        }

        /// <summary>
        /// Get the Vendor ID attribute
        /// </summary>
        public async Task<ushort> GetVendorID(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Application Name attribute
        /// </summary>
        public async Task<string> GetApplicationName(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Product ID attribute
        /// </summary>
        public async Task<ushort> GetProductID(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Application attribute
        /// </summary>
        public async Task<Application> GetApplication(SecureSession session) {
            return new Application((object[])(await GetAttribute(session, 4))!);
        }

        /// <summary>
        /// Get the Status attribute
        /// </summary>
        public async Task<ApplicationStatusEnum> GetStatus(SecureSession session) {
            return (ApplicationStatusEnum)await GetEnumAttribute(session, 5);
        }

        /// <summary>
        /// Get the Application Version attribute
        /// </summary>
        public async Task<string> GetApplicationVersion(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 6))!;
        }

        /// <summary>
        /// Get the Allowed Vendor List attribute
        /// </summary>
        public async Task<ushort[]> GetAllowedVendorList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 7))!);
            ushort[] list = new ushort[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUShort(i)!.Value;
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Application Basic Cluster";
        }
    }
}