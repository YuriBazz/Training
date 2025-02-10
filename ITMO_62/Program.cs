using System;
using System.Collections.Generic;
using System.Linq;

// TODO: Not working
namespace ITMO_62
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var token = Read();
            var (s, A, B) = (token[2], token[3], token[4]);
            var a = Read().Select(x => (x, A)).ToArray();
            Array.Sort(a);
            var b = Read().Select(x => (x, B)).ToArray();
            Array.Sort(b, (x,y) => -x.CompareTo(y));
            var res = 0L;
            for (var (l, r, w, c) = (0, 0, 0L, 0L); r < Math.Max(a.Length, b.Length); ++r)
            {
                
            }
            
            Console.WriteLine(res);
            var list = new List<int>();
            for(var i = 0L; i < 2e10; ++i)
                list.Add(1);
        }
    }
}