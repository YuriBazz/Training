using System; 
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ITMo_2
{

    class Program
    {
        static void Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; ++i)
            {
                var temp = Console.ReadLine();
                int[] z1 = ZFunction(temp), z2 = ZFunction(new string(temp.Reverse().ToArray()));
                var res = 0;
                for (var j = 0; j < z1.Length; ++j)
                {
                    res += z1[i] * z2[i] == 0 ? z1[i] + z2[i] : 0;
                }
                Console.WriteLine(res);
            }
        }
        
        static int[] ZFunction(string s)
        {
            var z = new int[s.Length];
            int l = 0, r = 0;
            for (var i = 1; i < s.Length; ++i)
            {
                if (r >= i)
                    z[i] = Math.Min(z[i - l], r - i + 1);

                while (z[i] + i < s.Length && s[z[i]] == s[z[i] + i])
                    z[i]++;
                
                if (i + z[i] - 1 > r)
                {
                    l = i;
                    r = z[i] + i - 1;
                }
            }
            return z;
        }
    }
}