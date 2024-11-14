using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;

namespace ITMO_4
{

    class Program
    {
        static void Main(string[] args)
        {
            var q = int.Parse(Console.ReadLine());
            for (; q > 0; --q)
            {
                
                var s = Console.ReadLine();
                var t = Console.ReadLine();
                int r = 0, last = 0;
                for(var i = 0; i <= s.Length - t.Length; ++i)
                    if (s.Substring(i, t.Length) == t)
                    {
                        r += (i + 1 - last) * (s.Length + 1 - i - t.Length);
                        last = i + 1;
                    }
                Console.WriteLine((s.Length * s.Length + s.Length) / 2 - r);
            }
        }
        
    }
}