namespace ITMO_86;

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
            if(c[0] == 1) tree.Mul(c[1],c[2],c[3]);
            else Console.WriteLine(tree.Sum(c[1],c[2]));
        }
    }
}

class SegTree
{
    private readonly int size;
    private const int mod = (int)1e9 + 7;
    private (long sum, long mul)[] tree;

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long, long)[(size << 1) - 1];
        Build(n, 0,0,size);

        void Build(int n, int x, int lx, int rx)
        {
            if (rx - lx == 1)
            {
                if (lx < n) tree[x] = (1, 1);
                else tree[x] = (0, 1);
                return;
            }

            var m = lx + (rx - lx) / 2;
            Build(n, (x << 1) + 1, lx, m);
            Build(n, (x << 1) + 2, m, rx);
            tree[x].sum = tree[(x << 1) + 1].sum + tree[(x << 1) + 2].sum;
            tree[x].mul = 1;
        }
    }

    private void Mul(int l, int r, int v, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x] = (tree[x].sum * v % mod,  tree[x].mul * v % mod);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Mul(l,r,v, (x << 1) + 1, lx, m);
        Mul(l,r,v,(x << 1) + 2, m,rx);
        tree[x].sum = (tree[(x << 1) + 1].sum + tree[(x << 1) + 2].sum) % mod * tree[x].mul % mod;
    }

    public void Mul(int l, int r, int v) => Mul(l, r, v, 0, 0, size);

    private long Sum(int l, int r, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return 0;
        if (l <= lx && rx <= r) return tree[x].sum % mod;
        var m = lx + (rx - lx) / 2;
        return (Sum(l, r, (x << 1) + 1, lx, m) + Sum(l, r, (x << 1) + 2, m, rx)) % mod * tree[x].mul % mod;
    }

    public long Sum(int l, int r) => Sum(l, r, 0, 0, size);
}