namespace Kontur_W_2024;

class Program
{
    static void Main(string[] args)
    {
        var N = int.Parse(Console.ReadLine());
        var targets = new HashSet<(int, int)>();
        for (var i = 0; i < N; i++)
        {
            var token = Console.ReadLine().Split();
            targets.Add((int.Parse(token[0]), int.Parse(token[1])));
        }

        var hits = new HashSet<(int, int)>();
        for (var i = 0; i < N; i++)
        {
            var token = Console.ReadLine().Split();
            hits.Add((int.Parse(token[0]), int.Parse(token[1])));
        }

        foreach (var hit in hits)
        {
            foreach (var target in targets)
            {
                var u = hit.Item1 - target.Item1;
                var v = hit.Item2 - target.Item2;

                var flag = true;
                foreach (var hit1 in hits)
                {
                    if (!targets.Contains((hit1.Item1 - u, hit1.Item2 - v)))
                    {
                        flag = false; break;
                    }
                }

                if (flag)
                {
                    Console.WriteLine(u + " " + v);
                    return;
                }
            }
        }
    }
}