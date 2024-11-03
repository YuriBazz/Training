using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_5
{

    class Program
    {
        static void Main(string[] args)
        {
            var z = ZFunction(Console.ReadLine());
            for(var i = 0; i < z.Length - 1; ++i)
                Console.Write(z[i] + " ");
            Console.Write(z[z.Length - 1]);
        }
        
        static int[] ZFunction(string s)
        {
            var z = new int[s.Length];
            int l = 0, r = 0;
            for (var i = 1; i < s.Length; ++i)
            {
                if (r >= i)
                    z[i] = Math.Min(z[i - l], r - i + 1);

                while (z[i] + i < s.Length && s[z[i]] == s[z[i] + i])
                    z[i]++;
                
                if (i + z[i] - 1 > r)
                {
                    l = i;
                    r = z[i] + i - 1;
                }
            }
            return z;
        }
    }
}