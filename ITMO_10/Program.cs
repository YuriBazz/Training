using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_10
{

    class Program
    {
        static void Main(string[] args)
        {
            for (var q = int.Parse(Console.ReadLine()); q > 0; --q)
            {
                var s = Console.ReadLine();
                var t = Console.ReadLine();
                if (s.Length != t.Length) Console.WriteLine(-1);
                else Console.WriteLine(Do(ZFunction(t + '$' + s + s), t));
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
        
        static void Write(int[] z)
        {
            foreach (var t in z)
            {
                Console.Write(t + " ");
                
            }
            Console.Write("\n");
        }

        static int Do(int[] z, string t)
        {
            for (var i = t.Length; i < z.Length; ++i)
                if (z[i] == t.Length)
                    return i - t.Length - 1;
            return -1;
        }
    }
}