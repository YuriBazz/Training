using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_52
{
    static class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine().Split(" ").Select(long.Parse).ToArray()[1];
            var array = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            long res = 0, sum = 0;
            for (var (l,r) = (0,0); r < array.Length; ++r)
            {
                sum += array[r];
                while (sum - array[l] >= s)
                {
                    sum -= array[l];
                    ++l;
                }

                if (sum >= s) res += l + 1;
            }
            Console.WriteLine(res);
        }
    }
}