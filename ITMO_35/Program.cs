using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ITMO_35
{
    static class Program
    {
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static (int, int) ReadPair()
        {
            var token = ReadInts();
            var test = new StringBuilder();
            return (token[0], token[1]);
        }

        static (int, int)[] ReadPairs(int n)
        {
            var res = new (int, int)[n];
            for (var i = 0; i < n; ++i)
                res[i] = ReadPair();
            return res;
        }
        
        static void Main(string[] args)
        {
              var (n, k) = ReadPair();
              var array = ReadPairs(n);
              double left = -1, right = 1e5 + 1;
              for (var iter = 0; iter < 1e2; ++iter)
              {
                  var X = left + (right - left) / 2;
                  if (Check(array, k, X)) left = X;
                  else right = X;
              }
              Console.WriteLine(left);
        }

        static bool Check((int, int)[] array, int k, double X)
        {
            var count = 0;
            var sum = 0.0;
            var temp = array.Select(x => (x.Item1, x.Item2, X)).ToArray();
            Array.Sort(temp, new MyComparer());
            for(var i = temp.Length - 1; i > -1; --i)
            {
                sum += temp[i].Item1 - temp[i].Item2 * temp[i].Item3;
                count++;
                if (count == k) return sum >= 0;
            }
            /*foreach (var item in array)
            {
                if (item.Item1 >= X * item.Item2)
                    count++;
                if (count == k) return true;
            }*/

            return false;
        }
        
    }

    class MyComparer : IComparer<(int,int,double)>
    {
        public int Compare((int,int,double) x, (int,int,double) y)
        {
            return (x.Item1 - x.Item2 * x.Item3).CompareTo(y.Item1 - y.Item2 * y.Item3);
        }
    }
}