var d = new Dictionary<int, int>();
var n = int.Parse(Console.ReadLine());
var input = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray() ;
for(int i = 0; i < n; i++)
{
    d[input[i]] = i;
    if (d.ContainsKey(-input[i]))
    { Console.WriteLine((d[-input[i]]+1) + " " + (i + 1)); return; }
}
 Console.WriteLine(1 + " " + (d[-input[0]] + 1));
