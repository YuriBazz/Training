using System;

namespace T_1_2025
{
    static class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine();
            Console.WriteLine(s.IndexOf('M') - s.IndexOf('R') > 0 ? "Yes" : "No");
        }
    }
}