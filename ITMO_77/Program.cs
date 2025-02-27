using System;

namespace ITMO_77
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            Read();
            var tree = new SegTree(Read());
            for (var m = Read()[0]; m > 0; --m)
            {
                var c = Read();
                if(c[0] == 0) tree.Set(c[1],c[2]);
                else Console.WriteLine((c[1] % 2 == 0 ? -1 : 1) * tree.Sum(c[1],c[2]));
            }
        }
    }

    class SegTree
    {
        public int size;
        public long[] tree;

        public void Build(int[] a, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                if (lx < a.Length)
                    tree[x] = (x % 2 == 0 ? -1 : 1) * a[lx];
                return;
            }

            var m = lx + (rx - lx) / 2;
            Build(a, 2 * x + 1, lx, m);
            Build(a, 2 * x + 2, m, rx);

            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }

        public SegTree(int[] a)
        {
            size = 1;
            while (size < a.Length) size *= 2;
            tree = new long[2 * size - 1];
            Build(a,0,0,size);
        }

        public void Set(int i, int v, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                tree[x] = (x % 2 == 0 ? -1 : 1) * v;
                return;
            }

            var m = lx + (rx - lx) / 2;
            if(i < m) Set(i,v,2 * x + 1, lx, m);
            else Set(i,v,2 * x + 2, m ,rx);
            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }

        public void Set(int i, int j) => Set(i - 1, j, 0, 0, size);

        public long Sum(int l, int r, int x, int lx, int rx)
        {
            if (lx >= r || rx <= l) return 0;
            if (l <= lx && rx <= r) return tree[x];
            var m = lx + (rx - lx) / 2;
            return Sum(l, r, 2 * x + 1, lx, m) + Sum(l, r, 2 * x + 2, m, rx);
        }

        /*public long Sum(int l, int r)
        {
            var t = Sum(l++-1,r,0,0,size);
            var i = 1;
            while (l != r + 1)
                t += (int)Math.Pow(-1, i++) * 2 * Sum(l++ - 1, r, 0, 0, size);
            return t;
        }*/

        public long Sum(int l, int r) => Sum(l - 1, r, 0, 0, size);
    }
}