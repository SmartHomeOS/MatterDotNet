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
using MatterDotNet.Protocol.Sessions;

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides information about an application running on a TV or media player device which is represented as an endpoint.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ApplicationBasic : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050d;

        /// <summary>
        /// This cluster provides information about an application running on a TV or media player device which is represented as an endpoint.
        /// </summary>
        public ApplicationBasic(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ApplicationBasic(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Application Status
        /// </summary>
        public enum ApplicationStatus : byte {
            /// <summary>
            /// Application is not running.
            /// </summary>
            Stopped = 0x00,
            /// <summary>
            /// Application is running, is visible to the user, and is the active target for input.
            /// </summary>
            ActiveVisibleFocus = 0x01,
            /// <summary>
            /// Application is running but not visible to the user.
            /// </summary>
            ActiveHidden = 0x02,
            /// <summary>
            /// Application is running and visible, but is not the active target for input.
            /// </summary>
            ActiveVisibleNotFocus = 0x03,
        }
        #endregion Enums

        #region Attributes
        /// <summary>
        /// Get the Vendor Name attribute
        /// </summary>
        public async Task<string> GetVendorName(SecureSession session) {
            return (string)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Vendor ID attribute
        /// </summary>
        public async Task<ushort> GetVendorID(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1) ?? 0x0;
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
            return (ushort?)(dynamic?)await GetAttribute(session, 3) ?? 0x0;
        }

        /// <summary>
        /// Get the Application attribute
        /// </summary>
        public async Task<ApplicationLauncher.Application> GetApplication(SecureSession session) {
            return new ApplicationLauncher.Application((object[])(await GetAttribute(session, 4))!);
        }

        /// <summary>
        /// Get the Status attribute
        /// </summary>
        public async Task<ApplicationStatus> GetStatus(SecureSession session) {
            return (ApplicationStatus)await GetEnumAttribute(session, 5);
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
            return "Application Basic";
        }
    }
}