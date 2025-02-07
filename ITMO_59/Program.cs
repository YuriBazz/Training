using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_59
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        static long[] ReadL() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var s = ReadL()[1];
            var w = Read();
            var c = Pref(Read());

            var sum = 0L;
            var max = long.MinValue;
            for (var (l, r) = (0, 0); r < w.Length; ++r)
            {
                sum += w[r];
                while (l < r && sum > s)
                    sum -= w[l++];
                if (sum <= s) max = Math.Max(max, c[r + 1] - c[l]);
            }
            Console.WriteLine(Math.Max(max, 0));
        }

        static long[] Pref(int[] a)
        {
            var res = new long[a.Length + 1];
            for (var i = 1; i < a.Length + 1; ++i)
            {
                res[i] = res[i - 1] + a[i - 1];
            }

            return res;
        }
    }
}