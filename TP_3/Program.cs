using System.Security.AccessControl;
using Microsoft.VisualBasic.CompilerServices;

namespace TP_3;
class Program
{
    static long[] Read() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();

    static void Write<T>(T[] a) => Console.WriteLine(string.Join(" ", a));
    
    static void Main(string[] args)
    {
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            Console.ReadLine();
            var a = Read();
            for (var i = 0; i < a.Length - 1; ++i)
                if (a[i + 1] % a[i] == 0)
                    a[i]++;
            for (var i = 1; i < a.Length; ++i)
                if (a[i] % a[i - 1] == 0)
                    a[i]++;
            Write(a);
        }
    }
}
