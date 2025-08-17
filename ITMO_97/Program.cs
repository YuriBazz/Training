namespace ITMO_97;
class Program
{
    // КАК-ТО МОЖНО ПОСТРОИТЬ ДЕРЕВО, НА СУММУ, СО СТРАННОЙ ОПЕРАЦИЕЙ => ПОНЯТЬ, ЧТО ЗА ОПЕРАЦИЯ
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        var t = Read();
        var a = Read();
        var tree = new SegTree(a);
        while (t[1]-- > 0)
        {
            var c = Read();
            if(c[0] == 1) tree.Add(c[1],c[2],c[3]);
            else Console.WriteLine(tree.Sum(c[1],c[2]));
        }
    }
}

class SegTree
{
    private int size;
    private (long value1, long value2, long op)[] tree;
    private long[] pref;

    public SegTree(int[] a)
    {
        size = 1;
        while (size < a.Length) size <<= 1;
        tree = new (long,long, long)[2 * size - 1];
        pref = new long[size + 1];
        for (var i = 1; i <= size; ++i)
            pref[i] = pref[i - 1] + i; 
        Build(a,0,0,size);
    }

    private void Reset(int x, int lx, int rx)
    {
        long val1, val2;
        if (rx - lx == 1)
        {
            val1 = 0;
            val2 = 0;
        }
        else
        {
            val1 = tree[Left(x)].value1 + tree[Right(x)].value1;
            val2 = tree[Left(x)].value2 + tree[Right(x)].value2 + tree[Right(x)].value1 * (rx - lx) / 2;
        }

        val1 += (rx - lx) * tree[x].op;
        val2 +=  (rx - lx) * (rx - lx + 1) / 2 * tree[x].op;
        tree[x] = (val1, val2, tree[x].op);
    }

    private void PushDown(int x, int lx, int rx)
    {
        if (rx - lx == 1 || tree[x].op == 0) return;
        var op = tree[x].op;
        tree[x].op = 0;
        tree[Left(x)].op += op;
        tree[Right(x)].op += op;
        var m = lx + (rx - lx) / 2;
        Reset(Left(x), lx, m);
        Reset(Right(x),m,rx);
        Reset(x, lx,rx);
    }
    
    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => Left(x) + 1;

    private void Build(int[] a, int x, int lx, int rx)
    {
        if (rx - lx == 1)
        {
            if (lx < a.Length) tree[x] = ( a[lx], a[lx], a[lx]);
            else tree[x] = (0, 0,0);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Build(a,Left(x),lx,m);
        Build(a,Right(x),m,rx);

        tree[x].value1 = tree[Left(x)].value1 + tree[Right(x)].value1;
        tree[x].value2 = tree[Left(x)].value2 + tree[Right(x)].value2 + (rx - lx) * tree[Right(x)].value1 / 2;
    }

    private void Add(int l, int r, int d, int x, int lx, int rx)
    {
        PushDown(x, lx, rx);
        if(lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].op += d;
            Reset(x,lx,rx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Add(l,r,d,Left(x),lx,m);
        Add(l,r,d,Right(x),m,rx);
        
        tree[x].value1 = tree[Left(x)].value1 + tree[Right(x)].value1;
        tree[x].value2 = tree[Left(x)].value2 + tree[Right(x)].value2 + (rx - lx) * tree[Right(x)].value1 / 2;
    }

    public void Add(int l, int r, int d) => Add(l - 1, r, d, 0,0,size);

    private long Sum(int l, int r, int x, int lx, int rx)
    {
        PushDown(x,lx,rx);
        if (lx >= r || rx <= l) return 0;
        if (l <= lx && rx <= r)
        {
            return tree[x].value2 + tree[x].value1 * (lx - l);
        }

        var m = lx + (rx - lx) / 2;
        return Sum(l, r, Left(x), lx, m) + Sum(l, r, Right(x), m, rx);
    }

    public long Sum(int l, int r) => Sum(l - 1, r, 0, 0, size);
}