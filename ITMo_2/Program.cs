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
            for (; t > 0; --t)
            {
                var s = Console.ReadLine();
                var sr = new string(s.Reverse().ToArray());
                var pr = new HashSet<string>();
                var sf = new HashSet<string>();
                var res = 0;
                for (var i = 1; i <= s.Length; ++i)
                {
                    pr.Add(s.Substring(0, i));
                    sf.Add(s.Substring(i, s.Length - i));
                }

                pr.Remove(s);

                for (var i = 0; i < s.Length; ++i)
                {
                    for (var j = 1; j <= s.Length - i; ++j)
                    {
                        var sub = s.Substring(i, j);
                        if (pr.Contains(sub) ^ sf.Contains(sub)) res++;
                    }
                }
                Console.WriteLine(res);
            }
        }
    }
}