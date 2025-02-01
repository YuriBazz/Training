using System;
using System.Collections.Generic;
using System.Linq;

namespace T_6_2025
{
    static class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var set = new HashSet<(int, int)>();
            for (var n = int.Parse(Console.ReadLine()); n > 0; --n)
            {
                var token = ReadInts();
                set.Add((token[0], token[1]));
            }

            var m = 1;
            foreach (var point1 in set)
            {
                var temp = new Dictionary<(int, int), int>();
                foreach (var point2 in set)
                {
                    if(point1 == point2) continue;
                    var dx = point2.Item1 - point1.Item1;
                    var dy = point2.Item2 - point1.Item2;
                    if (dx == 0) temp[(0, 1)] = temp.GetValueOrDefault((0, 1), 0) + 1;
                    else
                    {
                        var sgn = dx * dy < 0 ? -1 : 1;
                        var gcd = Math.Abs(GCD(dx, dy));
                        dx = Math.Abs(dx) / gcd;
                        dy = Math.Abs(dy) / gcd;
                        var k = (sgn * dy, dx);
                        temp[k] = temp.GetValueOrDefault(k, 0) + 1;
                    }
                }

                m = Math.Max(m, 1 + temp.Values.Max());
            }

            Console.WriteLine(m <= 2.0 * set.Count / 3 ? set.Count / 3 : set.Count - m);
        }

        static int GCD(int a, int b)
        {
            while (b != 0)
                (a, b) = (b, a % b);
            return a;
        }
    }
}