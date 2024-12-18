using System;
using System.Collections.Generic;  
using System.Linq;

namespace ITMO_37
{

    static class Program
    {
        static void Main(string[] args)
        {
            var token = Console.ReadLine().Split(' ');
            var n = int.Parse(token[0]);
            var k =  long.Parse(token[1]);
            long left = 0, right = n * n + 1;
        }
        
        
    }
}