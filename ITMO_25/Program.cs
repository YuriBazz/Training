using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_25
{
    static class Program
    {
        static void Main(string[] args)
        {
            var c = double.Parse(Console.ReadLine());
            double l = 0, r = 1e5;
            for (var i = 0; i < 1e4; ++i)
            {
                var m = l + (r - l) / 2;
                if (m * m + Math.Pow(m, 0.5) >= c) r = m;
                else l = m;
            }
            Console.WriteLine(r);
        }
    }
}