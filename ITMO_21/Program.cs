using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_21
{
    class Program
    {
        private readonly int w, h,n;

        public Program(int w, int h,int n)
        {
            this.w = w;
            this.h = h;
            this.n = n;
        }
        static void Main(string[] args)
        {
            var token = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var p = new Program(token[0], token[1],token[2]);
            Console.WriteLine(p.BS(0, long.MaxValue));
            
        }

        bool Check(long x) => (double)(x / w) * (x / h) >= n;

        long BS(long l, long r) // f(l) = 0, f(r) = 1
        {
            while (r > l + 1)
            {
                var m = l + (r - l) / 2;
                if (Check(m)) r = m;
                else l = m;
            }
            return r;
        }
    }
}