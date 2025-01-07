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

namespace MatterDotNet.Clusters.Application
{
    /// <summary>
    /// Application Launcher Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ApplicationLauncherCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x050C;

        /// <summary>
        /// Application Launcher Cluster
        /// </summary>
        public ApplicationLauncherCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ApplicationLauncherCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        public enum StatusEnum {
            /// <summary>
            /// Command succeeded
            /// </summary>
            Success = 0,
            /// <summary>
            /// Requested app is not available.
            /// </summary>
            AppNotAvailable = 1,
            /// <summary>
            /// Video platform unable to honor command.
            /// </summary>
            SystemBusy = 2,
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

            [SetsRequiredMembers]
            internal ApplicationEP(object[] fields) {
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

        #region Payloads
        private record LaunchAppPayload : TLVPayload {
            public required Application Application { get; set; }
            public byte[]? Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                Application.Serialize(writer, 0);
                if (Data != null)
                    writer.WriteBytes(1, Data);
                writer.EndContainer();
            }
        }

        private record StopAppPayload : TLVPayload {
            public required Application Application { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                Application.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        private record HideAppPayload : TLVPayload {
            public required Application Application { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                Application.Serialize(writer, 0);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Launcher Response - Reply from server
        /// </summary>
        public struct LauncherResponse() {
            public required StatusEnum Status { get; set; }
            public byte[]? Data { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Launch App
        /// </summary>
        public async Task<LauncherResponse?> LaunchApp(SecureSession session, Application Application, byte[]? Data) {
            LaunchAppPayload requestFields = new LaunchAppPayload() {
                Application = Application,
                Data = Data,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (byte[]?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Stop App
        /// </summary>
        public async Task<LauncherResponse?> StopApp(SecureSession session, Application Application) {
            StopAppPayload requestFields = new StopAppPayload() {
                Application = Application,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
                Data = (byte[]?)GetOptionalField(resp, 1),
            };
        }

        /// <summary>
        /// Hide App
        /// </summary>
        public async Task<LauncherResponse?> HideApp(SecureSession session, Application Application) {
            HideAppPayload requestFields = new HideAppPayload() {
                Application = Application,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new LauncherResponse() {
                Status = (StatusEnum)(byte)GetField(resp, 0),
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
        public async Task<List<ushort>> GetCatalogList(SecureSession session) {
            List<ushort> list = new List<ushort>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(reader.GetUShort(i)!.Value);
            return list;
        }

        /// <summary>
        /// Get the Current App attribute
        /// </summary>
        public async Task<ApplicationEP?> GetCurrentApp(SecureSession session) {
            return new ApplicationEP((object[])(await GetAttribute(session, 1))!) ?? null;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Application Launcher Cluster";
        }
    }
}