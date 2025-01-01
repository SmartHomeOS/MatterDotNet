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
    public class GeneratorUtil
    {
        public static string SanitizeName(string name)
        {
            if (name.EndsWith("struct", StringComparison.InvariantCultureIgnoreCase))
                name = name.Substring(0, name.Length - 6);
            bool cap = true;
            StringBuilder ret = new StringBuilder(name.Length);
            foreach (char c in name)
            {
                if (c == ' ' || c == '-')
                    cap = true;
                else if (c == '/')
                {
                    cap = true;
                    ret.Append('_');
                }
                else if (cap)
                {
                    ret.Append(char.ToUpper(c));
                    cap = false;
                }
                else
                    ret.Append(c);
            }
            return ret.ToString();
        }

        public static string SanitizeClassName(string name)
        {
            return name.Replace(" ", "").Replace('/', '_').Replace("-", "");
        }

        public static string FieldNameToComment(string name)
        {
            if (name.EndsWith("struct", StringComparison.InvariantCultureIgnoreCase))
                name = name.Substring(0, name.Length - 6);
            if (name.EndsWith("enum", StringComparison.InvariantCultureIgnoreCase))
                name = name.Substring(0, name.Length - 4);
            StringBuilder ret = new StringBuilder(name.Length);
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]) && i > 0 && (i + 1 != name.Length) && (!char.IsUpper(name[i+1]) || (!char.IsUpper(name[i-1]))))
                    ret.Append(' ');

                ret.Append(name[i]);
            }
            return ret.ToString();
        }
    }
}
