using System.Collections.Generic;
using System.Linq;
using System;

namespace ITMO_22
{

    class Program
    {
        static void Main(string[] args)
        {
            var nk = Console.ReadLine().Split().Select(double.Parse).ToArray();
            var list = new List<double>();
            double l = 0, r = 1e8;
            for (var i = 0; i < nk[0]; ++i)
            {
                var t = double.Parse(Console.ReadLine());
                list.Add(t);
                r = Math.Max(r, t);
            }

            r *= 2;
            for (var i = 0; i < 100; ++i) // f(l) = 1, f(r) = 0
            {
                var m = l + (r - l) / 2;
                if (GoodLength(m, nk[1], list)) l = m;
                else r = m;
            }

            Console.WriteLine(l);
        }

        static bool GoodLength(double x, double k, List<double> r)
            => r
                .Select(l => (int)(l / x))
                .Sum() >=k;
    }
}