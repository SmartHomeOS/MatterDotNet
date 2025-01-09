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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Software Diagnostics Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class SoftwareDiagnosticsCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0034;

        /// <summary>
        /// Software Diagnostics Cluster
        /// </summary>
        public SoftwareDiagnosticsCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected SoftwareDiagnosticsCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Node makes available the metrics for high watermark related to memory consumption.
            /// </summary>
            Watermarks = 1,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Thread Metrics
        /// </summary>
        public record ThreadMetrics : TLVPayload {
            /// <summary>
            /// Thread Metrics
            /// </summary>
            public ThreadMetrics() { }

            [SetsRequiredMembers]
            internal ThreadMetrics(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ID = reader.GetULong(0)!.Value;
                Name = reader.GetString(1, true);
                StackFreeCurrent = reader.GetUInt(2, true);
                StackFreeMinimum = reader.GetUInt(3, true);
                StackSize = reader.GetUInt(4, true);
            }
            public required ulong ID { get; set; }
            public string? Name { get; set; } = "";
            public uint? StackFreeCurrent { get; set; }
            public uint? StackFreeMinimum { get; set; }
            public uint? StackSize { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteULong(0, ID);
                if (Name != null)
                    writer.WriteString(1, Name, 8);
                if (StackFreeCurrent != null)
                    writer.WriteUInt(2, StackFreeCurrent);
                if (StackFreeMinimum != null)
                    writer.WriteUInt(3, StackFreeMinimum);
                if (StackSize != null)
                    writer.WriteUInt(4, StackSize);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Reset Watermarks
        /// </summary>
        public async Task<bool> ResetWatermarks(SecureSession session) {
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
        /// Get the Thread Metrics attribute
        /// </summary>
        public async Task<ThreadMetrics[]> GetThreadMetrics(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            ThreadMetrics[] list = new ThreadMetrics[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new ThreadMetrics(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Current Heap Free attribute
        /// </summary>
        public async Task<ulong> GetCurrentHeapFree(SecureSession session) {
            return (ulong)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Current Heap Used attribute
        /// </summary>
        public async Task<ulong> GetCurrentHeapUsed(SecureSession session) {
            return (ulong)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Current Heap High Watermark attribute
        /// </summary>
        public async Task<ulong> GetCurrentHeapHighWatermark(SecureSession session) {
            return (ulong)(dynamic?)(await GetAttribute(session, 3))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Software Diagnostics Cluster";
        }
    }
}