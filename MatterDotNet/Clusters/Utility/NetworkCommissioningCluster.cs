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

namespace MatterDotNet.Clusters.Utility
{
    /// <summary>
    /// Network Commissioning Cluster
    /// </summary>
    [ClusterRevision(CLUSTER_ID, 2)]
    public class NetworkCommissioningCluster : ClusterBase
    {
        internal const uint CLUSTER_ID = 0x0031;

        /// <summary>
        /// Network Commissioning Cluster
        /// </summary>
        public NetworkCommissioningCluster(ushort endPoint) : base(CLUSTER_ID, endPoint) { }
        /// <inheritdoc />
        protected NetworkCommissioningCluster(uint cluster, ushort endPoint) : base(cluster, endPoint) { }

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
        }

        /// <summary>
        /// Network Commissioning Status
        /// </summary>
        public enum NetworkCommissioningStatusEnum {
            /// <summary>
            /// OK, no error
            /// </summary>
            Success = 0,
            /// <summary>
            /// <see cref="OutOfRange"/> Value Outside Range
            /// </summary>
            OutOfRange = 1,
            /// <summary>
            /// <see cref="BoundsExceeded"/> A collection would exceed its size limit
            /// </summary>
            BoundsExceeded = 2,
            /// <summary>
            /// <see cref="NetworkIdNotFound"/> The NetworkID is not among the collection of added networks
            /// </summary>
            NetworkIDNotFound = 3,
            /// <summary>
            /// <see cref="DuplicateNetworkId"/> The NetworkID is already among the collection of added networks
            /// </summary>
            DuplicateNetworkID = 4,
            /// <summary>
            /// <see cref="NetworkNotFound"/> Cannot find AP: SSID Not found
            /// </summary>
            NetworkNotFound = 5,
            /// <summary>
            /// <see cref="RegulatoryError"/> Cannot find AP: Mismatch on band/channels/regulatory domain / 2.4GHz vs 5GHz
            /// </summary>
            RegulatoryError = 6,
            /// <summary>
            /// <see cref="AuthFailure"/> Cannot associate due to authentication failure
            /// </summary>
            AuthFailure = 7,
            /// <summary>
            /// <see cref="UnsupportedSecurity"/> Cannot associate due to unsupported security mode
            /// </summary>
            UnsupportedSecurity = 8,
            /// <summary>
            /// <see cref="OtherConnectionFailure"/> Other association failure
            /// </summary>
            OtherConnectionFailure = 9,
            /// <summary>
            /// <see cref="Ipv6Failed"/> Failure to generate an IPv6 address
            /// </summary>
            IPV6Failed = 10,
            /// <summary>
            /// <see cref="IpBindFailed"/> Failure to bind Wi-Fi +&lt;-&gt;+ IP interfaces
            /// </summary>
            IPBindFailed = 11,
            /// <summary>
            /// <see cref="UnknownError"/> Unknown error
            /// </summary>
            UnknownError = 12,
        }

        /// <summary>
        /// WiFi Band
        /// </summary>
        public enum WiFiBandEnum {
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
        /// Thread Capabilities Bitmap
        /// </summary>
        [Flags]
        public enum ThreadCapabilitiesBitmap {
            /// <summary>
            /// Thread Border Router functionality is present
            /// </summary>
            IsBorderRouterCapable = 1,
            /// <summary>
            /// Router mode is supported (interface could be in router or REED mode)
            /// </summary>
            IsRouterCapable = 2,
            /// <summary>
            /// Sleepy end-device mode is supported
            /// </summary>
            IsSleepyEndDeviceCapable = 4,
            /// <summary>
            /// Device is a full Thread device (opposite of Minimal Thread Device)
            /// </summary>
            IsFullThreadDevice = 8,
            /// <summary>
            /// Synchronized sleepy end-device mode is supported
            /// </summary>
            IsSynchronizedSleepyEndDeviceCapable = 16,
        }

        /// <summary>
        /// WiFi Security Bitmap
        /// </summary>
        [Flags]
        public enum WiFiSecurityBitmap {
            /// <summary>
            /// Supports unencrypted Wi-Fi
            /// </summary>
            Unencrypted = 1,
            /// <summary>
            /// Supports Wi-Fi using WEP security
            /// </summary>
            WEP = 2,
            /// <summary>
            /// Supports Wi-Fi using WPA-Personal security
            /// </summary>
            WPAPERSONAL = 4,
            /// <summary>
            /// Supports Wi-Fi using WPA2-Personal security
            /// </summary>
            WPA2PERSONAL = 8,
            /// <summary>
            /// Supports Wi-Fi using WPA3-Personal security
            /// </summary>
            WPA3PERSONAL = 16,
        }
        #endregion Enums

        #region Records
        /// <summary>
        /// Network Info
        /// </summary>
        public record NetworkInfo : TLVPayload {
            /// <summary>
            /// Network Info
            /// </summary>
            public NetworkInfo() { }

            [SetsRequiredMembers]
            internal NetworkInfo(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                NetworkID = reader.GetBytes(0, false, 32, 1
)!;
                Connected = reader.GetBool(1)!.Value;
            }
            public required byte[] NetworkID { get; set; }
            public required bool Connected { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NetworkID, 32, 1);
                writer.WriteBool(1, Connected);
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

            [SetsRequiredMembers]
            internal ThreadInterfaceScanResult(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                PanId = reader.GetUShort(0)!.Value;
                ExtendedPanId = reader.GetULong(1)!.Value;
                NetworkName = reader.GetString(2, false, 16, 1)!;
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
                writer.WriteUShort(0, PanId, 65534);
                writer.WriteULong(1, ExtendedPanId);
                writer.WriteString(2, NetworkName, 16, 1);
                writer.WriteUShort(3, Channel);
                writer.WriteByte(4, Version);
                writer.WriteBytes(5, ExtendedAddress.GetAddressBytes());
                writer.WriteSByte(6, RSSI);
                writer.WriteByte(7, LQI);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// WiFi Interface Scan Result
        /// </summary>
        public record WiFiInterfaceScanResult : TLVPayload {
            /// <summary>
            /// WiFi Interface Scan Result
            /// </summary>
            public WiFiInterfaceScanResult() { }

            [SetsRequiredMembers]
            internal WiFiInterfaceScanResult(object[] fields) {
                FieldReader reader = new FieldReader(fields);
                Security = (WiFiSecurityBitmap)reader.GetUShort(0)!.Value;
                SSID = reader.GetBytes(1, false)!;
                BSSID = reader.GetBytes(2, false)!;
                Channel = reader.GetUShort(3)!.Value;
                WiFiBand = (WiFiBandEnum)reader.GetUShort(4, true)!.Value;
                RSSI = reader.GetSByte(5, true);
            }
            public required WiFiSecurityBitmap Security { get; set; }
            public required byte[] SSID { get; set; }
            public required byte[] BSSID { get; set; }
            public required ushort Channel { get; set; }
            public WiFiBandEnum? WiFiBand { get; set; }
            public sbyte? RSSI { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteUShort(0, (ushort)Security);
                writer.WriteBytes(1, SSID, 32);
                writer.WriteBytes(2, BSSID, 6);
                writer.WriteUShort(3, Channel);
                if (WiFiBand != null)
                    writer.WriteUShort(4, (ushort?)WiFiBand);
                if (RSSI != null)
                    writer.WriteSByte(5, RSSI);
                writer.EndContainer();
            }
        }
        #endregion Records

        #region Payloads
        private record ScanNetworksPayload : TLVPayload {
            public byte[]? SSID { get; set; } = null;
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                if (SSID != null)
                    writer.WriteBytes(0, SSID, 32, 1);
                if (Breadcrumb != null)
                    writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Scan Networks Response - Reply from server
        /// </summary>
        public struct ScanNetworksResponse() {
            public required NetworkCommissioningStatusEnum NetworkingStatus { get; set; }
            public string? DebugText { get; set; }
            public required List<WiFiInterfaceScanResult> WiFiScanResults { get; set; }
            public required List<ThreadInterfaceScanResult> ThreadScanResults { get; set; }
        }

        private record AddOrUpdateWiFiNetworkPayload : TLVPayload {
            public required byte[] SSID { get; set; }
            public required byte[] Credentials { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, SSID, 32);
                writer.WriteBytes(1, Credentials, 64);
                if (Breadcrumb != null)
                    writer.WriteULong(2, Breadcrumb);
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
                writer.WriteBytes(0, NetworkID, 32, 1);
                if (Breadcrumb != null)
                    writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Network Config Response - Reply from server
        /// </summary>
        public struct NetworkConfigResponse() {
            public required NetworkCommissioningStatusEnum NetworkingStatus { get; set; }
            public string? DebugText { get; set; }
            public byte? NetworkIndex { get; set; }
        }

        private record ConnectNetworkPayload : TLVPayload {
            public required byte[] NetworkID { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NetworkID, 32, 1);
                if (Breadcrumb != null)
                    writer.WriteULong(1, Breadcrumb);
                writer.EndContainer();
            }
        }

        /// <summary>
        /// Connect Network Response - Reply from server
        /// </summary>
        public struct ConnectNetworkResponse() {
            public required NetworkCommissioningStatusEnum NetworkingStatus { get; set; }
            public string? DebugText { get; set; }
            public required int ? ErrorValue { get; set; }
        }

        private record ReorderNetworkPayload : TLVPayload {
            public required byte[] NetworkID { get; set; }
            public required byte NetworkIndex { get; set; }
            public ulong? Breadcrumb { get; set; }
            internal override void Serialize(TLVWriter writer, long structNumber = -1) {
                writer.StartStructure(structNumber);
                writer.WriteBytes(0, NetworkID, 32, 1);
                writer.WriteByte(1, NetworkIndex);
                if (Breadcrumb != null)
                    writer.WriteULong(2, Breadcrumb);
                writer.EndContainer();
            }
        }
        #endregion Payloads

        #region Commands
        /// <summary>
        /// Scan Networks
        /// </summary>
        public async Task<ScanNetworksResponse?> ScanNetworks(SecureSession session, byte[]? SSID, ulong? Breadcrumb) {
            ScanNetworksPayload requestFields = new ScanNetworksPayload() {
                SSID = SSID,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x00, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ScanNetworksResponse() {
                NetworkingStatus = (NetworkCommissioningStatusEnum)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                WiFiScanResults = (List<WiFiInterfaceScanResult>)GetField(resp, 2),
                ThreadScanResults = (List<ThreadInterfaceScanResult>)GetField(resp, 3),
            };
        }

        /// <summary>
        /// Add Or Update WiFi Network
        /// </summary>
        public async Task<NetworkConfigResponse?> AddOrUpdateWiFiNetwork(SecureSession session, byte[] SSID, byte[] Credentials, ulong? Breadcrumb) {
            AddOrUpdateWiFiNetworkPayload requestFields = new AddOrUpdateWiFiNetworkPayload() {
                SSID = SSID,
                Credentials = Credentials,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x02, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkConfigResponse() {
                NetworkingStatus = (NetworkCommissioningStatusEnum)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                NetworkIndex = (byte?)GetOptionalField(resp, 2),
            };
        }

        /// <summary>
        /// Add Or Update Thread Network
        /// </summary>
        public async Task<NetworkConfigResponse?> AddOrUpdateThreadNetwork(SecureSession session, byte[] OperationalDataset, ulong? Breadcrumb) {
            AddOrUpdateThreadNetworkPayload requestFields = new AddOrUpdateThreadNetworkPayload() {
                OperationalDataset = OperationalDataset,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x03, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkConfigResponse() {
                NetworkingStatus = (NetworkCommissioningStatusEnum)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                NetworkIndex = (byte?)GetOptionalField(resp, 2),
            };
        }

        /// <summary>
        /// Remove Network
        /// </summary>
        public async Task<NetworkConfigResponse?> RemoveNetwork(SecureSession session, byte[] NetworkID, ulong? Breadcrumb) {
            RemoveNetworkPayload requestFields = new RemoveNetworkPayload() {
                NetworkID = NetworkID,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x04, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkConfigResponse() {
                NetworkingStatus = (NetworkCommissioningStatusEnum)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                NetworkIndex = (byte?)GetOptionalField(resp, 2),
            };
        }

        /// <summary>
        /// Connect Network
        /// </summary>
        public async Task<ConnectNetworkResponse?> ConnectNetwork(SecureSession session, byte[] NetworkID, ulong? Breadcrumb) {
            ConnectNetworkPayload requestFields = new ConnectNetworkPayload() {
                NetworkID = NetworkID,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x06, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new ConnectNetworkResponse() {
                NetworkingStatus = (NetworkCommissioningStatusEnum)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                ErrorValue = (int )GetField(resp, 2),
            };
        }

        /// <summary>
        /// Reorder Network
        /// </summary>
        public async Task<NetworkConfigResponse?> ReorderNetwork(SecureSession session, byte[] NetworkID, byte NetworkIndex, ulong? Breadcrumb) {
            ReorderNetworkPayload requestFields = new ReorderNetworkPayload() {
                NetworkID = NetworkID,
                NetworkIndex = NetworkIndex,
                Breadcrumb = Breadcrumb,
            };
            InvokeResponseIB resp = await InteractionManager.ExecCommand(session, endPoint, CLUSTER_ID, 0x08, requestFields);
            if (!ValidateResponse(resp))
                return null;
            return new NetworkConfigResponse() {
                NetworkingStatus = (NetworkCommissioningStatusEnum)(byte)GetField(resp, 0),
                DebugText = (string?)GetOptionalField(resp, 1),
                NetworkIndex = (byte?)GetOptionalField(resp, 2),
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
        /// Get the Max Networks attribute
        /// </summary>
        public async Task<byte> GetMaxNetworks(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 0))!;
        }

        /// <summary>
        /// Get the Networks attribute
        /// </summary>
        public async Task<List<NetworkInfo>> GetNetworks(SecureSession session) {
            List<NetworkInfo> list = new List<NetworkInfo>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 1))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add(new NetworkInfo(reader.GetStruct(i)!));
            return list;
        }

        /// <summary>
        /// Get the Scan Max Time Seconds attribute
        /// </summary>
        public async Task<byte> GetScanMaxTimeSeconds(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 2))!;
        }

        /// <summary>
        /// Get the Connect Max Time Seconds attribute
        /// </summary>
        public async Task<byte> GetConnectMaxTimeSeconds(SecureSession session) {
            return (byte)(dynamic?)(await GetAttribute(session, 3))!;
        }

        /// <summary>
        /// Get the Interface Enabled attribute
        /// </summary>
        public async Task<bool> GetInterfaceEnabled(SecureSession session) {
            return (bool?)(dynamic?)await GetAttribute(session, 4) ?? true;
        }

        /// <summary>
        /// Set the Interface Enabled attribute
        /// </summary>
        public async Task SetInterfaceEnabled (SecureSession session, bool? value = true) {
            await SetAttribute(session, 4, value);
        }

        /// <summary>
        /// Get the Last Networking Status attribute
        /// </summary>
        public async Task<NetworkCommissioningStatusEnum?> GetLastNetworkingStatus(SecureSession session) {
            return (NetworkCommissioningStatusEnum?)await GetEnumAttribute(session, 5, true);
        }

        /// <summary>
        /// Get the Last Network ID attribute
        /// </summary>
        public async Task<byte[]?> GetLastNetworkID(SecureSession session) {
            return (byte[]?)(dynamic?)await GetAttribute(session, 6, true) ?? null;
        }

        /// <summary>
        /// Get the Last Connect Error Value attribute
        /// </summary>
        public async Task<int ?> GetLastConnectErrorValue(SecureSession session) {
            return (int ?)(dynamic?)await GetAttribute(session, 7, true) ?? null;
        }

        /// <summary>
        /// Get the Supported WiFi Bands attribute
        /// </summary>
        public async Task<List<WiFiBandEnum>> GetSupportedWiFiBands(SecureSession session) {
            List<WiFiBandEnum> list = new List<WiFiBandEnum>();
            FieldReader reader = new FieldReader((IList<object>)(await GetAttribute(session, 8))!);
            for (int i = 0; i < reader.Count; i++)
                list.Add((WiFiBandEnum)reader.GetUShort(i)!.Value);
            return list;
        }

        /// <summary>
        /// Get the Supported Thread Features attribute
        /// </summary>
        public async Task<ThreadCapabilitiesBitmap> GetSupportedThreadFeatures(SecureSession session) {
            return (ThreadCapabilitiesBitmap)await GetEnumAttribute(session, 9);
        }

        /// <summary>
        /// Get the Thread Version attribute
        /// </summary>
        public async Task<ushort> GetThreadVersion(SecureSession session) {
            return (ushort)(dynamic?)(await GetAttribute(session, 10))!;
        }
        #endregion Attributes

        /// <inheritdoc />
        public override string ToString() {
            return "Network Commissioning Cluster";
        }
    }
}