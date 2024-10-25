using System;
using System.Linq;
namespace CF_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine();
            var a = "hello".ToArray();
            int j = 0;
            for(int i = 0; i < s.Length; ++i)
            {
                if (j < 5 && s[i] == a[j] && (j == 0 || a[j-1] == '-'))
                {
                    a[j] = '-';
                    j++;
                }
            }
            Console.WriteLine(j == 5 ? "YES": "NO");
        }
    }
}
