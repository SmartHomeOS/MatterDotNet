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

namespace Generator
{
    internal class Generator
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("outputs");
            ClusterGenerator.Generate();
            foreach (string file in Directory.EnumerateFiles("..\\..\\..\\Structures"))
            {
                Tag[] structs;
                using (FileStream fs = File.OpenRead(file))
                    structs = StructParser.ParseStruct(fs);
                if (structs.Length == 0)
                    Console.WriteLine("Failed to parse structure");
                else
                    Console.WriteLine("Read Structure Successfully: \n******************************************************\n" + string.Join('\n', (object[])structs) + "\n*************************************");
                foreach (Tag tag in structs)
                {
                    if (tag.Namespace != null && !Directory.Exists($"outputs\\{tag.Namespace}\\"))
                        Directory.CreateDirectory($"outputs\\{tag.Namespace}\\");

                    string path = $"outputs\\{((tag.Namespace != null) ? tag.Namespace + "\\" : "")}" + tag.Name + ".cs";
                    if (File.Exists(path))
                        File.Delete(path);
                    using (FileStream outstream = File.OpenWrite(path))
                    {
                        if (ClassGenerator.Emit(outstream, tag))
                            Console.WriteLine(tag.Name + " Written Successfully!");
                        else
                            Console.WriteLine("Write Failed");
                    }
                }
            }
        }

        
    }
}
