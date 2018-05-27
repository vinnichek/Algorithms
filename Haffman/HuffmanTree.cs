using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Huffman
{
    public class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

        public void Build(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Frequencies.ContainsKey(input[i]))
                {
                    Frequencies.Add(input[i], 0);
                }

                Frequencies[input[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in Frequencies)
            {
                nodes.Add(new Node { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                var orderedNodes = nodes.OrderBy(node => node.Frequency).ToList();

                if (orderedNodes.Count >= 2)
                {
                    var takenNodes = orderedNodes.Take(2).ToList();

                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = takenNodes[0].Frequency + takenNodes[1].Frequency,
                        Left = takenNodes[0],
                        Right = takenNodes[1]
                    };

                    nodes.Remove(takenNodes[0]);
                    nodes.Remove(takenNodes[1]);
                    nodes.Add(parent);
                }

                Root = nodes.FirstOrDefault();
            }
        }

        public BitArray Encode(string input)
        {
            List<bool> encodedInput = new List<bool>();

            for (int i = 0; i < input.Length; i++)
            {
                List<bool> encodedSymbol = Root.Traverse(input[i], new List<bool>());
                encodedInput.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedInput.ToArray());

            return bits;
        }

        public string Decode(BitArray bits)
        {
            Node current = Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (Node.IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = Root;
                }
            }

            return decoded;
        }
    }
}