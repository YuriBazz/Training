namespace ITMO_91;

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
            tree.Set(c[0],c[1],c[2]);
            Console.WriteLine(tree.Get);
        }
    }
}

class SegTree
{
    private const long NeutralModify = (long)1e9 + 1;
    private int size;
    private (long sum, long seg, long pref, long suf, long toModify)[] tree;


    private long modify(long prev, long toModify)
    {
        if (toModify == NeutralModify) return prev;
        return toModify;
    }
    
    private void modify(int x, int len)
    {
        if (tree[x].toModify == NeutralModify) return;
        var sum = tree[x].toModify * len;
        if (tree[x].toModify <= 0)
            tree[x] = (sum, 0, 0, 0, tree[x].toModify);
        else tree[x] = (sum, sum, sum, sum, tree[x].toModify);
        
    }

    public long Get => tree[0].seg;
    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long sum, long seg, long pref, long suf, long toModify)[(size << 1) - 1];
        Array.Fill(tree, (0,0,0,0,(long)1e9 + 1));
    }

    public void Set(int l, int r, int v) => Set(l, r, v, 0, 0, size);

    private void Set(int l, int r, long v, int x, int lx, int rx)
    {
        PushModifyDown(x, lx, rx);
        if (lx >= r || rx <= l) return;
        
        if (l <= lx && rx <= r)
        {
            tree[x].toModify = modify(tree[x].toModify, v);
            modify(x, rx - lx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Set(l, r, v, (x << 1) + 1, lx, m);
        Set(l, r, v, (x << 1) + 2, m, rx);
        GetResult(x);
        modify(x, Math.Min(r,rx) - Math.Max(l,lx));
    }
    
    private void PushModifyDown(int x, int lx, int rx)
    {
        if (rx - lx == 1) return;
        var m = lx + (rx - lx) / 2;
        tree[(x << 1) + 1].toModify = modify(tree[(x << 1) + 1].toModify, tree[x].toModify);
        tree[(x << 1) + 2].toModify = modify(tree[(x << 1) + 2].toModify, tree[x].toModify);
        modify((x << 1) + 1, m - lx);
        modify((x << 1) + 2, rx - m);
        tree[x].toModify = NeutralModify;
    }

    private void GetResult(int x)
    {
        var left = tree[(x << 1) + 1];
        var right = tree[(x << 1) + 2];
        var sum = left.sum + right.sum;
        var seg = new[] { left.seg, right.seg, left.suf + right.pref }.Max();
        var suf = Math.Max(right.suf, right.sum + left.suf);
        var pref = Math.Max(left.pref, left.sum + right.pref);
        tree[x] = (sum, seg, pref, suf, tree[x].toModify);
    }
}
