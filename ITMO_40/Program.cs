using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_40
{
    class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        static void Print(int[] array) => Console.WriteLine(string.Join(" ", array));
        
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                Console.ReadLine();
                var nm = ReadInts();
                var vertexes = new int[nm[0]];
                for (var m = nm[1]; m > 0; --m)
                {
                    var edge = ReadInts();
                    vertexes[edge[0] - 1]++;
                    vertexes[edge[1] - 1]++;
                }
                Print(vertexes);
            }
        }
    }
}