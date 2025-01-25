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
using System.Diagnostics.CodeAnalysis;

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
        [SetsRequiredMembers]
        public ApplicationBasic(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ApplicationBasic(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            VendorName = new ReadAttribute<string>(cluster, endPoint, 0) {
                Deserialize = x => (string)(dynamic?)x!
            };
            VendorID = new ReadAttribute<ushort>(cluster, endPoint, 1) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0

            };
            ApplicationName = new ReadAttribute<string>(cluster, endPoint, 2) {
                Deserialize = x => (string)(dynamic?)x!
            };
            ProductID = new ReadAttribute<ushort>(cluster, endPoint, 3) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0

            };
            Application = new ReadAttribute<ApplicationLauncher.Application>(cluster, endPoint, 4) {
                Deserialize = x => (ApplicationLauncher.Application)(dynamic?)x!
            };
            Status = new ReadAttribute<ApplicationStatus>(cluster, endPoint, 5) {
                Deserialize = x => (ApplicationStatus)DeserializeEnum(x)!
            };
            ApplicationVersion = new ReadAttribute<string>(cluster, endPoint, 6) {
                Deserialize = x => (string)(dynamic?)x!
            };
            AllowedVendorList = new ReadAttribute<ushort[]>(cluster, endPoint, 7) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ushort[] list = new ushort[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetUShort(i)!.Value;
                    return list;
                }
            };
        }

        #region Enums
        /// <summary>
        /// Application Status
        /// </summary>
        public enum ApplicationStatus : byte {
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

        #region Attributes
        /// <summary>
        /// Vendor Name Attribute
        /// </summary>
        public required ReadAttribute<string> VendorName { get; init; }

        /// <summary>
        /// Vendor ID Attribute
        /// </summary>
        public required ReadAttribute<ushort> VendorID { get; init; }

        /// <summary>
        /// Application Name Attribute
        /// </summary>
        public required ReadAttribute<string> ApplicationName { get; init; }

        /// <summary>
        /// Product ID Attribute
        /// </summary>
        public required ReadAttribute<ushort> ProductID { get; init; }

        /// <summary>
        /// Application Attribute
        /// </summary>
        public required ReadAttribute<ApplicationLauncher.Application> Application { get; init; }

        /// <summary>
        /// Status Attribute
        /// </summary>
        public required ReadAttribute<ApplicationStatus> Status { get; init; }

        /// <summary>
        /// Application Version Attribute
        /// </summary>
        public required ReadAttribute<string> ApplicationVersion { get; init; }

        /// <summary>
        /// Allowed Vendor List Attribute
        /// </summary>
        public required ReadAttribute<ushort[]> AllowedVendorList { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Application Basic";
        }
    }
}