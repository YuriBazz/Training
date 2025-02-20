using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_65
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        static void Main(string[] args)
        {
            var t = Read();
            var tree = new SegTree(Read());
            for (; t[1] > 0; t[1]--)
            {
                var inp = Read();
                if(inp[0] == 1) tree.Set(inp[1], inp[2]);
                else Console.WriteLine(tree.Sum(inp[1],inp[2]));
            }
        }

        
    }

    class SegTree
    {
        public long[] tree;
        public int size;

        public SegTree(int[] a)
        {
            size = 1;
            while (size < a.Length)
                size *= 2;
            tree = new long[2 * size - 1];
            Build(a,0,0,size);
        }

        public void Build(int[] a, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                if(lx < a.Length)
                    tree[x] = a[lx];
                return;
            }
            var m = (lx + rx) / 2;
            Build(a, 2 * x + 1, lx , m); 
            Build(a, 2 * x + 2, m , rx);
            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }
        
        public void Set(int i, int v) => Set(i, v, 0, 0,  size);

        public void Set(int i, int v, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                tree[x] = v;
                return;
            }

            var m = (lx + rx) / 2;
            if (i < m) Set(i, v, 2 * x + 1, lx, m);
            else Set(i, v, 2 * x + 2, m, rx);
            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }

        public long Sum(int l, int r)
        {
            return Sum(l, r, 0, 0, size);
        }

        public long Sum(int l, int r, int x, int lx, int rx)
        {
            if (l >= rx || lx >= r) return 0;
            if (lx >= l && rx <= r) return tree[x];
            var m = (lx + rx) / 2;
            return Sum(l, r, 2 * x + 1, lx, m) + Sum(l, r, 2 * x + 2, m, rx);
        }
        
        static int Find(int n)
        {
            int l = -1, r = 17;
            while (r > l + 1)
            {
                var m = l + (r - l) / 2;
                if (n <= QuickPow(2, m)) r = m;
                else l = m;
            }

            return QuickPow(2, r + 1) - 1;
        }
        
        static int QuickPow(int value, int pow)
        {
            var result = 1;
            var temp = value;
            while (pow != 0)
            {
                result *= (pow % 2 == 0 ? 1 : temp);
                temp *=  temp;
                pow /= 2;
            }
            return result;
        }
    }
}