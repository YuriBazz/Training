using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TP_4;

class Program
{
    static long[] Read() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
    static int[] ReadI() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray(); // array of ints
    static HashSet<long> ReadSet() => Console.ReadLine().Split(" ").Select(long.Parse).ToHashSet();
    static void Write<T>(IEnumerable<T> a) => Console.WriteLine(string.Join(" ", a));
    private static Random seed = new();
    
    static void Main(string[] args)
    {
        var a = ReadI();
        Sort(a);
        Write(a);
        // ДА ХУЛИ ОНО МЕДЛЕННО ТО БЛЯТЬ
        
        // А С СОРТИРОВКОЙ ЧТО СЛУЧИЛОСЬ СУКА
        
    }
    
    static int Bs(int[] array, int value)
    {
        int l = -1, r = array.Length;
        while(r - l > 1)
        {
            var m = l + (r - l) / 2;
            if(array[m] <= value) l = m;
            else r = m;
        }
        return l;
    }

    static int Bs1(int[] array, int value)
    {
        int l = -1, r = array.Length;
        while(r - l > 1)
        {
            var m = l + (r - l) / 2;
            if(array[m] < value) l = m;
            else r = m;
        }
        return l;
    }

    static void Swap(ref int a, ref int b) => (a, b) = (b, a);
    
    static void Sort(int[] a) => Sort(a, 0, a.Length);

    static void Sort(int[] v, int l, int r)
    {
        if(r - l <= 1) return;
        int t1 = seed.Next(l, r), x = v[t1];
        int i = l, j = l; // a[k] <= x : k < i, a[k] < x : k < j
        for(var k = l; k < r; ++k)
            if(v[k] <= x) Swap(ref v[k], ref v[i++]);
        
        Sort(v, i, r); // I can sort all greater elements

        for (var k = l; k < i; ++k)
            if(v[k] < x) Swap(ref v[k], ref v[j++]);

        Sort(v, l, j); // Now I can finally sort all fewer elements
    }
}
