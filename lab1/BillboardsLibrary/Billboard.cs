using System;
using System.Collections.Generic;

namespace BillboardsLibrary
{
    public class Billboard
    {
        private int[] position;
        private int[] profit;

        public Billboard(int[] position, int[] profit)
        {
            this.position = position;
            this.profit = profit;
        }

        public List<int> FindMaxProfit()
        {
            var sum = new List<int> { 0, profit[0] };

            for (int i = 2; i < position.Length; i++)
            {
                sum.Add(Math.Max(profit[i] + sum[FindNextBillboard(position, i)], sum[i - 1]));
            }

            return sum;
        }

        private int FindNextBillboard(int[] lhs, int rhs)
        {
            var current = lhs[rhs];
            rhs--;
            while (current - lhs[rhs] < 5)
                rhs--;
            
            return rhs;
        }
    }
}
