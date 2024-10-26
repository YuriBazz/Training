using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace CF_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t = ReadLongs();
            var fr = new (long, long)[t[0]];
            for(int i = 0; i < t[0]; i++)
            {
                var t1 = ReadLongs();
                fr[i] = (t1[0], t1[1]);
            }
            fr.OrderBy(x => x.Item1).ToArray();
            long g = 0;
            long temp = 0;
            long minMoney = 0;
            for(int r = 0; r < t[0]; r++)
            {
                if(minMoney == 0 || fr[r].Item1 <= minMoney)
                    minMoney = fr[r].Item1;
                if()
            }
        }

        static long[] ReadLongs()
        {
            return Console.ReadLine().Split().Select(long.Parse).ToArray();
        }
    }
}
