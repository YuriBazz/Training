using System;
using System.Collections.Generic;  
using System.Linq;

namespace ITMO_37
{

    static class Program
    {
        static void Main(string[] args)
        {
            var token = Console.ReadLine().Split(' ');
            var k =  long.Parse(token[1]);
            /*if (k == 1)
            {
                Console.WriteLine(1);
                return;
            }*/
            var n = int.Parse(token[0]);
            /*for (var i = 0; i < n * n; ++i)
            {
                Console.Write(BS(n,i) + " ");
            }*/
            Console.WriteLine(BS(n, k - 1));
        }
        
        static long BS(int n, long k)
        {
            long left = 1, right = (long)n * n + 1;
            while (right > left + 1)
            {
                var x = left + (right - left) / 2;
                var count = 0L;
                var div = 0L;
                for (var i = 1L; i <= n; ++i)
                {
                    count += Math.Min(x / i, n);
                    if (x % i == 0 && x / i <= n)
                        div++;
                }
                count -= div;
                if (count <= k) left = x;
                else right = x;
            }

            return left;
        }
    }
}