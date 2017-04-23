using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleGPUJobs
{
    class Program
    {
        // Offset to the GPU jobs flag
        private const int OFFSET = 0x10D6;
        // File to search in
        private const string FILENAME = "globalgamemanagers";

        static void Main(string[] args)
        {
            var dataDirectory = Directory.EnumerateDirectories(".", "*_Data").FirstOrDefault();
            if (dataDirectory == null)
            {
                Console.Error.WriteLine("No data directory found!");
                return;
            }

            var fileToChange = Path.Combine(dataDirectory, FILENAME);
            if(!File.Exists(fileToChange))
            {
                Console.Error.WriteLine("Could not find {0}!", fileToChange);
                return;
            }

            using (var file = File.Open(fileToChange, FileMode.Open, FileAccess.ReadWrite))
            {
                file.Seek(OFFSET, SeekOrigin.Begin);
                int currentValue = file.ReadByte();
                byte newValue;
                switch(currentValue)
                {
                    case 0:
                        Console.WriteLine("Turning GPU render jobs on!");
                        newValue = 1;
                        break;
                    case 1:
                        Console.WriteLine("Turning GPU render jobs off!");
                        newValue = 0;
                        break;
                    default:
                        Console.Error.WriteLine("Invalid value ({0}) found, aborting! (Wrong offet?)", currentValue);
                        return;
                }
                file.Seek(-1, SeekOrigin.Current);
                file.WriteByte(newValue);
            }
            
        }
    }
}
