namespace TP_2;

class Program
{
    private const long mod = (long)1e9 + 7;
    
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        var d = new Dictionary<int, long>();
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var n = int.Parse(Console.ReadLine());
            if (!d.ContainsKey(n)) d[n] = Fact(n);
            Console.WriteLine(d[n] * n % mod * (n - 1) % mod);
        }
    }

    static long Fact(int n)
    {
        var res = 1L;
        for (var i = 1; i <= n; ++i)
        {
            res *= i;
            res %= mod;
        }

        return res;
    }
}