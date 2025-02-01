using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_46
{
    static class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        static void Write(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
        }
        
        static void Main(string[] args)
        {
            var nm = ReadInts();
            var a = ReadInts();
            var b = ReadInts();
            var res = new int[a.Length + b.Length];
            for (var (i, j) = (0, 0); i < a.Length || j < b.Length;)
            {
                if (i == a.Length)
                {
                    res[i + j] = b[j];
                    ++j;
                    continue;
                }
                if (j == b.Length)
                {
                    res[i + j] = a[i];
                    ++i;
                    continue;
                }
                if (a[i] < b[j])
                {
                    res[i + j] = a[i];
                    ++i;
                }
                else
                {
                    res[i + j] = b[j];
                    ++j;
                }
            }
            
            Write(res);
        }
    }
}