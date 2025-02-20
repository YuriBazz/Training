using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace ITMO_64
{

    static class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine().Split(" ").Select(int.Parse).ToList()[1];
            var a = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            var min = 2 * (int)1e5;
            var br = new BitArray(new bool[1001]);
            br.Set(0,true);
            BitArray brT;
            for (var (l, r) = (0, 0); r < a.Length; ++r)
            {
                brT = (BitArray)br.Clone();
                br = br.LeftShift(a[r]).Or(brT);
                if (!br[s]) continue;
                l = r;
                var bl = new BitArray(new bool[1001]);
                bl.Set(0,true);
                BitArray blT;
                while (!bl[s])
                {
                    blT = (BitArray)bl.Clone();
                    bl = bl.LeftShift(a[l--]).Or(blT);
                }
                min = Math.Min(min, r - l);
                r = l + 1;
                br = new BitArray(new bool[1001]);
                br.Set(0, true);
            }
            Console.WriteLine(min == 2 * (int)1e5 ? -1 : min);
        }
    }
}