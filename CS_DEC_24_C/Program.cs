namespace CS_DEC_24_C;

class Program
{
    static void Main(string[] args)
    {
        var token = ReadInts();
        var rows = new Dictionary<int, int>();
        var columns = new Dictionary<int, int>();
        for (var row = 1; row <= token[0]; ++row)
            rows[row] = 0;
        for(var column = 1; column <= token[1]; ++column)
            columns[column] = 0;
        for (var T = ReadInts()[0]; T > 0; --T)
        {
            token = ReadInts();
            int row = token[0], column = token[1], color = token[2];
            rows[row] = color;
            columns[column] = color;
        }
        Print(rows.Select(x => (x.Key, x.Value)));
        Print(columns.Select(x => (x.Key, x.Value)));
    }

    static int[] ReadInts()
    {
        return Console.ReadLine().Split().Select(int.Parse).ToArray();
    }

    static void Print(IEnumerable<(int,int)> input)
    {
        foreach(var c in input)
            Console.Write(c + " ");
        Console.Write("\n");
    }
}