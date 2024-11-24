using System;
using System.Collections.Generic;
using System.Linq;

namespace ITMO_17
{

    static class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            
            var array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var q = Console.ReadLine().Split().Select(int.Parse).ToArray();
            foreach (var x in q)
            {
                Console.WriteLine(BSForTask(array,x) ? "YES" : "NO");
            }
        }
        
        static bool PrimitiveBs(int[] array, int value)
        {
            int l = 0, r = array.Length - 1; // указатели концов мн-ва
            while (r >= l)
            {
                int m = l + (r - l) / 2;
                if (array[m] == value) return true;
                if (array[m] < value) l = m + 1;
                if (array[m] > value) r = m - 1;
            }
            return false;
        }

        static int BetterBsLowerOrEqual(int[] array, int value)
        {
            int l = -1, r = array.Length; // а тут указатели поддерживают инвариант: a_l <= x, x < a_r
            while (r > l + 1)
            {
                int m = l + (r - l) / 2;
                if (array[m] <= value) l = m;
                else r = m;
            }
            return l;
        }
        
        static int BetterBsBiggerOrEqual(int[] array, int value)
        {
            int l = -1, r = array.Length; // а тут указатели поддерживают инвариант: a_l < x, x <= a_r
            while (r > l + 1)
            {
                int m = l + (r - l) / 2;
                if (array[m] < value) l = m;
                else r = m;
            }
            return r;
        }

        static bool BSForTask(int[] array, int value)
        {
            int l = -1, r = array.Length;
            while (r > l + 1)
            {
                var m = l + (r - l) / 2;
                if (array[m] <= value) l = m;
                else r = m;
            }
            return l >= 0 && array[l] == value;
        }
    }
}