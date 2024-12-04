using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_28
{
    static class Program
    {
        static int[] ReadInts()
        {
            return Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        }
        
        static void Main(string[] args)
        {

            var dic = new Dictionary<char, int>
            {
                ['B'] = int.MaxValue,
                ['C'] = int.MaxValue,
                ['S'] = int.MaxValue
            };
            foreach (var letter in Console.ReadLine())
            {
                if (dic[letter] == int.MaxValue)
                    dic[letter] = 0;
                dic[letter]++;
            }

            var token = ReadInts();
            var (nb, ns, nc) = (token[0], token[1], token[2]);
            token = ReadInts();
            var (pb, ps, pc) = (token[0], token[1], token[2]);
            var r = long.Parse(Console.ReadLine());
            long l = 0, right = (long)1e14 + 1;
            while (right > l + 1)
            {
                var m = l + (right - l) / 2;
                if (Check(dic, m,r,nb,nc,ns,pb,ps,pc)) l = m;
                else right = m;
            }
            Console.WriteLine(l);
        }

        static bool Check(Dictionary<char, int> dic, long m, long r,int nb, int nc,int ns, int pb,int ps,int pc)
        {
            long mb = dic['B'] == int.MaxValue ? 0 : Math.Max(m * dic['B'] - nb, 0),
                mc = dic['C'] == int.MaxValue ? 0 : Math.Max(m * dic['C'] - nc,0),
                ms = dic['S'] == int.MaxValue ? 0 : Math.Max(m * dic['S'] - ns,0);
            return mb * pb  + ms * ps  + mc * pc  <= r;
        }
    }
}