using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_38
{

    class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        static void Main(string[] args)
        {
            var token = Console.ReadLine().Split(" ");
            var n = int.Parse(token[0]);
            var k = long.Parse(token[1]);
            int[] a = ReadInts(), b = ReadInts();
            Array.Sort(a);
            
            long left = -1, right = (long)2e9 + 1;
            while (right > left + 1)
            {
                var middle = left + (right - left) / 2;
                var sum = 0L;
                for (var i = 0; i < n; ++i)
                    sum += InsideBS(a, middle - b[i]) + 1;
                if (sum <= k - 1) left = middle;
                else right = middle;
            }
            Console.WriteLine(left + 1);
        }

        static int InsideBS(int[] array, long value)
        {
            int left = -1, right = array.Length;
            while (right > left + 1)
            {
                var middle = left + (right - left) / 2;
                if (array[middle] <= value) left = middle;
                else right = middle;
            }
            return left;
        }
    }
}