using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRActivator
{
    class Program
    {
        private const string CONFIG_TEXT = "Assets / Scene.unity";
        private static byte[] PREAMBLE = new byte[] { 0x2E, 0x75, 0x6E, 0x69, 0x74, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static byte[] SEARCH_STRING = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00  };
        private static byte[] REPLACE_STRING = new byte[] {  0x02, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x4E, 0x6F, 0x6E, 0x65, 0x06, 0x00, 0x00, 0x00, 0x4F, 0x70, 0x65, 0x6E, 0x56, 0x52, 0x00, 0x00 };

        private static int START_INDEX = 0xF63;
        private static byte[] REAL_SEARCH_STRING = new byte[] { 0x40, 0x0A, 0xD7, 0xA3, 0x3B };
        //private static byte[] FILE_START = new byte[] { 0x3C, 0x00, 0x00, 0x00, 0x0E, 0x00, 0x00, 0x00 };

        static void Main(string[] args)
        {
            //args = new string[] { @"E:\Dev\Unity\YookaLaylee\YookaLayleeVR_Data\globalgamemanagers.original" };
            args = new string[] { @"D:\Games\Steam\steamapps\common\YookaLaylee\YookaLaylee64_Data\globalgamemanagers" };

            if (args.Length < 1 )
            {
                Console.Error.WriteLine("Please provide the path to globalgamemanagers!");
                return;
            }


            var bytes = File.ReadAllBytes(args[0]);
            int settingsPos = FindBytes(bytes, REAL_SEARCH_STRING);
            int stringConfigPos = FindBytes(bytes, PREAMBLE) + PREAMBLE.Length;
            int offsetFromStartPos = settingsPos - START_INDEX;
            byte[] offsetBytes = BitConverter.GetBytes(offsetFromStartPos).ToArray();

            // Activate OpenVR
            bytes = bytes.Take(stringConfigPos).Concat(REPLACE_STRING).Concat(bytes.Skip(stringConfigPos + 4)).ToArray();
            //Array.Copy(REPLACE_STRING, 0, bytes, )
             //+ ReplaceBytes(bytes, SEARCH_STRING, REPLACE_STRING);
            int sizeDiff = REPLACE_STRING.Length - 4/* - SEARCH_STRING.Length*/;

            // Update file size in header
            var totalSize = BitConverter.GetBytes(bytes.Length).Reverse().ToArray();
            Array.Copy(totalSize, 0, bytes, 4, 4);

            // Update offsets
            int pos = FindBytes(bytes, offsetBytes);
            int previousOffset = 0;
            while(true)
            {
                
                int offset = BitConverter.ToInt32(bytes, pos);
                int size = BitConverter.ToInt32(bytes, pos + 4);

                if(previousOffset > offset)
                {
                    break;
                }
                previousOffset = offset;

                Console.WriteLine("GO ON");
                if(offset > offsetFromStartPos)
                {
                    // Change offset!
                    offset += sizeDiff;
                    Array.Copy(BitConverter.GetBytes(offset), 0, bytes, pos, 4);
                } else
                {
                    // Change size!
                    size += sizeDiff;
                    Array.Copy(BitConverter.GetBytes(size), 0, bytes, pos + 4, 4);
                }

                pos += 20 + 4 + 4;
            }

            //using (var input = new MemoryStream(bytes))
            //using (var output = File.Open(args[0], FileMode.Open))
            //{
            //    // Truncate
            //    output.SetLength(0);

            //    input.CopyTo(output, replacementIndex);
            //}

            //File.WriteAllBytes(@"E:\Dev\Unity\YookaLaylee\YookaLayleeVR_Data\globalgamemanagers", bytes);
            File.WriteAllBytes(@"D:\Games\Steam\steamapps\common\YookaLaylee\YookaLaylee64_Data\globalgamemanagers.new", bytes);
        }


        public static int FindBytes(byte[] src, byte[] find)
        {
            int index = -1;
            int matchIndex = 0;
            // handle the complete source array
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == find[matchIndex])
                {
                    if (matchIndex == (find.Length - 1))
                    {
                        index = i - matchIndex;
                        break;
                    }
                    matchIndex++;
                }
                else if (src[i] == find[0])
                {
                    matchIndex = 1;
                }
                else
                {
                    matchIndex = 0;
                }

            }
            return index;
        }

        public static byte[] ReplaceBytes(byte[] src, byte[] search, byte[] repl)
        {
            byte[] dst = null;
            int index = FindBytes(src, search);
            if (index >= 0)
            {
                dst = new byte[src.Length - search.Length + repl.Length];
                // before found array
                Buffer.BlockCopy(src, 0, dst, 0, index);
                // repl copy
                Buffer.BlockCopy(repl, 0, dst, index, repl.Length);
                // rest of src array
                Buffer.BlockCopy(
                    src,
                    index + search.Length,
                    dst,
                    index + repl.Length,
                    src.Length - (index + search.Length));
            }
            return dst;
        }


    }

    public static class SearchExtension
    {
        /// <summary>
        /// Searches in the haystack array for the given needle using the default equality operator and returns the index at which the needle starts.
        /// </summary>
        /// <typeparam name="T">Type of the arrays.</typeparam>
        /// <param name="haystack">Sequence to operate on.</param>
        /// <param name="needle">Sequence to search for.</param>
        /// <returns>Index of the needle within the haystack or -1 if the needle isn't contained.</returns>
        public static IEnumerable<int> IndexOf<T>(this T[] haystack, T[] needle)
        {
            if ((needle != null) && (haystack.Length >= needle.Length))
            {
                for (int l = 0; l < haystack.Length - needle.Length + 1; l++)
                {
                    if (!needle.Where((data, index) => !haystack[l + index].Equals(data)).Any())
                    {
                        yield return l;
                    }
                }
            }
        }
    }
}
