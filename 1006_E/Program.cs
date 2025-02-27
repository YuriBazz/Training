using System;
using System.Collections.Generic;

namespace _1006_E
{
    class Program
    {

        static void Main(string[] args)
        {
            for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
            {
                var k = int.Parse(Console.ReadLine());
                int horizont = 0, r = (int)1e3 + 1;
                while (r > horizont + 1)
                {
                    var m = horizont + (r - horizont) / 2;
                    if (m * (m - 1) <= 2 * k) horizont = m;
                    else r = m;
                }
               
                var add = k - (horizont - 1) * horizont / 2;
                int vert = 0, r2 = (int)1e3 + 1;
                while (r2 > vert + 1)
                {
                    var m = vert + (r2 - vert) / 2;
                    if (m * (m - 1) + m <= 2 * add) vert = m;
                    else r2 = m;
                }

                var x = -(int)1e9 - 1;
                var y = -(int)1e9;
                var list = new List<(int x, int y)>();
                var count = (horizont - 1) * horizont / 2;
                while (horizont != 0)
                {
                    x++;
                    list.Add((x,y));
                    horizont--;
                }
                
                var dy = 1;
                for (var i = 0; vert != 0; ++i)
                {
                    var v = 1;
                    while (vert != 0&&count + v * (v + 1)/ 2 <= k)
                    {
                        list.Add((list[i].x, list[i].y + dy));
                        dy++;
                        v++;
                        vert--;
                        count += v * (v - 1) / 2;
                    }
                }
                Console.WriteLine(list.Count);
                foreach (var p in list)
                {
                    Console.WriteLine(p.x + " " + p.y);
                }
            }
        }
    }
}