namespace ITMO_80;

static class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        var t = Read();
        var a = Read();
        var tree = new SegTree(a);
        while (t[1]-- > 0)
        {
            var c = Read();
            if (c[0] == 1) Console.WriteLine(tree.Find(c[1], c[2]));
            else tree.Set(c[1],c[2]);
        }
    }
}

class SegTree
{
    public (int[]?, int count) temp;
    public int size;
    public (int[]? arr, int count)[] tree;

    public SegTree(int[] a)
    {
        size = 1;
        while (size < a.Length) size <<= 1;
        tree = new (int[]?, int)[2 * size - 1];
        Build(a,0,0,size);
    }

    private (int[]?,int) Do((int[]? arr, int count) left, (int[]? arr, int count) right)
    {
        if(left.arr is null) return right;
        if (right.arr is null) return left;

        var arr = new int[40];
        var count = 0;
        for (var i = 0; i < 40; ++i)
        {
            arr[i] = left.arr[i] + right.arr[i];
            count += arr[i] != 0 ? 1 : 0;
        }

        return (arr, count);
    }

    private void Build(int[] a, int x, int lx, int rx)
    {
        if (rx - lx == 1)
        {
            if (lx < a.Length)
            {
                tree[x] = (new int[40], 1);
                tree[x].arr[a[lx] - 1]++;
            }
            return;
        }

        var m = lx + (rx - lx) / 2;
        Build(a, 2 * x + 1, lx, m);
        Build(a, 2 * x + 2, m, rx);

        tree[x] = Do(tree[2 * x + 1], tree[2 * x + 2]);
    }

    private void Find(int l, int r, int x, int lx, int rx)
    {
        if (l >= rx || r <= lx) return;
        if (l <= lx && rx <= r) {
            temp = Do(temp, tree[x]);
            return;
        }
        var m = lx + (rx - lx) / 2;
        Find(l,r,2 * x + 1, lx,m);
        Find(l, r, 2 * x + 2, m, rx);
    }

    public int Find(int l, int r)
    {
        temp = (null, 0);
        Find(l-1,r,0,0,size);
        return temp.count;
    }

    private void Set(int i, int val, int x, int lx, int rx)
    {
        if (rx - lx == 1)
        {
            tree[x] = (new int[40], 1);
            tree[x].arr[val - 1]++;

            while (x != 0)
            {
                x = (x - 1) >> 1;
                tree[x] = Do(tree[2 * x + 1], tree[2 * x + 2]);
            }

            return;
        }

        var m = lx + (rx - lx) / 2;
        if(i < m) Set(i, val, 2 * x + 1, lx,m);
        else Set(i,val, 2* x + 2, m,rx);
    }

    public void Set(int i, int val) => Set(i - 1, val, 0,0,size);
}