using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Text;
 
namespace TP_1;
 
    
static class Program
{
    static string[] Read() => Console.ReadLine().Select(x => new string(new[] {x})).ToArray();
    static long[] ReadL(Func<long, long>? a = null) => 
        Console.ReadLine()
            .Split(" ")
            .Select(x => a is null ? long.Parse(x) : a(long.Parse(x)))
            .ToArray(); 
    static int[] ReadI(Func<int, int>? a = null) => 
        Console.ReadLine()
            .Split(" ")
            .Select(x => a is null ? int.Parse(x) : a(int.Parse(x)))
            .ToArray(); 
    static ulong[] ReadUL(Func<ulong, ulong>? a = null) => 
        Console.ReadLine()
            .Split(" ")
            .Select(x => a is null ? ulong.Parse(x) : a(ulong.Parse(x)))
            .ToArray();
    static (int val, int ind)[] ReadII(int i = 0) =>
        Console.ReadLine().Split(" ").Select(x => (int.Parse(x), i++)).ToArray();
    static (long val, int ind)[] ReadLI(int i = 0) =>
        Console.ReadLine().Split(" ").Select(x => (long.Parse(x), i++)).ToArray();
    static void Write<T>(IEnumerable<T> a) => Console.WriteLine(string.Join(" ", a));
    static int ReverseComp<T>(T x, T y) where T : IComparable => -1 * x.CompareTo(y);
 
    
    static void Main(string[] args)
    {
        var GENERIC_VARIABLE_NAME = ReadI()[0];
        for (; GENERIC_VARIABLE_NAME > 0; --GENERIC_VARIABLE_NAME)
        {
            Solve();
        }
    }
    /*
    1
    10
    1 3 2 5 4 5 10 10 7 9
    */

    
    static void Solve()
    {
        var n = ReadI()[0];
        var a = ReadI();
        HashSet<int> grey = new(), black = new();
        for (var i = 0; i < n; ++i)
        {
            while (!black.Contains(a[i]))
            {
                if (grey.Contains(a[i]))
                    black.Add(a[i]);
                grey.Add(a[i]);
                i = a[i] - 1;
                
            }
        }

        var count = 0;
        var u = new HashSet<int>();
        for (var i = 0; i < n; ++i)
        {
            if(u.Contains(a[i]) || black.Contains(i + 1)) continue;
            count++;
            u.Add(a[i]);
        }
        
        Console.WriteLine(count + 2);
    }
    
}