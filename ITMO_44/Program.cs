using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_44
{
    static class Program
    {
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                Console.ReadLine();
                var n = int.Parse(Console.ReadLine());
                var array = new (int, int)[n];
                var i = 0;
                foreach (var deg in Console.ReadLine().Split(" ").Select(int.Parse))
                {
                    array[i] = (deg, i);
                    ++i;
                }
                var set = new HashSet<string>();
                var temp = new Temp();
                Array.Sort(array, temp);
                while (array[0].Item1 != 0)
                {
                    for (var next = 1; next < array.Length; ++next)
                    {
                        if (array[0].Item1 == 0) break;
                        array[0].Item1--;
                        array[next].Item1--;
                        set.Add($"{array[0].Item2 + 1} {array[next].Item2 + 1}");
                    }

                    Array.Sort(array, temp);
                }
                Console.WriteLine(set.Count);
                foreach (var pair in set)
                    Console.WriteLine(pair);
            }
        }
    }

    class Temp : IComparer<(int,int)>
    {
        public int Compare((int, int) x, (int, int) y) => -1 * x.Item1.CompareTo(y.Item1);
    }
}