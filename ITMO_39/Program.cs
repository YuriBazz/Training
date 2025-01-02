using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_39
{

    class Program
    {
        static void Main(string[] args)
        {
            for (var count = int.Parse(Console.ReadLine()); count > 0; --count)
            {
                Console.ReadLine();
                var dic = new Dictionary<int, HashSet<int>>();
                var f = false;
                for (var m = Console.ReadLine().Split(" ").Select(int.Parse).ToArray()[1]; m > 0; --m)
                {
                    var token = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                    var (u, v) = (token[0], token[1]);
                    if ((dic.ContainsKey(u) && dic[u].Contains(v)) || (dic.ContainsKey(v) && dic[v].Contains(u)) || u == v)
                    {
                        f = true;
                    }
                    if (!dic.ContainsKey(u)) dic[u] = new HashSet<int>();
                    if (!dic.ContainsKey(v)) dic[v] = new HashSet<int>();
                    dic[u].Add(v);
                    dic[v].Add(u);
                }
                if(f) Console.WriteLine("NO");
                else Console.WriteLine("YES");
            }
        }
    }
}