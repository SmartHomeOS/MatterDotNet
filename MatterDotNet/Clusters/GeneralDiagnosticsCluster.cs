// MatterDotNet Copyright (C) 2024 
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
        public GeneralDiagnosticsCluster(ushort endPoint) : base(endPoint) { }

        #region Enums
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
        public record NetworkInterface : TLVPayload {
            public required string Name { get; set; }
            public required bool IsOperational { get; set; }
            public bool? OffPremiseServicesReachableIPv4 { get; set; } = null;
            public bool? OffPremiseServicesReachableIPv6 { get; set; } = null;
            public required byte[] HardwareAddress { get; set; }
            public required List<byte[]> IPv4Addresses { get; set; }
            public required List<byte[]> IPv6Addresses { get; set; }
            public required InterfaceTypeEnum Type { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteString(0, Name);
                writer.WriteBool(1, IsOperational);
                if (OffPremiseServicesReachableIPv4 != null)
                    writer.WriteBool(2, OffPremiseServicesReachableIPv4);
                if (OffPremiseServicesReachableIPv6 != null)
                    writer.WriteBool(3, OffPremiseServicesReachableIPv6);
                writer.WriteBytes(4, HardwareAddress);
                {
                    writer.StartList(5);
                    foreach (var item in IPv4Addresses) {
                    }
                    writer.EndContainer();
                }
                {
                    writer.StartList(6);
                    foreach (var item in IPv6Addresses) {
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
                writer.WriteBytes(0, EnableKey);
                writer.WriteULong(1, EventTrigger);
                writer.EndContainer();
            }
        }

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
                writer.WriteBytes(0, EnableKey);
                writer.WriteByte(1, Value);
                writer.WriteUShort(2, Count);
                writer.EndContainer();
            }
        }

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
            return validateResponse(resp);
        }

        /// <summary>
        /// Time Snapshot
        /// </summary>
        public async Task<TimeSnapshotResponse?> TimeSnapshot(SecureSession session) {
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x01);
            if (!validateResponse(resp))
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
            if (!validateResponse(resp))
                return null;
            return new PayloadTestResponse() {
                Payload = (byte[])GetField(resp, 0),
            };
        }
        #endregion Commands

        #region Attributes
        public List<NetworkInterface> NetworkInterfaces { get; }

        public ushort RebootCount { get; }

        public ulong UpTime { get; }

        public uint TotalOperationalHours { get; }

        public BootReasonEnum BootReason { get; }

        public List<HardwareFaultEnum> ActiveHardwareFaults { get; }

        public List<RadioFaultEnum> ActiveRadioFaults { get; }

        public List<NetworkFaultEnum> ActiveNetworkFaults { get; }

        public bool TestEventTriggersEnabled { get; }
        #endregion Attributes
    }
}