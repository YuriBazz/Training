﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_11
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var q = int.Parse(Console.ReadLine()); q > 0; --q)
            {
                var z = ZFunction(Console.ReadLine());
                var count = new int[z.Length + 1];
                foreach (var i in z)
                {
                    count[i]++;
                }

                var g = new int[z.Length + 1];
                g[z.Length] = count[z.Length];
                for (var i = z.Length - 1; i >= 0; --i)
                {
                    g[i] = g[i + 1] + count[i];
                }
                Write(g);
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
            z[0] = z.Length;
            return z;
        }
        
        static void Write(int[] z)
        {
            foreach (var t in z.Skip(1))
            {
                Console.Write(t + " ");
                
            }
            Console.Write("\n");
        }
    }
}