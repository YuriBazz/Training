namespace Kontur_S_2025_E;

class Program
{
    // ОНО НЕ РАБОТАЕТ; Я В ТИЛЬТЕ; НАДЕЮСЬ ЭТО КТО-ТО ПРОЧИТАЕТ И УВИДИТ, ЧТО Я ХОТЯ БЫ ПЫТАЛСЯ 
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    static void Write<T>(IEnumerable<T> arr, string sep) => Console.WriteLine(string.Join(sep, arr));
    
    static void Main(string[] args)
    {
        var p = Read();
        var n = p[0];
        var k = p[1];
        var q = p[2];
        var db = new Cluster[n];
        for (var i = 0; i < n; ++i) db[i] = new Cluster();
        var str = Read();
        for(var i = 0; i < n; ++i)
            for(var j = 0; j < str[i]; ++j) db[i].Append(new Server());
        while (q-- > 0)
        {
            var inp = Console.ReadLine().Split();
            var c = int.Parse(inp[1]) - 1;
            var s = int.Parse(inp[2]);
            var v = inp[3];
            db[c].L1 = db[c].L2;
            db[c].L2 = s;
            db[c].Count++;
            switch (inp[0])
            {
                case "+":
                {
                    db[c].GetOnI(db[c].Root, s).Server.Add(v);
                    break;
                }
                case "p":
                {
                    db[c].GetOnI(db[c].Root,s).Server.SetPref(v);
                    break;
                }
                case "c":
                {
                    Console.WriteLine(db[c].GetOnI(db[c].Root, s).Server.Count(v));
                    break;
                }
            }

            if (db[c].Count != k) continue;
            //Да, я понял, что merge и split стоило сделать статическими методами (не бейте >_<)
            var l = Math.Min(db[c].L1, db[c].L2);
            var r = Math.Max(db[c].L1, db[c].L2);
            var (left, midRight) = db[c].Split(db[c].Root, l - 1);
            var (mid, right) = db[c].Split(midRight, r - l + 1);
            db[c].Root = db[c].Merge(left, right);
            db[c].Count = 0;
            db[c].L2 = 0;
            db[c].L1 = 0;
            var target = db[(c + 1) % n];
            var cent = target.Count / 2;
            var (leftTarget, other) = target.Split(target.Root, cent);
            var ll = target.Merge(leftTarget, mid);
            target.Root = target.Merge(ll, other);
        }
    }
}

class ServerNode
{
    public const int Shift = 'a';
    public readonly ServerNode?[] Children;
    public int Count;

    public ServerNode()
    {
        Children = new ServerNode[26];
        Count = 0;
    }

    public void Add(string s)
    {
        if (s.Length == 0) return;
        var temp = this;
        temp.Count++;
        foreach (var l in s)
        {
            
            if (temp.Children[l - Shift] == null)
                temp.Children[l - Shift] = new ServerNode();
            temp = temp.Children[l - Shift];
            temp.Count++;
        }
    }

    public int StartWith(string s)
    {
        var temp = this;
        foreach (var l in s)
        {
            if (temp.Children[l - Shift] == null) return 0;
            temp = temp.Children[l - Shift];
        }

        return temp.Count;
    }
}

class Server
{
    private string ToProp;
    private ServerNode data;

    public Server()
    {
        ToProp = "";
        data = new ServerNode();
    }

    private void Propagate()
    {
        if (data.Count == 0) ToProp = "";
        if (ToProp.Length == 0) return;
        
        var temp = new ServerNode();
        var temp2 = temp;
        temp.Count++;
        foreach (var l in ToProp)
        {
            temp.Children[l - ServerNode.Shift] = new ServerNode();
            temp = temp.Children[l - ServerNode.Shift];
            temp.Count++;
        }

        for (var i = 0; i < 26; ++i)
            temp.Children[i] = data.Children[i];
        data = temp2;
        ToProp = "";
    }

    public void SetPref(string s)
    {
        Propagate();
        ToProp = s;
    }

    public void Add(string s)
    {
        Propagate();
        data.Add(s);
    }

    public int Count(string s)
    {
        Propagate();
        return data.StartWith(s);
    }
}

class ClusterNode
{
    public int Size;
    public int y;
    public Server Server;
    public ClusterNode? Left, Right;

    public ClusterNode(Server server, Random rnd)
    {
        Server = server;
        y = rnd.Next();
        Size = 1;
    }
}

class Cluster
{
    public ClusterNode? Root;
    private Random rnd = new();
    public int Count = 0;
    public int L1 = 0, L2 = 0;
    public ClusterNode Merge(ClusterNode? left, ClusterNode? right)
    {
        if (left == null) return right;
        if (right == null) return left;

        if (left.y < right.y)
        {
            left.Right = Merge(left.Right, right);
            left.Size = SizeOf(left.Left) + SizeOf(left.Right) + 1;
            return left;
        }

        right.Left = Merge(left, right.Left);
        right.Size = SizeOf(right.Left) + SizeOf(right.Right) + 1;
        return right;
    }

    private int SizeOf(ClusterNode? node) => node == null ? 0 : node.Size;

    public void Append(Server server) => Root = Merge(Root, new ClusterNode(server, rnd));

    public ClusterNode? GetOnI(ClusterNode? node, int i) // a.Left.Size == L then a has L + 1 index <=> a has L + 1 index when a.Left.Size == L; L \in \[0, c_i\)
    {
        if (node == null) return null;
        if (i == SizeOf(node.Left) + 1) return node;
        if (i <= SizeOf(node.Left)) return GetOnI(node.Left, i);
        return GetOnI(node.Right, i - SizeOf(node.Left) - 1);
    }

    public (ClusterNode? left, ClusterNode? right) Split(ClusterNode? node, int last)
    {
        if (node == null) return (null, null);
        if (last <= SizeOf(node.Left))
        {
            var (l, r) = Split(node.Left, last);
            node.Left = r;
            node.Size = SizeOf(node.Left) + SizeOf(node.Right) + 1;
            return (l, node);
        }
        else
        {
            var (l, r) = Split(node.Right, last - SizeOf(node.Left) - 1);
            node.Right = l;
            node.Size = SizeOf(node.Left) + SizeOf(node.Right) + 1;
            return (node, r);
        }
    }
}