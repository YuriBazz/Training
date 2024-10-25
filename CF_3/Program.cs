using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
namespace CF_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t = ReadInts();

            var cats = ReadInts();
            var r = new HashSet<int>();
            var q = new Queue<(int,int)>();
            var tree = new Tree();
            for(int i = 0; i < t[0] -1; i++)
            {
                var p = ReadInts();
                tree.AddEdge(p[0], p[1]);
            }

            q.Enqueue((1, cats[0]));
            while (q.Count > 0)
            {
                var v = q.Dequeue();
                if(tree.IsLeaf(v.Item1) && v.Item2 <= t[1])
                { r.Add(v.Item1); continue; }
                foreach(var n in tree.GetIncedents(v.Item1))
                {
                    var c = cats[n - 1] == 0 ? 0 : v.Item2 + 1;
                    if (c <= t[1]) q.Enqueue((n, c));
                }
            }
            Console.WriteLine(r.Count);
        }

        static int[] ReadInts()
        {
            return Console.ReadLine().Split().Select(int.Parse).ToArray();
            }
        }


            public class Tree
            {
            private readonly Dictionary<int, List<int>> Vertecies = new Dictionary<int, List<int>>();
            public void AddEdge(int from, int to)
            {
            if (!Vertecies.ContainsKey(from)) Vertecies[from] = new List<int>();
            Vertecies[from].Add(to);
            }

            public bool IsLeaf(int vertex) => !Vertecies.ContainsKey(vertex);

            public IEnumerable<int> GetIncedents(int vertex) => Vertecies[vertex];
            }
}
