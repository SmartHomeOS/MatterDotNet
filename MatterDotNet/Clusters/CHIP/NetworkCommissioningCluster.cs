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
using System.Net.NetworkInformation;

namespace MatterDotNet.Clusters.CHIP
{
    /// <summary>
    /// Functionality to configure, enable, disable network credentials and access on a Matter device.
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 1)]
    public class NetworkCommissioning : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0031;

        /// <summary>
        /// Functionality to configure, enable, disable network credentials and access on a Matter device.
        /// </summary>
        [SetsRequiredMembers]
        public NetworkCommissioning(ushort endPoint) : this(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        [SetsRequiredMembers]
        protected NetworkCommissioning(uint cluster, ushort endPoint) : base(cluster, endPoint) {
            MaxNetworks = new ReadAttribute<byte>(cluster, endPoint, 0) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            Networks = new ReadAttribute<NetworkInfo[]>(cluster, endPoint, 1) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    NetworkInfo[] list = new NetworkInfo[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = new NetworkInfo(reader.GetStruct(i)!);
                    return list;
                }
            };
            ScanMaxTimeSeconds = new ReadAttribute<byte>(cluster, endPoint, 2) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            ConnectMaxTimeSeconds = new ReadAttribute<byte>(cluster, endPoint, 3) {
                Deserialize = x => (byte)(dynamic?)x!
            };
            InterfaceEnabled = new ReadWriteAttribute<bool>(cluster, endPoint, 4) {
                Deserialize = x => (bool)(dynamic?)x!
            };
            LastNetworkingStatus = new ReadAttribute<NetworkCommissioningStatus?>(cluster, endPoint, 5, true) {
                Deserialize = x => (NetworkCommissioningStatus?)DeserializeEnum(x)
            };
            LastNetworkID = new ReadAttribute<byte[]?>(cluster, endPoint, 6, true) {
                Deserialize = x => (byte[]?)(dynamic?)x
            };
            LastConnectErrorValue = new ReadAttribute<int?>(cluster, endPoint, 7, true) {
                Deserialize = x => (int?)(dynamic?)x
            };
            SupportedWiFiBands = new ReadAttribute<WiFiBand[]>(cluster, endPoint, 8) {
                Deserialize = x => {
                    FieldReader reader = new FieldReader((IList<object>)x!);
                    WiFiBand[] list = new WiFiBand[reader.Count];
                    for (int i = 0; i < reader.Count; i++)
                        list[i] = (WiFiBand)reader.GetUShort(i)!.Value;
                    return list;
                }
            };
            SupportedThreadFeatures = new ReadAttribute<ThreadCapabilities>(cluster, endPoint, 9) {
                Deserialize = x => (ThreadCapabilities)DeserializeEnum(x)!
            };
            ThreadVersion = new ReadAttribute<ushort>(cluster, endPoint, 10) {
                Deserialize = x => (ushort)(dynamic?)x!
            };
        }

        #region Enums
        /// <summary>
        /// Supported Features
        /// </summary>
        [Flags]
        public enum Feature {
            /// <summary>
            /// Wi-Fi related features
            /// </summary>
            WiFiNetworkInterface = 1,
            /// <summary>
            /// Thread related features
            /// </summary>
            ThreadNetworkInterface = 2,
            /// <summary>
            /// Ethernet related features
            /// </summary>
            EthernetNetworkInterface = 4,
            /// <summary>
            /// Device related features
            /// </summary>
            PerDeviceCredentials = 8,
        }

        /// <summary>
        /// Network Commissioning Status
        /// </summary>
        public enum NetworkCommissioningStatus : byte {
            /// <summary>
            /// OK, no error
            /// </summary>
            Success = 0x0,
            /// <summary>
            /// <see cref="OutOfRange"/> Value Outside Range
            /// </summary>
            OutOfRange = 0x1,
            /// <summary>
            /// <see cref="BoundsExceeded"/> A collection would exceed its size limit
            /// </summary>
            BoundsExceeded = 0x2,
            /// <summary>
            /// <see cref="NetworkIdNotFound"/> The NetworkID is not among the collection of added networks
            /// </summary>
            NetworkIDNotFound = 0x3,
            /// <summary>
            /// <see cref="DuplicateNetworkId"/> The NetworkID is already among the collection of added networks
            /// </summary>
            DuplicateNetworkID = 0x4,
            /// <summary>
            /// <see cref="NetworkNotFound"/> Cannot find AP: SSID Not found
            /// </summary>
            NetworkNotFound = 0x5,
            /// <summary>
            /// <see cref="RegulatoryError"/> Cannot find AP: Mismatch on band/channels/regulatory domain / 2.4GHz vs 5GHz
            /// </summary>
            RegulatoryError = 0x6,
            /// <summary>
            /// <see cref="AuthFailure"/> Cannot associate due to authentication failure
            /// </summary>
            AuthFailure = 0x7,
            /// <summary>
            /// <see cref="UnsupportedSecurity"/> Cannot associate due to unsupported security mode
            /// </summary>
            UnsupportedSecurity = 0x8,
            /// <summary>
            /// <see cref="OtherConnectionFailure"/> Other association failure
            /// </summary>
            OtherConnectionFailure = 0x9,
            /// <summary>
            /// <see cref="Ipv6Failed"/> Failure to generate an IPv6 address
            /// </summary>
            IPV6Failed = 0xA,
            /// <summary>
            /// <see cref="IpBindFailed"/> Failure to bind Wi-Fi +<->+ IP interfaces
            /// </summary>
            IPBindFailed = 0xB,
            /// <summary>
            /// <see cref="UnknownError"/> Unknown error
            /// </summary>
            UnknownError = 0xC,
        }

        /// <summary>
        /// WiFi Band
        /// </summary>
        public enum WiFiBand : byte {
            /// <summary>
            /// 2.4GHz - 2.401GHz to 2.495GHz (802.11b/g/n/ax)
            /// </summary>
            _2G4 = 0,
            /// <summary>
            /// 3.65GHz - 3.655GHz to 3.695GHz (802.11y)
            /// </summary>
            _3G65 = 1,
            /// <summary>
            /// 5GHz - 5.150GHz to 5.895GHz (802.11a/n/ac/ax)
            /// </summary>
            _5G = 2,
            /// <summary>
            /// 6GHz - 5.925GHz to 7.125GHz (802.11ax / Wi-Fi 6E)
            /// </summary>
            _6G = 3,
            /// <summary>
            /// 60GHz - 57.24GHz to 70.20GHz (802.11ad/ay)
            /// </summary>
            _60G = 4,
            /// <summary>
            /// Sub-1GHz - 755MHz to 931MHz (802.11ah)
            /// </summary>
            _1G = 5,
        }

        /// <summary>
        /// WiFi Security
        /// </summary>
        [Flags]
        public enum WiFiSecurity : byte {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Supports unencrypted Wi-Fi
            /// </summary>
            Unencrypted = 0x01,
            /// <summary>
            /// Supports Wi-Fi using WEP security
            /// </summary>
            WEP = 0x02,
            /// <summary>
            /// Supports Wi-Fi using WPA-Personal security
            /// </summary>
            WPAPERSONAL = 0x04,
            /// <summary>
            /// Supports Wi-Fi using WPA2-Personal security
            /// </summary>
            WPA2PERSONAL = 0x08,
            /// <summary>
            /// Supports Wi-Fi using WPA3-Personal security
            /// </summary>
            WPA3PERSONAL = 0x10,
            WPA3MatterPDC = 0x20,
        }

        /// <summary>
        /// Thread Capabilities
        /// </summary>
        [Flags]
        public enum ThreadCapabilities : ushort {
            /// <summary>
            /// Nothing Set
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Thread Border Router functionality is present
            /// </summary>
            IsBorderRouterCapable = 0x0001,
            /// <summary>
            /// Router mode is supported (interface could be in router or REED mode)
            /// </summary>
            IsRouterCapable = 0x0002,
            /// <summary>
            /// Sleepy end-device mode is supported
            /// </summary>
            IsSleepyEndDeviceCapable = 0x0004,
            /// <summary>
            /// Device is a full Thread device (opposite of Minimal Thread Device)
            /// </summary>
            IsFullThreadDevice = 0x0008,
            /// <summary>
            /// Synchronized sleepy end-device mode is supported
            /// </summary>
            IsSynchronizedSleepyEndDeviceCapable = 0x0010,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// WiFi Interface Scan Result
        /// </summary>
        public record WiFiInterfaceScanResult : TLVPayload {
            /// <summary>
            /// WiFi Interface Scan Result
            /// </summary>
            public WiFiInterfaceScanResult() { }

            /// <summary>
            /// WiFi Interface Scan Result
            /// </summary>
            [SetsRequiredMembers]
            public WiFiInterfaceScanResult(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Security = (WiFiSecurity)reader.GetUInt(0)!.Value;
                SSID = reader.GetBytes(1, false, 32)!;
                BSSID = reader.GetBytes(2, false, 6)!;
                Channel = reader.GetUShort(3)!.Value;
                WiFiBand = (WiFiBand)reader.GetUShort(4)!.Value;
                RSSI = reader.GetSByte(5)!.Value;
            }
            public required WiFiSecurity Security { get; set; }
            public required byte[] SSID { get; set; }
            public required byte[] BSSID { get; set; }
            public required ushort Channel { get; set; }
            public required WiFiBand WiFiBand { get; set; }
            public required sbyte RSSI { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUInt(0, (uint)Security);
                writer.WriteBytes(1, SSID, 32);
                writer.WriteBytes(2, BSSID, 6);
                writer.WriteUShort(3, Channel);
                writer.WriteUShort(4, (ushort)WiFiBand);
                writer.WriteSByte(5, RSSI);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Thread Interface Scan Result
        /// </summary>
        public record ThreadInterfaceScanResult : TLVPayload {
            /// <summary>
            /// Thread Interface Scan Result
            /// </summary>
            public ThreadInterfaceScanResult() { }

            /// <summary>
            /// Thread Interface Scan Result
            /// </summary>
            [SetsRequiredMembers]
            public ThreadInterfaceScanResult(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                PanId = reader.GetUShort(0)!.Value;
                ExtendedPanId = reader.GetULong(1)!.Value;
                NetworkName = reader.GetString(2, false, 16)!;
                Channel = reader.GetUShort(3)!.Value;
                Version = reader.GetByte(4)!.Value;
                ExtendedAddress = new PhysicalAddress(reader.GetBytes(5, false, 8, 6)!);
                RSSI = reader.GetSByte(6)!.Value;
                LQI = reader.GetByte(7)!.Value;
            }
            public required ushort PanId { get; set; }
            public required ulong ExtendedPanId { get; set; }
            public required string NetworkName { get; set; }
            public required ushort Channel { get; set; }
            public required byte Version { get; set; }
            public required PhysicalAddress ExtendedAddress { get; set; }
            public required sbyte RSSI { get; set; }
            public required byte LQI { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, PanId);
                writer.WriteULong(1, ExtendedPanId);
                writer.WriteString(2, NetworkName, 16);
                writer.WriteUShort(3, Channel);
                writer.WriteByte(4, Version);
                writer.WriteBytes(5, ExtendedAddress.GetAddressBytes());
                writer.WriteSByte(6, RSSI);
                writer.WriteByte(7, LQI);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Network Info
        /// </summary>
        public record NetworkInfo : TLVPayload {
            /// <summary>
            /// Network Info
            /// </summary>
            public NetworkInfo() { }

            /// <summary>
            /// Network Info
            /// </summary>
            [SetsRequiredMembers]
            public NetworkInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                NetworkID = reader.GetBytes(0, false, 32)!;
                Connected = reader.GetBool(1)!.Value;
                NetworkIdentifier = reader.GetBytes(2, true, 20);
                ClientIdentifier = reader.GetBytes(3, true, 20);
            }
            public required byte[] NetworkID { get; set; }
            public required bool Connected { get; set; }
            public byte[]? NetworkIdentifier { get; set; }
            public byte[]? ClientIdentifier { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NetworkID, 32);
                writer.WriteBool(1, Connected);
                if (NetworkIdentifier != null)
                    writer.WriteBytes(2, NetworkIdentifier, 20);
                if (ClientIdentifier != null)
                    writer.WriteBytes(3, ClientIdentifier, 20);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record ScanNetworksPayload : TLVPayload {
            public byte[]? SSID { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (SSID != null)
                    writer.WriteBytes(0, SSID, 32);
                if (Breadcrumb != null)
                    writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Scan Networks Response - Reply from server
        /// </summary>
        public struct ScanNetworksResponse() {
            public required NetworkCommissioningStatus NetworkingStatus { get; set; }
            public string? DebugText { get; set; }
            public WiFiInterfaceScanResult[]? WiFiScanResults { get; set; }
            public ThreadInterfaceScanResult[]? ThreadScanResults { get; set; }
        }

        private record AddOrUpdateWiFiNetworkPayload : TLVPayload {
            public required byte[] SSID { get; set; }
            public required byte[] Credentials { get; set; }
            public ulong? Breadcrumb { get; set; }
            public byte[]? NetworkIdentity { get; set; }
            public byte[]? ClientIdentifier { get; set; }
            public byte[]? PossessionNonce { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, SSID, 32);
                writer.WriteBytes(1, Credentials, 64);
                if (Breadcrumb != null)
                    writer.WriteULong(2, Breadcrumb);
                if (NetworkIdentity != null)
                    writer.WriteBytes(3, NetworkIdentity, 140);
                if (ClientIdentifier != null)
                    writer.WriteBytes(4, ClientIdentifier, 20);
                if (PossessionNonce != null)
                    writer.WriteBytes(5, PossessionNonce, 32);
                writer.EndContainer();
            }
        }

        private record AddOrUpdateThreadNetworkPayload : TLVPayload {
            public required byte[] OperationalDataset { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, OperationalDataset, 254);
                if (Breadcrumb != null)
                    writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        private record RemoveNetworkPayload : TLVPayload {
            public required byte[] NetworkID { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NetworkID, 32);
                if (Breadcrumb != null)
                    writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Network Config Response - Reply from server
        /// </summary>
        public struct NetworkConfigResponse() {
            public required NetworkCommissioningStatus NetworkingStatus { get; set; }
            public string? DebugText { get; set; }
            public byte? NetworkIndex { get; set; }
            public byte[]? ClientIdentity { get; set; }
            public byte[]? PossessionSignature { get; set; }
        }

        private record ConnectNetworkPayload : TLVPayload {
            public required byte[] NetworkID { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NetworkID, 32);
                if (Breadcrumb != null)
                    writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Connect Network Response - Reply from server
        /// </summary>
        public struct ConnectNetworkResponse() {
            public required NetworkCommissioningStatus NetworkingStatus { get; set; }
            public string? DebugText { get; set; }
            public required int? ErrorValue { get; set; }
        }

        private record ReorderNetworkPayload : TLVPayload {
            public required byte[] NetworkID { get; set; }
            public required byte NetworkIndex { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NetworkID, 32);
                writer.WriteByte(1, NetworkIndex);
                if (Breadcrumb != null)
                    writer.WriteULong(2, Breadcrumb);
                writer.EndContainer();
            }
        }

        private record QueryIdentityPayload : TLVPayload {
            public required byte[] KeyIdentifier { get; set; }
            public byte[]? PossessionNonce { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, KeyIdentifier, 20);
                if (PossessionNonce != null)
                    writer.WriteBytes(1, PossessionNonce, 32);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Query Identity Response - Reply from server
        /// </summary>
        public struct QueryIdentityResponse() {
            public required byte[] Identity { get; set; }
            public byte[]? PossessionSignature { get; set; }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Scan Networks
        /// </summary>
        public async Task<ScanNetworksResponse?> ScanNetworks(SecureSession session, byte[]? sSID, ulong? breadcrumb) {
            ScanNetworksPayload requestFields = new ScanNetworksPayload() {
                SSID = sSID,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ScanNetworksResponse() {
                NetworkingStatus = (NetworkCommissioningStatus)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                WiFiScanResults = GetOptionalArrayField<WiFiInterfaceScanResult>(resp, 2),
                ThreadScanResults = GetOptionalArrayField<ThreadInterfaceScanResult>(resp, 3),
            };
        }

        /// <summary>
        /// Add Or Update WiFi Network
        /// </summary>
        public async Task<NetworkConfigResponse?> AddOrUpdateWiFiNetwork(SecureSession session, byte[] sSID, byte[] credentials, ulong? breadcrumb, byte[]? networkIdentity, byte[]? clientIdentifier, byte[]? possessionNonce) {
            AddOrUpdateWiFiNetworkPayload requestFields = new AddOrUpdateWiFiNetworkPayload() {
                SSID = sSID,
                Credentials = credentials,
                Breadcrumb = breadcrumb,
                NetworkIdentity = networkIdentity,
                ClientIdentifier = clientIdentifier,
                PossessionNonce = possessionNonce,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkConfigResponse() {
                NetworkingStatus = (NetworkCommissioningStatus)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                NetworkIndex = (byte?)GetOptionalField(resp, 2),
                ClientIdentity = (byte[]?)GetOptionalField(resp, 3),
                PossessionSignature = (byte[]?)GetOptionalField(resp, 4),
            };
        }

        /// <summary>
        /// Add Or Update Thread Network
        /// </summary>
        public async Task<NetworkConfigResponse?> AddOrUpdateThreadNetwork(SecureSession session, byte[] operationalDataset, ulong? breadcrumb) {
            AddOrUpdateThreadNetworkPayload requestFields = new AddOrUpdateThreadNetworkPayload() {
                OperationalDataset = operationalDataset,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x03, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkConfigResponse() {
                NetworkingStatus = (NetworkCommissioningStatus)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                NetworkIndex = (byte?)GetOptionalField(resp, 2),
                ClientIdentity = (byte[]?)GetOptionalField(resp, 3),
                PossessionSignature = (byte[]?)GetOptionalField(resp, 4),
            };
        }

        /// <summary>
        /// Remove Network
        /// </summary>
        public async Task<NetworkConfigResponse?> RemoveNetwork(SecureSession session, byte[] networkID, ulong? breadcrumb) {
            RemoveNetworkPayload requestFields = new RemoveNetworkPayload() {
                NetworkID = networkID,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x04, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkConfigResponse() {
                NetworkingStatus = (NetworkCommissioningStatus)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                NetworkIndex = (byte?)GetOptionalField(resp, 2),
                ClientIdentity = (byte[]?)GetOptionalField(resp, 3),
                PossessionSignature = (byte[]?)GetOptionalField(resp, 4),
            };
        }

        /// <summary>
        /// Connect Network
        /// </summary>
        public async Task<ConnectNetworkResponse?> ConnectNetwork(SecureSession session, byte[] networkID, ulong? breadcrumb) {
            ConnectNetworkPayload requestFields = new ConnectNetworkPayload() {
                NetworkID = networkID,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x06, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ConnectNetworkResponse() {
                NetworkingStatus = (NetworkCommissioningStatus)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                ErrorValue = (int?)GetField(resp, 2),
            };
        }

        /// <summary>
        /// Reorder Network
        /// </summary>
        public async Task<NetworkConfigResponse?> ReorderNetwork(SecureSession session, byte[] networkID, byte networkIndex, ulong? breadcrumb) {
            ReorderNetworkPayload requestFields = new ReorderNetworkPayload() {
                NetworkID = networkID,
                NetworkIndex = networkIndex,
                Breadcrumb = breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x08, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkConfigResponse() {
                NetworkingStatus = (NetworkCommissioningStatus)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                NetworkIndex = (byte?)GetOptionalField(resp, 2),
                ClientIdentity = (byte[]?)GetOptionalField(resp, 3),
                PossessionSignature = (byte[]?)GetOptionalField(resp, 4),
            };
        }

        /// <summary>
        /// Query Identity
        /// </summary>
        public async Task<QueryIdentityResponse?> QueryIdentity(SecureSession session, byte[] keyIdentifier, byte[]? possessionNonce) {
            QueryIdentityPayload requestFields = new QueryIdentityPayload() {
                KeyIdentifier = keyIdentifier,
                PossessionNonce = possessionNonce,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, cluster, 0x09, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new QueryIdentityResponse() {
                Identity = (byte[])GetField(resp, 0),
                PossessionSignature = (byte[]?)GetOptionalField(resp, 1),
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
        /// Max Networks Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> MaxNetworks { get; init; }

        /// <summary>
        /// Networks Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<NetworkInfo[]> Networks { get; init; }

        /// <summary>
        /// Scan Max Time Seconds Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> ScanMaxTimeSeconds { get; init; }

        /// <summary>
        /// Connect Max Time Seconds Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte> ConnectMaxTimeSeconds { get; init; }

        /// <summary>
        /// Interface Enabled Attribute [Read/Write]
        /// </summary>
        public required ReadWriteAttribute<bool> InterfaceEnabled { get; init; }

        /// <summary>
        /// Last Networking Status Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<NetworkCommissioningStatus?> LastNetworkingStatus { get; init; }

        /// <summary>
        /// Last Network ID Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<byte[]?> LastNetworkID { get; init; }

        /// <summary>
        /// Last Connect Error Value Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<int?> LastConnectErrorValue { get; init; }

        /// <summary>
        /// Supported WiFi Bands Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<WiFiBand[]> SupportedWiFiBands { get; init; }

        /// <summary>
        /// Supported Thread Features Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ThreadCapabilities> SupportedThreadFeatures { get; init; }

        /// <summary>
        /// Thread Version Attribute [Read Only]
        /// </summary>
        public required ReadAttribute<ushort> ThreadVersion { get; init; }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Network Commissioning";
        }
    }
}