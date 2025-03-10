namespace TP_4;

class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            Console.ReadLine();
            var a = Read();
            var b = Read();
            if (a.SequenceEqual(b) || a.SequenceEqual(b.Reverse())) 
                Console.WriteLine("Bob");
            else Console.WriteLine("Alice");
        }
    }
}