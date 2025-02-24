using System;

namespace ITMO_76
{

    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var t = Read();
            var (n, m) = (t[0], t[1]);
            var tree = new SegTree(n);
            while (m-- > 0)
            {
                var c = Read();
                if (c[0] == 1)
                {
                    tree.Set(c[1],c[3]);
                    tree.Set(c[2],c[3],false);
                }
                else Console.WriteLine(tree.Find(c[1]));
            }
        }
    }

    class SegTree
    {
        public int size;
        public long[] tree;

        public SegTree(int n)
        {
            size = 1;
            while (size < n) size *= 2;
            tree = new long[2 * size - 1];
        }

        public long Find(int i) => Find(i + 1 , 0, 0, size);

        public long Find(int i, int x, int lx, int rx)
        {
            if (lx >= i) return 0;
            if (rx <= i) return tree[x];
            var m = lx + (rx - lx) / 2;
            return Find(i, 2 * x + 1, lx, m) + Find(i, 2 * x + 2, m, rx);
        }

        public void Set(int i, int v, bool left = true) => Set(v,i,0,0,size, left);
        

        public void Set(long v, int i, int x, int lx, int rx, bool left)
        {
            if (rx - lx == 1)
            {
                tree[x] += left ? v : -v;
                return;
            }

            var m = lx + (rx - lx) / 2;
            if(i < m) Set(v,i,2 * x + 1,lx, m, left);
            else Set(v, i, 2 * x +2,m,rx,left);

            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }
    }
}