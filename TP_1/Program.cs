namespace TP_1;

class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
        
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            Console.ReadLine();
            var s = Console.ReadLine();
            var c = s[s.Length / 2];
            var count = 0;
            for (int r = s.Length / 2, l = s.Length % 2 == 0 ? r - 1 : r; l >= 0 && r < s.Length; l--, r++)
            {
                if (s[l] == c && s[r] == c) count += 2;
                else break;
                if (l == r) count--;
            }
            Console.WriteLine(count);
        }
    }
    
}