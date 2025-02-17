using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_62
{
    static class Program
    {
        static List<int> Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToList();
        
        static void Main(string[] args)
        {
            var token = Read();
            var (s, A, B) = ((long)token[2], token[3], token[4]);
            var a = Read().Select(x => (x, A)).ToList();
            a.Sort();
            var b = Read().Select(x => (x,B)).ToList();
            b.Sort((x,y) => -x.CompareTo(y));
            a.AddRange(b);
            var res = 0L;
            for (var (l, r, c, w) = (0, 0, 0L, 0L); r < a.Count; r++)
            {
                c += a[r].x;
                w += a[r].A;
                while (w > s)
                {
                    w -= a[l].A;
                    c -= a[l++].x;
                }

                if (w <= s) res = Math.Max(res, c);
            }
            Console.WriteLine(res);
        }
    }
}