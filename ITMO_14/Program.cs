using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_14
{

    class Program
    {
        static void Main(string[] args)
        {
            for (var q = int.Parse(Console.ReadLine()); q > 0; --q)
            {
                var s = Console.ReadLine();
                var t = Console.ReadLine();
                var st = Do(ZFunction(s + '$' + t), s, t);
                var ts = Do(ZFunction(t + '$' + s), t, s);
                Console.WriteLine(st.Length < ts.Length ? st : ts);
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
            foreach (var t in z.Skip(0))
            {
                Console.Write(t + " ");
                
            }
            Console.Write("\n");
        }

        static string Do(int[] z, string s, string t) // z = s + $ + t
        {
            for (var i = 0; i < t.Length; ++i)
            {
                if (z[i + s.Length + 1] == s.Length) return t;
                if (z[i + s.Length + 1] == t.Length - i) return t + s.Substring(z[i + s.Length + 1]);
            }
            return s + t;
        }
    }
}