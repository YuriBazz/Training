namespace TP_3;
class Program
{
    static long[] Read() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
    static void Write<T>(T[] a) => Console.WriteLine(string.Join(" ", a));
    
    // 0 - 6, 1 - 2, 2 - 5, 3 - 5, 4 - 4, 5 - 5, 6 - 6, 7 - 3, 8 - 7, 9 - 6
    // 0 - 9 = 49
    // 10 - 19 = 49 + 2 * 10 = 69
    // 20 - 29 = 49 + 5 * 10 = 99
    // 30 - 39 = 49 + 5 * 10 = 99
    // 40 - 49 = 49 + 4 * 10 = 89
    
    // 100 - 199 = prev + 2 * 100
    static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        if (n == 0)
        {
            Console.WriteLine("Values on level 0: 1");
            return;
        }
        long[] prev = new long[n + 1], curr = new long[n + 1];
        curr[0] = 1;
        for (var i = 0; i < n; ++i)
        {
            Write(i, curr);
            (prev, curr) = (curr, prev);
            for (var k = 0; k < i + 2; ++k)
                curr[k] = prev[k] + (k - 1 < 0 ? 0 : prev[k - 1]);
        }
        Write(n, curr);
    }

    static void Write(int level, long[] ar) =>
        Console.WriteLine($"Values on level {level}: {string.Join(" ", ar.Where(x => x != 0))}");
}

