using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_79
{
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var t = Read();
            var a = Read();
            var tree = new SegTree(a);
            while (t[1]-- > 0)
            {
                var c = Read();
                if(c[0] == 1) Console.WriteLine(tree.Find(c[1],c[2]));
                else tree.Set(c[1],c[2]);
            }
        }
    }

    class SegTree
    {
        public int size;
        public (int[]? arr,long count)[] tree;
        private (int[], long count) temp;

        public SegTree(int[] a)
        {
            size = 1;
            while (size < a.Length) size <<= 1;
            tree = new (int[]?,long)[2 * size - 1];
            Build(a, 0, 0, size);
        }

        private void Build(int[] a, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                
                if (lx < a.Length)
                {
                    tree[x] = (new int[40], 0);
                    tree[x].arr[a[lx] - 1]++;
                }

                return;
            }

            var m = lx + (rx - lx) / 2;
            Build(a, 2 * x + 1, lx, m);
            Build(a, 2 * x + 2, m ,rx);
            
            tree[x] = Do(tree[2 * x + 1], tree[2 * x + 2]);
        }


        private void Set(int j, int val, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                Array.Fill(tree[x].arr, 0);
                tree[x].arr[val - 1]++;
                return;
            }

            var m = lx + (rx - lx) / 2;
            if (j < m) Set(j, val, 2 * x + 1, lx, m);
            else Set(j,val,2 * x + 2, m, rx);
            
            tree[x] = Do(tree[2 * x + 1], tree[2 * x + 2]);
        }

        public void Set(int i, int val) => Set(i - 1, val, 0, 0, size);
        
        private void Find(int l, int r, int x, int lx, int rx)
        {
            if (lx >= rx) return;
            if (rx <= l || lx >= r) return;
            if (l <= lx && rx <= r)
            {
                temp = Do(temp, tree[x]);
                return;
            }
            var m = lx + (rx - lx) / 2;
            Find(l, r, 2 * x + 1, lx, m);
            Find(l,r,2 * x + 2,m,rx);
        }

        public long Find(int l, int r)
        {
            temp = (new int[40], 0);
            Find(l - 1, r, 0, 0, size);
            return temp.count;
        }
        
        private (int[]?, long) Do((int[]? arr, long count) left, (int[]? arr, long count) right)
        {
            if (left.arr is null) return right;
            if (right.arr is null) return left;
            var pref = new long[41];
            for (var i = 1; i < 41; ++i)
                pref[i] = pref[i - 1] + right.arr[i - 1];
            var less = 0L;
            var arr = new int[40];
            for (var i = 0; i < 40; ++i)
            {
                less += left.arr[i] * pref[i];
                arr[i] = left.arr[i] + right.arr[i];
            }

            return (arr, left.count + right.count + less );
        }
    }
}