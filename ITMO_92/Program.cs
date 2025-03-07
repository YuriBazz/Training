namespace ITMO_92;

class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    static void Main(string[] args)
    {
        var t = Read();
        var tree = new SegTree(t[0]);
        while (t[1]-- > 0)
        {
            var c = Read();
            if(c[0] == 1) tree.Modify(c[1],c[2]);
            else Console.WriteLine(tree.Get(c[1]));
        }
    }
}

class SegTree
{
    private readonly int size;
    private readonly (int sum, bool toModify)[] tree;

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (int, bool)[(size << 1) - 1];

    }

    private void Swap(int x, int len) => tree[x].sum = len - tree[x].sum; 

    private void PushModify(int x, int lx, int rx)
    {
        if (!tree[x].toModify || rx - lx == 1) return;
        tree[Left(x)].toModify ^= true;
        tree[Right(x)].toModify ^= true;
        var m = lx + (rx - lx) / 2;
        
        Swap(Left(x), m - lx);
        Swap(Right(x),rx - m);
        tree[x].toModify = false;
    }
    
    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => (x << 1) + 2;

    private void Modify(int l, int r, int x, int lx, int rx)
    {
        PushModify(x, lx,rx);
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].toModify ^= true;
            Swap(x,rx - lx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Modify(l,r,(x << 1) + 1, lx, m);
        Modify(l,r,(x << 1) + 2, m, rx);
        tree[x].sum = tree[(x << 1) + 1].sum + tree[(x << 1) + 2].sum;
        if(tree[x].toModify) Swap(x, Math.Min(r,rx) - Math.Max(l,lx));
    }

    public void Modify(int l, int r) => Modify(l, r, 0, 0, size);

    private int Get(int k, int x, int lx, int rx)
    {
        if (rx - lx == 1) return lx;
        var m = lx + (rx - lx) / 2;
        PushModify(x, lx,rx);
        if (tree[(x << 1) + 1].sum > k) return Get(k, (x << 1) + 1, lx, m);
        return Get(k - tree[(x << 1) + 1].sum, (x << 1) + 2, m, rx);
    }

    public int Get(int k) => Get(k, 0, 0, size);
}