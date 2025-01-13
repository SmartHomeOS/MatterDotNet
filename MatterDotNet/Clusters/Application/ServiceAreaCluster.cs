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

using MatterDotNet.Messages;
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
    /// Service Area Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ServiceAreaCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0150;

        /// <summary>
        /// Service Area Cluster
        /// </summary>
        public ServiceAreaCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected ServiceAreaCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// The device allows changing the selected areas while running
            /// </summary>
            SelectWhileRunning = 1,
            /// <summary>
            /// The device implements the progress reporting feature
            /// </summary>
            ProgressReporting = 2,
            /// <summary>
            /// The device has map support
            /// </summary>
            Maps = 4,
        }

        /// <summary>
        /// Operational Status
        /// </summary>
        public enum OperationalStatusEnum {
            /// <summary>
            /// The device has not yet started operating at the given area, or has not finished operating at that area but it is not currently operating at the area
            /// </summary>
            Pending = 0x00,
            /// <summary>
            /// The device is currently operating at the given area
            /// </summary>
            Operating = 0x01,
            /// <summary>
            /// The device has skipped the given area, before or during operating at it, due to a SkipArea command, due an out of band command (e.g. from the vendor's application), due to a vendor specific reason, such as a time limit used by the device, or due the device ending operating unsuccessfully
            /// </summary>
            Skipped = 0x02,
            /// <summary>
            /// The device has completed operating at the given area
            /// </summary>
            Completed = 0x03,
        }

        /// <summary>
        /// Select Areas Status
        /// </summary>
        public enum SelectAreasStatus {
            Success = 0x00,
            UnsupportedArea = 0x01,
            DuplicatedAreas = 0x02,
            InvalidInMode = 0x03,
            InvalidSet = 0x04,
        }

        /// <summary>
        /// Skip Area Status
        /// </summary>
        public enum SkipAreaStatus {
            Success = 0x00,
            InvalidAreaList = 0x01,
            InvalidInMode = 0x02,
            InvalidSkippedArea = 0x03,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Area Info
        /// </summary>
        public record AreaInfo : TLVPayload {
            /// <summary>
            /// Area Info
            /// </summary>
            public AreaInfo() { }

            /// <summary>
            /// Area Info
            /// </summary>
            [SetsRequiredMembers]
            public AreaInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LocationInfo = new LocationDescriptor((object[])fields[0]);
                LandmarkInfo = new LandmarkInfo((object[])fields[1]);
            }
            public required LocationDescriptor? LocationInfo { get; set; }
            public required LandmarkInfo? LandmarkInfo { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (LocationInfo == null)
                    writer.WriteNull(0);
                else
                    LocationInfo.Serialize(writer, 0);
                if (LandmarkInfo == null)
                    writer.WriteNull(1);
                else
                    LandmarkInfo.Serialize(writer, 1);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Area
        /// </summary>
        public record Area : TLVPayload {
            /// <summary>
            /// Area
            /// </summary>
            public Area() { }

            /// <summary>
            /// Area
            /// </summary>
            [SetsRequiredMembers]
            public Area(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                AreaID = reader.GetUInt(0)!.Value;
                MapID = reader.GetUInt(1, true);
                AreaInfo = new AreaInfo((object[])fields[2]);
            }
            public required uint AreaID { get; set; }
            public required uint? MapID { get; set; }
            public required AreaInfo AreaInfo { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, AreaID);
                writer.WriteUInt(1, MapID);
                AreaInfo.Serialize(writer, 2);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Landmark Info
        /// </summary>
        public record LandmarkInfo : TLVPayload {
            /// <summary>
            /// Landmark Info
            /// </summary>
            public LandmarkInfo() { }

            /// <summary>
            /// Landmark Info
            /// </summary>
            [SetsRequiredMembers]
            public LandmarkInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                LandmarkTag = reader.GetByte(0)!.Value;
                RelativePositionTag = reader.GetByte(1, true);
            }
            public required byte LandmarkTag { get; set; }
            public required byte? RelativePositionTag { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteByte(0, LandmarkTag);
                writer.WriteByte(1, RelativePositionTag);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Map
        /// </summary>
        public record Map : TLVPayload {
            /// <summary>
            /// Map
            /// </summary>
            public Map() { }

            /// <summary>
            /// Map
            /// </summary>
            [SetsRequiredMembers]
            public Map(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                MapID = reader.GetUInt(0)!.Value;
                Name = reader.GetString(1, false)!;
            }
            public required uint MapID { get; set; }
            public required string Name { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, MapID);
                writer.WriteString(1, Name, 64);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Progress
        /// </summary>
        public record Progress : TLVPayload {
            /// <summary>
            /// Progress
            /// </summary>
            public Progress() { }

            /// <summary>
            /// Progress
            /// </summary>
            [SetsRequiredMembers]
            public Progress(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                AreaID = reader.GetUInt(0)!.Value;
                Status = (OperationalStatusEnum)reader.GetUShort(1)!.Value;
                TotalOperationalTime = TimeUtil.FromSeconds(reader.GetUInt(2, true));
                InitialTimeEstimate = TimeUtil.FromSeconds(reader.GetUInt(3, true));
            }
            public required uint AreaID { get; set; }
            public required OperationalStatusEnum Status { get; set; }
            public TimeSpan? TotalOperationalTime { get; set; }
            public TimeSpan? InitialTimeEstimate { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, AreaID);
                writer.WriteUShort(1, (ushort)Status);
                if (TotalOperationalTime != null)
                    writer.WriteUInt(2, (uint)TotalOperationalTime!.Value.TotalSeconds);
                if (InitialTimeEstimate != null)
                    writer.WriteUInt(3, (uint)InitialTimeEstimate!.Value.TotalSeconds);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record SelectAreasPayload : TLVPayload {
            public required uint[] NewAreas { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in NewAreas) {
                        writer.WriteUInt(-1, item);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Select Areas Response - Reply from server
        /// </summary>
        public struct SelectAreasResponse() {
            public required SelectAreasStatus Status { get; set; }
            public required string StatusText { get; set; }
        }

        private record SkipAreaPayload : TLVPayload {
            public required uint SkippedArea { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, SkippedArea);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Skip Area Response - Reply from server
        /// </summary>
        public struct SkipAreaResponse() {
            public required SkipAreaStatus Status { get; set; }
            public required string StatusText { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Select Areas
        /// </summary>
        public async Task<SelectAreasResponse?> SelectAreas(SecureSession session, uint[] NewAreas) {
            SelectAreasPayload requestFields = new SelectAreasPayload() {
                NewAreas = NewAreas,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SelectAreasResponse() {
                Status = (SelectAreasStatus)(byte)GetField(resp, 0),
                StatusText = (string)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Skip Area
        /// </summary>
        public async Task<SkipAreaResponse?> SkipArea(SecureSession session, uint SkippedArea) {
            SkipAreaPayload requestFields = new SkipAreaPayload() {
                SkippedArea = SkippedArea,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new SkipAreaResponse() {
                Status = (SkipAreaStatus)(byte)GetField(resp, 0),
                StatusText = (string)GetField(resp, 1),
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
        /// Get the Supported Areas attribute
        /// </summary>
        public async Task<Area[]> GetSupportedAreas(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            Area[] list = new Area[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Area(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Supported Maps attribute
        /// </summary>
        public async Task<Map[]> GetSupportedMaps(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            Map[] list = new Map[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Map(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Selected Areas attribute
        /// </summary>
        public async Task<uint[]> GetSelectedAreas(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 2))!);
            uint[] list = new uint[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = reader.GetUInt(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Current Area attribute
        /// </summary>
        public async Task<uint?> GetCurrentArea(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 3, true) ?? null;
        }

        /// <summary>
        /// Get the Estimated End Time attribute
        /// </summary>
        public async Task<DateTime?> GetEstimatedEndTime(SecureSession session) {
            return (DateTime?)(dynamic?)await GetAttribute(session, 4, true) ?? null;
        }

        /// <summary>
        /// Get the Progress attribute
        /// </summary>
        public async Task<Progress[]> GetProgress(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 5))!);
            Progress[] list = new Progress[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new Progress(reader.GetStruct(i)!);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Service Area Cluster";
        }
    }
}