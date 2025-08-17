namespace Kontur_S_2025_D;

class Program
{
    static long[] Read() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
    static void Write<T>(IEnumerable<T> arr, string sep) => Console.WriteLine(string.Join(sep, arr));
    
    static void Main(string[] args)
    {
        var inp = Read();
        var n = inp[0];
        var m = inp[1];
        var k = inp[2];
        var gr = new Graph(n,k);
        while (m-- > 0)
        {
            var p = Read();
            gr.Add(p[0], p[1],p[2]);
        }
        
        Console.WriteLine(gr.Solve()); 
    }
}

class Graph
{
    private readonly Dictionary<long, List<(long, long)>> gr; // entry : (exit, cost)
    private readonly long K;
    private long min;
    
    public Graph(long n,long k)
    {
        K = k;
        gr = new Dictionary<long, List<(long, long)>>();
        min = -1;
        for (var i = 1; i <= n; ++i)
            gr[i] = new List<(long, long)>();
        
    }

    private IEnumerable<(long vertex, long cost)> GetIncidentsWithCost(long vertex) => gr[vertex];

    public void Add(long u, long v, long s)
    {
        min = Math.Max(s, min);
        gr[u].Add((v, s));
    }

    private bool DFS(long start, long weight, out long max)
    {
        var stack = new Stack<(long vertex, long count, long maxCost)>();
        stack.Push((start, 0,  -1));
        while (stack.Count > 0)
        {
            var triple = stack.Pop();
            if ((triple.vertex == start && triple.count >= 1) || triple.count == K) 
            {
                max = triple.maxCost;
                return true;
            }
            foreach(var next in GetIncidentsWithCost(triple.vertex).Where(p => p.cost <= weight))
                stack.Push((next.vertex, triple.count + 1, Math.Max(next.cost, triple.maxCost)));
        }

        max = -1;
        return false;
    }

    public long Solve()
    {
        var l = -1L;
        var r = (long)1e12;
        
        while (r > l + 1)
        {
            var mid = l + (r - l) / 2;
            var temp = 0L;
            if (gr.Keys.Any(vertex => DFS(vertex, mid, out temp)))
            {
                r = Math.Min(temp, mid);
            }
            else l = mid;
        }

        var t = 0L;
        return r < 1e12 ? r : -1;
    }
}
