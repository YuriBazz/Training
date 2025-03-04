using System.Drawing;

namespace ITMO_82;

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
            if(c[0] == 1) tree.Add(c[1],c[2],c[3]);
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
    }

    private void Add(int l, int r, int val, int x, int lx, int rx)
    {
        if(lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x] += val;
            return;
        }

        var m = lx + (rx - lx) / 2;
        Add(l, r, val, 2 * x + 1, lx, m);
        Add(l,r,val,2 * x + 2, m,rx);
    }

    public void Add(int l, int r, int val) => Add(l, r, val, 0, 0, size);

    private long Get(int i, int x, int lx, int rx)
    {
        if (rx - lx == 1) return tree[x];
        var m = lx + (rx - lx) / 2;
        if (i < m) return tree[x] + Get(i, 2 * x + 1, lx, m);
        return tree[x] +  Get(i, 2 * x + 2, m, rx);

    }

    public long Get(int i) => Get(i, 0, 0, size);
}