using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_53
{
    static class Program
    {
        static void Main(string[] args)
        {
            var k = Console.ReadLine().Split(" ").Select(int.Parse).ToArray()[1];
            var array = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            var res = 0L;
            var current = 0;
            var map = new Dictionary<int, int>();
            for (var (l, r) = (0, 0); r < array.Length; ++r)
            {
                if (!map.ContainsKey(array[r]))
                {
                    map[array[r]] = 0;
                }
                ++map[array[r]];
                if (map[array[r]] == 1)
                    ++current;
                while (current > k)
                {
                    --map[array[l]];
                    if (map[array[l]] == 0) --current;
                    ++l;
                }
                res += r - l + 1;
            }
            Console.WriteLine(res);
        }
    }
}