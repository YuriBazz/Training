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
                if (c[0] == 1) tree.Add(c[1],c[2], c[3], c[2] < n);
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
 
        public long Find(int i) => Sum(0, i + 1, 0, 0, size);
 
        public void Add(int l, int r, int v, bool f)
        {
            Set(l,v, 0, 0, size);
            if(f)
                Set(r, -v, 0, 0, size);
        }
 
        public long Sum(int l, int r, int x, int lx, int rx)
        {
            if(rx <= l || r <= lx) return 0;
            if (l <= lx && rx <= r) return tree[x];
            var m = lx + (rx - lx) / 2;
            return Sum(l, r, 2 * x + 1, lx, m) + Sum(l, r, 2 * x + 2,m,rx);
        }
 
        public void Set(int i, int v, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                tree[x] += v;
                return;
            }
            
            var m = lx + (rx - lx) / 2;
            if(i < m) Set(i, v,2 * x + 1, lx,m);
            else Set(i, v, 2 * x + 2, m, rx);
 
            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }
    }
}