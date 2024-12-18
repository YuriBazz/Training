using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_36
{

    static class Program
    {
        static (long, long) ReadLongs()
        {
            var line = Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
            return (line[0], line[1]);
        }
        
        static void Main(string[] args)
        {
            var token = Console.ReadLine().Split(" ");
            var n = int.Parse(token[0]);
            var k = long.Parse(token[1]);
            var list = new List<(long, long)>();
            long left =  2 * (long)1e9, right = - 2 * (long)1e9;
            for (; n > 0; --n)
            {
                var (l,r) = ReadLongs();
                left = Math.Min(left, l);
                right = Math.Max(right, r);
                list.Add((l, r));
            }

            left--;
            right++;
            while (right > left + 1)
            {
                var x = left + (right - left) / 2;
                if (list.Select(item => Count(x, item)).Sum() <= k)
                    left = x;
                else right = x;
            }
            Console.WriteLine(left);
        }

        static long Count(long x, (long, long) section)
        {
            if (x <= section.Item1) return 0;
            if (x > section.Item2) return section.Item2 - section.Item1 + 1;
            return x - section.Item1;
        }
    }
}