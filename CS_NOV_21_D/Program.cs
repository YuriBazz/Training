var token = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
// t[0] = n; t[1] = k_1; t[2] = k_2;
var f = Console.ReadLine().Split().Select(int.Parse).ToList();
var s = Console.ReadLine().Split().Select(int.Parse).ToList();
f.Add(f[0]); s.Add(s[0]);
var t = new HashSet<(int, int)>();
for (int i = 1; i < f.Count; i++)
    t.Add((f[i - 1], f[i]));
for(int i = 1; i < s.Count; i++)
{
    if (t.Contains((s[i - 1], s[i])) || t.Contains((s[i], s[i-1])))
    { Console.WriteLine(Math.Min(s[i - 1], s[i]) + " " + Math.Max(s[i - 1], s[i])); return; }
}

