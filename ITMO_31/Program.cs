using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_31
{

    static class Program
    {
        static int[] ReadInts()
        {
            return Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        }

        static long[] ReadLongs()
        {
            return Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        }
        
        static void Main(string[] args)
        {
            var token = ReadInts();
            var (n, k) = (token[0], token[1]);
            var array = ReadLongs();
            long l = 0, r = (long)1e13;
            while (r > l + 1)
            {
                var m = l + (r - l) / 2;
                if (Check(array, k, m)) l = m;
                else r = m;
            }
            Console.WriteLine(l);
        }

        static bool Check(long[] array, int k, long m)
        {
            
            for(var t = 0; t < array.Length; ++t)
            {
                var i = t;
                var c = 1;
                while (i < array.Length)
                {
                    var f = true;
                    for (var j = i + 1; j < array.Length; ++j)
                        if (array[j] - array[i] >= m)
                        {
                            i = j;
                            f = false;
                            c++;
                            break;
                        }

                    if (f) break;
                }
                if (c == k) return true;
            }

            return false;
        }
    }
}