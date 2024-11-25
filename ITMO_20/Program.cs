using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_20
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            var array = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).ToArray();
            var r = new List<int>();
            for (var q = int.Parse(Console.ReadLine()); q > 0; --q)
            {
                var token = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var (a, b) = (token[0], token[1]);
                r.Add(array.BS(b +1,1) - array.BS(a - 1,0) - 1);
            }
            Console.WriteLine(string.Join(" ", r));
        }

        static int BS(this int[] array, int value, int side)
        {
            int l = -1, r = array.Length;
            while (r > l + 1)
            {
                int m = l + (r - l) / 2;
                if (side == 0 ? array[m] <= value : array[m] < value)
                    l = m;
                else r = m;
            }
            return side == 0 ? l : r;
        }
    }
}