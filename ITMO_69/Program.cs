using System;

namespace ITMO_69
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
                if (c[0] == 1) tree.Set(c[1]);
                else Console.WriteLine(tree.Find(c[1]));
            }
        }
    }
    
    class SegTree
    {
        public int[] tree;
        public int size;

        public SegTree(int[] a)
        {
            size = FindLenght(a.Length);
            tree = new int[2 * size - 1];
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
        
        public void Set(int i) => Set(i, 0, 0,  size);

        public void Set(int i, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                tree[x] = (tree[x] + 1) % 2;
                return;
            }

            var m = (lx + rx) / 2;
            if (i < m) Set(i, 2 * x + 1, lx, m);
            else Set(i, 2 * x + 2, m, rx);
            tree[x] = tree[2 * x + 1] + tree[2 * x + 2];
        }

        public int Find(int k) => Find(k, 0);

        public int Find(int k, int x)
        {
            if (2 * x + 1 >= tree.Length) return (x + 1) %  size;
            if (tree[2 * x + 1] <= k) return Find(k - tree[2 * x + 1], 2 * x + 2);
            return Find(k, 2 * x + 1);
        }
        
        static int FindLenght(int n)
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