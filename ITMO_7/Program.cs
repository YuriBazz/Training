﻿using System;
using System.Collections.Generic;
using System.Linq;
 
namespace ITMO_7
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var t = ReadInts()[0]; t > 0; --t)
            {
                Console.ReadLine();
                var z = ReadInts();
                Console.WriteLine(Do(z));
            }
        }

        static string Do(int[] z)
        {
            var s = "";
            var l = 0;
            var let = 'a';
            var j = 0;
            for (var i = 0; i < z.Length; ++i)
            {
                if (z[i] > z.Length - i)
                    return "!";
                if (z[i] == 0 && l == 0)
                {
                    if (let <= 'z')
                    {
                        s += let;
                        let++;
                    }
                    else
                        s += (char)(let - 1);
                }

                if (z[i] > l)
                {
                    j = 0;
                    l = z[i];
                }

                if (l > 0)
                {
                    s += s[j];
                    j++;
                    l--;
                }
            }

            var z1 = ZFunction(s);
            return z1.SequenceEqual(z) ? s : "!";
        }
 
        static int[] ReadInts()
        {
            return Console.ReadLine().Split().Select(int.Parse).ToArray();
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
            foreach (var i  in z)
            {
                Console.Write(i + " ");
            }
            Console.Write("\n");
        }
    }
}