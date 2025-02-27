using System;

namespace _1006_B
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                Console.ReadLine();
                var a = Console.ReadLine();
                var n = 0L;
                var m = 0L;
                for (var i = 0; i < a.Length; ++i)
                {
                    if (a[i] == '_') m++;
                    else n++;
                }
                Console.WriteLine(n / 2 * (n - n / 2) * m);
            }
        }
    }
}