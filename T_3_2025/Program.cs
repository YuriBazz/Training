using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace T_3_2025
{
    static class Program
    {
        static string[] Read() => Console.ReadLine().Select(x => new string(new[] {x})).ToArray();
        static long[] ReadL() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray(); 
        static int[] ReadI() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray(); 
        static (int val, int ind)[] ReadII(int i = 0) =>
            Console.ReadLine().Split(" ").Select(x => (int.Parse(x), i++)).ToArray();
        static (long val, int ind)[] ReadLI(int i = 0) =>
            Console.ReadLine().Split(" ").Select(x => (long.Parse(x), i++)).ToArray();
        static void Write<T>(IEnumerable<T> a) => Console.WriteLine(string.Join(" ", a));
        
        static void Main(string[] args)
        {
            var GENERIC_VARIABLE_NAME = 1;
            for (; GENERIC_VARIABLE_NAME > 0; --GENERIC_VARIABLE_NAME)
            {
                var t = new Node();
                Console.ReadLine();
                var cur = t;
                var s = Console.ReadLine();
                for (var x = 0; x < s.Length; ++x)
                {
                    var temp = cur;
                    var next = new Node(x + 1);
                    if (s[x] == 'L')
                    {
                        next.Prev = temp.Prev;
                        if (temp.Prev == null)
                            t = next;
                        else temp.Prev.Next = next;
                        temp.Prev = next;
                        next.Next = temp;
                    }
                    else
                    {
                        next.Next = temp.Next;
                        temp.Next = next;
                        next.Prev = temp;
                    }
                    cur = next;
                }

                while (t != null)
                {
                    Console.Write(t.Value + " ");
                    t = t.Next;
                }

                Console.WriteLine();
            }
        }
    }

    class Node
    {
        public Node? Prev, Next;
        public int Value;

        public Node(int val = 0)
        {
            Value = val;
        }
    }
}