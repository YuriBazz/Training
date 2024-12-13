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
        static double[] PrefixSum(double[] array, double weigh)
        {
            var r = new double[array.Length + 1];
            r[0] = 0;
            for (var i = 1; i < array.Length + 1 ; ++i)
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
            double left = -1, right = 101;
            int rl = -1, rr = -1;
            var array = ReadDoubles();
            if (d == 1)
            {
                double max = -1;
                var index = -1;
                for(var i = 0; i < array.Length; ++i)
                    if (array[i] > max)
                    {
                        max = array[i];
                        index = i;
                    }
                Console.WriteLine(index+ 1 + " " + (index + 1));
                return;
            }
            for (var iter = 0; iter < 200; ++iter)
            {
                var X = left + (right - left) / 2;
                var p = PrefixSum(array, X);
                var m = MinArray(p);
                var a = PrefixSum(array, 0);
                var f = false;
                for (var r = d ; r < array.Length + 1; ++r)
                {
                    if (m[r- d].Item1 <= p[r]) 
                    {
                        rr = r;
                        f = true;
                        rl = m[r - d].Item2;
                        break;
                    }
                    
                }
                /*Console.WriteLine($"--------{{ ITERATION {iter} }}--------");
                Console.WriteLine($"X = {X}");
                Console.WriteLine($"p = [{string.Join(",", p)}]");
                Console.WriteLine($"m = [{string.Join(",", m)}]");
                Console.WriteLine($"Flag is {f}");
                Console.WriteLine();*/
                if (f) left = X;
                else right = X;
            }
            Console.WriteLine(rl + 1  +  " " + (rr));
            //Console.WriteLine(left);
            //Console.WriteLine($"p = [{string.Join(", ", PrefixSum(array, 0 ))}]");
           // Console.WriteLine($"p = [{string.Join(", ", PrefixSum(array, 6.666666666 ))}]");
            //Console.WriteLine($"p = [{string.Join(", ", MinArray(PrefixSum(array, 6.6666666666666 )))}]");
        }
        
    }
}
