using System;
using System.Text;
using System.Collections;
using System.IO;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            string textFromFile;
            using (FileStream fstream = File.OpenRead(@"../../text.txt"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                textFromFile = Encoding.Default.GetString(array);
            }

            string input = textFromFile;
            HuffmanTree huffmanTree = new HuffmanTree();

            huffmanTree.Build(input);

            BitArray encoded = huffmanTree.Encode(input);

            /*
            Console.Write("Encoded: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();
            */

            using (FileStream fstream = new FileStream(@"../../encoded.txt", FileMode.OpenOrCreate))
            {
                foreach (bool bit in encoded)
                {
                    byte[] array = Encoding.Default.GetBytes((bit ? 1 : 0) + "");
                    fstream.Write(array, 0, array.Length);
                }
            }
            string decoded = huffmanTree.Decode(encoded);

            using (FileStream fstream = new FileStream(@"../../decoded.txt", FileMode.OpenOrCreate))
            {
                byte[] array = Encoding.Default.GetBytes(decoded);
                fstream.Write(array, 0, array.Length);
            }

            /*
            Console.WriteLine("Decoded: " + decoded);
            Console.ReadLine();
            */
        }
    }
}