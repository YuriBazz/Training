using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_56
{
    //  TL on 31th test -> WA on 9th test (???????) -> TL on 25th test -> TL on 99th test
    static class Program
    {
        static void Main(string[] args)
        {
            var p = Console.ReadLine().Split(" ").Select(long.Parse).ToArray()[1];
            var a = new List<int>();
            var all = 0L;
            foreach (var num in Console.ReadLine().Split(" "))
            {
                var t = long.Parse(num);
                a.Add((int)t);
                all += t;
            }
            var sum = 0L;
            var min = long.MaxValue;
            var k = -1;
            for (var (l, r) = (0, 0L); l < a.Count; ++r)
            {

                if ((p - sum) / all > 1)
                {
                    var c = (p - sum) / all;
                    sum += c * all;
                    r += c * a.Count;
                    if (sum >= p)
                    {
                        sum -= all;
                        r -= a.Count;
                    }
                }
                
                sum += a[(int)(r % a.Count)];
                while (l < a.Count && sum - a[l] >= p)
                    sum -= a[l++];
                if (l < a.Count && sum >= p && r - l + 1 < min)
                {
                    min = r - l + 1;
                    k = l;
                }
            }
            Console.WriteLine(k + 1 + " " + min);
        }
        
    }
}