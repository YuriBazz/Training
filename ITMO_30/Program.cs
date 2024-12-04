using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_30
{

    static class Program
    {
        static int[] ReadInts()
        {
            return Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        }
        
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            int[] x = new int[n], v = new int[n];
            var indM = -1;
            for (var i = 0; i < n; ++i)
            {
                var token = ReadInts();
                x[i] = token[0];
                v[i] = token[1];
                if (indM == -1 || Math.Abs(x[i]) > Math.Abs(x[indM]))
                    indM = i;
            }

            double l = 0, r = Math.Abs(x[indM]) * 1.0;
            for (var i = 0; i < 1000; ++i)
            {
                var m = l + (r - l) / 2;
                if (CheckT(x, v, m)) r = m;
                else l = m;
            }
            Console.WriteLine(r);
        }
        
        // |x_i - X| <= Tv_i <=> x_i - Tv_i <= X <= x_i + Tv_i  
        static bool CheckT(int[] x, int[] v, double T)
        {
            double r = double.PositiveInfinity, l = double.NegativeInfinity;
            for (var i = 0; i < x.Length; ++i)
            {
                r = Math.Min(r, x[i] + T * v[i]);
                l = Math.Max(l, x[i] - T * v[i]);
            }
            return r >= l;
        }
    }
}