namespace Kontur_W_2024_E;

class Program
{
    static void Main(string[] args)
    {
        var token = ReadInts();
        var field = new Field(token[0], token[1]);
        for (; token[2] > 0; token[2]--)
        {
            var input = ReadInts();
            field.Makeshot(input[0],input[1]);
        }
    }
    
    static int[] ReadInts()
    {
        return Console.ReadLine().Split().Select(int.Parse).ToArray();
    }
}

class Field
{
    private readonly char[,] field;

    public Field(int n, int m)
    {
        field = new char[n, m];
        for (var i = 0; i < n; i++)
        {
            var token = Console.ReadLine();
            for (var j = 0; j < m; j++)
                field[i, j] = token[j];
        }
    }

    public void Makeshot(int x, int y)
    {
        if(field[x-1,y - 1]== '.' || field[x-1,y-1] == '$') 
            Console.WriteLine("MISS");
        else
        {
            var deltas = new[] { (-1, 0), (0, -1), (1, 0), (0, 1) };
            var stack = new Stack<(int,int)>();
            field[x - 1, y - 1] = '$';
            var visited = new HashSet<(int, int)>();
            stack.Push((x-1,y-1));
            while (stack.Count > 0)
            {
                var cur = stack.Pop();
                visited.Add(cur);
                foreach (var next in deltas
                             .Select(x => (x.Item1 + cur.Item1, x.Item2 + cur.Item2))
                             .Where(x => x.Item1 < field.GetLength(0) && x.Item2 < field.GetLength(1))
                             .Where(x => x.Item1 >= 0 && x.Item2 >= 0) 
                             .Where(x => field[x.Item1,x.Item2] != '.'))
                {
                    if (field[next.Item1, next.Item2] == 'X')
                    {
                        Console.WriteLine("HIT");
                        return;
                    }
                    if(!visited.Contains(next))
                        stack.Push(next);
                }
            }
            Console.WriteLine("DESTROY");
        }
    }
}

class MyQueue
{
    private readonly Queue<(int, int)> queue;
    private readonly HashSet<(int, int)> set;
    
    public int Count
    {
        get { return set.Count; }
    }
    public MyQueue()
    {
        queue = new Queue<(int, int)>();
        set = new HashSet<(int, int)>();
    }

    public void Enqueue((int, int) input)
    {
        if (!set.Contains(input))
        {
            set.Add(input);
            queue.Enqueue(input);
        }
    }

    public (int, int) Dequeue()
    {
        var t = queue.Dequeue();
        set.Remove(t);
        return t;
    }
}