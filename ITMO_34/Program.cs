using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ITMO_34
{
    static class Program
    {
        static void Main(string[] args)
        {
            var token = ReadInts();
            var net = new Net(token[0]);
            for (var i = 0; i < token[1]; ++i)
            {
                var line = ReadInts();
                net.AddEdge(line[0],line[1],line[2]);
            }

            double left = -1, right = 101;
            for (var iter = 0; iter < 1e3; ++iter)
            {
                var X = left + (right - left) / 2;
                if (net.Check(X)) right = X;
                else left = X;
            }
            net.Path.Write();
            
        }

        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    }

    class Net
    {
        public Dictionary<int, List<(int, int)>> Edges = new Dictionary<int, List<(int, int)>>();
        public int N;
        public PathNode? Path = null;

        public Net(int n)
        {
            N = n;
        }

        public void AddEdge(int a, int b, int c)
        {
            if (!Edges.ContainsKey(b))
                Edges[b] = new List<(int, int)>();
            Edges[b].Add((a,c));
        }

        public bool Check(double X)
        {
            if (Path != null && Path.CurrentSum - Path.EdgesCount * X <= 0)
                return true;
            var visited = new HashSet<int>();
            for (var queue = new Queue<PathNode>([new PathNode(N)]); queue.Count > 0;)
            {
                var current = queue.Dequeue();
                foreach (var nextPair in Edges[current.CurrentNumber])
                {
                    if(visited.Contains(nextPair.Item1))
                        continue;
                    var newPath = 
                        new PathNode(nextPair.Item1,
                            current.CurrentSum + nextPair.Item2, 
                            current.EdgesCount + 1, current);
                    if (nextPair.Item1 == 1)
                    {
                        Path = newPath;
                        return Path.CurrentSum - Path.EdgesCount * X <= 0;
                    }
                    queue.Enqueue(newPath);
                }
            }
            return false;
        }
    }

    
    class PathNode 
    {
        public PathNode? Next = null;
        public int CurrentNumber;
        public int CurrentSum;
        public int EdgesCount;

        public PathNode(int currentNumber, int currentSum = 0, int edgesCount = 0, PathNode next = null)
        {
            Next = next;
            CurrentNumber = currentNumber;
            CurrentSum = currentSum;
            EdgesCount = edgesCount;
        }

        public void Write()
        {
            Console.WriteLine(EdgesCount);
            for (var iter = this; iter != null;)
            {
                Console.Write(iter.CurrentNumber + " ");
                iter = iter.Next;
            }
        }
    }
}