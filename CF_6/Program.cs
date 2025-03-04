namespace CF_6;

class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

    static void Write(int[] a, int r)
    {
        
        for(var i = r; i < a.Length; ++i)
            Console.Write(a[i]  + " ");
        
        for(var i = 0; i < r; ++i)
            Console.Write(a[i] + " ");
        
    }
    
    static void Main(string[] args)
    {
        var array = new int[51];
        Array.Fill(array, -1);
        var m = Read()[1];
        var dmax = -1;
        var set = new HashSet<(int, int)>();
        while (m-- > 0)
        {
            var p = Read();
            array[p[0]] += array[p[0]] == -1 ? 2 : 1;
            array[p[1]] += array[p[1]] == -1 ? 1 : 0;
            dmax = Math.Max(dmax, array[p[0]]);
            set.Add((p[0], p[1]));
        }

        foreach (var pair in set)
            if (array[pair.Item1] < array[pair.Item2])
            {
                Console.WriteLine("NO");
                return;
            }
        
        var order = array.Select((x, i) => (x, i)).Where(x => x.x != -1).OrderByDescending(x => x.x).ThenBy(x => x.i).Select(x => x.i)
            .ToArray();
        Console.WriteLine("YES");
        Console.WriteLine(order.Length);
        for (var k = 0; k < order.Length ; ++k)
        {
            Write(order, order.Length - k);
            Console.Write("\n");
        }
    }
}