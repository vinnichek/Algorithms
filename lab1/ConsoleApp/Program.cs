using System.IO;
using BillboardsLibrary;
using FileLibrary;

namespace ConsoleApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var text = new FileReader("../../../text.txt");

            int[] position = text.ReadAndParse().Item1;
            int[] profit = text.ReadAndParse().Item2;

            var billboards = new Billboard(position, profit);
            int[] maxProfit = billboards.FindMaxProfit().ToArray();

            File.WriteAllText("../../../text2.txt", string.Join(" ", maxProfit));
        }
    }
}
