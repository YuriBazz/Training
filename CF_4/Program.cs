using System;
using System.Collections.Generic;
using System.Linq;

namespace CF_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var token = ReadLongs();
            var friends = new (long, long)[token[0]];
            for (int i = 0; i < token[0]; ++i)
            {
                friends[i] = ReadFriends();
            }

            long max = 0, current = 0, left = 0, right = 0, minMoney = -1;
            
            var sorted = friends.OrderBy(x => x.Item1).ToArray();
            while(right < sorted.Length)
            {
                var t = sorted[right];
                if (minMoney != -1 && Math.Abs(minMoney - t.Item1) >= token[1])
                {
                    max = Math.Max(max, current);
                    current -= sorted[left].Item2;
                    left++;
                    minMoney = sorted[left].Item1;
                }
                else
                {
                    minMoney = minMoney == -1 ? t.Item1 : Math.Min(minMoney, t.Item1);
                    current += t.Item2;
                    right++;
                }
            }
            Console.WriteLine(Math.Max(current, max));
        }

        static long[] ReadLongs()
        {
            return Console.ReadLine().Split().Select(long.Parse).ToArray();
        }
        
        static (long, long) ReadFriends()
        {
            var temp = ReadLongs();
            return (temp[0], temp[1]);
        }
    }
}
