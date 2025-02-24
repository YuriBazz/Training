using System;

namespace ITMO_73
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        static void Write<T>(T[] a)
        { 
            foreach (var t in a) Console.Write(t + " ");
        }

        static void Main(string[] args)
        {
            var n = Read()[0];
            var tree = new SegTree(n);
            var r = new int[n];
            var temp = Read();
            int prev = -1, del = 0;
            for (var i = n - 1; i > -1; --i)
            {
                r[i] = tree.Find(temp[i]) + 1;
            }
            
            Write(r);
        }
    }

    class SegTree
    {
        public int size;
        public int[] tree;

        public SegTree(int n)
        {
            size = 1;
            while (size < n) size *= 2;
            tree = new int[2 * size - 1];
            Build(n, 0, 0, size);
        }

        public void Build(int n, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                if (lx < n)
                    tree[x] = 1;
                return;
            }

            var m = (lx + rx) / 2;
            Build(n, 2 * x + 1, lx, m);
            Build(n, 2 * x + 2, m, rx);
            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }


        public int Find(int k) => Find(k, 0, 0, size);

        public int Find(int k, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                tree[x] = 0;
                ReBuild(x);
                return x + 1 - size;
            }
            var m = (lx + rx) / 2;
            if (tree[2 * x + 2] <= k) return Find(k - tree[2 * x + 2], 2 * x + 1, lx, m);
            return Find(k, 2 * x + 2,m, rx);
        }
        
        public void ReBuild(int x)
        {
            while (x != 0)
            {
                x = (x - (x % 2 == 0 ? 2 : 1)) / 2;
                tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
            }
        }
    }
}