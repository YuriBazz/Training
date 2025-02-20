using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_63
{
    class Program
    {
        static void Main(string[] args)
        {
            var all = new List<(int, int)>();
            var check = new Dictionary<int, int>
            {
                { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }
            };

            for (var i = 0; i < 4; ++i)
            {
                Console.ReadLine();
                all.AddRange(Console.ReadLine().Split(" ").Select(x => (int.Parse(x), i)));
            }

            all.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            var min = int.MaxValue;
            IEnumerable<int> res = new int[1];
            for (var (l, r) = (0, 0); r < all.Count; ++r)
            {
                check[all[r].Item2]++;
                while (l < all.Count && Check(check))
                {
                    check[all[l++].Item2]--;
                }

                if (l > 0 && all[r].Item1 - all[l- 1].Item1 < min)
                {
                    min = all[r].Item1 - all[l - 1].Item1;
                    var temp = new List<(int, int)> { all[l-1] };
                    var set = new HashSet<int> { all[r].Item2, all[l - 1].Item2 };
                    for (var j = l; j < r + 1; ++j)
                    {
                        if(!set.Contains(all[j].Item2))
                        {
                            temp.Add(all[j]);
                            set.Add(all[j].Item2);
                        }
                    }
                    temp.Add(all[r]);
                    temp.Sort((x,y) => x.Item2.CompareTo(y.Item2));
                    res = temp.Select(x => x.Item1);
                }
            }
            Console.WriteLine(string.Join(" ", res));
        }

        static bool Check(Dictionary<int, int> dic)
        {
            return dic[0] > 0 && dic[1] > 0 && dic[2] > 0 && dic[3] > 0;
        }
    }
}