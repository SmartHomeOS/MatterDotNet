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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Access Control Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class AccessControlCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x001F;

        /// <summary>
        /// Access Control Cluster
        /// </summary>
        public AccessControlCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected AccessControlCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Access Control Entry Auth Mode
        /// </summary>
        public enum AccessControlEntryAuthModeEnum {
            /// <summary>
            /// Passcode authenticated session
            /// </summary>
            PASE = 1,
            /// <summary>
            /// Certificate authenticated session
            /// </summary>
            CASE = 2,
            /// <summary>
            /// Group authenticated session
            /// </summary>
            Group = 3,
        }

        /// <summary>
        /// Access Control Entry Privilege
        /// </summary>
        public enum AccessControlEntryPrivilegeEnum {
            /// <summary>
            /// Can read and observe all (except Access Control Cluster and as seen by a non-Proxy)
            /// </summary>
            View = 1,
            /// <summary>
            /// Can read and observe all (as seen by a Proxy)
            /// </summary>
            Proxy = 2,
            /// <summary>
            /// View privileges, and can perform the primary function of this Node (except Access Control Cluster)
            /// </summary>
            Operate = 3,
            /// <summary>
            /// Operate privileges, and can modify persistent configuration of this Node (except Access Control Cluster)
            /// </summary>
            Manage = 4,
            /// <summary>
            /// Manage privileges, and can observe and modify the Access Control Cluster
            /// </summary>
            Administer = 5,
        }

        /// <summary>
        /// Change Type
        /// </summary>
        public enum ChangeTypeEnum {
            /// <summary>
            /// Entry or extension was changed
            /// </summary>
            Changed = 0,
            /// <summary>
            /// Entry or extension was added
            /// </summary>
            Added = 1,
            /// <summary>
            /// Entry or extension was removed
            /// </summary>
            Removed = 2,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Access Control Entry
        /// </summary>
        public record AccessControlEntry : TLVPayload {
            [SetsRequiredMembers]
            internal AccessControlEntry(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Privilege = (AccessControlEntryPrivilegeEnum)reader.GetUShort(1)!.Value;
                AuthMode = (AccessControlEntryAuthModeEnum)reader.GetUShort(2)!.Value;
                {
                    Subjects = new List<ulong>();
                    foreach (var item in (List<object>)fields[3]) {
                        Subjects.Add(reader.GetULong(-1)!.Value);
                    }
                }
                {
                    Targets = new List<AccessControlTarget>();
                    foreach (var item in (List<object>)fields[4]) {
                        Targets.Add(new AccessControlTarget((object[])item));
                    }
                }
            }
            public required AccessControlEntryPrivilegeEnum Privilege { get; set; }
            public required AccessControlEntryAuthModeEnum AuthMode { get; set; }
            public required List<ulong>? Subjects { get; set; }
            public required List<AccessControlTarget>? Targets { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, (ushort)Privilege);
                writer.WriteUShort(2, (ushort)AuthMode);
                if (Subjects != null)
                {
                    writer.StartList(3);
                    foreach (var item in Subjects) {
                        writer.WriteULong(-1, item);
                    }
                    writer.EndContainer();
                }
                else
                    writer.WriteNull(3);
                if (Targets != null)
                {
                    writer.StartList(4);
                    foreach (var item in Targets) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                else
                    writer.WriteNull(4);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Access Control Extension
        /// </summary>
        public record AccessControlExtension : TLVPayload {
            [SetsRequiredMembers]
            internal AccessControlExtension(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Data = reader.GetBytes(1, false)!;
            }
            public required byte[] Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(1, Data, 128);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Access Control Target
        /// </summary>
        public record AccessControlTarget : TLVPayload {
            [SetsRequiredMembers]
            internal AccessControlTarget(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Cluster = reader.GetUInt(0)!.Value;
                Endpoint = reader.GetUShort(1)!.Value;
                DeviceType = (DeviceTypeEnum)reader.GetUInt(2)!.Value;
            }
            public required uint? Cluster { get; set; }
            public required ushort? Endpoint { get; set; }
            public required DeviceTypeEnum? DeviceType { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, Cluster);
                writer.WriteUShort(1, Endpoint);
                writer.WriteUInt(2, (uint?)DeviceType);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Attributes
        /// <summary>
        /// Get the ACL attribute
        /// </summary>
        public async Task<List<AccessControlEntry>> GetACL(SecureSession session) {
            List<AccessControlEntry> list = new List<AccessControlEntry>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new AccessControlEntry(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Set the ACL attribute
        /// </summary>
        public async Task SetACL (SecureSession session, List<AccessControlEntry> value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Extension attribute
        /// </summary>
        public async Task<List<AccessControlExtension>> GetExtension(SecureSession session) {
            List<AccessControlExtension> list = new List<AccessControlExtension>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new AccessControlExtension(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Set the Extension attribute
        /// </summary>
        public async Task SetExtension (SecureSession session, List<AccessControlExtension> value) {
            await SetAttribute(session, 1, value);
        }

        /// <summary>
        /// Get the Subjects Per Access Control Entry attribute
        /// </summary>
        public async Task<ushort> GetSubjectsPerAccessControlEntry(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 2) ?? 4;
        }

        /// <summary>
        /// Get the Targets Per Access Control Entry attribute
        /// </summary>
        public async Task<ushort> GetTargetsPerAccessControlEntry(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 3) ?? 3;
        }

        /// <summary>
        /// Get the Access Control Entries Per Fabric attribute
        /// </summary>
        public async Task<ushort> GetAccessControlEntriesPerFabric(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 4) ?? 4;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Access Control Cluster";
        }
    }
}