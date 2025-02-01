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

using MatterDotNet.Attributes;
using MatterDotNet.Messages.InteractionModel;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using MatterDotNet.Protocol.Subprotocols;
using System.Diagnostics.CodeAnalysis;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Software Diagnostics Cluster provides a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class SoftwareDiagnostics : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0034;

        /// <summary>
        /// The Software Diagnostics Cluster provides a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems.
        /// </summary>
        [SetsRequiredMembers]
        public SoftwareDiagnostics(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected SoftwareDiagnostics(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            ThreadMetrics = new ReadAttribute<ThreadMetricsStruct[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ThreadMetricsStruct[] list = new ThreadMetricsStruct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ThreadMetricsStruct(reader.GetStruct(i)!);
                    return list;
                }
            };
            CurrentHeapFree = new ReadAttribute<ulong>(cluster, endPoint, 1) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            CurrentHeapUsed = new ReadAttribute<ulong>(cluster, endPoint, 2) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            CurrentHeapHighWatermark = new ReadAttribute<ulong>(cluster, endPoint, 3) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
        }

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
        public record ThreadMetricsStruct : TLVPayload {
            /// <summary>
            /// Thread Metrics
            /// </summary>
            public ThreadMetricsStruct() { }

            /// <summary>
            /// Thread Metrics
            /// </summary>
            [SetsRequiredMembers]
            public ThreadMetricsStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                ID = reader.GetULong(0)!.Value;
                Name = reader.GetString(1, true, 8);
                StackFreeCurrent = reader.GetUInt(2, true);
                StackFreeMinimum = reader.GetUInt(3, true);
                StackSize = reader.GetUInt(4, true);
            }
            public required ulong ID { get; set; }
            public string? Name { get; set; }
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
        public async Task<bool> ResetWatermarks(SecureSession session, CancellationToken token = default) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, null, token);
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
        /// Thread Metrics Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ThreadMetricsStruct[]> ThreadMetrics { get; init; }

        /// <summary>
        /// Current Heap Free Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> CurrentHeapFree { get; init; }

        /// <summary>
        /// Current Heap Used Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> CurrentHeapUsed { get; init; }

        /// <summary>
        /// Current Heap High Watermark Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> CurrentHeapHighWatermark { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Software Diagnostics";
        }
    }
}