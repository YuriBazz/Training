using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_8
{

    class Program
    {
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; t--)
            {
                var s = Console.ReadLine();
                var z = ZFunction(s);
                var f = true;
                for (var i = 0; i < s.Length; i++)
                {
                    if (z[i] + i == s.Length)
                    {
                        Console.WriteLine(s.Substring(0,i));
                        f = false;
                        break;
                    }
                }
                if(f) Console.WriteLine(s);
            }
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

        static int[] ReadInts()
        {
            return Console.ReadLine().Split().Select(int.Parse).ToArray();
            // x => int.Parse(x)
        }
    }
}