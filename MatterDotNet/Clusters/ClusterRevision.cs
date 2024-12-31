

namespace MatterDotNet.Clusters
{
    public sealed class ClusterRevision : Attribute
    {
        public ClusterRevision(uint clusterID, int revision)
        {
            Revision = revision;
            ClusterID = clusterID;
        }

        public int Revision { get; set; }
        public uint ClusterID { get; set; }
    }
}
