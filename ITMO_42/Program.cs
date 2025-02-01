using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_42
{
    class Program
    {
        public static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                Console.ReadLine();
                var p = ReadInts();
                var (n, m) = (p[0], p[1]);
                var set = ReadInts().ToHashSet();
                var graph = new Graph(n);
                graph.MakeGraph(m);
                Console.WriteLine(graph.Check(set) ? "YES" : "NO");
            }
        }
    }

    class Graph
    {
        public DisjoinSet CComp;

        public Graph(int n)
        {
            CComp = new DisjoinSet(n);
        }

        public void MakeGraph(int m)
        {
            for (; m > 0; --m)
            {
                var t = Program.ReadInts();
                CComp.Union(t[0],t[1]);
            }
        }

        public bool Check(HashSet<int> set)
        {
            var full = new Dictionary<int, (List<int>, List<int>)>(); // 1 - gr, 2 - set
            for(var i = 0; i < CComp.N; ++i)
            {
                var temp = CComp.Find(i + 1);
                if (!full.ContainsKey(temp))
                    full[temp] = (new List<int>(), new List<int>());
                full[temp].Item1.Add(i+1);
                if (set.Contains(i + 1)) 
                    full[temp].Item2.Add(i+1);
            }

            return full.Keys.Where(key => full[key].Item2.Count != 0).All(key => full[key].Item1.SequenceEqual(full[key].Item2));
        }
    }

    class DisjoinSet
    {
        public int N;
        public int[] parent;
        public int[] rank;

        public DisjoinSet(int n)
        {
            N = n;
            parent = new int[n];
            rank = new int[n];
            for (var i = 0; i < n; ++i)
                parent[i] = i + 1;
        }
        
        public void Union(int from, int to)
        {
            int f = Find(from), t = Find(to);
            if (f == t) return;
            if (rank[f - 1] > rank[t - 1])
                parent[t - 1] = f;
            else
            {
                parent[f - 1] = t;
                if (rank[f - 1] == rank[t - 1])
                    rank[t - 1]++;
            }
        }

        public int Find(int vertex)
        {
            if (vertex != parent[vertex - 1])
                parent[vertex - 1] = Find(parent[vertex - 1]);
            return parent[vertex - 1];
        }
    }
}