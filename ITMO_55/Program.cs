using System;
using System.Collections.Generic;
using System.Linq;
namespace ITMO_55
{
    //TODO: Не работает
    static class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            var array = Console.ReadLine().Split(" ").Select(ulong.Parse).ToArray();
            var res = int.MaxValue;
            var gcd = 0UL;
            for (var (l, r) = (0, 0); r < array.Length; ++r)
            {
                var check = false;
                gcd = GCD(gcd, array[r]);
                while (gcd == 1)
                {
                    check = true;
                    ++l;
                    gcd = 0;
                    for (var i = l; i < r + 1; ++i)
                        gcd = GCD(gcd, array[i]);
                }
                if (check) res = Math.Min(res, r - l + 2);
            }
            Console.WriteLine(res == int.MaxValue ? -1 : res);
        }

        static ulong GCD(ulong a, ulong b)
        {
            if (b == 0) return a;
            return GCD(b, a % b);
        }

        static ulong SCM(ulong a, ulong b)
        {
            return a * b / GCD(a, b);
        }
    }
}