using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Text;

namespace TP_2;
     
static class Program
{
    static string[] Read() => Console.ReadLine().Select(x => new string(new[] {x})).ToArray(); // string performed as array of strings
    static long[] ReadL() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray(); // array of longs
    static int[] ReadI() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray(); // array of ints
    static void Write<T>(IEnumerable<T> a) => Console.WriteLine(string.Join(" ", a));

    public static void Main(string[] args)
    {
        var s = Console.ReadLine();
        if(s is null) return;
        unsafe
        {
            fixed (char* a = s) Console.WriteLine(*(a + s.Length));
                
            
        }
        
        
        for (var t =0 /* ReadL()[0]*/; t > 0; --t)
        { 
        }
    }

    static unsafe void ChangeA(string str)
    {
        fixed (char* i = str)
        {
            for (var x = 0; x < str.Length; ++x)
                *(i + x) = *(i + x) == 'a' ? '1' : *(i + x);
        }
        
        Console.WriteLine(str);
    }
}