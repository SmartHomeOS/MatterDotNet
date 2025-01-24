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

namespace MatterDotNet.Clusters.Media
{
    /// <summary>
    /// This cluster provides an interface for launching content on a media player device such as a TV or Speaker.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ApplicationLauncher : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050c;

        /// <summary>
        /// This cluster provides an interface for launching content on a media player device such as a TV or Speaker.
        /// </summary>
        public ApplicationLauncher(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ApplicationLauncher(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Support for attributes and commands required for endpoint to support launching any application within the supported application catalogs
            /// </summary>
            ApplicationPlatform = 1,
        }

        /// <summary>
        /// Status
        /// </summary>
        public enum Status : byte {
            /// <summary>
            /// Command succeeded
            /// </summary>
            Success = 0,
            /// <summary>
            /// Requested app is not available
            /// </summary>
            AppNotAvailable = 1,
            /// <summary>
            /// Video platform unable to honor command
            /// </summary>
            SystemBusy = 2,
            /// <summary>
            /// User approval for app download is pending
            /// </summary>
            PendingUserApproval = 3,
            /// <summary>
            /// Downloading the requested app
            /// </summary>
            Downloading = 4,
            /// <summary>
            /// Installing the requested app
            /// </summary>
            Installing = 5,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Application EP
        /// </summary>
        public record ApplicationEP : TLVPayload {
            /// <summary>
            /// Application EP
            /// </summary>
            public ApplicationEP() { }

            /// <summary>
            /// Application EP
            /// </summary>
            [SetsRequiredMembers]
            public ApplicationEP(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Application = new Application((object[])fields[0]);
                Endpoint = reader.GetUShort(1, true);
            }
            public required Application Application { get; set; }
            public ushort? Endpoint { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                Application.Serialize(writer, 0);
                if (Endpoint != null)
                    writer.WriteUShort(1, Endpoint);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Application
        /// </summary>
        public record Application : TLVPayload {
            /// <summary>
            /// Application
            /// </summary>
            public Application() { }

            /// <summary>
            /// Application
            /// </summary>
            [SetsRequiredMembers]
            public Application(object[] fields) {
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

        #region Payloads
        private record LaunchAppPayload : TLVPayload {
            public Application? Application { get; set; }
            public byte[]? Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (Application != null)
                    Application.Serialize(writer, 0);
                if (Data != null)
                    writer.WriteBytes(1, Data);
                writer.EndContainer();
            }
        }

        private record StopAppPayload : TLVPayload {
            public Application? Application { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (Application != null)
                    Application.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record HideAppPayload : TLVPayload {
            public Application? Application { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (Application != null)
                    Application.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Launcher Response - Reply from server
        /// </summary>
        public struct LauncherResponse() {
            public required Status Status { get; set; }
            public byte[]? Data { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Launch App
        /// </summary>
        public async Task<LauncherResponse?> LaunchApp(SecureSession session, Application? application, byte[]? data) {
            LaunchAppPayload requestFields = new LaunchAppPayload() {
                Application = application,
                Data = data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (byte[]?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Stop App
        /// </summary>
        public async Task<LauncherResponse?> StopApp(SecureSession session, Application? application) {
            StopAppPayload requestFields = new StopAppPayload() {
                Application = application,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (byte[]?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Hide App
        /// </summary>
        public async Task<LauncherResponse?> HideApp(SecureSession session, Application? application) {
            HideAppPayload requestFields = new HideAppPayload() {
                Application = application,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (Status)(byte)GetField(resp, 0),
                Data = (byte[]?)GetOptionalField(resp, 1),
            };
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
        /// Get the Catalog List attribute
        /// </summary>
        public async Task<ushort[]> GetCatalogList(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ushort[] list = new ushort[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Current App attribute
        /// </summary>
        public async Task<ApplicationEP?> GetCurrentApp(SecureSession session) {
            return new ApplicationEP((object[])(await GetAttribute(session, 1))!);
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Application Launcher";
        }
    }
}