using System;
using System.Collections.Generic;
using System.Linq;

namespace JB_1_F
{
    static class Program
    {
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                var n = int.Parse(Console.ReadLine());
                var res = 0L;
                for (var c = 1; c <= n; ++c)
                {
                    var b = BS(c);
                    var a = BS(b,c);
                    res += a * a + b * b == c * c ? 1 : 0;
                }
                Console.WriteLine(res);
            }
        }
        
        static long BS(long c)
        {
            long l = 0, r = c;
            while (r > l + 1)
            {
                var b = l + (r - l) / 2;
                if (c * c - c <= b * b + b) l = b;
                else r = b;
            }
            return l;
        }

        static long BS(long b, long c)
        {
            long l = 0, r = c;
            while (r > l + 1)
            {
                var a = l + (r - l) / 2;
                if (a * a <= c * c - b * b) l = a;
                else r = a;
            }

            return l;
        }
    }
}