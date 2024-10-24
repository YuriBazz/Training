var input = Console.ReadLine();
var d = "rgby".ToDictionary(x => x, x => 0);
for (int i = 1; i < 10; i++)
    for (int j = 1; j < 10; j++)
    {
        if ((i * j) % 2 == 0) d['r']++;
        else if ((i * j) % 3 == 0) d['g']++;
        else if ((i * j) % 5 == 0) d['b']++;
        else d['y']++;
    }
Console.WriteLine(d[input[0]]);