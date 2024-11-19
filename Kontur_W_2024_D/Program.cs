namespace Kontur_W_2024_D;

class Program
{
    static void Main(string[] args)
    {
        var token = ReadLongs();
        var map = new Map(token[0], token[1]);
        Console.ReadLine();
        map.AddX(ReadLongs());
        map.AddY(ReadLongs());
        map.Prep();
        for (var q = int.Parse(Console.ReadLine()); q > 0; q--)
        {
            var input = ReadLongs();
            if(map.CheckPoints((input[0],input[1]),(input[2],input[3])))
                Console.WriteLine("YES");
            else
                Console.WriteLine("NO");
        }
    }

    static long[] ReadLongs()
    {
        return Console.ReadLine().Split().Select(long.Parse).ToArray();
    }
}

class Map
{
    private readonly long N, M;
    private readonly List<long> U, V;
    private long[] U1, V1;
    private readonly Dictionary<(long, long), (int, int)> memory;

    public Map(long n, long m)
    {
        N = n;
        M = m;
        U = new List<long>();
        V = new List<long>();
        memory = new Dictionary<(long, long), (int, int)>();
    }

    private void AddX(long x) => U.Add(x);
    private void AddY(long y) => V.Add(y);

    private int BS(long value, long[] array)
    {
        int l = 0, r = array.Length;
        while (l < r)
        {
            if (array[l] < value && array[l + 1] > value)
                return l;
            var mid = l + (r - l) / 2;
            if (array[mid] < value && array[mid + 1] > value)
                return mid;
            if (array[mid] > value)
                r = mid;
            else
                l = mid + 1;
        }

        return -1;
    }
    
    private (int,int) MarkPoint((long, long) p)
    {
        var t = (BS(p.Item1, U1), BS(p.Item2, V1));
        memory[p] = t;
        return t;
    }
    
    public bool CheckPoints((long, long) p1, (long, long) p2)
    {
        var mark1 = memory.ContainsKey(p1) ? memory[p1] : MarkPoint(p1);
        var mark2 = memory.ContainsKey(p2) ? memory[p2] : MarkPoint(p2);
        return mark1 == mark2;
    }

    public void AddX(IEnumerable<long> input)
    {
        foreach (var item in input)
            AddX(item);
    }

    public void AddY(IEnumerable<long> input)
    {
        foreach (var item in input)
            AddY(item);
    }

    public void Prep()
    {
        U.Add(long.MinValue);
        V.Add(long.MinValue);
        U.Add(long.MaxValue);
        V.Add(long.MaxValue);
        U.Sort();
        V.Sort();
        U1 = U.ToArray();
        V1 = V.ToArray();
    }
}