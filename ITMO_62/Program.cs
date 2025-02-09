using System;
using System.Collections.Generic;
using System.Linq;

// TODO: Not working
namespace ITMO_62
{
    static class Program
    {
        static List<int> Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToList();
        
        static void Main(string[] args)
        {
            var token = Read();
            var (s, A, B) = (token[2], token[3], token[4]);
            var all = Read().Select(x => (x, A)).ToList();
            all.AddRange(Read().Select(x => (x,B)));
            all.Sort(Compare);
            var res = 0L;
            for (var (l, r, w, c) = (0, 0, 0L, 0L); r < all.Count; ++r)
            {
                w += all[r].A;
                c += all[r].x;
                while (w > s)
                {
                    w -= all[l].A;
                    c -= all[l++].x;
                }

                res = Math.Max(res, c);
            }
            
            Console.WriteLine(res);
            return;

            int Compare((int, int) x, (int, int) y)
            {
                if (x.Item1 == y.Item1) return x.Item2.CompareTo(y.Item2);
                return x.Item1.CompareTo(y.Item1);
            }
        }
    }
}