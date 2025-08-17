using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace T_1_2025
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
                var r = ReadI();
                int n = r[0], m = r[1];
                Console.WriteLine(n - 7 > 0 ? n - 7 : m + 7 );
            }
        }
    }
}