using System;
using System.Collections.Generic;
using System.Linq;

namespace T_5_2025
{
    static class Program
    {
        static long[] ReadLongs() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var s = ReadLongs()[1];
            var a = ReadLongs();
            var res = 0L;
            for(var l = 0; l < a.Length; ++l)
                for (var r = l; r < a.Length; ++r)
                    res += Do(a, s, l, r);
            Console.WriteLine(res);
            //Console.WriteLine(NotSquare(a,s));
        }

        static long Do(long[] a, long s, int l, int r)
        {
            var res = 0L;
            var sum = 0L;
            for (; l <= r; ++l)
            {
                if (sum + a[l] <= s)
                {
                    sum += a[l];
                    continue;
                }
                ++res;
                sum = a[l];
            }
            if (sum <= s) ++res;
            return res;
        }

        static long NotSquare(long[] a, long s) // doesn't work (>_<)
        {
            var result = 0L;
            var sum = 0L;
            for (var (l, r) = (0, 0); r < a.Length; ++r)
            {
                var f = false;
                sum += a[r];
                while (l < a.Length && sum > s)
                {
                    sum -= a[l];
                    ++l;
                }
                
            }
            // Ну так я могу найти индексы максимальных хороших отрезков
            // Далее не очевидно...
            return result;
        }
        
    }
}