namespace Kontur_W_2024_C;

class Program
{
    static void Main(string[] args)
    {
        var token = ReadInts();
        int currentQ, currentLen = 0, max = int.MinValue;
        var buildings = ReadInts();
        
        for (var i = 0; i < buildings.Length; i++)
        {
            currentQ = 0;
            currentLen = 0;
            for (var j = i; j < buildings.Length; j++)
            {
                if (buildings[j] < token[1])
                {
                    currentLen++;
                }
                else
                {
                    if (currentQ < token[2])
                    {
                        currentLen++;
                        currentQ++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            max = Math.Max(max, currentLen);
        }

        var check = buildings[buildings.Length - 1] < token[1] || token[2] >= 1 ? 1 : int.MinValue;
        Console.WriteLine(Math.Max(max,check));
    }
    
       

    static int[] ReadInts()
    {
        return Console.ReadLine().Split().Select(int.Parse).ToArray();
    }
}