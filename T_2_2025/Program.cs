using System;

namespace T_2_2025
{
    static class Program
    {
        static void Main(string[] args)
        {
            for (var n = int.Parse(Console.ReadLine()); n > 0; --n)
            {
                var s = Convert.ToString(long.Parse(Console.ReadLine()), 2);
                var sum = 0L;
                var count = 0;
                for (var i = 0; i < s.Length ; ++i)
                {
                    if (s[i] == '1')
                    {
                        sum += QuickPow(2, s.Length - 1 - i);
                        ++count;
                    }
                    if (count == 3) break;
                }
                
                Console.WriteLine(count == 3 ? sum : -1);
            }
        }
        
        static long QuickPow(long value, long pow)
        {
            var result = 1L;
            var temp = value;
            while (pow != 0)
            {
                result *= (pow % 2 == 0 ? 1 : temp);
                temp *=  temp;
                pow /= 2;
            }
            return result;
        }
    }
}