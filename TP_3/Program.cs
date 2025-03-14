using System.Security.AccessControl;
using Microsoft.VisualBasic.CompilerServices;

namespace TP_3;
class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

    static void Write<T>(T[] a) => Console.WriteLine(string.Join(" ", a));
    
    static void Main(string[] args)
    {
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            Console.ReadLine();
            var s = Read();
            var bonuses = new Stack<int>();
            var last = Array.LastIndexOf(s, 0);
            var count = 0;
            var used = new HashSet<int>();
            var st = 0L;
            for(var i = 0; i < s.Length; ++i)
                if (s[i] == 0)
                    st += Max(used, s, i);
            Console.WriteLine(st);
        }
    }

    static int Max(HashSet<int> set, int[] s, int last)
    {
        var max =0;
        var ind = -1;
        for (var i = 0; i <= last; ++i)
        {
            if (max <= s[i] && !set.Contains(i))
            {
                ind = i;
                max = s[i];
            }
        }

        if (max != 0)
            set.Add(ind);
        return max;
    }

    
}
