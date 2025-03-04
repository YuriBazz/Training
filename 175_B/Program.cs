namespace _175_B;

class Program
{
    static void Main(string[] args)
    {
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var nxk = Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
            var x = nxk[1];
            var s = Console.ReadLine();
            var count = 0;
            for (var i = 0; i < s.Length; ++i)
            {
                if (s[i] == 'L') x--;
                else x++;
                if (x == 0) {i = -1;
                    count++;
                }
            }
            
            Console.WriteLine(count == 0 ? 0 : nxk[2] / (count * s.Length));
        }
    }
}