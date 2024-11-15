namespace CS_DEC_24_A;

class Program
{
    static void Main(string[] args)
    {
        var token = ReadInts();
        int n = token[0], m = token[1]; 
        if(n == m)
            Console.WriteLine(n + 2 * m);
        if (n < m)
            Console.WriteLine(3 * n + 2);
        if(n > m)
            Console.WriteLine(3 *m + 1);
    }

    static int[] ReadInts()
    {
        return Console.ReadLine().Split().Select(int.Parse).ToArray();
    }
}