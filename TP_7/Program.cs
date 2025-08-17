using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Text;

namespace TP_7;

    
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
    static (int val, int ind)[] ReadII(int i = 0) =>
        Console.ReadLine().Split(" ").Select(x => (int.Parse(x), i++)).ToArray();
    static (long val, int ind)[] ReadLI(int i = 0) =>
        Console.ReadLine().Split(" ").Select(x => (long.Parse(x), i++)).ToArray();
    static void Write<T>(IEnumerable<T> a) => Console.WriteLine(string.Join(" ", a));

    static void Main(string[] args)
    {
        var GENERIC_VARIABLE_NAME = ReadI()[0];
        for (; GENERIC_VARIABLE_NAME > 0; --GENERIC_VARIABLE_NAME)
        {
            var s = Console.ReadLine();
            var a = new char[s.Length + 1];
            for (var x = 0; x < s.Length; ++x)
                a[x] = s[x];
            a[^1] = '\0';
            int j = -1;
            for (int i = 0; i < s.Length + 1; ++i)
            {
                if(j == -1 || a[j] == ' ')
                    while (a[i] != '\0' && a[i] == ' ')
                        i++;
                a[++j] = a[i];
            }
            j--;
            while (j > -1 && a[j] == ' ') j--;
            a[j + 1] = '\0';
            for (var k = 0; k < j + 1; ++k)
                Console.Write(a[k]);
            Console.WriteLine();
        } 
    }
}