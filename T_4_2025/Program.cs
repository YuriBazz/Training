using System;
using System.Collections.Generic;
using System.Linq;

namespace T_4_2025
{
    static class Program
    {
        static long[] ReadLongs() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        
        static int[] ReadInts() => Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var token = ReadInts();
            int x = token[1], y = token[2], z = token[3];
            Console.WriteLine(Do(ReadLongs(),x,y,z));
        }

        static int Do(long[] a, int x, int y, int z)
        {
            int  lcmXY = LCM(x,y), lcmXZ = LCM(x,z), lcmYZ = LCM(y,z), lcmXYZ = LCM(x,lcmYZ);
            
            int[] ax = new int[a.Length],
                ay = new int[a.Length],
                az = new int[a.Length],
                axy = new int[a.Length],
                axz = new int[a.Length],
                ayz = new int[a.Length],
                axyz = new int[a.Length];
            
            for (var i = 0; i < a.Length; ++i)
            {
                Set(ax, i, a[i],x);
                Set(ay, i, a[i],y);
                Set(az, i, a[i],z);
                Set(axy, i, a[i],lcmXY);
                Set(axz, i, a[i],lcmXZ);
                Set(ayz, i, a[i],lcmYZ);
                Set(axyz, i, a[i],lcmXYZ);
            }

            var result = axyz.Min();
            
            if (a.Length == 1) return result;

            int[][] simpleDivisors = { ax, ay, az }; // Ну я напишу тут, что я в порядке запутался и понять не мог, что не так 	(ノ°益°)ノ
            int[][] lcmDivisors = { ayz, axz, axy };

            for (var i = 0; i < 3; ++i)
            {
                foreach (var index1 in simpleDivisors[i]
                             .Select((value, index) => (value,index))
                             .OrderBy(item=> item.value)
                             .Select(item => item.index)
                             .Take(2))
                {
                    foreach (var index2 in lcmDivisors[i]
                                 .Select((value, index) => (value,index))
                                 .OrderBy(item => item.value)
                                 .Select(item => item.index)
                                 .Take(2))
                    {
                        if (index1 == index2) continue;
                        result = Math.Min(result, simpleDivisors[i][index1] + lcmDivisors[i][index2]);
                    }
                }
            }

            if (a.Length == 2) return result;

            foreach (var indexX in ax
                         .Select((value, index) => (value,index))
                         .OrderBy(item => item.value)
                         .Select(item => item.index)
                         .Take(3))
            {
                foreach (var indexY in ay
                             .Select((value, index) => (value,index))
                             .OrderBy(item => item.value)
                             .Select(item => item.index)
                             .Take(3))
                {
                    foreach (var indexZ in az
                                 .Select((value, index) => (value,index))
                                 .OrderBy(item => item.value)
                                 .Select(item => item.index)
                                 .Take(3))
                    {
                        if(indexX == indexY || indexX == indexZ || indexZ == indexY) continue;
                        result = Math.Min(ax[indexX] + az[indexZ] + ay[indexY], result);
                    }
                }
            }

            return result;
        }
        
        static void Set(int[] a, int i, long v, int x) => a[i] = (int)(x - v % x) % x;
        static int GCD(int a, int b)
        {
            while (b != 0)
                (a, b) = (b, a % b);
            return a;
        }

        static int LCM(int a, int b) => a * b / GCD(a, b);
    }
    
}