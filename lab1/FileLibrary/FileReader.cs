using System;
using System.IO;
using System.Linq;

namespace FileLibrary
{
    public class FileReader
    {
        private string path;

        public FileReader(string path)
        {
            this.path = path;
        }

        public Tuple<int[], int[]> ReadAndParse()
        {
            string firstString = File.ReadLines(path).First();
            string secondString = File.ReadLines(path).Skip(1).First();

            int[] position = firstString
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => int.Parse(n)).ToArray();

            int[] profit = secondString
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => int.Parse(n)).ToArray();

            return Tuple.Create(position, profit);
        }
    }
}
