namespace TP_4;

class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var n = long.Parse(Console.ReadLine());
            
        }
    }
}