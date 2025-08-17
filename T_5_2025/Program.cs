using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace T_5_2025
{
    static class Program
    {
        static string[] Read() => Console.ReadLine().Select(x => new string(new[] {x})).ToArray();
        static long[] ReadL() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray(); 
        static int[] ReadI() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray(); 
        static (int val, int ind)[] ReadII(int i = 0) =>
            Console.ReadLine().Split(" ").Select(x => (int.Parse(x), i++)).ToArray();
        static (long val, int ind)[] ReadLI(int i = 0) =>
            Console.ReadLine().Split(" ").Select(x => (long.Parse(x), i++)).ToArray();
        static void Write<T>(IEnumerable<T> a) => Console.WriteLine(string.Join(" ", a));
        
        static void Main(string[] args)
        {
            var GENERIC_VARIABLE_NAME = ReadI()[0];
            for (; GENERIC_VARIABLE_NAME > 0; --GENERIC_VARIABLE_NAME)
            {
                var p = ReadI();
                int n = p[0], q = p[1];
                var gr = new int[n + 1, n + 1];
                while (q-- > 0)
                {
                    p = ReadI();
                    gr[p[0] - 1, p[1]] = 1;
                    gr[p[1], p[0] - 1] = 1;
                }

                var l = Temp(gr, n);
                Console.WriteLine(l > 0 ? $"Yes\n{l}" : "No");
            }
        }

        static int Temp(int[,] gr, int n)
        {
            // positive - straight; negative - back
            var q = new Queue<(int ver, int len, int dir)>();
            q.Enqueue((0,0, 1));
            while (q.Count > 0)
            {
                int cur = q.Peek().ver, dir = q.Peek().dir, len = q.Peek().len;
                if (cur == n) return len;
                q.Dequeue();
                if (dir > 0)
                {
                    for (var x = cur + 1; x < n + 1; ++x)
                    {
                        if (gr[cur, x] == 1)
                        {
                            q.Enqueue((x, len + 1, -dir));
                            gr[cur, x] = 0;
                            gr[x, cur] = 0;
                        }
                    }
                }
                else
                {
                    for (var x = cur - 1; x > - 1; --x)
                    {
                        if (gr[cur, x] == 1)
                        {
                            q.Enqueue((x, len + 1, -dir));
                            gr[cur, x] = 0;
                            gr[x, cur] = 0;
                        }
                    }
                }
            }

            return -1;
        }
    }
}