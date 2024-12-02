using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace ITMO_23
{

    class Program
    {
        static void Main(string[] args)
        {
            var token = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Console.WriteLine(BS(token[1], token[2], token[0]));
        }

        static bool Check(long time, int x, int y, int n)
        {
            if (time < Min(x, y)) return false;
            return (time - Min(x,y)) / x + (time - Min(x,y)) / y + 1 >= n;

        }

        static long BS(int x, int y, int n) // f(l) = 0, f(r) = 1
        {
            long l = 0, r = n * Min(x,y);
            while (r > l + 1)
            {
                long m = l + (r - l) / 2;
                if (Check(m,x,y, n)) r = m;
                else l = m;
            }
            return r; }
        
    }
}
