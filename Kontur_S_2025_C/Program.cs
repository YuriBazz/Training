namespace Kontur_S_2025_C;

class Program
{
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    static void Write<T>(IEnumerable<T> arr, string sep) => Console.WriteLine(string.Join(sep, arr));
    
    static void Main(string[] args)
    {
        var m = Read()[1];
        var a = Read();
        var first = new int[m];
        var last = new int[m];
        
        for (var i = 0; i < a.Length; ++i)
        {
            first[a[i] - 1] = Math.Min(i + 1, first[a[i] - 1] == 0 ? i + 1 : first[a[i] - 1]);
            last[a[i] - 1] = Math.Max(i + 1, last[a[i] - 1]);
        }

        var temp = new List<(int val, int l, int r)>();
        var stack = new Stack<int>();
        
        temp.Sort((x,y) => x.l.CompareTo(y.l));
        var used = new HashSet<int>();
        for (var i = 0; i < a.Length; ++i)
        {
            
            if (used.Contains(a[i]))
            {
                Console.WriteLine(-1);
                return;
            }
            if (first[a[i] - 1] == last[a[i] - 1])
            {
                used.Add(a[i]);
                temp.Add((a[i],first[a[i] - 1], last[a[i] -1]));
                continue;
            }
            if (stack.Count == 0){ stack.Push(a[i]); continue;}

            if (stack.Peek() == a[i])
            {
                if (last[a[i] - 1] == i + 1)
                {
                    stack.Pop();
                    used.Add(a[i]);
                    temp.Add((a[i], first[a[i] - 1], last[a[i] - 1]));
                }
            }
            else
            {
                if (first[a[i] - 1] == i + 1) stack.Push(a[i]);
                else 
                {
                    Console.WriteLine(-1);
                    return;
                }
            }
        }
        temp.Sort((x,y) => x.l.CompareTo(y.l));
        Console.WriteLine(temp.Count);
        foreach(var x in temp)
            Console.Write(x.val + " " + x.l + " " + x.r + "\n");
    }
}