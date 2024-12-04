using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_24
{

    class Program
    {
        static long[] ReadLongs()
        {
            return Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        }
        
        static void Main(string[] args)
        {
            var token = ReadLongs();
            var (m, n) = (token[0], token[1]);
            long[] t = new long[n], z = new long[n], y = new long[n];
            for (var i = 0; i < n; ++i)
            {
                token = ReadLongs();
                t[i] = token[0];
                z[i] = token[1];
                y[i] = token[2];
            }

            var T = BS(t, z, y, m);
            Console.WriteLine(T);
            for(long i = 0; i < n; ++i)
            {
                var b = Math.Min(BlownBy(t,z,y,T,i), m);
                Console.Write(b + " ");
                m -= b;
            }
        }

        static long BS(long[] t, long[] z, long[] y, long m)
        {
            long l = -1, r = 15000 * 200 + 1; // f(l) = 0, f(r) = 1;
            while (r > l + 1)
            {
                var mid = l + (r - l) / 2;
                if (Check(t, z, y, m, mid))
                    r = mid;
                else l = mid;
            }

            return r;
        }

        static bool Check(long[] t, long[] z, long[] y, long m, long T)
        {
            long sum = 0;
            for (long i = 0; i < t.Length; ++i)
                sum += BlownBy(t, z, y, T, i);
            return sum >= m;
        }

        static long BlownBy(long[] t, long[] z, long[] y, long T, long i)
        {
            return T / (z[i] * t[i] + y[i]) * z[i] + Math.Min(T % (z[i] * t[i] + y[i]) / t[i], z[i]);
        }
    }
}