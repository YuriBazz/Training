using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_48
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
            if (a.Length < b.Length)
                (a, b) = (b, a);
            long temp = 0, res = 0;
            for (var (i, j) = (0, 0); j < b.Length; ++j)
            {
                if (j > 0 && b[j] == b[j - 1])
                {
                    res += temp;
                    continue;
                }
                
                temp = 0;
                while (i < a.Length && a[i] <= b[j])
                {
                    temp += a[i] == b[j] ? 1 : 0;
                    ++i;
                }
                res += temp;
            }
            Console.WriteLine(res);
        }
    }
}