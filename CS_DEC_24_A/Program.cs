namespace CS_DEC_24_A;

class Program
{
    static void Main(string[] args)
    {
        
    }

    static int[] ReadInts()
    {
        return Console.ReadLine().Split().Select(int.Parse).ToArray();
    }
}