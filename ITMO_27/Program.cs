using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_27
{

    static class Program
    {
        static void Main(string[] args)
        {
            var k = int.Parse(Console.ReadLine());
            var n = int.Parse(Console.ReadLine());
            var a = new int[n];
            for (var i = 0; i < n; ++i)
                a[i] = int.Parse(Console.ReadLine());
            Console.WriteLine(BS(a,k));
        }

        static long BS(int[] a, int k)
        {
            long l = 1, r = 1 +  50 * (long)1e9 / 2;
            while (r > l + 1)
            {
                var m = l + (r - l) / 2;
                if (Check(a, k, m)) l = m;
                else r = m;
            }

            return l;
        }

        static bool Check(int[] a, int k, long m)
        {
            var p = m * k;
            foreach (var x in a)
            {
                p -= Math.Min(m, x);
            }

            return p <= 0;
        }
    }
}