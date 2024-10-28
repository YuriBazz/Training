using System;
using System.Collections.Generic;
using System.Linq;

namespace CF_5
{

    class Program
    {
        static void Main(string[] args)
        {
            var t = ReadInts()[0];
            for (int i = 0; i < t; ++i)
                Do();
        }

        static int[] ReadInts()
        {
            return Console.ReadLine().Split().Select(int.Parse).ToArray();
        }

        static void Do()
        {
            ReadInts();
            var s = Console.ReadLine().ToArray();
            if (s.Length % 2 == 0)
            {
                Console.WriteLine(Do1(s));
            }
            else
            {
                var c = new List<char>();
                var min = int.MaxValue;
                for (int i = 0; i < s.Length; ++i)
                {
                    for (int j = 0; j < s.Length; ++j)
                    {
                        if (j != i) c.Add(s[j]);
                    }

                    min = Math.Min(min, Do1(c.ToArray()));
                    c.Clear();
                }

                Console.WriteLine(min + 1);
            }
        }

        static int Do1(char[] s)
        {
            if (s.Length == 0) return 0;
            var d = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i += 2)
            {
                if (!d.ContainsKey(s[i]))
                    d[s[i]] = 0;
                d[s[i]]++;
            }

            var even = d.Max(x => x.Value);
            d.Clear();
            for (int i = 1; i < s.Length; i += 2)
            {
                if (!d.ContainsKey(s[i]))
                    d[s[i]] = 0;
                d[s[i]]++;
            }

            var odd = d.Max(x => x.Value);
            return s.Length - even - odd;
        }
    }
}