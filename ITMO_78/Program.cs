using System;
using System.Linq;

namespace ITMO_78
{
    static class Program
    {
        public static long[] Read() => Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        
        static void Main(string[] args)
        {
            var t = Read();
            var (r, n, m) = (t[0], t[1], t[2]);
            Matrix.SetR(r);
            var a = new Matrix[n];
            for (var i = 0; i < n; ++i)
                a[i] = Matrix.ReadMatrix();

            var tree = new DisjoinSparseTable(a);
            while (m-- > 0)
            {
                var c = Read();
                tree.Mul((int)c[0] - 1,(int)c[1] - 1).Write();
            }
        }
    }

    class Matrix
    {
        private static long _r;
        private readonly long[,] _data;

        private Matrix(long a00, long a01, long a10, long a11)
        {
            _data = new long[2, 2];
            _data[0, 0] = a00 % _r;
            _data[0, 1] = a01 % _r;
            _data[1, 0] = a10 % _r;
            _data[1, 1] = a11 % _r;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            var a00 = m1._data[0, 0] * m2._data[0, 0]  + m1._data[0, 1] * m2._data[1, 0];
            var a01 = m1._data[0, 0] * m2._data[0, 1]  + m1._data[0, 1] * m2._data[1, 1]  ;
            var a10 = m1._data[1, 0] * m2._data[0, 0]  + m1._data[1, 1] * m2._data[1, 0] ;
            var a11 = m1._data[1, 0] * m2._data[0, 1]  + m1._data[1, 1] * m2._data[1, 1]  ;
            return new Matrix(a00, a01, a10, a11);
        }
        
        public static Matrix ReadMatrix()
        {
            var a = Program.Read();
            var b = Program.Read();
            Console.ReadLine();
            return new Matrix(a[0],a[1],b[0],b[1]);
        }

        public static void SetR(long r) => _r = r;

        public static Matrix Neutral => new (1, 0, 0, 1);

        public void Write()
        {
            Console.WriteLine($"{_data[0,0]} {_data[0,1]}");
            Console.WriteLine($"{_data[1,0]} {_data[1,1]}\n");
            
        }
        
    }



    class DisjoinSparseTable
    {
        public int[] bits;
        public int size;
        public int log;
        public Matrix[,] table;
        public Matrix[] arr;
        public int zero;

        public DisjoinSparseTable(Matrix[] a)
        {
            size = 1;
            log = 0;
            while (size < a.Length)
            {
                size *= 2;
                log++;
            }
            arr = a;
            var neutral = Matrix.Neutral;
            if (log != 0)
            {
                bits = new int[size];
                for (var i = 2; i < size; ++i)
                    bits[i] = bits[i >> 1] + 1;
                table = new Matrix[log, size];
                for (var k = 0; k < log; ++k)
                {
                    var len = 1 << k;
                    for (var m = len; m < size; m += 2 * len)
                    {
                        table[k, m] = m < arr.Length ? arr[m] : neutral;
                        for (var i = m + 1; i < m + len; ++i)
                            table[k, i] = i < arr.Length ? table[k, i - 1] * arr[i] : table[k, i - 1];
                        table[k, m - 1] = m - 1 < arr.Length ? arr[m - 1] : neutral;
                        for (var i = m - 2; i > m - len - 1; --i)
                            table[k, i] = i < arr.Length ? arr[i] * table[k, i + 1] : table[k, i + 1];
                    }
                }
            }
        }
        
        // надо узнать номер первого бита, в котором различаются l и r, при проходе слева направо, но с нумерацией от 0 на старшем бите

        public Matrix Mul(int l, int r)
        {
            if (l == r) return arr[l];
            var k = bits[l ^ r];
            return table[k, l] * table[k, r];
        }

        
    }
}