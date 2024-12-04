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
            long l = 0, r = array.Sum();
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
            var k1 = 0;
            var sum = 0;
            for (var i = 0; i < array.Length; ++i)
            {
                
            }
        }
    }
}