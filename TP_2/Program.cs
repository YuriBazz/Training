namespace TP_2;

class Program
{
    
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
       
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            Console.ReadLine();
            var a = Read();
            var gcd = 0;
            foreach (var n in a)
                gcd = GCD(n, gcd);
            Console.WriteLine( a[^1] / gcd);
        }
    }

    static int GCD(int a, int b)
    {
        while (b != 0) (a, b) = (b, a % b);
        return a;
    }
}