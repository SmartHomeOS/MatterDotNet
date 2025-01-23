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
        public GeneralDiagnostics(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected GeneralDiagnostics(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
            Unspecified = 0x00,
            /// <summary>
            /// The Node has encountered a fault with at least one of its radios.
            /// </summary>
            Radio = 0x01,
            /// <summary>
            /// The Node has encountered a fault with at least one of its sensors.
            /// </summary>
            Sensor = 0x02,
            /// <summary>
            /// The Node has encountered an over-temperature fault that is resettable.
            /// </summary>
            ResettableOverTemp = 0x03,
            /// <summary>
            /// The Node has encountered an over-temperature fault that is not resettable.
            /// </summary>
            NonResettableOverTemp = 0x04,
            /// <summary>
            /// The Node has encountered a fault with at least one of its power sources.
            /// </summary>
            PowerSource = 0x05,
            /// <summary>
            /// The Node has encountered a fault with at least one of its visual displays.
            /// </summary>
            VisualDisplayFault = 0x06,
            /// <summary>
            /// The Node has encountered a fault with at least one of its audio outputs.
            /// </summary>
            AudioOutputFault = 0x07,
            /// <summary>
            /// The Node has encountered a fault with at least one of its user interfaces.
            /// </summary>
            UserInterfaceFault = 0x08,
            /// <summary>
            /// The Node has encountered a fault with its non-volatile memory.
            /// </summary>
            NonVolatileMemoryError = 0x09,
            /// <summary>
            /// The Node has encountered disallowed physical tampering.
            /// </summary>
            TamperDetected = 0x0A,
        }

        /// <summary>
        /// Radio Fault
        /// </summary>
        public enum RadioFault : byte {
            /// <summary>
            /// The Node has encountered an unspecified radio fault.
            /// </summary>
            Unspecified = 0x00,
            /// <summary>
            /// The Node has encountered a fault with its Wi-Fi radio.
            /// </summary>
            WiFiFault = 0x01,
            /// <summary>
            /// The Node has encountered a fault with its cellular radio.
            /// </summary>
            CellularFault = 0x02,
            /// <summary>
            /// The Node has encountered a fault with its 802.15.4 radio.
            /// </summary>
            ThreadFault = 0x03,
            /// <summary>
            /// The Node has encountered a fault with its NFC radio.
            /// </summary>
            NFCFault = 0x04,
            /// <summary>
            /// The Node has encountered a fault with its BLE radio.
            /// </summary>
            BLEFault = 0x05,
            /// <summary>
            /// The Node has encountered a fault with its Ethernet controller.
            /// </summary>
            EthernetFault = 0x06,
        }

        /// <summary>
        /// Network Fault
        /// </summary>
        public enum NetworkFault : byte {
            /// <summary>
            /// The Node has encountered an unspecified fault.
            /// </summary>
            Unspecified = 0x00,
            /// <summary>
            /// The Node has encountered a network fault as a result of a hardware failure.
            /// </summary>
            HardwareFailure = 0x01,
            /// <summary>
            /// The Node has encountered a network fault as a result of a jammed network.
            /// </summary>
            NetworkJammed = 0x02,
            /// <summary>
            /// The Node has encountered a network fault as a result of a failure to establish a connection.
            /// </summary>
            ConnectionFailed = 0x03,
        }

        /// <summary>
        /// Boot Reason
        /// </summary>
        public enum BootReason : byte {
            /// <summary>
            /// The Node is unable to identify the Power-On reason as one of the other provided enumeration values.
            /// </summary>
            Unspecified = 0x00,
            /// <summary>
            /// The Node has booted as the result of physical interaction with the device resulting in a reboot.
            /// </summary>
            PowerOnReboot = 0x01,
            /// <summary>
            /// The Node has rebooted as the result of a brown-out of the Node's power supply.
            /// </summary>
            BrownOutReset = 0x02,
            /// <summary>
            /// The Node has rebooted as the result of a software watchdog timer.
            /// </summary>
            SoftwareWatchdogReset = 0x03,
            /// <summary>
            /// The Node has rebooted as the result of a hardware watchdog timer.
            /// </summary>
            HardwareWatchdogReset = 0x04,
            /// <summary>
            /// The Node has rebooted as the result of a completed software update.
            /// </summary>
            SoftwareUpdateCompleted = 0x05,
            /// <summary>
            /// The Node has rebooted as the result of a software initiated reboot.
            /// </summary>
            SoftwareReset = 0x06,
        }

        /// <summary>
        /// Interface Type
        /// </summary>
        public enum InterfaceType : byte {
            /// <summary>
            /// Indicates an interface of an unspecified type.
            /// </summary>
            Unspecified = 0x00,
            /// <summary>
            /// Indicates a Wi-Fi interface.
            /// </summary>
            WiFi = 0x01,
            /// <summary>
            /// Indicates a Ethernet interface.
            /// </summary>
            Ethernet = 0x02,
            /// <summary>
            /// Indicates a Cellular interface.
            /// </summary>
            Cellular = 0x03,
            /// <summary>
            /// Indicates a Thread interface.
            /// </summary>
            Thread = 0x04,
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
        /// Get the Network Interfaces attribute
        /// </summary>
        public async Task<NetworkInterface[]> GetNetworkInterfaces(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 0))!);
            NetworkInterface[] list = new NetworkInterface[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = new NetworkInterface(reader.GetStruct(i)!);
            return list;
        }

        /// <summary>
        /// Get the Reboot Count attribute
        /// </summary>
        public async Task<ushort> GetRebootCount(SecureSession session) {
            return (ushort?)(dynamic?)await GetAttribute(session, 1) ?? 0x0000;
        }

        /// <summary>
        /// Get the Up Time attribute
        /// </summary>
        public async Task<ulong> GetUpTime(SecureSession session) {
            return (ulong?)(dynamic?)await GetAttribute(session, 2) ?? 0x0000000000000000;
        }

        /// <summary>
        /// Get the Total Operational Hours attribute
        /// </summary>
        public async Task<uint> GetTotalOperationalHours(SecureSession session) {
            return (uint?)(dynamic?)await GetAttribute(session, 3) ?? 0x00000000;
        }

        /// <summary>
        /// Get the Boot Reason attribute
        /// </summary>
        public async Task<BootReason> GetBootReason(SecureSession session) {
            return (BootReason)await GetEnumAttribute(session, 4);
        }

        /// <summary>
        /// Get the Active Hardware Faults attribute
        /// </summary>
        public async Task<HardwareFault[]> GetActiveHardwareFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 5))!);
            HardwareFault[] list = new HardwareFault[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (HardwareFault)reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Active Radio Faults attribute
        /// </summary>
        public async Task<RadioFault[]> GetActiveRadioFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 6))!);
            RadioFault[] list = new RadioFault[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (RadioFault)reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Active Network Faults attribute
        /// </summary>
        public async Task<NetworkFault[]> GetActiveNetworkFaults(SecureSession session) {
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 7))!);
            NetworkFault[] list = new NetworkFault[reader.Count];
            for (int i = 0; i < reader.Count; i++)
                list[i] = (NetworkFault)reader.GetUShort(i)!.Value;
            return list;
        }

        /// <summary>
        /// Get the Test Event Triggers Enabled attribute
        /// </summary>
        public async Task<bool> GetTestEventTriggersEnabled(SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 8))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "General Diagnostics";
        }
    }
}