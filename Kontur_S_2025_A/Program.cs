namespace Kontur_S_2025_A;

class Program
{
    private const string Letters = "CDHS";
    private const string Nums = "23456789TJQKA";

    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    static void Write<T>(IEnumerable<T> arr, string sep) => Console.WriteLine(string.Join(sep, arr));
    
    static HashSet<string> Create()
    {
        var all = new HashSet<string>();
        foreach (var num in Nums)
        foreach (var let in Letters) 
            all.Add(new string(new[] { num , let }));
        return all;
    }
    
    static void Main(string[] args)
    {
        var inp = Read();
        var r1 = inp[0];
        var s1 = inp[1];
        var r2 = inp[2];
        var s2 = inp[3];
        var all = Create();
        var max = Math.Max(Do(s1, r1, all), 0.0);
        all = Create();
        max = Math.Max(Do(s2, r2, all), max);
        Console.WriteLine(max);

    }

    static double Do(int s, int r, HashSet<string> all)
    {
        while(r-- > 0)
            Parse(Console.ReadLine(), all);
        
        var n = all.Count * 1.0; 
        while (s-- > 0)
        {
            if (n == 0) Console.ReadLine();
            else Parse(Console.ReadLine(), all);
        }

        if (n == 0) return 0;
        return Math.Round(1.0 - all.Count / n, 6);
    }

    static void Parse(string input, HashSet<string> all)
    {
        var nums = new HashSet<char>();
        var lett = new HashSet<char>();
        foreach (var c in input)
        {
            if (char.IsNumber(c) || c == 81 || c == 84 || c == 74 || c == 75 || c == 65) 
                nums.Add(c);
            else lett.Add(c);
        }
        
        if (lett.Count == 0)
        {
            foreach (var num in nums)
            foreach (var let in Letters)
                all.Remove(new string(new[] { num, let }));
        }
        else if (nums.Count == 0)
        {
            foreach(var num in Nums)
            foreach (var let in lett)
                all.Remove(new string(new[] { num, let }));
        }
        else
        {
            foreach (var num in nums)
            foreach (var let in lett)
                all.Remove(new string(new[] { num, let }));
        }
    }
}