namespace _175_A;

class Program
{
    static void Main(string[] args)
    {
        
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var n = int.Parse(Console.ReadLine());
            if(n < 3) Console.WriteLine(n + 1);
            else Console.WriteLine(n / 15 + (n - 1) / 15 + (n - 2) / 15 + 3);
        }
    }
}