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
            if (name.Length > 0 && Char.IsNumber(name[0]))
                ret.Append('_');
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

                if (name[i] == '>')
                    ret.Append("&gt;");
                else if (name[i] == '<')
                    ret.Append("&lt;");
                else if (name[i] == '&')
                    ret.Append("&amp;");
                else if (name[i] == '"')
                    ret.Append("&quot;");
                else
                    ret.Append(name[i]);
            }
            return ret.ToString().Replace("Wi Fi", "WiFi");
        }

        internal static string SanitizeComment(string summary)
        {
            summary = summary.Replace("Wi Fi", "WiFi");
            StringBuilder ret = new StringBuilder(summary.Length);
            bool space = false;
            foreach (char c in summary)
            {
                if (c == ' ')
                {
                    if (space)
                        continue;
                    else
                        space = true;
                }
                else
                    space = false;
                if (c == '>')
                    ret.Append("&gt;");
                else if (c == '<')
                    ret.Append("&lt;");
                else if (c == '&')
                    ret.Append("&amp;");
                else if (c == '"')
                    ret.Append("&quot;");
                else if (c != '\n' && c != '\r')
                    ret.Append(c);
            }
            return ret.ToString().Replace("[[ref_", "<see cref=\"").Replace("]]", "\"/>");
        }
    }
}
