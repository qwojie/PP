using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task37_FibonacciMemo : ITask
    {
        public string Title => "Задание 37. Мемоизация Фибоначчи через Dictionary";

        public string Description => "реализовать мемоизированную функцию Фибоначчи.";

        public string[] Requirements => new string[]
        {
        "Обычная рекурсивная версия (для сравнения на маленьком N).",
        "Мемоизированная версия с Dictionary<int, long>.",
        "Замерить время обеих на N = 30–35 через Stopwatch.",
        "Вывести оба времени и разницу."
        };

        public void Run()
        {
            int n = 35;

            Stopwatch sw = Stopwatch.StartNew();
            long res1 = FibRecursive(n);
            sw.Stop();
            long time1 = sw.ElapsedMilliseconds;

            Dictionary<int, long> memo = new Dictionary<int, long>();
            sw.Restart();
            long res2 = FibMemo(n, memo);
            sw.Stop();
            long time2 = sw.ElapsedMilliseconds;

            Console.WriteLine($"N = {n}");
            Console.WriteLine($"Обычная рекурсия: {res1} (время: {time1} мс)");
            Console.WriteLine($"Мемоизация: {res2} (время: {time2} мс)");
            Console.WriteLine($"Разница: {time1 - time2} мс");
        }

        private long FibRecursive(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            return FibRecursive(n - 1) + FibRecursive(n - 2);
        }

        private long FibMemo(int n, Dictionary<int, long> memo)
        {
            if (n <= 1)
            {
                return n;
            }

            if (memo.TryGetValue(n, out long cached))
            {
                return cached;
            }

            long result = FibMemo(n - 1, memo) + FibMemo(n - 2, memo);
            memo[n] = result;
            return result;
        }
    }
}
