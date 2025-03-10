namespace ITMO_96;

class Program
{
    static void Main(string[] args)
    {
        var tree = new SegTree();
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var s = Console.ReadLine().Split(" ");
            tree.Set(s[0], int.Parse(s[1]), int.Parse(s[2]));
            Console.WriteLine(tree.Get);
        }
    }
}

class SegTree
{
    private readonly int size;
    private readonly ((long count, long len, long left, long right) value, long operation)[] tree;
    private const long Shift =(long)5e5;

    private const long NeutralOp = -1;
    
    public SegTree()
    {
        size = 1;
        while (size < 1e6 + 1) size <<= 1;
        tree = new ((long count, long len,long,long), long operation)[(size << 1) - 1];
        Array.Fill(tree,((0,0,0,0),-1));
    }

    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => Left(x) + 1;

    public string Get => $"{tree[0].value.count} {tree[0].value.len}";

    private (long, long, long, long) Modify((long count, long len, long left, long righ) prev, long operation, long len)
    {
        if (operation == NeutralOp) return prev;
        if (operation == 0) return (0, 0, 0, 0);
        return (1, len, 1, 1);

    }

    private void PushDown(int x, int lx, int rx)
    {
        if (rx - lx == 1 || tree[x].operation == NeutralOp) return;
        var op = tree[x].operation;
        tree[x].operation = -1;
        var m = lx + (rx - lx) / 2;
        tree[Left(x)].operation = op;
        tree[Right(x)].operation = op;
        tree[Left(x)].value = Modify(tree[Left(x)].value, tree[Left(x)].operation, m - lx);
        tree[Right(x)].value = Modify(tree[Right(x)].value, tree[Right(x)].operation, rx - m);
    }

    
    private void Set(long l, long r, int v, int x, int lx, int rx)
    {
        PushDown(x, lx, rx);
        if (lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].operation = v;
            tree[x].value = Modify(tree[x].value, tree[x].operation, rx - lx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Set(l,r,v,Left(x),lx, m);
        Set(l,r,v,Right(x),m, rx);
        tree[x].value.len = tree[Left(x)].value.len + tree[Right(x)].value.len;
        tree[x].value.count = tree[Left(x)].value.count + tree[Right(x)].value.count;
        if (tree[Left(x)].value.right == 1 && tree[Right(x)].value.left == 1)
            tree[x].value.count--;
        tree[x].value.left = tree[Left(x)].value.left;
        tree[x].value.right = tree[Right(x)].value.right;
    }

    public void Set(string color, long x, long len) => Set(x + Shift, x + len + Shift, color == "W" ? 0 : 1, 0, 0, size);
}