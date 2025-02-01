using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_50
{
    static class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine().Split(" ").Select(long.Parse).ToArray()[1];
            var array = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            var res = int.MaxValue;
            var sum = 0L;
            for (var (l, r) = (0, 0); r < array.Length; ++r)
            {
                sum += array[r];
                while (sum - array[l] >= s)
                {
                    sum -= array[l];
                    ++l;
                }
                if(sum >= s)
                    res = Math.Min(res, r - l + 1);
            }
            Console.WriteLine(res == int.MaxValue ? -1 : res);
        }
    }
}