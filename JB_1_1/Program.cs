using System;
using System.Collections.Generic;
using System.Linq;

namespace JB_1_1
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                var token = Read();
                var (a, b, c) = (token[0], token[1], token[2]);
                if(a + c == b + c) Console.WriteLine(((long)a + b + c) % 2 == 0 ? "Second" : "First");
                else Console.WriteLine(a + c > b + c ? "First" : "Second");
            }
        }
    }
}