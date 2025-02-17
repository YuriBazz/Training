using System;
using System.Globalization;

namespace YA_1
{

    class Program
    {
        static void Main(string[] args)
        {
            double l = 30.0, r = 4000.0, prev = 0;
            for (var n = int.Parse(Console.ReadLine()); n > 0; --n)
            {
                var inp = Console.ReadLine().Split(" ");
                if (inp.Length == 1)
                {
                    prev = double.Parse(inp[0], CultureInfo.InvariantCulture);
                    continue;
                }

                var curr = double.Parse(inp[0], CultureInfo.InvariantCulture);
                switch (inp[1])
                {
                    case "closer":
                    {
                        if (Math.Abs(l - prev) < Math.Abs(l - curr))
                        {
                            var tempR = r;
                            var tempL = l;
                            for (var i = 0; i < 100; ++i)
                            {
                                var mid = tempL + (tempR - tempL) / 2;
                                if (Math.Abs(mid - prev) >= Math.Abs(mid - curr))
                                    tempR = mid;
                                else tempL = mid;
                            }

                            l = tempR;
                        }

                        if (Math.Abs(r - prev) < Math.Abs(r - curr))
                        {
                            var tempR = r;
                            var tempL = l;
                            for (var i = 0; i < 100; ++i)
                            {
                                var mid = tempL + (tempR - tempL) / 2;
                                if (Math.Abs(mid - prev) > Math.Abs(mid - curr))
                                    tempL = mid;
                                else tempR = mid;
                            }

                            r = tempL;
                        }

                        break;
                    }
                    case "further":
                    {
                        if (Math.Abs(l - prev) > Math.Abs(l - curr))
                        {
                            var tempR = r;
                            var tempL = l;
                            for (var i = 0; i < 100; ++i)
                            {
                                var mid = tempL + (tempR - tempL) / 2;
                                if (Math.Abs(mid - prev) < Math.Abs(mid - curr))
                                    tempR = mid;
                                else tempL = mid;
                            }

                            l = tempR;
                        }

                        if (Math.Abs(r - prev) > Math.Abs(r - curr))
                        {
                            var tempR = r;
                            var tempL = l;
                            for (var i = 0; i < 100; ++i)
                            {
                                var mid = tempL + (tempR - tempL) / 2;
                                if (Math.Abs(mid - prev) < Math.Abs(mid - curr))
                                    tempL = mid;
                                else tempR = mid;
                            }

                            r = tempL;
                        }

                        break;
                    }

                }

                prev = curr;
            }

            Console.WriteLine(l + " " + r);
        }
    }
}