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
using MatterDotNet.Protocol;
using MatterDotNet.Protocol.Parsers;
using MatterDotNet.Protocol.Payloads;
using MatterDotNet.Protocol.Sessions;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.NetworkInformation;

namespace MatterDotNet.Clusters
{
    /// <summary>
    /// General Diagnostics Cluster
    /// </summary>
    public class GeneralDiagnosticsCluster : ClusterBase
    {
        private const uint CLUSTER_ID = 0x0033;

        /// <summary>
        /// General Diagnostics Cluster
        /// </summary>
        public GeneralDiagnosticsCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }

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
        /// Boot Reason
        /// </summary>
        public enum BootReasonEnum {
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
        /// Hardware Fault
        /// </summary>
        public enum HardwareFaultEnum {
            /// <summary>
            /// The Node has encountered an unspecified fault.
            /// </summary>
            Unspecified = 0,
            /// <summary>
            /// The Node has encountered a fault with at least one of its radios.
            /// </summary>
            Radio = 1,
            /// <summary>
            /// The Node has encountered a fault with at least one of its sensors.
            /// </summary>
            Sensor = 2,
            /// <summary>
            /// The Node has encountered an over-temperature fault that is resettable.
            /// </summary>
            ResettableOverTemp = 3,
            /// <summary>
            /// The Node has encountered an over-temperature fault that is not resettable.
            /// </summary>
            NonResettableOverTemp = 4,
            /// <summary>
            /// The Node has encountered a fault with at least one of its power sources.
            /// </summary>
            PowerSource = 5,
            /// <summary>
            /// The Node has encountered a fault with at least one of its visual displays.
            /// </summary>
            VisualDisplayFault = 6,
            /// <summary>
            /// The Node has encountered a fault with at least one of its audio outputs.
            /// </summary>
            AudioOutputFault = 7,
            /// <summary>
            /// The Node has encountered a fault with at least one of its user interfaces.
            /// </summary>
            UserInterfaceFault = 8,
            /// <summary>
            /// The Node has encountered a fault with its non-volatile memory.
            /// </summary>
            NonVolatileMemoryError = 9,
            /// <summary>
            /// The Node has encountered disallowed physical tampering.
            /// </summary>
            TamperDetected = 10,
        }

        /// <summary>
        /// Interface Type
        /// </summary>
        public enum InterfaceTypeEnum {
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

        /// <summary>
        /// Network Fault
        /// </summary>
        public enum NetworkFaultEnum {
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
        /// Radio Fault
        /// </summary>
        public enum RadioFaultEnum {
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
        #endregion Enums

        #region Records
        /// <summary>
        /// Network Interface
        /// </summary>
        public record NetworkInterface : TLVPayload {
            [SetsRequiredMembers]
            internal NetworkInterface(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Name = reader.GetString(0, false)!;
                IsOperational = reader.GetBool(1)!.Value;
                OffPremiseServicesReachableIPv4 = reader.GetBool(2)!.Value;
                OffPremiseServicesReachableIPv6 = reader.GetBool(3)!.Value;
                HardwareAddress = new PhysicalAddress(reader.GetBytes(4, false, 8, 6)!);
                {
                    IPv4Addresses = new List<IPAddress>();
                    foreach (var item in (List<object>)fields[5]) {
                        IPv4Addresses.Add(new IPAddress(reader.GetBytes(-1, false, 4, 4)!));
                    }
                }
                {
                    IPv6Addresses = new List<IPAddress>();
                    foreach (var item in (List<object>)fields[6]) {
                        IPv6Addresses.Add(new IPAddress(reader.GetBytes(-1, false, 16, 16)!));
                    }
                }
                Type = (InterfaceTypeEnum)reader.GetUShort(7)!.Value;
            }
            public required string Name { get; set; }
            public required bool IsOperational { get; set; }
            public required bool? OffPremiseServicesReachableIPv4 { get; set; } = null;
            public required bool? OffPremiseServicesReachableIPv6 { get; set; } = null;
            public required PhysicalAddress HardwareAddress { get; set; }
            public required List<IPAddress> IPv4Addresses { get; set; }
            public required List<IPAddress> IPv6Addresses { get; set; }
            public required InterfaceTypeEnum Type { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Name, 32);
                writer.WriteBool(1, IsOperational);
                writer.WriteBool(2, OffPremiseServicesReachableIPv4);
                writer.WriteBool(3, OffPremiseServicesReachableIPv6);
                writer.WriteBytes(4, HardwareAddress.GetAddressBytes());
                {
                    Constrain(IPv4Addresses, 0, 4);
                    writer.StartList(5);
                    foreach (var item in IPv4Addresses) {
                        writer.WriteBytes(-1, item.GetAddressBytes());
                    }
                    writer.EndContainer();
                }
                {
                    Constrain(IPv6Addresses, 0, 8);
                    writer.StartList(6);
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
            public required ulong SystemTimeMs { get; set; }
            public required ulong? PosixTimeMs { get; set; } = null;
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
        public async Task<bool> TestEventTrigger(SecureSession session, byte[] EnableKey, ulong EventTrigger) {
            TestEventTriggerPayload requestFields = new TestEventTriggerPayload() {
                EnableKey = EnableKey,
                EventTrigger = EventTrigger,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            return ValidateResponse(resp);
        }

        /// <summary>
        /// Time Snapshot
        /// </summary>
        public async Task<TimeSnapshotResponse?> TimeSnapshot(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x01);
            if (!ValidateResponse(resp))
                return null;
            return new TimeSnapshotResponse() {
                SystemTimeMs = (ulong)GetField(resp, 0),
                PosixTimeMs = (ulong)GetField(resp, 1),
            };
        }

        /// <summary>
        /// Payload Test Request
        /// </summary>
        public async Task<PayloadTestResponse?> PayloadTestRequest(SecureSession session, byte[] EnableKey, byte Value, ushort Count) {
            PayloadTestRequestPayload requestFields = new PayloadTestRequestPayload() {
                EnableKey = EnableKey,
                Value = Value,
                Count = Count,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x03, requestFields);
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
        public async Task<List<NetworkInterface>> GetNetworkInterfaces (SecureSession session) {
            return (List<NetworkInterface>)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Reboot Count attribute
        /// </summary>
        public async Task<ushort> GetRebootCount (SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 1))!;
        }

        /// <summary>
        /// Get the Up Time attribute
        /// </summary>
        public async Task<ulong> GetUpTime (SecureSession session) {
            return (ulong)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Total Operational Hours attribute
        /// </summary>
        public async Task<uint> GetTotalOperationalHours (SecureSession session) {
            return (uint)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Boot Reason attribute
        /// </summary>
        public async Task<BootReasonEnum> GetBootReason (SecureSession session) {
            return (BootReasonEnum)await GetEnumAttribute(session, 4);
        }

        /// <summary>
        /// Get the Active Hardware Faults attribute
        /// </summary>
        public async Task<List<HardwareFaultEnum>> GetActiveHardwareFaults (SecureSession session) {
            return (List<HardwareFaultEnum>)(dynamic?)(await GetAttribute(session, 5))!;
        }

        /// <summary>
        /// Get the Active Radio Faults attribute
        /// </summary>
        public async Task<List<RadioFaultEnum>> GetActiveRadioFaults (SecureSession session) {
            return (List<RadioFaultEnum>)(dynamic?)(await GetAttribute(session, 6))!;
        }

        /// <summary>
        /// Get the Active Network Faults attribute
        /// </summary>
        public async Task<List<NetworkFaultEnum>> GetActiveNetworkFaults (SecureSession session) {
            return (List<NetworkFaultEnum>)(dynamic?)(await GetAttribute(session, 7))!;
        }

        /// <summary>
        /// Get the Test Event Triggers Enabled attribute
        /// </summary>
        public async Task<bool> GetTestEventTriggersEnabled (SecureSession session) {
            return (bool)(dynamic?)(await GetAttribute(session, 8))!;
        }
        #endregion Attributes
    }
}