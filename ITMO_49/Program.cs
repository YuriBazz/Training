using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_49
{
    static class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine().Split(" ").Select(long.Parse).ToArray()[1];
            var array = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            var res = 0;
            var sum = 0L;
            for (var (l,r) = (0,0); r < array.Length; ++r)
            {
                sum += array[r];
                while (sum > s)
                {
                    sum -= array[l];
                    ++l;
                }

                res = Math.Max(res, r - l + 1);
            }
            Console.WriteLine(res);
        }
    }
}