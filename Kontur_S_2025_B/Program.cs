namespace Kontur_S_2025_B;

class Program
{
    static long[] Read() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
    static void Write<T>(IEnumerable<T> arr, string sep) => Console.WriteLine(string.Join(sep, arr));
    
    static void Main(string[] args)
    {
        Console.ReadLine();
        var a = Read();
        var b = Read();
        Array.Sort(b);
        var max = long.MinValue;
        foreach (var x in a)
        {
            max = Math.Max(max, BS(x,b));
        }
        Console.WriteLine(max);
    }

    static long BS(long x, long[] b)
    {
        var l = -1;
        var r = b.Length;
        while (r > l + 1)
        {
            var m = l + (r - l) / 2;
            if (b[m] < x) l = m;
            else r = m;
        }

        return Math.Min(l == -1 ? long.MaxValue : x - b[l], r == b.Length ? long.MaxValue : b[r] - x);
    }
}