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
            var n = int.Parse(Console.ReadLine());
            var s = (n + 1) * n / 2;
            var list = new List<int[]>();
            while (n-- > 0)
                list.Add(Read());
            for (var i = 0; i < list.Count; ++i)
            {
                var non = s - list[i].Sum();
                var first = true;
                for (var j = 0; j < list.Count; ++j)
                {
                    if (i == j) continue;
                    if (list[j][0] != non)
                    {
                        first = false;
                        break;
                    }
                }

                if (first)
                {
                    Console.WriteLine(non + " " + string.Join(" ", list[i]));
                    break;
                }
            }
        }
    }
}
