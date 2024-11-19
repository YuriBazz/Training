namespace Kontur_W_2024_A;

class Program
{
    static void Main(string[] args)
    {
        var max = int.MinValue;
        string current ="";
        for (var n = int.Parse(Console.ReadLine()); n > 0; n--)
        {
            var s = Console.ReadLine();
            var t = NumebrOfUnic(s);
            if (t> max)
            {
                max = t;
                current = s;
            }
        }
        Console.WriteLine(max + " " + current);
    }

    static int NumebrOfUnic(string str)
    {
        return str.ToHashSet().Count;
    }
}