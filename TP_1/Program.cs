namespace TP_1;

class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var n = int.Parse(Console.ReadLine());
            var b = Read();
            var f = false;
            for (var i = 1; i < b.Length - 1; ++i)
            {
                if (b[i] == 0)
                {
                    f = b[i - 1] == 1 && b[i + 1] == 1;
                }
                if(f) break;
            }
            Console.WriteLine(f ? "NO" : "YES");
        }
    }
    
}