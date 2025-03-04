namespace ITMO_85;

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
            else Console.WriteLine(tree.Get(c[1],c[2]));
        }
    }
}


class SegTree
{
    private readonly int size;
    private readonly (long min, long toAdd)[] tree;
    private readonly int _n;

    public SegTree(int n)
    {
        size = 1;
        _n = n;
        while (size < _n) size <<= 1;
        tree = new (long, long)[(size << 1) - 1];
    }

    private void Add(int l, int r, int v, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x] = (tree[x].min + v, tree[x].toAdd + v);
            return;
        }
        var m = lx + (rx - lx) / 2;
        Add(l,r,v,2 * x + 1, lx, m);
        Add(l,r,v, 2* x + 2, m ,rx);
        tree[x].min =  Math.Min(tree[2 * x + 1].min, tree[2 * x + 2].min) + tree[x].toAdd;
    }

    public void Add(int l, int r, int v) => Add(l, r, v, 0, 0, size);

    private long Get(int l, int r, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return long.MaxValue;
        if (l <= lx && rx <= r)
        {
            return tree[x].min;
        }
        
        var m = lx + (rx - lx) /2;
        return Math.Min(Get(l, r, 2 * x + 1, lx, m), Get(l, r, 2 * x + 2, m, rx)) + tree[x].toAdd;

    }

    public long Get(int l, int r) => Get(l, r, 0, 0, size);
}