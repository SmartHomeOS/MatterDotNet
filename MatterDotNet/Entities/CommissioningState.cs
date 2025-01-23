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

using MatterDotNet.Clusters;
using MatterDotNet.Clusters.CHIP;
using MatterDotNet.OperationalDiscovery;
using MatterDotNet.Protocol.Sessions;
using System.Text;

namespace MatterDotNet.Entities
{
    /// <summary>
    /// State information for a commissioning operation in progress
    /// </summary>
    public class CommissioningState
    {
        internal CommissioningState()
        {
            SupportedInterfaces = FabricInterface.None;
        }

        internal CommissioningState(Node node, SecureSession pase, FabricInterface iface, NetworkCommissioning.WiFiInterfaceScanResult[] wiFiNetworks, NetworkCommissioning.ThreadInterfaceScanResult[] threadNetworks, string[] connectedNetworks)
        {
            Node = node;
            PASE = pase;
            SupportedInterfaces = iface;
            WiFiNetworks = wiFiNetworks;
            ThreadNetworks = threadNetworks;
        }

        internal Node? Node { get; set; }
        internal SecureSession? PASE { get; set; }
        /// <summary>
        /// Supported Fabric Interface Types
        /// </summary>
        public FabricInterface SupportedInterfaces { get; internal set; }
        /// <summary>
        /// Detected WiFi Networks
        /// </summary>
        public NetworkCommissioning.WiFiInterfaceScanResult[] WiFiNetworks { get; internal set; } = [];
        /// <summary>
        /// Detected Thread Networks
        /// </summary>
        public NetworkCommissioning.ThreadInterfaceScanResult[] ThreadNetworks { get; internal set; } = [];
        /// <summary>
        /// A list of networks the node is currently connected to
        /// </summary>
        public string[] ConnectedNetworks { get; internal set; } = [];
        /// <summary>
        /// Returns true if commissioning is in progress. False if commissioning could not locate the Device.
        /// </summary>
        public bool CommissioningStarted { get { return Node != null; } }

        /// <summary>
        /// Add the provided network to the list of connected networks
        /// </summary>
        /// <param name="network"></param>
        internal void Upgrade(string network)
        {
            string[] ret = new string[ConnectedNetworks.Length + 1];
            ret[0] = network;
            for (int i = 0; i < ConnectedNetworks.Length; i++)
                ret[i+1] = ConnectedNetworks[i];
            ConnectedNetworks = ret;
        }

        public NetworkCommissioning.WiFiInterfaceScanResult? FindWiFi(string ssid)
        {
            byte[] ssidBytes = Encoding.UTF8.GetBytes(ssid);
            return FindWiFi(ssidBytes);
        }
        public NetworkCommissioning.WiFiInterfaceScanResult? FindWiFi(byte[] ssid)
        {
            foreach (var result in WiFiNetworks)
            {
                if (result.SSID.SequenceEqual(ssid))
                    return result;
            }
            return null;
        }

        public NetworkCommissioning.ThreadInterfaceScanResult? FindThread(ulong extendedPanId)
        {
            foreach (var result in ThreadNetworks)
            {
                if (result.ExtendedPanId == extendedPanId)
                    return result;
            }
            return null;
        }
        public NetworkCommissioning.ThreadInterfaceScanResult? FindThread(string networkName)
        {
            foreach (var result in ThreadNetworks)
            {
                if (result.NetworkName.SequenceEqual(networkName))
                    return result;
            }
            return null;
        }
    }
}
