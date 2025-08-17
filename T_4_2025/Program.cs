using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace T_4_2025
{
    static class Program
    {
        static string[] Read() => Console.ReadLine().Select(x => new string(new[] {x})).ToArray();
        static long[] ReadL() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray(); 
        static int[] ReadI() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray(); 
        static (int val, int ind)[] ReadII(int i = 0) =>
            Console.ReadLine().Split(" ").Select(x => (int.Parse(x), i++)).ToArray();
        static (long val, int ind)[] ReadLI(int i = 0) =>
            Console.ReadLine().Split(" ").Select(x => (long.Parse(x), i++)).ToArray();
        static void Write<T>(IEnumerable<T> a) => Console.WriteLine(string.Join(" ", a));
        
        static void Main(string[] args)
        {
            var GENERIC_VARIABLE_NAME = 1;
            for (; GENERIC_VARIABLE_NAME > 0; --GENERIC_VARIABLE_NAME)
            {
                var p = ReadI();
                int n = p[0], k = p[1];
                var a = ReadI();
                var dp = new int[n + 1, k + 1];
                for (var i = 1; i < n + 1; ++i)
                {
                    for (var j = 0; j < k + 1; ++j)
                    {
                        dp[i, j] = a[i - 1];
                        int m1 = -101, m2 = Math.Max(dp[i - 1, j], i - 2 > -1 ? dp[i - 2, j] : 0);
                        if(j > 0)
                            for (var x = 0; x < i; ++x) m1 = Math.Max(dp[x, j - 1], m1);
                        dp[i, j] += Math.Max(m1, m2);
                    }
                }
                Console.WriteLine(dp[n,k]);
            }
        }
    }
}