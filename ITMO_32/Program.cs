using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_32
{

    static class Program
    {
        static long[] ReadLongs() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();

        static void Main(string[] args)
        {
            var token = ReadLongs();
            var (n, m, d) = ((int)token[0], (int)token[1], (int)token[2]);
            var adjacencyList = new Dictionary<int, List<(int, long)>>();
            for (var i = 0; i < m; ++i)
            {
                token = ReadLongs();
                int u = (int)token[0], v = (int)token[1];
                if (!adjacencyList.ContainsKey((int)token[1]))
                    adjacencyList[v] = new List<(int, long)>();
                adjacencyList[v].Add((u, token[2]));
            }


            long l = -1, r = (long)1e9 + 1;
            PathItem path = new PathItem(-1);
            while (r > l + 1)
            {
                var mid = l + (r - l) / 2;
                var temp = FindPath(adjacencyList, d, n, mid);
                if (temp.Number != -1)
                {
                    path = temp;
                    r = mid;
                }
                else l = mid;
            }

            if (path.Number == -1)
                Console.WriteLine(-1);
            else
            {
                var t = path.ToArray();
                Console.WriteLine(t.Length - 1);
                Console.WriteLine(string.Join(" ", t));
            }
        }

        static PathItem FindPath(Dictionary<int, List<(int, long)>> adjacencyList, int d, int n, long m)
        {
            var queue = new Queue<PathItem>();

            queue.Enqueue(new PathItem(n));
            while (queue.Count > 0)
            {
                var currentPathItem = queue.Dequeue();
                if (currentPathItem.Edges + 1 > d) continue;

                var curr = currentPathItem.Number;
                if (!adjacencyList.ContainsKey(curr)) continue;

                foreach (var (next, weight) in adjacencyList[curr])
                {
                    if (weight > m) continue;
                        
                    if (next == 1 && currentPathItem.Edges + 1 <= d)
                        return new PathItem(next, currentPathItem.Edges + 1, currentPathItem);
                    queue.Enqueue(new PathItem(next, currentPathItem.Edges + 1, currentPathItem));
                }
            }

            return new PathItem(-1);
        }
    }

    class PathItem : IEnumerable<int>
    {

        public int Number;
        public PathItem Prev;
        public int Edges;

        public PathItem(int number, int count = 0, PathItem prev = null)
        {
            Number = number;
            Prev = prev;
            Edges = count;
        }
        public IEnumerator<int> GetEnumerator()
        {
            var t = this;
            while (t != null)
            {
                yield return t.Number;
                t = t.Prev;
            } 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}