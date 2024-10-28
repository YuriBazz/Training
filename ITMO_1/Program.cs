using System;
using System.Linq;
using System.Collections.Generic;

namespace ITMO_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; ++i)
            {
                var temp = Console.ReadLine();
                var s = temp + '$' + new string(temp.Reverse().ToArray());
                var z = ZFunction(s);
                var max = int.MinValue;
                for(var j = temp.Length + 1; j < z.Length; ++j)
                    if (z[j] == temp.Length - j + temp.Length + 1)
                        max = Math.Max(max, z[j]);
                Console.WriteLine(max);
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
    }
}