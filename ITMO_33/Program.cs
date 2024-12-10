using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace ITMO_33
{
    
    static class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        static double[] ReadDoubles() => Console.ReadLine().Split(" ").Select(double.Parse).ToArray();

        static double[] PrefixSum(double[] array, double weigh)
        {
            var r = new double[array.Length];
            r[0] = array[0] - weigh;
            for (var i = 1; i < array.Length ; ++i)
                r[i] = r[i - 1] + array[i - 1] - weigh;
            return r;
        }

        static (double,int)[] MinArray(double[] array)
        {
            var r = new (double, int)[array.Length];
            r[0] = (array[0], 0);
            for (var i = 1; i < r.Length; ++i)
            {
                if (r[i - 1].Item1 <= array[i])
                    r[i] = r[i - 1];
                else r[i] = (array[i], i);
            }

            return r;
        }
        
        static void Main(string[] args)
        {
            var d = ReadInts()[1];
            var array = ReadDoubles();
            int rl = -1, rr = -1;
            double left = 0, right = 1e7;
            for (var iter = 0; iter < 200; ++iter)
            {
                //TODO: Понять, почему не обрабатывается последний элемент массива
                var X = left + (right - left) / 2;
                var p = PrefixSum(array, X);
                var m = MinArray(p);
                var f = false;
                Console.WriteLine($"Iteration number is {iter}");
                Console.WriteLine($"X = {X}");
                Console.WriteLine($"p = [{string.Join(", ", p)}]");
                Console.WriteLine($"m = [{string.Join(", ", m)}]");
                Console.WriteLine("------------------");
                for (var r = d ; r < array.Length; ++r)
                {
                    if (m[r - d].Item1 <= p[r])
                    {
                        rr = r;
                        rl = m[r - d].Item2;
                        f = true;
                        break;
                    }
                }

                if (f) left = X;
                else right = X;
            }
            
            Console.WriteLine(rl + 1 + " " + rr);
        }
        
    }
}