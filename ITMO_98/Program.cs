namespace ITMO_98;
 
class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        var nk = Read();
        var tree = new SegTree(nk[0]);
        while (nk[1]-- > 0)
        {
            var c = Read();
            tree.Change(c[0],c[1],c[2],c[3]);
        }
        for(var i = 0; i < nk[0]; ++i)
            Console.WriteLine(tree.Get(i));
    }
}
 
class SegTree
{
    private int size;
    private ((int min, int max) value, (int min, int max) h)[] tree;
 
    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new ((int max, int min), (int, int) h)[(size << 1) - 1];
    }
 
    private (int,int) NeutralOp = (-1,(int)1e5 + 1);
 
    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => Left(x) + 1;
    
    private (int, int) Modify((int l, int r) op1, (int l, int r) op2)
    {
        if (op2 == NeutralOp)  return op1;
        if (op1 == NeutralOp) return op2;
        if ( op1.r< op2.l)
            return (op2.l, op2.l);
        

        if (op2.r < op1.l)
            return (op2.r, op2.r);

        return (Math.Max(op1.l, op2.l), Math.Min(op1.r, op2.r));
    }

    private void Update(int x, int lx, int rx)
    {
        if (tree[x].h == NeutralOp) return;
        tree[x].value = Modify(tree[x].value, tree[x].h);
        
    }
    
    private void Push(int x, int lx, int rx)
    {
        if (rx - lx == 1 || tree[x].h == NeutralOp) return;
        var op = tree[x].h;
        tree[x].h = NeutralOp;
        tree[Left(x)].h = Modify(tree[Left(x)].h, op);
        tree[Right(x)].h = Modify(tree[Right(x)].h, op);
        var m = lx + (rx - lx) / 2;
        Update(Left(x),lx,m);
        Update(Right(x),m,rx);
    }

    private void Change(int l, int r, (int, int) h, int x, int lx, int rx)
    {
        Push(x, lx,rx);
        if(lx >= r || rx <= l) return;
        if (l <= lx && rx <= r)
        {
            tree[x].h = h;
            Update(x,lx,rx);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Change(l,r,h,Left(x),lx,m);
        Change(l,r,h,Right(x),m,rx);

        tree[x].value.max = Math.Max(tree[Left(x)].value.max, tree[Right(x)].value.max);
        tree[x].value.min = Math.Min(tree[Left(x)].value.min, tree[Right(x)].value.min);
    }

    public void Change(int check, int l, int r, int h) => Change(l, r + 1, check == 1 ? (h, (int)1e5) : (0, h), 0, 0, size);

    private int Get(int i, int x, int lx, int rx)
    {
        Push(x,lx,rx);
        if (rx - lx == 1) return tree[x].value.min;

        var m = lx + (rx - lx) / 2;
        if (i < m) return Get(i, Left(x), lx, m);
        return Get(i, Right(x), m, rx);
    }

    public int Get(int i) => Get(i, 0, 0,size);
}