using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
 
namespace ITMO_54
{
    static class Program
    {
        static void Main(string[] args)
        {
            var k = Console.ReadLine().Split(" ").Select(long.Parse).ToArray()[1];
            var array = Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
            var queue = new Queue();
            var res = 0L;
            for (var (l, r) = (0, 0); r < array.Length; ++r)
            {
                queue.Enqueue(array[r]);
                while (queue.Count != 0 && queue.Delta() > k)
                {
                    queue.Dequeue();
                    ++l;
                }
                res += r - l + 1;
            }
            Console.WriteLine(res);
        }
    }
 
    class Queue
    {
        private readonly Stack<(long,long,long)> Left = new(), Right = new();

        public int Count => Right.Count + Left.Count;

        public void Enqueue(long item)
        {
            var f = Right.Count == 0;
            Right.Push((item, f ? item : Math.Min(item, Right.Peek().Item2), f ? item : Math.Max(item, Right.Peek().Item3)));
        }
        
        private void Transfer()
        {
            while (Right.Count > 0)
            {
                var temp = Right.Pop();
                Left.Push((temp.Item1, 
                    Left.Count == 0 ? temp.Item1 : Math.Min(Left.Peek().Item2, temp.Item1), 
                    Left.Count == 0 ? temp.Item1 : Math.Max(Left.Peek().Item3, temp.Item1)));
            }
        }
        
        public (long,long,long) Dequeue()
        {
            if (Left.Count == 0) Transfer();
            return Left.Pop();
        }

        public long Delta()
        {
            if(Left.Count == 0) Transfer();
            return
                Math.Max(Left.Peek().Item3, Right.Count == 0 ? 0 : Right.Peek().Item3) -
                Math.Min(Left.Peek().Item2, Right.Count == 0 ? (long)1e18 + 1 : Right.Peek().Item2);
        }
        
        
    }
}