using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task22_ParamsOverload : ITask
    {
        public string Title => "Задание 22. params-метод + перегрузка";

        public string Description => "написать метод суммы с params и его перегрузку.";

        public string[] Requirements => new string[]
        {
        "Sum(params int[] numbers) — сумма произвольного числа чисел.",
        "Перегрузка Sum(int[] numbers) — та же логика для массива.",
        "Продемонстрировать: Sum(1, 2, 3), Sum(new[] {1, 2, 3}), Sum().",
        "Пустой вызов — вернуть 0."
        };

        public void Run()
        {
            Console.WriteLine($"Sum(1, 2, 3) = {Sum(1, 2, 3)}");
            Console.WriteLine($"Sum(new[] {{ 1, 2, 3 }}) = {Sum(new int[] { 1, 2, 3 }, 0)}");
            Console.WriteLine($"Sum() = {Sum()}");
        }

        private int Sum(params int[] numbers)
        {
            return SumCore(numbers);
        }

        private int Sum(int[] numbers, int offset)
        {
            return SumCore(numbers) + offset;
        }

        private int SumCore(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                return 0;
            }

            int total = 0;
            foreach (int n in numbers)
            {
                total += n;
            }
            return total;
        }
    }
}
