namespace ITMO_90;

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
    private (long sum, long toModify)[] tree;

    private const long neutralModify = (long)1e9 + 1;
    private const long neutralOperation = 0;
    
    private long operation(long a, long b) => a + b;

    private long modify(long previous, long toModify, int len = 1)
    {
        if (toModify == neutralModify) return previous;
        return toModify * len;
    }

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long, long)[(size << 1) - 1];
        Array.Fill(tree, (0, neutralModify));

    }

    private void PushModifyDown(int x, int lx, int rx)
    {
        if (tree[x].toModify == neutralModify || rx - lx == 1) return;
        var m = lx + (rx - lx) / 2; 
        tree[(x << 1) + 2].toModify = modify(tree[(x << 1) + 2].toModify,tree[x].toModify);
        tree[(x << 1) + 1].toModify = modify(tree[(x << 1) + 1].toModify,tree[x].toModify);
        tree[(x << 1) + 1].sum = modify(tree[(x << 1) + 1].sum, tree[(x << 1) + 1].toModify, m - lx);
        tree[(x << 1) + 2].sum = modify(tree[(x << 1) + 2].sum, tree[(x << 1) + 2].toModify, rx - m);
        tree[x].toModify = neutralModify;
    }
    
    private void Modify(int l, int r, long v, int x, int lx, int rx)
    {
        PushModifyDown(x, lx, rx);
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].toModify = modify(tree[x].toModify, v);
            tree[x].sum = modify(tree[x].sum, v, rx - lx);
            return;
        }

        var m = lx + (rx - lx) / 2; 
        Modify(l,r,v, (x << 1) + 1, lx, m);
        Modify(l,r,v,(x << 1) + 2, m,rx);
        tree[x].sum = operation(tree[(x << 1) + 1].sum, tree[(x << 1) + 2].sum);
        tree[x].sum = modify(tree[x].sum, tree[x].toModify, rx - lx);
    }

    public void Modify(int l, int r, int v) => Modify(l, r, v, 0, 0, size);

    private long GetSum(int l, int r, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return neutralOperation;
        if (l <= lx && rx <= r) return tree[x].sum;
        var m = lx + (rx - lx) / 2;
        var left = GetSum(l, r, (x << 1) + 1, lx, m);
        var right = GetSum(l, r, (x << 1) + 2, m, rx);
        return modify(operation(left, right), tree[x].toModify, Math.Min(rx,r) - Math.Max(l,lx));
    }

    public long GetSum(int l, int r) => GetSum(l, r, 0, 0, size);
}