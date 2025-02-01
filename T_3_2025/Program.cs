using System;
using System.Collections.Generic;
using System.Linq;

namespace T_3_2025
{
    static class Program
    {
        static long[] ReadLongs() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var token = ReadLongs();
            var m = (int)token[1];
            var days = ReadLongs();
            long first = days[0], second = days[1];
            var min = long.MaxValue;
            Array.Sort(days);
            for (var (l, r) = (0, 0); r < days.Length; ++r)
            {
                var f = false;
                while (l < days.Length && CountGoodPoints(days[l], days[r], days) >= m + 2)
                {
                    ++l;
                    f = true;
                }
                if (f)
                    min = Math.Min(min, Math.Abs(second - days[r]) + Math.Abs(first - days[l - 1]));
            }
            Console.WriteLine(min);
        }

        static int CountGoodPoints(long l, long r, long[] array) => array.BS(r + 1, 1) - array.BS(l - 1, 0) - 1;
        
        static int BS(this long[] array, long value, int side)
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