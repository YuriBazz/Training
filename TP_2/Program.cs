namespace TP_2;

class Program
{
    
    static int[] Read() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
    
    static void Main(string[] args)
    {
       
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var p = Read();
            var a = new int[p[0]];
            var m = p[1];
            for (var i = 0; i < a.Length; ++i)
            {
                a[i] = i + 1;
               
            }

            var res = new int[p[0]];
            var temp = 0;
            for (var k = m - 1; k < res.Length; k += m)
                res[k] = a[temp++];
            for (var i = 0; i < res.Length; ++i)
            {
                if(res[i] != 0 ) continue;
                res[i] = a[temp++];
            }
            Console.WriteLine(string.Join(" ", res));
        }
    }

    
    
}