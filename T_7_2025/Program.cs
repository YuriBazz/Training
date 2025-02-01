using System;
using System.Collections.Generic;
using System.Linq;

namespace T_7_2025
{
    static class Program
    {
        private const long MOD = 998244353;
        static long[] ReadLongs() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var k = ReadInts()[1];
            var array = ReadLongs();
            var sums = Sums(array, k);
            for(var p = 1; p <= k; ++p)
            {
                var cnk = Cnk(p);
                var f = 0L;
                for (var m = 0; m <= p; ++m)
                {
                    f = f + (cnk[m] * (sums[m] * sums[p - m] % MOD - sums[p]) / 2 % MOD);
                }
                Console.WriteLine(f);
            }
        }
        
        static long[] Cnk(long p)
        {
            var temp = new long[p + 1];
            temp[0] = 1;
            for (var i = 1; i < p + 1; ++i)
                temp[i] = (temp[i - 1] * (p - i + 1) / i) % MOD;
            return temp;
        }

        static long[] Sums(long[] array, int k)
        {
            var temp = (long[])array.Clone();
            var result = new long[k + 1];
            result[0] = array.Length;
            for (var i = 1; i < k + 1; ++i)
            {
                var sum = 0L;
                for (var j = 0; j < array.Length; ++j)
                {
                    sum += array[j];
                    array[j] = (array[j] * temp[j]) % MOD;
                }

                result[i] = sum % MOD;
            }

            return result;
        }
    }
}