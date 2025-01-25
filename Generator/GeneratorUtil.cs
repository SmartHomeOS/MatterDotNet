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
        public static string SanitizeName(string name, bool paramName = false, bool trimSuffix = true, string? ensureSuffix = null)
        {
            if (name == "Event")
                name = "@Event";
            if (trimSuffix)
            {
                if (name.EndsWith("struct", StringComparison.InvariantCultureIgnoreCase))
                    name = name.Substring(0, name.Length - 6);
                else if (name.EndsWith("enum", StringComparison.InvariantCultureIgnoreCase))
                    name = name.Substring(0, name.Length - 4);
                else if (name.EndsWith("bitmap", StringComparison.InvariantCultureIgnoreCase))
                    name = name.Substring(0, name.Length - 6);
            }
            else if (!name.EndsWith(ensureSuffix ?? string.Empty))
                name += ensureSuffix;
            
            bool cap = !paramName;
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
                else if (c == '.')
                {
                    cap = true;
                    ret.Append('_');
                }
                else if (paramName)
                {
                    ret.Append(char.ToLower(c));
                    paramName = false;
                }
                else if (cap)
                {
                    ret.Append(char.ToUpper(c));
                    cap = false;
                }
                else
                    ret.Append(c);
            }
            return ret.ToString().Replace("Percent100ths", "Percent");
        }

        public static string SanitizeClassName(string name)
        {
            return name.Replace(" ", "").Replace('/', '_').Replace("-", "").Replace("&", "And").Replace('.', '_');
        }

        public static string FieldNameToComment(string name, string? type = null)
        {
            if (name.EndsWith("struct", StringComparison.InvariantCultureIgnoreCase))
                name = name.Substring(0, name.Length - 6);
            if (name.EndsWith("enum", StringComparison.InvariantCultureIgnoreCase))
                name = name.Substring(0, name.Length - 4);
            if (name.EndsWith("bitmap", StringComparison.InvariantCultureIgnoreCase))
                name = name.Substring(0, name.Length - 6);
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
            if (type != null)
            {
                switch (type)
                {
                    case "power_mw":
                        ret.Append(" [mW]");
                        break;
                    case "amperage_ma":
                        ret.Append(" [mA]");
                        break;
                    case "voltage_mv":
                        ret.Append(" [mV]");
                        break;
                    case "energy_mwh":
                        ret.Append(" [mWh]");
                        break;
                    case "percent":
                        ret.Append(" [%]");
                        break;
                    case "percent100ths":
                        ret.Append(" [%]");
                        break;
                    case "temperature":
                    case "TemperatureDifference":
                        ret.Append(" [°C]");
                        break;
                }
            }
            return ret.ToString().Replace("Wi Fi", "WiFi");
        }

        internal static string? SanitizeComment(string? summary)
        {
            if (summary == null)
                return null;
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

        internal static string EnsureHex(string? value, int hexLength = 2)
        {
            if (value == null)
                return string.Empty;
            if (value.StartsWith("0x"))
            {
                if (hexLength == 2)
                {
                    if (value.Length > 3)
                        return value.ToUpperInvariant().Replace("0X", "0x");
                    else
                        return value.ToUpperInvariant().Replace("0X", "0x0");
                }
                else
                {
                    if (value.Length > 5)
                        return value.ToUpperInvariant().Replace("0X", "0x");
                    else if(value.Length > 3)
                        return value.ToUpperInvariant().Replace("0X", "0x00");
                    else
                        return value.ToUpperInvariant().Replace("0X", "0x000");
                }
            }
            long raw = long.Parse(value);
            return "0x" + raw.ToString(hexLength == 2 ? "X2" : "X4");
        }

        internal static string FormatValue(string value, string max)
        {
            long val = value.StartsWith("0x") ? Convert.ToInt64(value, 16) : long.Parse(value);
            long maxVal = 0;
            if (max != null)
                maxVal = max.StartsWith("0x") ? Convert.ToInt64(max, 16) : long.Parse(max);
            if (maxVal >= 10)
                return "0x" + val.ToString("X");
            else
                return val.ToString();
        }
    }
}
