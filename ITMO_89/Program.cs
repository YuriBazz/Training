namespace ITMO_89;

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
    private const long neutralOperation = long.MaxValue;
    
    private long operation(long a, long b) => Math.Min(a,b);

    private long modify(long previous, long toModify, long len = 1)
    {
        if (toModify == neutralModify) return previous;
        return toModify;
    }

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long, long)[(size << 1) - 1];
        Array.Fill(tree, (0, neutralModify));

    }

    private void PushModifyDown(int x, long newModify = neutralModify)
    {
        //Конкретно тут хватит и такого
        if (tree[x].toModify != neutralModify && (x << 1) + 2 < (size << 1) - 1)
        {
            tree[(x << 1) + 1].toModify = tree[x].toModify;
            tree[(x << 1) + 2].toModify = tree[x].toModify;
            tree[(x << 1) + 1].sum = modify(tree[(x << 1) + 1].sum, tree[(x << 1) + 1].toModify);
            tree[(x << 1) + 2].sum = modify(tree[(x << 1) + 2].sum, tree[(x << 1) + 2].toModify);
        }

        tree[x].toModify = newModify;
        tree[x].sum = modify(tree[x].sum, tree[x].toModify);

        //ПОЛНОЕ ПРОТАЛКИВАНИЕ ВНИЗ
        /*if (x >= (size << 1) - 1) return;
        var c = tree[x].toModify;
        tree[x].toModify = newModify;
        tree[x].sum = modify(tree[x].sum, tree[x].toModify);
        if (c == neutralModify) return;

        PushModifyDown((x << 1) + 1, c);
        PushModifyDown((x << 1) + 2, c);*/
    }
    
    private void Modify(int l, int r, long v, int x, int lx, int rx)
    {
        PushModifyDown(x);
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].toModify = modify(tree[x].toModify, v);
            tree[x].sum = modify(tree[x].sum, v);
            return;
        }

        var m = lx + (rx - lx) / 2; 
        Modify(l,r,v, (x << 1) + 1, lx, m);
        Modify(l,r,v,(x << 1) + 2, m,rx);
        tree[x].sum = operation(tree[(x << 1) + 1].sum, tree[(x << 1) + 2].sum);
        tree[x].sum = modify(tree[x].sum, tree[x].toModify);
    }

    public void Modify(int l, int r, int v) => Modify(l, r, v, 0, 0, size);

    private long GetSum(int l, int r, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return neutralOperation;
        if (l <= lx && rx <= r) return tree[x].sum;
        var m = lx + (rx - lx) / 2;
        var left = GetSum(l, r, (x << 1) + 1, lx, m);
        var right = GetSum(l, r, (x << 1) + 2, m, rx);
        return modify(operation(left, right), tree[x].toModify);
    }

    public long GetSum(int l, int r) => GetSum(l, r, 0, 0, size);
}