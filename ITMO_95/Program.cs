namespace ITMO_95;

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
            if (c[0] == 1) tree.Add(c[1], c[2], c[3], c[4]);
            else Console.WriteLine(tree.GetAt(c[1]));
        }
    }
}

class SegTree
{
    private readonly int size;
    private readonly (long sum, long operation)[] tree;
    private readonly long NeutralOp = 0;
    
    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long sum, long operation)[2 * size - 1];
        
    }

    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => Left(x) + 1;

    private long Modify(long prev, long operation, int len = 1) => prev + operation * len;

    private void PushDown(int x, int lx, int rx)
    {
        if(rx - lx == 1 || tree[x].operation == 0) return;
        var op = tree[x].operation;
        tree[x].operation = 0;
        var m = lx + (rx - lx) / 2;
        tree[Left(x)].operation += op;
        tree[Right(x)].operation += op;
        tree[Left(x)].sum += (m - lx) * op;
        tree[Right(x)].sum += (rx - m) * op;
    }

    private void Add(int l, int r, long val, int x, int lx, int rx)
    {
        PushDown(x,lx,rx);
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].operation += val;
            tree[x].sum += val * (rx - lx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Add(l,r,val,Left(x),lx,m);
        Add(l,r,val,Right(x),m,rx);
        tree[x].sum = Modify(tree[Left(x)].sum + tree[Right(x)].sum, tree[x].operation, Math.Min(rx,r) - Math.Max(lx,l));
    }

    public void Add(int l, int r, long a, long d)
    {
        Add(l-1,l,a,0,0,size);
        Add(l, r,d,0,0,size);
        Add(r,r+1, -a - (r - l) * d,0,0,size);
    }

    private long Sum(int l, int r, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return 0;
        if (l <= lx && rx <= r) return tree[x].sum;

        var m = lx + (rx - lx) / 2;
        return Modify(Sum(l, r, Left(x), lx, m) + Sum(l, r, Right(x), m, rx), tree[x].operation, Math.Min(rx,r) - Math.Max(lx,l));
    }

    public long GetAt(int i) => Sum(0, i, 0, 0, size);
}