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
using MatterDotNet.Clusters.General;
using MatterDotNet.Protocol.Sessions;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;

namespace MatterDotNet.Entities
{
    /// <summary>
    /// A node End Point containing clusters and device types
    /// </summary>
    public class EndPoint
    {
        private Node? node; //TODO - Remove if not used when groups are implemented
        private Dictionary<uint, ClusterBase> clusters;
        private ushort index;
        private Dictionary<ushort, EndPoint> children;
        /// <summary>
        /// Device Types for this End Point
        /// </summary>
        public DeviceTypeEnum[] DeviceTypes { get; private set; } = [];

        /// <summary>
        /// Create a new End Point
        /// </summary>
        /// <param name="index"></param>
        /// <param name="clusters"></param>
        /// <param name="children"></param>
        public EndPoint(ushort index, Dictionary<uint, ClusterBase> clusters, Dictionary<ushort, EndPoint> children)
        {
            this.index = index;
            this.clusters = clusters;
            this.children = children;
        }

        /// <summary>
        /// Create a new End Point
        /// </summary>
        /// <param name="index"></param>
        public EndPoint(ushort index)
        {
            this.index = index;
            this.clusters = new Dictionary<uint, ClusterBase>();
            this.children = new Dictionary<ushort, EndPoint>();
            this.clusters.Add(Descriptor.CLUSTER_ID, new Descriptor(index));
        }

        /// <summary>
        /// Create a new End Point
        /// </summary>
        /// <param name="index"></param>
        /// <param name="node"></param>
        public EndPoint(ushort index, Node node) : this(index)
        {
            SetNode(node);
        }

        /// <summary>
        /// Returns true if this EndPoint is the provided DeviceType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsType(DeviceTypeEnum type)
        {
            return DeviceTypes.Contains(type);
        }

        /// <summary>
        /// Returns true if the EndPoint or any of it's children are the provided DeviceType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasType(DeviceTypeEnum type)
        {
            if (IsType(type))
                return true;
            foreach (var child in children.Values)
            {
                if (child.HasType(type))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Find the EndPoint with the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public EndPoint? Find(DeviceTypeEnum type)
        {
            if (IsType(type))
                return this;
            foreach (var child in children.Values)
            {
                EndPoint? result = child.Find(type);
                if (result != null)
                    return result;
            }
            return null;
        }

        internal void SetNode(Node? node)
        {
            this.node = node;
        }

        internal bool AddCluster(ClusterBase cluster)
        {
            if (cluster.GetType().Equals(typeof(UnknownCluster)))
                return clusters.TryAdd(((UnknownCluster)cluster).ClusterID, cluster);
            ClusterRevision revision = (ClusterRevision)cluster.GetType().GetCustomAttribute(typeof(ClusterRevision))!;
            return clusters.TryAdd(revision.ClusterID, cluster);
        }

        internal void AddChild(EndPoint ep)
        {
            ep.SetNode(node);
            children.Add(ep.index, ep);
        }

        internal void RemoveChild(EndPoint ep)
        {
            ep.SetNode(null);
            children.Remove(ep.index);
        }

        internal EndPoint? RemoveChild(ushort index)
        {
            if (children.Remove(index, out EndPoint? ep))
                return ep;
            return null;
        }

        /// <summary>
        /// The list of child EndPoints
        /// </summary>
        public ImmutableList<EndPoint> Children { get { return children.Values.ToImmutableList(); } }

        /// <summary>
        /// Returns true if the Cluster is supported
        /// </summary>
        /// <typeparam name="T">Any cluster</typeparam>
        /// <returns></returns>
        public bool HasCluster<T>() where T : ClusterBase
        {
            ClusterRevision revision = (ClusterRevision)typeof(T).GetCustomAttribute(typeof(ClusterRevision))!;
            return clusters.ContainsKey(revision.ClusterID);
        }

        /// <summary>
        /// Get a Cluster by Type
        /// </summary>
        /// <typeparam name="T">Any cluster</typeparam>
        /// <returns></returns>
        public T GetCluster<T>() where T : ClusterBase
        {
            ClusterRevision revision = (ClusterRevision)typeof(T).GetCustomAttribute(typeof(ClusterRevision))!;
            if (clusters.TryGetValue(revision.ClusterID, out var cluster))
                return (T)cluster;
            return (new UnknownCluster(0, index) as T)!;
        }

        internal async Task EnumerateClusters(SecureSession session)
        {
            uint[] clusterIds = await GetCluster<Descriptor>().ServerList.Get(session);
            foreach (var clusterId in clusterIds) {
                if (clusterId != Descriptor.CLUSTER_ID)
                    AddCluster(ClusterBase.Create(clusterId, index));
            }
            Descriptor.DeviceType[] devices = await GetCluster<Descriptor>().DeviceTypeList.Get(session);
            DeviceTypes = devices.Select(t => t.DeviceTypeField).ToArray();

            foreach (EndPoint ep in children.Values)
                await ep.EnumerateClusters(session);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("");
        }

        /// <summary>
        /// Return a string representation of the End Point
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns></returns>
        public string ToString(string tabs)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "EndPoint " + index + ":");
            sb.AppendLine(tabs + "* Types: " + string.Join(',', DeviceTypes.Select(t => t.ToString())));
            foreach (ClusterBase cluster in clusters.Values)
                sb.AppendLine(tabs + "\t" + cluster.ToString());

            sb.AppendLine(tabs + "Children:");
            foreach (EndPoint endPoint in children.Values)
                sb.Append(endPoint.ToString(tabs + "\t"));
            return sb.ToString();
        }
    }
}
