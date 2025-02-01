using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_41
{
    class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                Console.ReadLine();
                var nmk = ReadInts();
                int n = nmk[0], m = nmk[1];
                bool cycle = false, path = false;
                var sq = ReadInts();
                if (sq[0] == sq[^1]) cycle = true;
                else path = true;
                var graph = new HashSet<(int, int)>();
                for (; m > 0; --m)
                {
                    var token = ReadInts();
                    graph.Add((token[0], token[1]));
                    graph.Add((token[1], token[0]));
                }

                var visited = new HashSet<int>();
                if (cycle)
                {
                    bool simple = true, none = false;
                    
                    visited.Add(sq[0]);
                    for (var i = 1; i < sq.Length; ++i)
                    {
                        if (visited.Contains(sq[i]) && i != sq.Length-1) simple = false;
                        if (graph.Contains((sq[i - 1], sq[i])))
                            visited.Add(sq[i]);
                        else
                        {
                            Console.WriteLine("none");
                            none = true;
                            break;
                        }
                    }

                    if (!none)
                        Console.WriteLine(simple ? "simple cycle" : "cycle");
                }
                else
                {
                    bool simple = true, none = false;
                    visited.Add(sq[0]);
                    for (var i = 1; i < sq.Length; ++i)
                    {
                        if (visited.Contains(sq[i])) simple = false;
                        if (graph.Contains((sq[i - 1], sq[i])))
                            visited.Add(sq[i]);
                        else
                        {
                            Console.WriteLine("none");
                            none = true;
                            break;
                        }
                    }
                    if(!none) Console.WriteLine(simple ? "simple path" : "path");
                }
            }
        }
    }
}