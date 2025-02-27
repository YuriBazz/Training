using System;
using System.Linq;

namespace _1006_A
{
    class Program
    {
        static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        static void Main(string[] args)
        {
            for (var t = Read()[0]; t > 0; --t)
            {
                var r = Read();
                var (n, k, p) = (r[0], r[1], r[2]);
                var sum = 0;
                var count = 0;
                while (n-- > 0 && sum != k)
                {
                    if (sum < k) sum += k - sum <= p ? k - sum : p;
                    else sum -= sum - k <= p ? sum - k : p;
                    count++;
                }
                Console.WriteLine(sum == k ? count : -1);
            }
        }
    }
}