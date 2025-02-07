using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_57
{
    static class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine().Split(" ").Select(long.Parse).ToArray()[1];
            var a = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            var res = 0L;
            for (var (l, r, sum) = (0, 0, 0L); l < a.Length; ++l)
            {
                while (r < a.Length && sum + a[r] <= s)
                {
                    sum += a[r];
                    res += (long)(r - l + 1) * (r - l + 2) / 2;
                    ++r;
                }

                sum -= a[l];
            }
            Console.WriteLine(res);
        }
    }
}