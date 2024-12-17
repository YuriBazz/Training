using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            double left = -1, right = 100;
            for (var iter = 0; iter < 100; ++iter)
            {
                var X = left + (right - left) / 2;
                if (net.Check(X)) right = X;
                else left = X;
            }

            if (!net.Flag)
                net.Check(right);
            var stack = new Stack<int>();
            
            var counter = -1;
            for (var temp = net.N; temp != -1;)
            {
                counter++;
                stack.Push(temp);
                temp = net.Prev[temp];
            }

            Console.WriteLine(counter);
            while(stack.Count > 0)
                Console.Write(stack.Pop() + " ");
        }

        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    }

    class Net
    {
        public Dictionary<int, List<(int, int)>> Edges = new Dictionary<int, List<(int, int)>>();
        public int N;
        public double[] D;
        public int[] Prev;
        public bool Flag;

        public Net(int n)
        {
            N = n;
            D = new double[N + 1];
            Prev = new int[N + 1];
        }

        public void AddEdge(int a, int b, int c)
        {
            if (!Edges.ContainsKey(b))
                Edges[b] = new List<(int, int)>();
            Edges[b].Add((a,c));
        }

        public bool Check(double X)
        {
            Array.Fill(D, double.PositiveInfinity);
            D[1] = 0;
            Array.Fill(Prev,-1);
            for (var k = 2; k < N + 1; ++k)
            {
                if(!Edges.ContainsKey(k))
                    continue;
                foreach (var pair in Edges[k])
                {
                    if (D[pair.Item1] + pair.Item2 - X < D[k])
                    {
                        D[k] = D[pair.Item1] + pair.Item2 - X;
                        Prev[k] = pair.Item1;
                    }
                }
            }
            Flag = D[N] <= 0;
            return Flag;
        }
    }
}