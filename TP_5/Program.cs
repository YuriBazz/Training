using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Text;

namespace TP_5;
static class Program
{
    static string[] Read() => Console.ReadLine().Select(x => new string(new[] {x})).ToArray(); // string performed as array of strings
    static long[] ReadL() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray(); // array of longs
    static int[] ReadI() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray(); // array of ints

    static (int val, int ind)[] ReadII(int i = 0) =>
            Console.ReadLine().Split(" ").Select(x => (int.Parse(x), i++)).ToArray();
    static (long val, int ind)[] ReadLI(int i = 0) =>
            Console.ReadLine().Split(" ").Select(x => (long.Parse(x), i++)).ToArray();
        
        
    static void Write<T>(IEnumerable<T> a) => Console.WriteLine(string.Join(" ", a));

    

    public static void Main(string[] args)
    {
        var tb = new long[21, 181];
        for (var i = 0; i < 10; ++i)
            tb[1, i] = 1;
        for (var x = 2; x < 21; ++x)
            tb[x, 0] = 1;
        for (var len = 2; len < 21; ++len)
        for (var sum = 1; sum < 181; ++sum)
        for (var x = 1; x < 10 && sum - x >= 0; ++x)
            tb[len, sum] += tb[len - 1, sum - x];
            
        for (var t = ReadL()[0]; t > 0; --t)
        {
            var n = ReadI()[0];
            if (n == 0)
            {
                Console.WriteLine(0);
                continue;
            }
            var s = Sum(n);
            var res = 0L;
            for (var len = 0; len < 21; ++len)
                res += tb[len, s];
            Console.WriteLine(res);
            
        }
    }

    static int Sum(int a)
    {
        var b = 0;
        while (a != 0)
        {
            b += a % 10;
            a /= 10;
        }
        return b;
    }
}


    