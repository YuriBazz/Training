using System.Data.Common;

namespace CS_DEC_24_C;

class Program
{
    static void Main(string[] args)
    {
        //нужно всего лишь сохранять шаг
    }

    static int[] ReadInts()
    {
        return Console.ReadLine().Split().Select(int.Parse).ToArray();
    }

    static void Print(IEnumerable<(int,int)> input)
    {
        foreach(var c in input)
            Console.Write(c + " ");
        Console.Write("\n");
    }
}