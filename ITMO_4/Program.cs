using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;

namespace ITMO_4
{

    class Program
    {
        static void Main(string[] args)
        {
            KAVO();
            /*var q = int.Parse(Console.ReadLine());
            for (; q > 0; --q)
            {
                
                var s = Console.ReadLine();
                var t = Console.ReadLine();
                var b = new bool[s.Length];
                int r = 0, k = 0;
                for (var i = 0; i <= s.Length - t.Length; ++i)
                {
                    b[i] = s.Substring(i, t.Length) == t;
                }

                while (k < s.Length)
                {
                    while (k < s.Length && b[k])
                        k++;
                    var start = k;
                    while (k < s.Length && !b[k])
                        k++;
                    r += ((k - start) * (k - start) + k - start) / 2;
                }
                Console.WriteLine(r);
            }*/
        }
        
        static void KAVO()
        {
            // Чтение количества наборов данных
            int q = int.Parse(Console.ReadLine());
            var results = new List<int>();

            for (int i = 0; i < q; i++)
            {
                // Чтение строки s и подстроки t
                string s = Console.ReadLine();
                string t = Console.ReadLine();
            
                // Подсчет "хороших" подстрок
                results.Add(CountGoodSubstrings(s, t));
            }

            // Вывод результатов
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        static int CountGoodSubstrings(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;

            // Массив для отметки индексов, которые содержат подстроку t
            bool[] badIndices = new bool[n];

            // Поиск всех вхождений подстроки t в строке s
            int pos = s.IndexOf(t, StringComparison.Ordinal);
            while (pos != -1)
            {
                // Отметим индексы, где начинается подстрока t
                for (int k = pos; k < Math.Min(pos + m, n); k++)
                {
                    badIndices[k] = true;
                }
                pos = s.IndexOf(t, pos + 1, StringComparison.Ordinal);
            }

            // Подсчет количества "хороших" подстрок
            int count = 0;
            int i = 0;
        
            while (i < n)
            {
                // Пропускаем "плохие" индексы
                if (badIndices[i])
                {
                    i++;
                    continue;
                }

                // Находим длину "хорошего" участка
                int start = i;
                while (i < n && !badIndices[i])
                {
                    i++;
                }
                int length = i - start;

                // Добавляем количество подстрок для этого участка
                count += length * (length + 1) / 2;
            }

            return count;
        }
    }
}