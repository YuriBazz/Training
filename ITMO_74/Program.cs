using System;

namespace ITMO_74
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        static void Write<T>(T[] a) => Console.WriteLine(string.Join(" ", a));
        
        static void Main(string[] args)
        {
            Read();
            var a = Read();
            var set = new Dictionary<int, int>();
            var temp = new int[a.Length];
            var tree = new SegTree(temp);
            var result = new int[a.Length / 2];
            for (var i = 0; i < a.Length; ++i)
            {
                if (!set.ContainsKey(a[i]))
                {
                    set[a[i]] = i;
                    continue;
                }

                result[a[i] - 1] = tree.Sum(set[a[i]], i + 1);
                tree.Set(set[a[i]]);
            }
            Write(result);
        }
    }

    class SegTree
    {
        public int[] tree;
        public int size;

        public SegTree(int[] a)
        {
            size = 1;
            while (size < a.Length) size *= 2;
            tree = new int[2 * size - 1];
        }

        public void Set(int i) => Set(i, 0, 0, size);
        
        public void Set(int i, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                tree[x] = 1;
                return;
            }

            var m = lx + (rx - lx) / 2;
            if(i < m) Set(i, 2 * x + 1, lx, m);
            else Set(i, 2 * x + 2, m, rx);

            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }

        public int Sum(int l, int r, int x, int lx, int rx)
        {
            if (rx <= l || lx >= r) return 0;
            if (l <= lx && rx <= r) return tree[x];
            var m = lx + (rx - lx) / 2;
            return Sum(l, r, 2 * x + 1, lx, m) + Sum(l, r, 2 * x + 2, m, rx);
        }

        public int Sum(int l, int r) => Sum(l, r, 0, 0, size);
    }
}