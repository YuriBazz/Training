using System;
using System.Collections.Generic;
using System.Linq;


namespace ITMO_60
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            var s = Console.ReadLine();
            var cards = new Dictionary<char, int>();
            foreach (var c in Console.ReadLine())
                cards[c] = cards.GetValueOrDefault(c, 0) + 1;
            var res = 0L;
            var temp = new Dictionary<char, int>();
            for (var (l, r) = (0, 0); l < s.Length; ++l)
            {
                while (r < s.Length && cards.ContainsKey(s[r]) && temp.GetValueOrDefault(s[r],0) + 1 <= cards[s[r]])
                {
                    temp[s[r]] = temp.GetValueOrDefault(s[r], 0) + 1;
                    ++r;
                } 
                res += r - l;
                if (!cards.ContainsKey(s[l]))
                {
                    temp.Clear();
                    ++r;
                }
                else temp[s[l]] = temp[s[l]] - 1 >= 0 ? temp[s[l]] - 1 : 0;
            }
            Console.WriteLine(res);
        }
    }
}