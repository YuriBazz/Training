using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_47
{
    static class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        static void Write<T>(T[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
        }
        
        static void Main(string[] args)
        { 
            Console.ReadLine();
            var a = ReadInts();
            var b = ReadInts();
            var res = new long[b.Length];
            for (var (i, j) = (0, 0); j < b.Length; ++j)
            {
                while (i < a.Length && a[i] < b[j])
                    ++i;
                res[j] = i;
            }
            Write(res);
        }
    }
}