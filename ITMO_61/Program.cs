using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_61
{
    //TODO: Not working
    static class Program
    {
        static void Main(string[] args)
        {
            var c = Console.ReadLine().Split(" ").Select(long.Parse).ToArray()[1];
            var s = Console.ReadLine();
            int a = 0, b = 0;
            var rude = 0L;
            var res = 0;
            for (var (l, r) = (0, 0); r < s.Length; ++r)
            {
                if (s[r] == 'a') a++;
                if (s[r] == 'b')
                {
                    rude += a;
                    b += 1;
                }
                
                while (rude > c)
                {
                    if (s[l] == 'a')
                    {
                        rude -= b;
                        a -= 1;
                    }

                    if (s[l] == 'b') b -= 1;
                    l++;
                }

                if (rude <= c) res = Math.Max(res, r - l + 1);
            }
            Console.WriteLine(res);
        }
    }
}