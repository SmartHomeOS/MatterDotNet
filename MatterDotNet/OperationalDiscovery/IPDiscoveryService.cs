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

using TinyDNS;
using TinyDNS.Enums;
using TinyDNS.Records;

namespace MatterDotNet.OperationalDiscovery
{
    /// <summary>
    /// IP Operational Discovery Service
    /// </summary>
    public class IPDiscoveryService
    {
        bool running = false;
        MDNS mdns = new MDNS();
        private static IPDiscoveryService service = new IPDiscoveryService();

        /// <summary>
        /// Return a shared instance of the discovery service
        /// </summary>
        public static IPDiscoveryService Shared
        {
            get 
            { 
                lock (service)
                {
                    if (!service.running)
                    {
                        service.mdns.Start().Wait();
                        service.running = true;
                    }
                }
                return service; 
            }
        }

        /// <summary>
        /// Find IP commissionable nodes matching the given params
        /// </summary>
        /// <param name="commissioningPayload"></param>
        /// <returns></returns>
        public async Task<ODNode?> Find(CommissioningPayload commissioningPayload)
        {
            List<ODNode> results = await Find(commissioningPayload.Discriminator, commissioningPayload.LongDiscriminator);
            foreach (ODNode result in results)
            {
                if (result.Vendor != 0 && commissioningPayload.VendorID != 0 && result.Vendor != commissioningPayload.VendorID)
                    continue;
                if (result.Product != 0 && commissioningPayload.ProductID != 0 && result.Product != commissioningPayload.ProductID)
                    continue;
                return result;
            }
            return null;
        }

        /// <summary>
        /// Find IP commissionable nodes matching the given discriminator
        /// </summary>
        /// <param name="discriminator"></param>
        /// <param name="fullLen"></param>
        /// <returns></returns>
        public async Task<List<ODNode>> Find(uint discriminator, bool fullLen)
        {
            string instance;
            if (fullLen)
                instance = "_L" + discriminator.ToString();
            else
                instance = "_S" + discriminator.ToString();
            string domain = instance + "._sub._matterc._udp.local";
            List<Message> results = [];
            for (int i = 0; i < 10; i++)
            {
                results = await mdns.ResolveQuery(domain, false, DNSRecordType.PTR);
                if (results.Count > 0)
                    break;
            }
            foreach (Message msg in results)
            {
                foreach (ResourceRecord record in msg.Answers)
                {
                    if (record is PtrRecord ptr && ptr.Name == domain)
                    {
                        List<ODNode> nodes = Parse(await mdns.ResolveQuery(ptr.Domain, false, DNSRecordType.SRV, DNSRecordType.TXT, DNSRecordType.A, DNSRecordType.AAAA));
                        if (nodes.Count > 0)
                            return nodes;
                    }
                }
            }
            return [];
        }

        /// <summary>
        /// Query operational information about the provided instance
        /// </summary>
        /// <param name="operationalInstanceName"></param>
        /// <param name="extendedSearch"></param>
        /// <returns></returns>
        public async Task<ODNode?> Find(string operationalInstanceName, bool extendedSearch = false)
        {
            Console.WriteLine("Looking for " + operationalInstanceName);
            List<ODNode> results;
            int length = extendedSearch ? 20 : 10; // 60 / 30 seconds
            for (int i = 0; i < length; i++)
            {
                results = Parse(await mdns.ResolveServiceInstance(operationalInstanceName, "_matter._tcp", "local"));
                if (results.Count > 0)
                    return results[0];
            }
            return null;
        }

        /// <summary>
        /// Find all commissionable nodes accessible by IP
        /// </summary>
        /// <returns></returns>
        public async Task<ODNode[]> FindAll()
        {
            List<ODNode> ret = new List<ODNode>();
            List<Message> results = await mdns.ResolveQuery("_matterc._udp.local", false, DNSRecordType.PTR);
            foreach (Message result in results)
            {
                foreach (ResourceRecord answer in result.Answers)
                {
                    if (answer is PtrRecord ptr)
                    {
                        List<ODNode> nodes = Parse(await mdns.ResolveQuery(ptr.Domain, false, DNSRecordType.SRV, DNSRecordType.TXT, DNSRecordType.A, DNSRecordType.AAAA));
                        if (nodes.Count > 0)
                            ret.Add(nodes[0]);
                    }
                }
            }
            return ret.ToArray();
        }

        private List<ODNode> Parse(List<Message> msgs)
        {
            List<ODNode> ret = new List<ODNode>();
            foreach (Message msg in msgs)
            {
                ODNode node = new ODNode();
                foreach (ResourceRecord answer in msg.Answers)
                {
                    if (answer is SRVRecord service)
                        node.Port = service.Port;
                    else if (answer is TxtRecord txt)
                        PopulateText(txt, ref node);
                }
                foreach (ResourceRecord additional in msg.Additionals)
                {
                    if (node.Port == 0 && additional is SRVRecord service)
                        node.Port = service.Port;
                    else if (node.IPAddress == null && additional is ARecord A)
                        node.IPAddress = A.Address;
                    else if (node.IPAddress == null && additional is AAAARecord AAAA)
                        node.IPAddress = AAAA.Address;
                    else if (additional is TxtRecord txt)
                        PopulateText(txt, ref node);
                }
                if (node.IPAddress == null || node.Port == 0)
                    continue;
                ret.Add(node);
            }
            return ret;
        }

        private void PopulateText(TxtRecord txt, ref ODNode node)
        {
            //TODO - RI, PH, PI
            foreach (string str in txt.Strings)
            {
                string[] kv = str.Split('=', 2);
                switch (kv[0])
                {
                    case "DT":
                        node.Type = (DeviceTypeEnum)uint.Parse(kv[1]);
                        break;
                    case "DN":
                        node.DeviceName = kv[1];
                        break;
                    case "VP":
                        string[] VP = kv[1].Split("+", 2);
                        if (uint.TryParse(VP[0], out uint vendor))
                            node.Vendor = vendor;
                        if (VP.Length > 1 && uint.TryParse(VP[1], out uint product))
                            node.Product = product;
                        break;
                    case "CM":
                        if (byte.TryParse(kv[1], out byte mode))
                            node.CommissioningMode = (CommissioningMode)mode;
                        break;
                    case "D":
                        if (ushort.TryParse(kv[1], out ushort descriminator))
                            node.Discriminator = descriminator;
                        break;
                    case "SII":
                        if (int.TryParse(kv[1], out int sessionIdle))
                            node.IdleInterval = sessionIdle;
                        break;
                    case "SAI":
                        if (int.TryParse(kv[1], out int sessionActive))
                            node.ActiveInterval = sessionActive;
                        break;
                    case "SAT":
                        if (int.TryParse(kv[1], out int activeThreshold))
                            node.ActiveThreshold = activeThreshold;
                        break;
                    case "ICD":
                        node.LongIdleTime = kv[1] == "1";
                        break;
                    case "T":
                        if (byte.TryParse(kv[1], out byte transport))
                            node.Transports = (SupportedTransportMode)transport;
                        break;
                }
            }
        }
    }
}
