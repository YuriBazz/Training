using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_26
{

    static class Program
    {
        static void Main(string[] args)
        {
            var t = Console.ReadLine();
            var p = Console.ReadLine();
            var z = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            Console.WriteLine(BS(t,p,z));
        }

        static int BS(string t, string p, int[] z)
        {
            int l = 0, r = t.Length; // f(l) = 1, f(r) = 0;
            while (r > l + 1)
            {
                var m = l + (r - l) / 2;
                if (Check(t, p, z, m)) l = m;
                else r = m;
            }

            return l;
        }

        static bool Check(string t, string p, int[] z, int m)
        {
            var temp = t.Select(x => string.Join("",x)).ToArray();
            for (var i = 0; i < m; ++i)
            {
                temp[z[i] - 1] = "";
            }
            return CheckStrings(string.Join("", temp), p);
        }

        static bool CheckStrings(string t, string p)
        {
            if (t.Length == p.Length) return t == p;
            var pIndexer = 0;
            foreach (var t1 in t)
            {
                if (t1 == p[pIndexer])
                    ++pIndexer;
                if (pIndexer == p.Length)
                    return true;
            }
            return false;
        }
    }
}