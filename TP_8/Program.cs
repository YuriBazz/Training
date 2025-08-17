namespace TP_8;

class Program
{
    static long[] Read() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
    static void Write<T>(T[] a) => Console.WriteLine(string.Join(" ", a));
    
    static void Main(string[] args)
    {
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var p = Read();
            var q = p[1];
            var a = Read();
            while (q-- > 0)
            {
                var c = Read();
                var k = c[0];
                var l = c[1] - 1;
                var r = c[2];
                var res = 0L;
                for (var i = l; i < r; ++i)
                {
                    k = k / BS(k, a[i]);
                    res += k;
                }
                Console.WriteLine(res);
            }
        }
    }

    static long BS(long k, long a)
    {
        var l = 1L;
        var r = Math.Min(k + 1);
        while (r > l + 1)
        {
            var m = l + (r - l) / 2;
            if (k % m == 0) l = m;
            else r = m;
        }

        return l;
    }
}