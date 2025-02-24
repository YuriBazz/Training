using System;
 
namespace ITMO_72
{
    
    static class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            Read();
            var a = Read();
            var temp = new int[a.Length];
            var tree = new SegTree(temp);
            for (var x = 0; x < a.Length; ++x)
            {
                Console.Write(tree.Find(a[x]) + " ");
                tree.Set(a[x] - 1);
            }
        }
    }
 
    class SegTree
    {
        public int Size;
        public long[] Tree;
 
        public SegTree(int[] a)
        {
            Size = 1;
            while (Size < a.Length) Size *= 2;
            Tree = new long[2 * Size - 1];
        }
 
 
        public void Set(int i) => Set(i, 0, 0, Size);
 
        public void Set(int i, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                Tree[x]++;
                return;
            }
 
            var m = (lx + rx) / 2;
            if(i < m) Set(i, 2 * x + 1, lx, m);
            else Set(i, 2 * x + 2, m, rx);
 
            Tree[x] = Tree[2 * x + 1] + Tree[2 * x + 2];
        }
 
        public long Find(int i) => Find(i, 0, 0, Size);
 
        public long Find(int l, int x, int lx, int rx)
        {
            if (rx <= l) return 0;
            if (lx >= l && rx <= Size) return Tree[x];
            var m = ( lx + rx ) / 2;
            return Find(l, 2 * x + 1, lx, m) + Find(l, 2 * x + 2, m, rx);
        }
    }
}