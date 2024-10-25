using System;
namespace Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            Console.WriteLine(n - 2 > 0 && n - 2 % 2 == 0 ? "YES" : "NO");
        }
    }
}