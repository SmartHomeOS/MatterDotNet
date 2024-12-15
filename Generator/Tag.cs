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
        public string? Namespace { get; set; }

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
