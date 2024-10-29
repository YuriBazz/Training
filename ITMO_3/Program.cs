using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITMO_3
{

    class Program
    {
        static void Main(string[] args)
        {
            var q = int.Parse(Console.ReadLine());
            for (; q > 0; --q)
            {
                var t = Console.ReadLine();
                var p = Console.ReadLine();
                var r = new List<int>();
                for (var i = 0; i <= t.Length - p.Length; ++i)
                    if (Compare(t.Substring(i, p.Length), p))
                        r.Add(i);
                Write(r);
            }
        }

        static bool Compare(string a, string b)
        {
            return !a.Where((t, i) => t != b[i] && b[i] != '?').Any();
        }

        static void Write(List<int> list)
        {
            Console.WriteLine(list.Count);
            var s = new StringBuilder();
            s.AppendJoin(' ', list);
            Console.WriteLine(s.ToString());
        }
    }
}