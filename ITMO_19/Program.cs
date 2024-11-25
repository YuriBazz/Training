using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_19
{

    static class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            var array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var q = Console.ReadLine().Split().Select(int.Parse).ToArray();
            foreach (var x in q)
            {
                Console.WriteLine(array.BS(x) + 1);
            }
        }

        static int BS(this int[] array, int value)
        {
            int l = -1, r = array.Length;
            while (r > l + 1)
            {
                int m = l + (r - l) / 2;
                if (array[m] < value) l = m;
                else r = m;
            }

            return r;
        }
    }
}