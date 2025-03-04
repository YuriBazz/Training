namespace ITMO_88;
// NB: Я ЧТО-ТО ПОТРОГАЛ => МОЖЕТ НЕ РАБОТАТЬ

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
            if(c[0] == 1) tree.Modify(c[1],c[2],c[3]);
            else Console.WriteLine(tree.GetSum(c[1],c[2]));
        }
    }
}

class SegTree
{
    private readonly int size;
    private (long sum, long toModif)[] tree;

    private const long neutral = (long)1e9 + 1;
    
    private long op(long a, long b) => Math.Min(a,b);
    private long modif(long a, long b, long len = 1) => a == neutral ? b : b;

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long, long)[(size << 1) - 1];

    }

    private void Modify(int l, int r, long v, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].sum = modif(tree[x].sum, v, rx - lx);
            tree[x].toModif = modif(tree[x].toModif, v);
            return;
        }

        var m = lx + (rx - lx) / 2; 
        Modify(l,r,v, (x << 1) + 1, lx, m);
        Modify(l,r,v,(x << 1) + 2, m,rx);
        tree[x].sum = op(tree[(x << 1) + 1].sum, tree[(x << 1) + 2].sum);
        tree[x].sum = modif(tree[x].sum, tree[x].toModif, rx - lx);
    }

    public void Modify(int l, int r, int v) => Modify(l, r, v, 0, 0, size);

    private long GetSum(int l, int r, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return 0;
        if (l <= lx && rx <= r) return tree[x].sum;
        var m = lx + (rx - lx) / 2;
        var left = GetSum(l, r, (x << 1) + 1, lx, m);
        var right = GetSum(l, r, (x << 1) + 2, m, rx);
        return modif(op(left, right), tree[x].toModif, Math.Min(rx, r) - Math.Max(lx, l));
    }

    public long GetSum(int l, int r) => GetSum(l, r, 0, 0, size);
}