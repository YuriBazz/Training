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
                var r = (s.Length * s.Length + s.Length) / 2;

            }
        }

        static int Include(string s, string t)
        {
            var r = 0;
            for(var i = 0; i <= s.Length - t.Length; ++i)
                if (s.Substring(i, t.Length) == t)
                    r += s.Length - t.Length - i + 1;
            return r;
        }
        
    }
}