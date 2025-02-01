using System;
using System.Collections.Generic;
using System.Linq;

// TODO: Не работает
namespace ITMO_45
{
    static class Program
    {
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                var temp = new Temp();
                var set = new HashSet<string>();
                Console.ReadLine();
                Console.ReadLine();
                var array = Console.ReadLine().Split(" ").Select(int.Parse).ToList().CreateDegreesArray();
                Array.Sort(array,temp);
                var falseCounter = array.Count(x => !x.Item3);
                while (array[0].Item1 != 0)
                {
                    if (array[0].Item3)
                    {
                        falseCounter++;
                        array[0].Item3 = false;
                    } 
                    for (var next = 1; next < array.Length; ++next)
                    {
                        if (array[0].Item1 == 0) break;
                        if (!array[next].Item3  && array[next].Item1 == 0) continue;
                        array[next].Item1--;
                        array[0].Item1--;
                        if (!set.Contains($"{array[next].Item2 + 1} {array[0].Item2 + 1}"))
                            set.Add($"{array[0].Item2 + 1} {array[next].Item2 + 1}");
                    }

                    if (falseCounter != array.Length - 1)
                    {
                        for(var i = 0; i < array.Length; ++i)
                            if (array[i].Item1 <= 0 && array[i].Item3)
                                array[i].Item1 = 1;
                    }
                    Array.Sort(array,temp);
                }
                Console.WriteLine($"{array.Length} {set.Count}");
                foreach (var item in set)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static (int,int,bool)[] CreateDegreesArray(this List<int> list)
        {
            var n = list.Max() + 1;
            var res = new (int, int, bool)[n];
            for (var i = 0; i < n; ++i)
            {
                if (i >= list.Count)
                    res[i] = (0, i, true);
                else
                    res[i] = (list[i], i, false);
            }
            return res;
        }
    }

    class Temp : IComparer<(int,int,bool)>
    {
        public int Compare((int, int, bool) x, (int, int, bool) y)
        {
            return -1 * x.Item1.CompareTo(y.Item1);
        }
    }
}