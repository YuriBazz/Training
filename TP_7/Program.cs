namespace TP_7;

class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            Console.ReadLine();
            var a = Read();

            var count = a.Length / 3;
            
        }
    }
}