using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace ITMO_33
{
    //TODO: Когда-нибудь в этом разобраться
    static class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        static double[] ReadDoubles() => Console.ReadLine().Split(" ").Select(double.Parse).ToArray();
        
        static double[] Array(double[] array, double X) => array.Select(x => x - X).ToArray();
        static double[] PrefixSum(double[] array, double weigh)
        {
            var r = new double[array.Length];
            r[0] = array[0] - weigh;
            for (var i = 1; i < array.Length ; ++i)
                r[i] = r[i - 1] + array[i] - weigh;
            return r;
        }

        static (double,int)[] MinArray(double[] array)
        {
            var r = new (double, int)[array.Length];
            r[0] = (array[0], 0);
            for (var i = 1; i < r.Length; ++i)
            {
                if (r[i - 1].Item1 < array[i])
                    r[i] = r[i - 1];
                else r[i] = (array[i], i);
            }

            return r;
        }
        
        static void Main(string[] args)
        {
            // В общем, я отчаялся написать так, как разбиралось на cf
            var d = ReadInts()[1];
            var array = ReadDoubles();
            int rl = -1, rr = -1;
            double left = 0, right = 101;
            for (var iter = 0; iter < 100; ++iter)
            {
            }
            Console.WriteLine(left);
            Console.WriteLine(rl + " " + rr);
        }
        
    }
}