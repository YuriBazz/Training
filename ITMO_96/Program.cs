namespace ITMO_96;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

class SegTree
{
    private readonly int size;
    private readonly ((long count, long len,bool first, bool last) value, long operation)[] tree;
    private const int Shift = (int)5e5;

    private const long NeutralOp = -1;
    
    public SegTree()
    {
        size = 1;
        while (size < 1e6) size <<= 1;
        tree = new ((long, long, bool,bool), long operation)[(size << 1) - 1];
        Array.Fill(tree,((0,0,false,false),-1));
    }

    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => Left(x) + 1;

    
    
    
    private (long, long, bool,bool) Modify((long count, long len, bool first, bool last) prev, long operation, int lx, int rx, int l = int.MinValue, int r = int.MaxValue)
    {
        if (operation == NeutralOp) return prev;
        var lc = Math.Max(l, lx);
        var rc = Math.Min(r, rx);
        
    }

    private void PushDown(int x, int lx, int rx)
    {
        if (rx - lx == 1 || tree[x].operation == NeutralOp) return;
        var op = -1L;
        (op, tree[x].operation) = (tree[x].operation, op);
        var m = lx + (rx - lx) / 2;
        tree[Left(x)].operation = op;
        tree[Right(x)].operation = op;
        
    }

    private void Set(int l, int r, int v, int x, int lx, int rx)
    {
        PushDown(x,lx,rx);
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].operation = v;
            ModifyNode(x, rx - lx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Set(l,r,v,Left(x),lx,m);
        Set(l,r,v,Right(x),m,rx);
        tree[x].value.len = tree[Left(x)].value.len + tree[Right(x)].value.len;
        tree[x].value.count = tree[Left(x)].value.count + tree[Right(x)].value.count;
        if()
        ModifyNode(x,Math.Min(r,rx) - Math.Max(l,lx));
    }

    public void Set(string color, int x, int len) => Set(x + Shift, x + len + Shift, color == "W" ? 0 : 1, 0, 0, size);
}