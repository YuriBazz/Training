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
            //if(c[0] == 1) tree.Add(c[1],c[2],c[3]);
            //else Console.WriteLine(tree.Sum(c[1],c[2]));
        }
    }
}

class SegTree
{
    private int size;
    private (long value1, long value2, long oper1, long oper2)[] tree;
    private long[] pref;

    public SegTree(int[] a)
    {
        size = 1;
        while (size < a.Length) size <<= 1;
        tree = new (long,long, long, long)[2 * size - 1];
        pref = new long[size + 1];
        for (var i = 1; i <= size; ++i)
            pref[i] = pref[i - 1] + i; 
        Build(a,0,0,size);
    }
    
    private int Left(int x) => (x << 1) + 1;
    private int Right(int x) => Left(x) + 1;

    private void Build(int[] a, int x, int lx, int rx)
    {
        if (rx - lx == 1)
        {
            if (lx < a.Length) tree[x] = ( a[lx], a[lx], 0, 0);
            else tree[x] = (0, 0, 0,0);
            return;
        }

        var m = lx + (rx - lx) / 2;
        Build(a,Left(x),lx,m);
        Build(a,Right(x),m,rx);

        tree[x].value1 = tree[Left(x)].value1 + tree[Right(x)].value1;
        tree[x].value2 = tree[Left(x)].value2 + tree[Right(x)].value1 + (rx - lx) * tree[Right(x)].value1 / 2;
    }
    

}