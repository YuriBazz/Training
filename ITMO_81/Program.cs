using System.Dynamic;

namespace ITMO_81;

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
            if(c[0] == 1) tree.Build(c[1],c[2]);
            else Console.Write(tree.Destroy(c[1],c[2],c[3])+"\n");
        }
    }
}

class SegTree
{
    private int size;
    private int[] tree;
    

    public SegTree(int n)
    {
        size = 1;
        while (size < n) size <<= 1;
        tree = new int[2 * size - 1];
        Array.Fill(tree, int.MaxValue);
    }

    public void Build(int i, int h) => Build(i, h, 0, 0, size);

    private void Build(int i, int h, int x, int lx, int rx)
    {
        if (rx - lx == 1)
        {
            tree[x] = h;
            return;
        }

        var m = lx + (rx - lx) / 2;
        if (i < m) Build(i, h, 2 * x + 1, lx, m);
        else Build(i,h,2 * x + 2, m,rx);

        tree[x] = Math.Min(tree[2 * x + 1], tree[2 * x + 2]);
    }

    private int Destroy(int l, int r, int p, int x, int lx, int rx)
    {
        if (rx <= l || lx >= r) return 0;
        if (tree[x] > p) return 0;
        if (rx - lx == 1)
        {
            var c = 1;
            if (tree[x] <= p) c--;
            tree[x] *= c;
            if (tree[x] == 0) tree[x] = int.MaxValue;
            return 1 - c;
        }
        var m = lx + (rx - lx) / 2;
        var des = Destroy(l, r, p, 2 * x + 1, lx, m) + Destroy(l, r, p, 2 * x + 2, m, rx);
        tree[x] = Math.Min(tree[2 * x + 1], tree[2 * x + 2]);
        return des;
    }

    public int Destroy(int l, int r, int p) => Destroy(l, r, p, 0, 0, size);

}