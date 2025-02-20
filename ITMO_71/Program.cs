using System;

namespace ITMO_71
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var t = Read();
            var tree = new SegTree(Read());
            while (t[1]-- > 0)
            {
                var c = Read();
                if(c[0] == 1) tree.Set(c[1],c[2]);
                else Console.WriteLine(tree.Find(c[1], c[2]));
            }
        }
    }

    class SegTree
    {
        public int[] Tree;
        public int Size;

        public SegTree(int[] a)
        {
            Size = 1;
            while (Size < a.Length) Size *= 2;
            Tree = new int[2 * Size - 1];
            Build(a, 0, 0, Size);
        }

        public void Build(int[] a, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                if (lx < a.Length)
                    Tree[x] = a[lx];
                return;
            }

            var m = (lx + rx) / 2;
            Build(a, 2 * x + 1, lx ,m);
            Build(a, 2 * x + 2, m, rx);
            Tree[x] = Math.Max(Tree[2 * x + 1], Tree[2 * x + 2]);
        }

        public void Set(int i, int v) => Set(i, v, 0, 0, Size);

        public void Set(int i, int v, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                Tree[x] = v;
                return;
            }

            var m = (lx + rx) / 2;
            if(i < m) Set(i, v, 2 * x + 1, lx, m);
            else Set(i, v, 2 * x + 2, m ,rx);

            Tree[x] = Math.Max(Tree[2 * x + 1], Tree[2 * x + 2]);
        }

        public int Find(int v, int l) => Find(v, l, 0 ,0, Size);

        public int Find(int v, int l, int x, int lx, int rx)
        {
            if (Tree[x] < v) return -1;
            if (rx <= l) return -1;
            if (rx - lx == 1) return lx;
            var m = (lx + rx) / 2;
            var left = Find(v, l, 2 * x + 1, lx, m);
            return left == -1 ? Find(v, l, 2 * x + 2, m, rx) : left;
        }
    }
}