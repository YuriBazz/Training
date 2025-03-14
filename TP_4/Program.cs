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
            var set = new HashSet<int>();
            var res = 0L;
            for (var i = 1; i < a.Length; ++i)
            {
                if (a[i] < a[i - 1])
                {
                    res += a[i - 1] - a[i];
                    set.Add(a[i - 1] - a[i]);
                    a[i] = a[i - 1];
                }
            }
            
            Console.WriteLine(res + set.Count);
        }
    }
}