using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace T_2_2025
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
                var s = Console.ReadLine();
                List<int> i = new(), j = new(), k = new();
                for (var x = 0; x < s.Length; ++x)
                {
                    if (s[x] == 'a') i.Add(x);
                    if (s[x] == 'b') j.Add(x);
                    if (s[x] == 'c') k.Add(x);
                }

                var res = 0L;
                for (var x = 0; x < k.Count; ++x)
                {
                    var t = Temp(j, k[x]);
                    if(t == 0) continue;
                    var p = Temp(i, j[t - 1]);
                    res += t * p;
                }
                Console.WriteLine(res);
            }
        }

        static int Temp(List<int> a, int v)
        {
            int l = -1, r = a.Count;
            while (r - l > 1)
            {
                var m = l + (r - l) / 2;
                if (a[m] < v) l = m;
                else r = m;
            }
            return l + 1;
        }
    }
}