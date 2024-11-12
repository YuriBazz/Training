using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_13
{

    class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine();
            var t = Console.ReadLine();
            if(s.Length != t.Length){Console.WriteLine("No"); return;}
            var z = ZFunction(t + '$' + s);
            var r = int.MinValue;
            var j = 0;
            for (var i = 0; i < s.Length; ++i)
            {
                if (z[i + s.Length + 1] == s.Length - i) 
                {
                    if (t.Substring(s.Length - i) == new string(s.Substring(0, i).Reverse().ToArray()))
                    {
                        Console.WriteLine("Yes");
                        Console.WriteLine(i);
                        return;
                    }
                }
            }
            Console.WriteLine("No");
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
    }
}