

using System.Text;

namespace Generator
{
    public class Tag
    {
        public Tag()
        {
            Children = new List<Tag>();
        }

        public DataType Type { get; set; }
        public string? Name { get; set; }
        public int LengthBytes { get; set; }
        public long Min { get; set; }
        public long Max { get; set; }
        public bool Nullable { get; set; }
        public bool Optional { get; set; }
        public List<Tag> Children { get; set; }
        public Tag? Parent { get; set; }
        public int TagNumber { get; set; }
        public string? ReferenceName { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Type == DataType.Reference)
                sb.AppendLine(ReferenceName + " " + Name + ":");
            sb.AppendLine(Type + " " + Name);
            foreach (Tag child in Children)
                sb.Append(getPrefix() + child.ToString());
            return sb.ToString();
        }

        private string getPrefix()
        {
            string prefix = string.Empty;
            Tag tag = this;
            while (tag.Parent != null)
            {
                prefix += '\t';
                tag = tag.Parent;
            }
            return prefix;
        }
    }
}
