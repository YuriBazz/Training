namespace TP_1;

class Program
{
    static void Main(string[] args)
    {
        for (var t = int.Parse(Console.ReadLine()); t > 0; --t)
        {
            var s = Console.ReadLine();
            while (s.Length != 0)
            {
                var rev = new string(s.Reverse().ToArray());
                if (s != rev)
                {
                    Console.WriteLine(s.Length);
                    break;
                }

                s = s.Substring(0, s.Length - 1);
            }
            
            if(s.Length == 0) Console.WriteLine(-1);
        }
    }
    
}