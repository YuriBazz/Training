using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeforcesTaskE
{
    // --- Trie для секретных строк ---
    // Каждый узел хранит количество строк, проходящих через него, и массив ссылок на детей (предполагается, что строки состоят из маленьких латинских букв).
    class Trie
    {
        public int Count;
        public Trie[] Children;

        public Trie()
        {
            Count = 0;
            Children = new Trie[26];
        }

        // Вставка строки s в trie (учитываем, что одинаковые строки добавляются несколько раз)
        public void Insert(string s)
        {
            Trie node = this;
            node.Count++;  // увеличиваем количество для корня
            foreach (char c in s)
            {
                int idx = c - 'a';
                if (node.Children[idx] == null)
                    node.Children[idx] = new Trie();
                node = node.Children[idx];
                node.Count++;
            }
        }

        // Подсчёт строк, начинающихся с префикса s
        public int Query(string s)
        {
            Trie node = this;
            foreach (char c in s)
            {
                int idx = c - 'a';
                if (node.Children[idx] == null)
                    return 0;
                node = node.Children[idx];
            }
            return node.Count;
        }
    }

    // --- Сервер ---
    // Каждый сервер содержит:
    // - поле lazy для накопленного префикса, который будет добавляться ко всем строкам
    // - trie для хранения добавленных строк (без lazy-префикса)
    class Server
    {
        public string Lazy;
        public Trie Trie;

        public Server()
        {
            Lazy = "";
            Trie = new Trie();
        }
    }

    // --- Имплицитное декартово дерево (treap) ---
    // Каждый узел соответствует серверу в кластере.
    class TreapNode
    {
        public Server Server;
        public int Priority;
        public int Size;
        public TreapNode Left, Right;

        public TreapNode(Server server, Random rnd)
        {
            Server = server;
            Priority = rnd.Next();
            Size = 1;
            Left = Right = null;
        }
    }

    class Treap
    {
        public TreapNode Root;
        private Random rnd = new Random();

        // Обновление размера узла
        private void UpdateSize(TreapNode node)
        {
            if (node != null)
                node.Size = 1 + GetSize(node.Left) + GetSize(node.Right);
        }

        private int GetSize(TreapNode node)
        {
            return node == null ? 0 : node.Size;
        }

        // Функция Split разбивает дерево t на два дерева: left (первые pos узлов) и right (остальные).
        public void Split(TreapNode t, int pos, out TreapNode left, out TreapNode right)
        {
            if (t == null)
            {
                left = right = null;
                return;
            }
            int leftSize = GetSize(t.Left);
            if (pos <= leftSize)
            {
                Split(t.Left, pos, out left, out t.Left);
                right = t;
            }
            else
            {
                Split(t.Right, pos - leftSize - 1, out t.Right, out right);
                left = t;
            }
            UpdateSize(t);
        }

        // Функция Merge объединяет два декартовых дерева.
        public TreapNode Merge(TreapNode left, TreapNode right)
        {
            if (left == null || right == null)
                return left ?? right;
            if (left.Priority < right.Priority)
            {
                left.Right = Merge(left.Right, right);
                UpdateSize(left);
                return left;
            }
            else
            {
                right.Left = Merge(left, right.Left);
                UpdateSize(right);
                return right;
            }
        }

        // Добавляем новый сервер в конец текущего дерева.
        public void Append(Server server)
        {
            TreapNode node = new TreapNode(server, rnd);
            Root = Merge(Root, node);
        }

        // Возвращает k-ый сервер (1-indexed) в дереве.
        public TreapNode GetKth(TreapNode node, int k)
        {
            if (node == null)
                return null;
            int leftSize = GetSize(node.Left);
            if (k == leftSize + 1)
                return node;
            else if (k <= leftSize)
                return GetKth(node.Left, k);
            else
                return GetKth(node.Right, k - leftSize - 1);
        }
    }

    // --- Кластер ---
    // Каждый кластер содержит:
    // - дерево серверов (Treap)
    // - счетчик запросов, который сбрасывается после достижения лимита m
    // - номера серверов, по которым пришли два последних запроса
    class Cluster
    {
        public Treap Servers;
        public int Counter;
        public int Last1, Last2;

        public Cluster()
        {
            Servers = new Treap();
            Counter = 0;
            Last1 = Last2 = 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Разбор входных данных.
            // Первая строка: N, m, Q
            string[] parts = Console.ReadLine().Split();
            int N = int.Parse(parts[0]);
            int m = int.Parse(parts[1]);
            int Q = int.Parse(parts[2]);

            Cluster[] clusters = new Cluster[N + 1]; // кластеры нумеруются от 1 до N
            for (int i = 1; i <= N; i++)
                clusters[i] = new Cluster();

            // Вторая строка содержит количество серверов в каждом кластере.
            parts = Console.ReadLine().Split();
            for (int i = 1; i <= N; i++)
            {
                int cnt = int.Parse(parts[i - 1]);
                // Для каждого кластера создаём cnt серверов.
                for (int j = 1; j <= cnt; j++)
                {
                    clusters[i].Servers.Append(new Server());
                }
            }

            // Обработка Q запросов.
            for (int qi = 0; qi < Q; qi++)
            {
                // Каждый запрос имеет формат: тип, номер кластера c, номер сервера s, строка v.
                parts = Console.ReadLine().Split();
                char type = parts[0][0];
                int c = int.Parse(parts[1]);
                int s = int.Parse(parts[2]);
                string v = parts[3];

                // Обновляем информацию о двух последних запросах в кластере c.
                clusters[c].Last1 = clusters[c].Last2;
                clusters[c].Last2 = s;

                // Находим сервер с номером s в текущем дереве.
                TreapNode node = clusters[c].Servers.GetKth(clusters[c].Servers.Root, s);
                Server srv = node.Server;

                if (type == '+')
                {
                    // Добавляем строку v в trie
                    srv.Trie.Insert(v);
                }
                else if (type == 'p')
                {
                    // Добавляем префикс v перед текущим lazy-префиксом
                    srv.Lazy = v + srv.Lazy;
                }
                else if (type == 'c')
                {
                    // Для запроса подсчёта:
                    // Если длина v меньше или равна длине lazy, то условие: lazy должен начинаться с v.
                    // Иначе, если v начинается с lazy, ищем оставшуюся часть в trie.
                    int ans = 0;
                    if (v.Length <= srv.Lazy.Length)
                    {
                        if (srv.Lazy.Substring(0, v.Length) == v)
                            ans = srv.Trie.Count;
                        else
                            ans = 0;
                    }
                    else
                    {
                        if (v.Substring(0, srv.Lazy.Length) != srv.Lazy)
                            ans = 0;
                        else
                        {
                            string rem = v.Substring(srv.Lazy.Length);
                            ans = srv.Trie.Query(rem);
                        }
                    }
                    Console.WriteLine(ans);
                }

                // Увеличиваем счётчик запросов для кластера.
                clusters[c].Counter++;
                // Если счётчик достиг лимита m, выполняем перенос серверов.
                if (clusters[c].Counter == m)
                {
                    // Определяем отрезок: от a до b, где a = min(Last1, Last2), b = max(Last1, Last2)
                    int s1 = clusters[c].Last1;
                    int s2 = clusters[c].Last2;
                    int a = Math.Min(s1, s2);
                    int b = Math.Max(s1, s2);

                    // Выполняем разбиение дерева кластера c на три части:
                    // left: сервера с номерами [1, a-1]
                    // mid: сервера с номерами [a, b]
                    // right: сервера с номерами [b+1, ...]
                    TreapNode left, mid, right;
                    clusters[c].Servers.Split(clusters[c].Servers.Root, a - 1, out left, out mid);
                    clusters[c].Servers.Split(mid, b - a + 1, out mid, out right);

                    // Убираем mid из кластера c: объединяем left и right.
                    clusters[c].Servers.Root = clusters[c].Servers.Merge(left, right);

                    // Сбрасываем счётчик и номера последних запросов
                    clusters[c].Counter = 0;
                    clusters[c].Last1 = clusters[c].Last2 = 0;

                    // Целевой кластер для переноса: следующий (если c == N, то 1)
                    int target = (c == N) ? 1 : c + 1;

                    // Находим позицию для вставки: после сервера с номером floor(size/2)
                    int sizeTarget = clusters[target].Servers.Root == null ? 0 : clusters[target].Servers.Root.Size;
                    int pos = sizeTarget / 2; // позиция для разбиения (0 означает вставку в начало)

                    TreapNode tleft, tright;
                    clusters[target].Servers.Split(clusters[target].Servers.Root, pos, out tleft, out tright);
                    // Вставляем отрезок mid между tleft и tright
                    clusters[target].Servers.Root = clusters[target].Servers.Merge(clusters[target].Servers.Merge(tleft, mid), tright);
                }
            }
        }
    }
}