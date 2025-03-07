namespace ITMO_93;

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
            if(c[0] == 1) tree.Add(c[1],c[2],c[3]);
            else Console.WriteLine(tree.Get(c[1],c[2]));
        }
    }
}

class SegTree
{
    private int size;
    private (long max, long toAdd)[] tree;

    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => (x << 1) + 2;

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new (long, long)[(size << 1) - 1];
        
    }

    private void PushDown(int x)
    {
        if (tree[x].toAdd == 0 || Right(x) >= tree.Length) return;
        tree[Left(x)].toAdd += tree[x].toAdd;
        tree[Right(x)].toAdd += tree[x].toAdd;
        tree[Left(x)].max += tree[x].toAdd;
        tree[Right(x)].max += tree[x].toAdd;
        tree[x].toAdd = 0;
    }
    
    private void Add(int l, int r, int v, int x, int lx, int rx)
    {
        PushDown(x);
        if(lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].max += v; 
            tree[x].toAdd += v;
            return;
        }

        var m = lx + (rx - lx) / 2;
        Add(l,r,v,Left(x), lx,m);
        Add(l,r,v,Right(x),m,rx);
        tree[x].max = Math.Max(tree[Left(x)].max, tree[Right(x)].max) + tree[x].toAdd;
    }

    private int Get(int v, int l,int x, int lx, int rx)
    {
        if (tree[x].max < v) return -1; 
        if (rx <= l) return -1;
        if (rx - lx == 1) return lx;
        PushDown(x);
        var m = lx + (rx - lx) / 2;
        var left = Get(v, l, Left(x), lx, m);
        return left != -1 ? left : Get(v, l, Right(x), m, rx);
    }

    public void Add(int l, int r, int v) => Add(l, r, v, 0, 0, size);

    public int Get(int x, int l) => Get(x, l, 0, 0, size);
}
