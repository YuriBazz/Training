using System.Drawing;
using System.Dynamic;

namespace ITMO_84;

static class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        var t = Read();
        var tree = new SegTree(t[0]);
        while (t[1]-- > 0)
        {
            var c = Read();
            if(c[0] == 1) tree.Set(c[1],c[2],c[3]);
            else Console.Write(tree.Get(c[1]) + "\n");
        }
    }
}

class SegTree
{
    private readonly int size;
    private readonly long[] tree;

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new long[(size << 1) - 1];
        Array.Fill(tree, (int)1e9 + 1);
        Set(0,size, 0);
    }
    

    private void Set(int l, int r, int val, int x, int lx, int rx)
    {
        if (tree[x] != (int)1e9 + 1 && 2 * x + 2 < (size << 1) - 1)
        {
            tree[2 * x + 1] = tree[x];
            tree[2 * x + 2] = tree[x];
            tree[x] = (int)1e9 + 1;
        }
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x] = val;
            return;
        }
        var m = lx + (rx - lx) / 2;
        Set(l, r, val, 2 * x + 1, lx, m);
        Set(l,r,val,2 * x + 2, m,rx);
    }

    public void Set(int l, int r, int val) => Set(l, r, val, 0, 0, size);
    
    public long Get(int i)
    {
        if (tree[0] != (int)1e9 + 1) return tree[0];
        long res = 0;
        for (var x = size + i - 1; x != 0; x = (x - 1) >> 1)
            res = tree[x] == (int)1e9 + 1 ? res : tree[x];
        return res;
    }
}