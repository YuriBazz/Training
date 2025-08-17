using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace T_6_2025
{
    static class Program
    {
        static long[] Read() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        public static long[] ReadSecondLineInts(string path)
        {
            var lines = File.ReadAllLines(path);
            // Берём вторую строку (индекс 1), разбиваем по ", " и парсим
            return lines[1]
                .Split(new[] {" "}, StringSplitOptions.None)
                .Select(long.Parse)
                .ToArray();
        }
        
        static void Main(string[] args)
        {
            Console.ReadLine();
            var res = 0L;
            var a = ReadSecondLineInts(@"C:\Users\Георгий\OneDrive\Рабочий стол\input.txt");
            var prev = new int[a.Length];
            var next = new int[a.Length];
            for (var i = 0; i < a.Length; ++i)
            {
                next[i] = i + 1;
                prev[i] = i - 1;
            }

            next[^1] = -1;
            var q = new PriorityQueue<(long val, int i, int j), long>();
            for(var i = 0; i < a.Length - 1; ++i)
                q.Enqueue((Math.Abs(a[i] - a[i + 1]), i, i + 1), -1 * Math.Abs(a[i] - a[i + 1]));
            var removed = new bool[a.Length];
            while (q.Count > 0)
            {
                var curr = q.Dequeue();
                if(removed[curr.i] || removed[curr.j] || next[curr.i] != curr.j || prev[curr.j] != curr.i) continue;
                res += curr.val;
                removed[curr.i] = true;
                removed[curr.j] = true;
                var l = prev[curr.i];
                var r = next[curr.j];
                if (l != -1) next[l] = r;
                if (r != -1) prev[r] = l;
                if(l != -1 && r != -1)
                    q.Enqueue((Math.Abs(a[l] - a[r]), l, r), -1 * Math.Abs(a[l] - a[r]));
            }
            Console.WriteLine(res);
        }
        
    }
}