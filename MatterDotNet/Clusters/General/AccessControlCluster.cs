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

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The Access Control Cluster exposes a data model view of a Node's Access Control List (ACL), which codifies the rules used to manage and enforce Access Control for the Node's endpoints and their associated cluster instances.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class AccessControl : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x001F;

        /// <summary>
        /// The Access Control Cluster exposes a data model view of a Node's Access Control List (ACL), which codifies the rules used to manage and enforce Access Control for the Node's endpoints and their associated cluster instances.
        /// </summary>
        public AccessControl(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected AccessControl(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Device provides ACL Extension attribute
            /// </summary>
            Extension = 1,
            /// <summary>
            /// Device is managed
            /// </summary>
            ManagedDevice = 2,
        }

        /// <summary>
        /// Access Control Entry Privilege
        /// </summary>
        public enum AccessControlEntryPrivilege : byte {
            /// <summary>
            /// Can read and observe all (except Access Control Cluster and as seen by a non-Proxy)
            /// </summary>
            View = 1,
            /// <summary>
            /// Can read and observe all (as seen by a Proxy)
            /// </summary>
            ProxyView = 2,
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
        /// Access Control Entry Auth Mode
        /// </summary>
        public enum AccessControlEntryAuthMode : byte {
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
        /// Access Restriction Type
        /// </summary>
        public enum AccessRestrictionType : byte {
            /// <summary>
            /// Clients on this fabric are currently forbidden from reading and writing an attribute
            /// </summary>
            AttributeAccessForbidden = 0,
            /// <summary>
            /// Clients on this fabric are currently forbidden from writing an attribute
            /// </summary>
            AttributeWriteForbidden = 1,
            /// <summary>
            /// Clients on this fabric are currently forbidden from invoking a command
            /// </summary>
            CommandForbidden = 2,
            /// <summary>
            /// Clients on this fabric are currently forbidden from reading an event
            /// </summary>
            EventForbidden = 3,
        }

        /// <summary>
        /// Change Type
        /// </summary>
        public enum ChangeType : byte {
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
        /// Access Control Target
        /// </summary>
        public record AccessControlTarget : TLVPayload {
            /// <summary>
            /// Access Control Target
            /// </summary>
            public AccessControlTarget() { }

            /// <summary>
            /// Access Control Target
            /// </summary>
            [SetsRequiredMembers]
            public AccessControlTarget(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Cluster = reader.GetUInt(0, true);
                Endpoint = reader.GetUShort(1, true);
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

        /// <summary>
        /// Access Control Entry
        /// </summary>
        public record AccessControlEntry : TLVPayload {
            /// <summary>
            /// Access Control Entry
            /// </summary>
            public AccessControlEntry() { }

            /// <summary>
            /// Access Control Entry
            /// </summary>
            [SetsRequiredMembers]
            public AccessControlEntry(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Privilege = (AccessControlEntryPrivilege)reader.GetUShort(1)!.Value;
                AuthMode = (AccessControlEntryAuthMode)reader.GetUShort(2)!.Value;
                {
                    Subjects = new ulong[reader.GetStruct(3)!.Length];
                    for (int n = 0; n < Subjects.Length; n++) {
                        Subjects[n] = reader.GetULong(n)!.Value;
                    }
                }
                {
                    Targets = new AccessControlTarget[reader.GetStruct(4)!.Length];
                    for (int n = 0; n < Targets.Length; n++) {
                        Targets[n] = new AccessControlTarget((object[])((object[])fields[4])[n]);
                    }
                }
            }
            public required AccessControlEntryPrivilege Privilege { get; set; }
            public required AccessControlEntryAuthMode AuthMode { get; set; }
            public required ulong[]? Subjects { get; set; }
            public required AccessControlTarget[]? Targets { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(1, (ushort)Privilege);
                writer.WriteUShort(2, (ushort)AuthMode);
                if (Subjects != null)
                {
                    writer.StartArray(3);
                    foreach (var item in Subjects) {
                        writer.WriteULong(-1, item);
                    }
                    writer.EndContainer();
                }
                else
                    writer.WriteNull(3);
                if (Targets != null)
                {
                    writer.StartArray(4);
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
            /// <summary>
            /// Access Control Extension
            /// </summary>
            public AccessControlExtension() { }

            /// <summary>
            /// Access Control Extension
            /// </summary>
            [SetsRequiredMembers]
            public AccessControlExtension(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Data = reader.GetBytes(1, false, 128)!;
            }
            public required byte[] Data { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(1, Data, 128);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Access Restriction Entry
        /// </summary>
        public record AccessRestrictionEntry : TLVPayload {
            /// <summary>
            /// Access Restriction Entry
            /// </summary>
            public AccessRestrictionEntry() { }

            /// <summary>
            /// Access Restriction Entry
            /// </summary>
            [SetsRequiredMembers]
            public AccessRestrictionEntry(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Endpoint = reader.GetUShort(0)!.Value;
                Cluster = reader.GetUInt(1)!.Value;
                {
                    Restrictions = new AccessRestriction[reader.GetStruct(2)!.Length];
                    for (int n = 0; n < Restrictions.Length; n++) {
                        Restrictions[n] = new AccessRestriction((object[])((object[])fields[2])[n]);
                    }
                }
            }
            public required ushort Endpoint { get; set; }
            public required uint Cluster { get; set; }
            public required AccessRestriction[] Restrictions { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, Endpoint);
                writer.WriteUInt(1, Cluster);
                {
                    writer.StartArray(2);
                    foreach (var item in Restrictions) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Access Restriction
        /// </summary>
        public record AccessRestriction : TLVPayload {
            /// <summary>
            /// Access Restriction
            /// </summary>
            public AccessRestriction() { }

            /// <summary>
            /// Access Restriction
            /// </summary>
            [SetsRequiredMembers]
            public AccessRestriction(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Type = (AccessRestrictionType)reader.GetUShort(0)!.Value;
                ID = reader.GetUInt(1, true);
            }
            public required AccessRestrictionType Type { get; set; }
            public required uint? ID { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Type);
                writer.WriteUInt(1, ID);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Commissioning Access Restriction Entry
        /// </summary>
        public record CommissioningAccessRestrictionEntry : TLVPayload {
            /// <summary>
            /// Commissioning Access Restriction Entry
            /// </summary>
            public CommissioningAccessRestrictionEntry() { }

            /// <summary>
            /// Commissioning Access Restriction Entry
            /// </summary>
            [SetsRequiredMembers]
            public CommissioningAccessRestrictionEntry(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Endpoint = reader.GetUShort(0)!.Value;
                Cluster = reader.GetUInt(1)!.Value;
                {
                    Restrictions = new AccessRestriction[reader.GetStruct(2)!.Length];
                    for (int n = 0; n < Restrictions.Length; n++) {
                        Restrictions[n] = new AccessRestriction((object[])((object[])fields[2])[n]);
                    }
                }
            }
            public required ushort Endpoint { get; set; }
            public required uint Cluster { get; set; }
            public required AccessRestriction[] Restrictions { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, Endpoint);
                writer.WriteUInt(1, Cluster);
                {
                    writer.StartArray(2);
                    foreach (var item in Restrictions) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record ReviewFabricRestrictionsPayload : TLVPayload {
            public required CommissioningAccessRestrictionEntry[] ARL { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                {
                    writer.StartArray(0);
                    foreach (var item in ARL) {
                        item.Serialize(writer, -1);
                    }
                    writer.EndContainer();
                }
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Review Fabric Restrictions Response - Reply from server
        /// </summary>
        public struct ReviewFabricRestrictionsResponse() {
            public required ulong Token { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Review Fabric Restrictions
        /// </summary>
        public async Task<ReviewFabricRestrictionsResponse?> ReviewFabricRestrictions(SecureSession session, CommissioningAccessRestrictionEntry[] aRL) {
            ReviewFabricRestrictionsPayload requestFields = new ReviewFabricRestrictionsPayload() {
                ARL = aRL,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ReviewFabricRestrictionsResponse() {
                Token = (ulong)GetField(resp, 0),
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
        /// Get the ACL attribute
        /// </summary>
        public async Task<AccessControlEntry[]> GetACL(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            AccessControlEntry[] list = new AccessControlEntry[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new AccessControlEntry(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the ACL attribute
        /// </summary>
        public async Task SetACL (SecureSession session, AccessControlEntry[] value) {
            await SetAttribute(session, 0, value);
        }

        /// <summary>
        /// Get the Extension attribute
        /// </summary>
        public async Task<AccessControlExtension[]> GetExtension(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            AccessControlExtension[] list = new AccessControlExtension[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new AccessControlExtension(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Set the Extension attribute
        /// </summary>
        public async Task SetExtension (SecureSession session, AccessControlExtension[] value) {
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

        /// <summary>
        /// Get the Commissioning ARL attribute
        /// </summary>
        public async Task<CommissioningAccessRestrictionEntry[]> GetCommissioningARL(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 5))!);
            CommissioningAccessRestrictionEntry[] list = new CommissioningAccessRestrictionEntry[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new CommissioningAccessRestrictionEntry(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the ARL attribute
        /// </summary>
        public async Task<AccessRestrictionEntry[]> GetARL(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 6))!);
            AccessRestrictionEntry[] list = new AccessRestrictionEntry[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new AccessRestrictionEntry(reader.GetStruct(i)!);
            return list;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Access Control";
        }
    }
}