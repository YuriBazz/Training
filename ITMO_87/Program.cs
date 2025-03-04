namespace ITMO_87;

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
            if(c[0] == 1) tree.Or(c[1],c[2],c[3]);
            else Console.WriteLine(tree.And(c[1],c[2]));
        }
    }
}

class SegTree
{
    private readonly int size;
    private (long and, long or)[] tree;

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long, long)[(size << 1) - 1];

    }

    private void Or(int l, int r, long v, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x] = (tree[x].and | v, tree[x].or | v);    
            return;
        }

        var m = lx + (rx - lx) / 2;
        Or(l,r,v, (x << 1) + 1, lx, m);
        Or(l,r,v,(x << 1) + 2, m,rx);
        tree[x].and = (tree[(x << 1) + 1].and & tree[(x << 1) + 2].and) | tree[x].or;
    }

    public void Or(int l, int r, int v) => Or(l, r, v, 0, 0, size);

    private long And(int l, int r, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return long.MaxValue;
        if (l <= lx && rx <= r) return tree[x].and;
        var m = lx + (rx - lx) / 2;
        return (And(l, r, (x << 1) + 1, lx, m) & And(l, r, (x << 1) + 2, m, rx)) | tree[x].or;
    }

    public long And(int l, int r) => And(l, r, 0, 0, size);
}