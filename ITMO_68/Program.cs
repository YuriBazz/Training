using System;
using System.Net.NetworkInformation;

namespace ITMO_68
{

    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var t = Read();
            var tree = new SegTree(Read());
            Console.WriteLine(tree.tree[0].seg);
            while (t[1]-- > 0)
            {
                var c = Read();
                tree.Set(c[0],c[1]);
                Console.WriteLine(tree.tree[0].seg);
            }
        }
    }
    
    class SegTree
    {
        public int size;
        public (long sum, long seg, long pref, long suf)[] tree;

        public SegTree(int[] a)
        {
            size = Find(a.Length);
            tree = new (long, long,long,long)[2 * size - 1];
            Build(a,0,0,size);
        }

        public void Build(int[] a, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                if(lx < a.Length)
                    tree[x] = (a[lx],Math.Max(a[lx], 0), Math.Max(a[lx],0),Math.Max(a[lx],0));
                return;
            }
            var m = (lx + rx) / 2;
            Build(a, 2 * x + 1, lx , m); 
            Build(a, 2 * x + 2, m , rx);
            var left = tree[2 * x + 1];
            var right = tree[2 * x + 2];
            var seg = new []{ left.seg, right.seg, left.suf + right.pref }.Max();
            var pref = Math.Max(left.pref, left.sum + right.pref);
            var suf = Math.Max(right.suf, right.sum + left.suf);
            var sum = left.sum + right.sum;
            tree[x] = (sum, seg, pref, suf);
        }
        
        public void Set(int i, int v) => Set(i, v, 0, 0,  size);

        public void Set(int i, int v, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                tree[x] = (v,Math.Max(v, 0), Math.Max(v,0),Math.Max(v,0));
                return;
            }

            var m = (lx + rx) / 2;
            if (i < m) Set(i, v, 2 * x + 1, lx, m);
            else Set(i, v, 2 * x + 2, m, rx);
            var left = tree[2 * x + 1];
            var right = tree[2 * x + 2];
            var seg = new []{ left.seg, right.seg, left.suf + right.pref }.Max();
            var pref = Math.Max(left.pref, left.sum + right.pref);
            var suf = Math.Max(right.suf, right.sum + left.suf);
            var sum = left.sum + right.sum;
            tree[x] = (sum, seg, pref, suf);
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

            return QuickPow(2, r);
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