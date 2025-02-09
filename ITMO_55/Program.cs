using System;
using System.Collections.Generic;
using System.Linq;
namespace ITMO_55
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            var array = Console.ReadLine().Split(" ").Select(ulong.Parse).ToArray();
            var res = int.MaxValue;
            var q = new Queue();
            for (var (l, r) = (0, 0); r < array.Length; ++r)
            {
                var f = false;
                q.Enqueue(array[r]);
                while (q.GCD == 1)
                {
                    f = true;
                    q.Dequeue();
                    ++l;
                }

                if(f) res = Math.Min(res, r - l + 2);
            }
            Console.WriteLine(res == int.MaxValue ? -1 : res);
        }
       
    }

    class Queue
    {
        private static ulong Gcd(ulong a, ulong b)
        {
            if (b == 0) return a;
            return Gcd(b, a % b);
        }
        
        private readonly Stack<(ulong,ulong)> _left = new(), _right = new();

        public void Enqueue(ulong value)
        {
            _right.Push((value, Gcd(value, _right.Count == 0 ? value : _right.Peek().Item2)));
        }

        private void Transfer()
        {
            while (_right.Count > 0)
            {
                var t = _right.Pop();
                _left.Push((t.Item1, Gcd(t.Item1, _left.Count == 0 ? t.Item1 : _left.Peek().Item2) ));
            }
        }

        public (ulong, ulong) Dequeue()
        {
            if(_left.Count == 0) Transfer();
            return _left.Pop();
        }

        public ulong GCD
        {
            get
            {
                if(_left.Count == 0) Transfer();
                if (_left.Count == 0) return 0;
                return Gcd(_left.Peek().Item2, _right.Count == 0 ? _left.Peek().Item2 : _right.Peek().Item2);
            }
        }
    }
}