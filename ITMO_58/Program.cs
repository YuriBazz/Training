using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_58
{
    //  NB: I hate longs
    static class Program
    {
        static void Main(string[] args)
        {
            var R = Console.ReadLine().Split(" ").Select(int.Parse).ToArray()[1];
            var d = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            var sum = 0L; 
            for (var (l, r) = (0, 0);  l < d.Length; ++l)
            {
                while (r < d.Length && d[r] - d[l] <= R) r++;
                sum +=  (long)d.Length - r;
            }
            Console.WriteLine(sum);
        }
    }
}