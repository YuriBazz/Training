using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_6
{

    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            for (; t > 0; --t)
            {
                Console.WriteLine(Recur(ReadInts()[1]));
            }
        }

        static int Recur(int j, int res = 0)
        {
            if (j % 2 == 1 || j == 0) return res;
            return Recur(j / 2, 2 * res + 1);
        }
        
        static int[] ReadInts()
        {
            return Console.ReadLine().Split().Select(int.Parse).ToArray();
        }
    }
}