using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_29
{
    static class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        static void Main(string[] args)
        {
            var token = ReadInts();
            var (n, k) = (token[0], token[1]);
            var array = ReadInts();
            long l = 0, r = (long)1e14;
            while (r > l + 1)
            {
                var X = l + (r - l) / 2;
                if (Check(k, array, X)) r = X;
                else l = X;
            }
            Console.WriteLine(r);
        }

        static bool Check(int k, int[] array, long x)
        {
            if (x == 0) return false;
            long s = 0;
            var i = 0;
            var filledContainers = 0;
            
                for (;filledContainers != k && i < array.Length; i++)
                {
                    
                    s += array[i];
                    if (s >= x)
                    {
                        if (s > x)
                            i--;
                        s = 0;
                        filledContainers++;
                    }
                }
            
            return i == array.Length && filledContainers <= k;
        }
    }
}