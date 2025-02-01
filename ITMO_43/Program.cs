using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_43
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                Console.ReadLine();
                var n = int.Parse(Console.ReadLine());
                var array = Console.ReadLine().Split(" ").Select(int.Parse);
                var d = 0L;
                var check = n > 1 ? -2 : -1;
                var f = false;
                foreach (var item in array)
                {
                    if (item >= n || (item == 0 && n > 1))
                    {
                        f = true;
                        break;
                    }
                    
                    if (item <= 1) check++;
                    d += item;
                }
                Console.WriteLine(!f && 2 * n - 2 == d && check >=0 ? "YES" :"NO");
            }
        }
    }
}
