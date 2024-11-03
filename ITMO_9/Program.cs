using System;

namespace ITMO_9
{

    class Program
    {
        static void Main(string[] args)
        {
            Write(ZFunction(Console.ReadLine()));
        }

        static int[] ZFunction(string s)
        {
            var z = new int[s.Length];
            int l = 0, r = 0;
            for (var i = 1; i < s.Length; ++i)
            {
                if (r >= i)
                    z[i] = Math.Min(z[i - l], r - i + 1);

                while (z[i] + i < s.Length && s[z[i]] == s[z[i] + i])
                    z[i]++;

                if (i + z[i] - 1 > r)
                {
                    l = i;
                    r = z[i] + i - 1;
                }
            }

            return z;
        }

        static void Write(int[] z)
        {
            foreach (var t in z)
            {
                Console.Write(t + " ");
            }
        }
    }
}