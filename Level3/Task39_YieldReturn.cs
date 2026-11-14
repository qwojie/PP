using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task39_YieldReturn : ITask
    {
        public string Title => "Задание 39. yield return — генератор чётных чисел";

        public string Description => "реализовать генератор чётных чисел через yield return.";

        public string[] Requirements => new string[]
        {
        "Метод IEnumerable<int> GetEvenNumbers(int max) — возвращает чётные числа до max.",
        "Реализовать именно через yield return, без промежуточного списка.",
        "Продемонстрировать работу в foreach.",
        "Дополнительно: показать ленивость — вывести сообщение внутри генератора и убедиться, что оно печатается по одному."
        };

        public void Run()
        {
            Console.WriteLine("Запуск цикла foreach по генератору до 10:");
            foreach (int even in GetEvenNumbers(10))
            {
                Console.WriteLine($"  Получено в цикле: {even}");
            }
        }

        private IEnumerable<int> GetEvenNumbers(int max)
        {
            for (int i = 2; i <= max; i += 2)
            {
                Console.WriteLine($"[Генератор] Вычисляю число: {i}");
                yield return i;
            }
        }
    }
}
