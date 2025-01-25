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
using System.Net;
using System.Net.NetworkInformation;

namespace MatterDotNet.Clusters.General
{
    /// <summary>
    /// The General Diagnostics Cluster, along with other diagnostics clusters, provide a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class GeneralDiagnostics : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0033;

        /// <summary>
        /// The General Diagnostics Cluster, along with other diagnostics clusters, provide a means to acquire standardized diagnostics metrics that MAY be used by a Node to assist a user or Administrative Node in diagnosing potential problems.
        /// </summary>
        [SetsRequiredMembers]
        public GeneralDiagnostics(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected GeneralDiagnostics(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            NetworkInterfaces = new ReadAttribute<NetworkInterface[]>(cluster, endPoint, 0) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    NetworkInterface[] list = new NetworkInterface[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new NetworkInterface(reader.GetStruct(i)!);
                    return list;
                }
            };
            RebootCount = new ReadAttribute<ushort>(cluster, endPoint, 1) {
                Deserialize = x => (ushort?)(dynamic?)x ?? 0x0000

            };
            UpTime = new ReadAttribute<ulong>(cluster, endPoint, 2) {
                Deserialize = x => (ulong?)(dynamic?)x ?? 0x0000000000000000

            };
            TotalOperationalHours = new ReadAttribute<uint>(cluster, endPoint, 3) {
                Deserialize = x => (uint?)(dynamic?)x ?? 0x00000000

            };
            BootReason = new ReadAttribute<BootReasonEnum>(cluster, endPoint, 4) {
                Deserialize = x => (BootReasonEnum)DeserializeEnum(x)!
            };
            ActiveHardwareFaults = new ReadAttribute<HardwareFault[]>(cluster, endPoint, 5) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    HardwareFault[] list = new HardwareFault[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (HardwareFault)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            ActiveRadioFaults = new ReadAttribute<RadioFault[]>(cluster, endPoint, 6) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    RadioFault[] list = new RadioFault[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (RadioFault)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            ActiveNetworkFaults = new ReadAttribute<NetworkFault[]>(cluster, endPoint, 7) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    NetworkFault[] list = new NetworkFault[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (NetworkFault)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            TestEventTriggersEnabled = new ReadAttribute<bool>(cluster, endPoint, 8) {
                Deserialize = x => (bool)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Support specific testing needs for extended Data Model features
            /// </summary>
            DataModelTest = 1,
        }

        /// <summary>
        /// Hardware Fault
        /// </summary>
        public enum HardwareFault : byte {
            /// <summary>
            /// The Node has encountered an unspecified fault.
            /// </summary>
            Unspecified = 0x0,
            /// <summary>
            /// The Node has encountered a fault with at least one of its radios.
            /// </summary>
            Radio = 0x1,
            /// <summary>
            /// The Node has encountered a fault with at least one of its sensors.
            /// </summary>
            Sensor = 0x2,
            /// <summary>
            /// The Node has encountered an over-temperature fault that is resettable.
            /// </summary>
            ResettableOverTemp = 0x3,
            /// <summary>
            /// The Node has encountered an over-temperature fault that is not resettable.
            /// </summary>
            NonResettableOverTemp = 0x4,
            /// <summary>
            /// The Node has encountered a fault with at least one of its power sources.
            /// </summary>
            PowerSource = 0x5,
            /// <summary>
            /// The Node has encountered a fault with at least one of its visual displays.
            /// </summary>
            VisualDisplayFault = 0x6,
            /// <summary>
            /// The Node has encountered a fault with at least one of its audio outputs.
            /// </summary>
            AudioOutputFault = 0x7,
            /// <summary>
            /// The Node has encountered a fault with at least one of its user interfaces.
            /// </summary>
            UserInterfaceFault = 0x8,
            /// <summary>
            /// The Node has encountered a fault with its non-volatile memory.
            /// </summary>
            NonVolatileMemoryError = 0x9,
            /// <summary>
            /// The Node has encountered disallowed physical tampering.
            /// </summary>
            TamperDetected = 0xA,
        }

        /// <summary>
        /// Radio Fault
        /// </summary>
        public enum RadioFault : byte {
            /// <summary>
            /// The Node has encountered an unspecified radio fault.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The Node has encountered a fault with its Wi-Fi radio.
            /// </summary>
            WiFiFault = 1,
            /// <summary>
            /// The Node has encountered a fault with its cellular radio.
            /// </summary>
            CellularFault = 2,
            /// <summary>
            /// The Node has encountered a fault with its 802.15.4 radio.
            /// </summary>
            ThreadFault = 3,
            /// <summary>
            /// The Node has encountered a fault with its NFC radio.
            /// </summary>
            NFCFault = 4,
            /// <summary>
            /// The Node has encountered a fault with its BLE radio.
            /// </summary>
            BLEFault = 5,
            /// <summary>
            /// The Node has encountered a fault with its Ethernet controller.
            /// </summary>
            EthernetFault = 6,
        }

        /// <summary>
        /// Network Fault
        /// </summary>
        public enum NetworkFault : byte {
            /// <summary>
            /// The Node has encountered an unspecified fault.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The Node has encountered a network fault as a result of a hardware failure.
            /// </summary>
            HardwareFailure = 1,
            /// <summary>
            /// The Node has encountered a network fault as a result of a jammed network.
            /// </summary>
            NetworkJammed = 2,
            /// <summary>
            /// The Node has encountered a network fault as a result of a failure to establish a connection.
            /// </summary>
            ConnectionFailed = 3,
        }

        /// <summary>
        /// Boot Reason
        /// </summary>
        public enum BootReasonEnum : byte {
            /// <summary>
            /// The Node is unable to identify the Power-On reason as one of the other provided enumeration values.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The Node has booted as the result of physical interaction with the device resulting in a reboot.
            /// </summary>
            PowerOnReboot = 1,
            /// <summary>
            /// The Node has rebooted as the result of a brown-out of the Node's power supply.
            /// </summary>
            BrownOutReset = 2,
            /// <summary>
            /// The Node has rebooted as the result of a software watchdog timer.
            /// </summary>
            SoftwareWatchdogReset = 3,
            /// <summary>
            /// The Node has rebooted as the result of a hardware watchdog timer.
            /// </summary>
            HardwareWatchdogReset = 4,
            /// <summary>
            /// The Node has rebooted as the result of a completed software update.
            /// </summary>
            SoftwareUpdateCompleted = 5,
            /// <summary>
            /// The Node has rebooted as the result of a software initiated reboot.
            /// </summary>
            SoftwareReset = 6,
        }

        /// <summary>
        /// Interface Type
        /// </summary>
        public enum InterfaceType : byte {
            /// <summary>
            /// Indicates an interface of an unspecified type.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// Indicates a Wi-Fi interface.
            /// </summary>
            WiFi = 1,
            /// <summary>
            /// Indicates a Ethernet interface.
            /// </summary>
            Ethernet = 2,
            /// <summary>
            /// Indicates a Cellular interface.
            /// </summary>
            Cellular = 3,
            /// <summary>
            /// Indicates a Thread interface.
            /// </summary>
            Thread = 4,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Network Interface
        /// </summary>
        public record NetworkInterface : TLVPayload {
            /// <summary>
            /// Network Interface
            /// </summary>
            public NetworkInterface() { }

            /// <summary>
            /// Network Interface
            /// </summary>
            [SetsRequiredMembers]
            public NetworkInterface(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Name = reader.GetString(0, false, 32)!;
                IsOperational = reader.GetBool(1)!.Value;
                OffPremiseServicesReachableIPv4 = reader.GetBool(2, true);
                OffPremiseServicesReachableIPv6 = reader.GetBool(3, true);
                HardwareAddress = new PhysicalAddress(reader.GetBytes(4, false, 8, 6)!);
                {
                    IPv4Addresses = new IPAddress[reader.GetStruct(5)!.Length];
                    for (int n = 0; n < IPv4Addresses.Length; n++) {
                        IPv4Addresses[n] = new IPAddress(reader.GetBytes(n, false, 4, 4)!);;
                    }
                }
                {
                    IPv6Addresses = new IPAddress[reader.GetStruct(6)!.Length];
                    for (int n = 0; n < IPv6Addresses.Length; n++) {
                        IPv6Addresses[n] = new IPAddress(reader.GetBytes(n, false, 16, 16)!);;
                    }
                }
                Type = (InterfaceType)reader.GetUShort(7)!.Value;
            }
            public required string Name { get; set; }
            public required bool IsOperational { get; set; }
            public required bool? OffPremiseServicesReachableIPv4 { get; set; }
            public required bool? OffPremiseServicesReachableIPv6 { get; set; }
            public required PhysicalAddress HardwareAddress { get; set; }
            public required IPAddress[] IPv4Addresses { get; set; }
            public required IPAddress[] IPv6Addresses { get; set; }
            public required InterfaceType Type { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Name, 32);
                writer.WriteBool(1, IsOperational);
                writer.WriteBool(2, OffPremiseServicesReachableIPv4);
                writer.WriteBool(3, OffPremiseServicesReachableIPv6);
                writer.WriteBytes(4, HardwareAddress.GetAddressBytes());
                {
                    writer.StartArray(5);
                    foreach (var item in IPv4Addresses) {
                        writer.WriteBytes(-1, item.GetAddressBytes());
                    }
                    writer.EndContainer();
                }
                {
                    writer.StartArray(6);
                    foreach (var item in IPv6Addresses) {
                        writer.WriteBytes(-1, item.GetAddressBytes());
                    }
                    writer.EndContainer();
                }
                writer.WriteUShort(7, (ushort)Type);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record TestEventTriggerPayload : TLVPayload {
            public required byte[] EnableKey { get; set; }
            public required ulong EventTrigger { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, EnableKey, 16);
                writer.WriteULong(1, EventTrigger);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Time Snapshot Response - Reply from server
        /// </summary>
        public struct TimeSnapshotResponse() {
            public required TimeSpan SystemTimeMs { get; set; }
            public required DateTimeOffset? PosixTimeMs { get; set; }
        }

        private record PayloadTestRequestPayload : TLVPayload {
            public required byte[] EnableKey { get; set; }
            public required byte Value { get; set; }
            public required ushort Count { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, EnableKey, 16);
                writer.WriteByte(1, Value);
                writer.WriteUShort(2, Count, 2048);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Payload Test Response - Reply from server
        /// </summary>
        public struct PayloadTestResponse() {
            public required byte[] Payload { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Test Event Trigger
        /// </summary>
        public async Task<bool> TestEventTrigger(SecureSession session, byte[] enableKey, ulong eventTrigger) {
            TestEventTriggerPayload requestFields = new TestEventTriggerPayload() {
                EnableKey = enableKey,
                EventTrigger = eventTrigger,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Time Snapshot
        /// </summary>
        public async Task<TimeSnapshotResponse?> TimeSnapshot(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x01);
            if (!ValidateResponse(resp))
                return null;
            return new TimeSnapshotResponse() {
                SystemTimeMs = (TimeSpan.FromMilliseconds((ulong)GetField(resp, 0))),
                PosixTimeMs = GetField(resp, 1) != null ? DateTimeOffset.FromUnixTimeMilliseconds((long)(ulong)GetField(resp, 1)) : null,
            };
        }

        /// <summary>
        /// Payload Test Request
        /// </summary>
        public async Task<PayloadTestResponse?> PayloadTestRequest(SecureSession session, byte[] enableKey, byte value, ushort count) {
            PayloadTestRequestPayload requestFields = new PayloadTestRequestPayload() {
                EnableKey = enableKey,
                Value = value,
                Count = count,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new PayloadTestResponse() {
                Payload = (byte[])GetField(resp, 0),
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
        /// Network Interfaces Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<NetworkInterface[]> NetworkInterfaces { get; init; }

        /// <summary>
        /// Reboot Count Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> RebootCount { get; init; }

        /// <summary>
        /// Up Time Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ulong> UpTime { get; init; }

        /// <summary>
        /// Total Operational Hours Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<uint> TotalOperationalHours { get; init; }

        /// <summary>
        /// Boot Reason Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<BootReasonEnum> BootReason { get; init; }

        /// <summary>
        /// Active Hardware Faults Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<HardwareFault[]> ActiveHardwareFaults { get; init; }

        /// <summary>
        /// Active Radio Faults Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<RadioFault[]> ActiveRadioFaults { get; init; }

        /// <summary>
        /// Active Network Faults Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<NetworkFault[]> ActiveNetworkFaults { get; init; }

        /// <summary>
        /// Test Event Triggers Enabled Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<bool> TestEventTriggersEnabled { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "General Diagnostics";
        }
    }
}