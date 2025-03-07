namespace ITMO_94;

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
            if(c[0] == 1) tree.Set(c[1],c[2],c[3]);
            if(c[0] == 2) tree.Add(c[1],c[2],c[3]);
            if(c[0] == 3) Console.WriteLine(tree.GetSum(c[1],c[2]));
        }
    }
}

class SegTree
{
    private readonly int size;
    private readonly (long sum, long operation)[] tree;

    private const long NeutralOP = -(long)1e11;
    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long,long)[(size << 1) - 1];
        Array.Fill(tree, (0, NeutralOP));
    }

    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => (x << 1) + 2;

    private void PushDown(int x, int lx, int rx)
    {
        if(tree[x].operation == NeutralOP || rx - lx == 1) return;
        var op = tree[x].operation;
        tree[x].operation = NeutralOP;
        var m = lx + (rx - lx) / 2;

        if (tree[Left(x)].operation == NeutralOP)
        {
            tree[Left(x)].operation = op;
            DoOperation(Left(x), m - lx);
        }
        else
        {
            if (op < 0)
            {
                tree[Left(x)].operation = op;
                DoOperation(Left(x), m - lx);
            }
            else
            {
                if (tree[Left(x)].operation >= 0)
                {
                    tree[Left(x)].operation += op;
                    tree[Left(x)].sum += op * (m - lx);
                }
                else
                {
                    tree[Left(x)].operation -= op;
                    DoOperation(Left(x), m - lx);
                } 
            }
        }

        if (tree[Right(x)].operation == NeutralOP)
        {
            tree[Right(x)].operation = op;
            DoOperation(Right(x), rx - m);
            
        }
        else
        {
            if (op < 0)
            {
                tree[Right(x)].operation = op;
                DoOperation(Right(x), rx - m);
            }
            else
            {
                if (tree[Right(x)].operation >= 0)
                {
                    tree[Right(x)].operation += op;
                    tree[Right(x)].sum += op * (rx - m);
                }
                else
                {
                    tree[Right(x)].operation -= op;
                    DoOperation(Right(x), rx - m);
                } 
            }
        }
    }

    private void DoOperation(int x, int len)
    {
        if(tree[x].operation == NeutralOP) return;
        var op = tree[x].operation;
        if (op < 0)
        {
            tree[x].sum = (-op - 1) * len;
        }
        else tree[x].sum += op * len;
    }
    
    private long DoOperation(long prev, int x, int len)
    {
        if (tree[x].operation == NeutralOP) return prev;
        var op = tree[x].operation;
        if (op < 0)
        {
            prev = (-op - 1) * len;
        }
        else prev += op * len;

        return prev;
    }

    private void Add(int l, int r, long v, int x, int lx, int rx)
    {
        PushDown(x, lx,rx);
        if(lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].operation = v;
            DoOperation(x, rx -lx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Add(l,r,v, Left(x), lx, m);
        Add(l,r,v,Right(x),m,rx);
        tree[x].sum = tree[Left(x)].sum + tree[Right(x)].sum;
        DoOperation(x, Math.Min(r,rx) - Math.Max(l,lx));
    }
    
    private void Set(int l, int r, long v, int x, int lx, int rx)
    {
        PushDown(x, lx,rx);
        if(lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].operation = -(v + 1);
            DoOperation(x, rx -lx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Set(l,r,v, Left(x), lx, m);
        Set(l,r,v,Right(x),m,rx);
        tree[x].sum = tree[Left(x)].sum + tree[Right(x)].sum;
        DoOperation(x, Math.Min(r,rx) - Math.Max(l,lx));
    }

    private long GetSum(int l, int r, int x, int lx, int rx)
    {
        if (lx >= r || rx <= l) return 0;
        if (l <= lx && rx <= r) return tree[x].sum;
        var m = lx + (rx - lx) / 2;

        var res = GetSum(l, r, Left(x), lx, m) + GetSum(l, r, Right(x), m, rx);
        return DoOperation(res, x, Math.Min(r, rx) - Math.Max(l, lx));
    }

    public void Add(int l, int r, int v) => Add(l, r, v, 0, 0, size);

    public void Set(int l, int r, int v) => Set(l, r, v, 0, 0, size);

    public long GetSum(int l, int r) => GetSum(l, r, 0, 0, size);
}