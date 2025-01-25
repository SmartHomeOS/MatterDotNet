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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Service Area cluster provides an interface for controlling the areas where a device should operate, and for querying the current area being serviced.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class ServiceArea : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0150;

        /// <summary>
        /// The Service Area cluster provides an interface for controlling the areas where a device should operate, and for querying the current area being serviced.
        /// </summary>
        [SetsRequiredMembers]
        public ServiceArea(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected ServiceArea(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            SupportedAreas = new ReadAttribute<Area[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Area[] list = new Area[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Area(reader.GetStruct(i)!);
                    return list;
                }
            };
            SupportedMaps = new ReadAttribute<Map[]>(cluster, endPoint, 1) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    Map[] list = new Map[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new Map(reader.GetStruct(i)!);
                    return list;
                }
            };
            SelectedAreas = new ReadAttribute<uint[]>(cluster, endPoint, 2) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    uint[] list = new uint[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = reader.GetUInt(i)!.Value;
                    return list;
                }
            };
            CurrentArea = new ReadAttribute<uint?>(cluster, endPoint, 3, true) {
                Deserialize = x => (uint?)(dynamic?)x
            };
            EstimatedEndTime = new ReadAttribute<DateTime?>(cluster, endPoint, 4, true) {
                Deserialize = x => TimeUtil.FromEpochSeconds((uint)(dynamic?)x)
            };
            Progress = new ReadAttribute<ProgressStruct[]>(cluster, endPoint, 5) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    ProgressStruct[] list = new ProgressStruct[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new ProgressStruct(reader.GetStruct(i)!);
                    return list;
                }
            };
        }

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
        public enum OperationalStatus : byte {
            /// <summary>
            /// The device has not yet started operating at the given area, or has not finished operating at that area but it is not currently operating at the area
            /// </summary>
            Pending = 0,
            /// <summary>
            /// The device is currently operating at the given area
            /// </summary>
            Operating = 1,
            /// <summary>
            /// The device has skipped the given area, before or during operating at it, due to a SkipArea command, due an out of band command (e.g. from the vendor's application), due to a vendor specific reason, such as a time limit used by the device, or due the device ending operating unsuccessfully
            /// </summary>
            Skipped = 2,
            /// <summary>
            /// The device has completed operating at the given area
            /// </summary>
            Completed = 3,
        }

        /// <summary>
        /// Select Areas Status
        /// </summary>
        public enum SelectAreasStatus : byte {
            Success = 0,
            UnsupportedArea = 1,
            InvalidInMode = 2,
            InvalidSet = 3,
        }

        /// <summary>
        /// Skip Area Status
        /// </summary>
        public enum SkipAreaStatus : byte {
            Success = 0,
            InvalidAreaList = 1,
            InvalidInMode = 2,
            InvalidSkippedArea = 3,
        }
        #endregion Enums

        #region Records
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
                LandmarkTag = (LandmarkTag)reader.GetUShort(0)!.Value;
                RelativePositionTag = (RelativePositionTag)reader.GetUShort(1)!.Value;
            }
            public required LandmarkTag LandmarkTag { get; set; }
            public required RelativePositionTag? RelativePositionTag { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)LandmarkTag);
                writer.WriteUShort(1, (ushort?)RelativePositionTag);
                writer.EndContainer();
            }
        }

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
                Name = reader.GetString(1, false, 64)!;
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
        /// Progress
        /// </summary>
        public record ProgressStruct : TLVPayload {
            /// <summary>
            /// Progress
            /// </summary>
            public ProgressStruct() { }

            /// <summary>
            /// Progress
            /// </summary>
            [SetsRequiredMembers]
            public ProgressStruct(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                AreaID = reader.GetUInt(0)!.Value;
                Status = (OperationalStatus)reader.GetUShort(1)!.Value;
                TotalOperationalTime = TimeUtil.FromSeconds(reader.GetUInt(2, true));
                EstimatedTime = TimeUtil.FromSeconds(reader.GetUInt(3, true));
            }
            public required uint AreaID { get; set; }
            public required OperationalStatus Status { get; set; }
            public TimeSpan? TotalOperationalTime { get; set; }
            public TimeSpan? EstimatedTime { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, AreaID);
                writer.WriteUShort(1, (ushort)Status);
                if (TotalOperationalTime != null)
                    writer.WriteUInt(2, (uint)TotalOperationalTime!.Value.TotalSeconds);
                if (EstimatedTime != null)
                    writer.WriteUInt(3, (uint)EstimatedTime!.Value.TotalSeconds);
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
        public async Task<SelectAreasResponse?> SelectAreas(SecureSession session, uint[] newAreas) {
            SelectAreasPayload requestFields = new SelectAreasPayload() {
                NewAreas = newAreas,
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
        public async Task<SkipAreaResponse?> SkipArea(SecureSession session, uint skippedArea) {
            SkipAreaPayload requestFields = new SkipAreaPayload() {
                SkippedArea = skippedArea,
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
        /// Supported Areas Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Area[]> SupportedAreas { get; init; }

        /// <summary>
        /// Supported Maps Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<Map[]> SupportedMaps { get; init; }

        /// <summary>
        /// Selected Areas Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint[]> SelectedAreas { get; init; }

        /// <summary>
        /// Current Area Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint?> CurrentArea { get; init; }

        /// <summary>
        /// Estimated End Time Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<DateTime?> EstimatedEndTime { get; init; }

        /// <summary>
        /// Progress Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ProgressStruct[]> Progress { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Service Area";
        }
    }
}