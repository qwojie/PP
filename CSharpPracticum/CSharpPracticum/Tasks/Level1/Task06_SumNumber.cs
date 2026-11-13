using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task06_SumNumber : ITask
    {
        public string Title => "Задание 6. Сумма цифр числа (строка + арифметика)";

        public string Description => "найти сумму цифр введённого числа";

        public string[] Requirements => new string[]
        {
        "Способ 1 (строка): число →ToString()→ пройти по символам →int.Parse каждой цифры.",
        "Способ 2 (арифметика): цикл while (n > 0) { sum += n % 10; n /= 10; }",
        "Оба способа оформить отдельными методами",
        "Отрицательные числа — брать модуль."
        };

        public void Run()
        {
            Console.WriteLine("Введите число: ");
            if (int.TryParse(Console.ReadLine(), out int s))
            {
                int sum1 = SumString(s);
                Console.WriteLine("Способ 1 (строка):" + sum1);

                int sum2 = SumArithmetic(s);
                Console.WriteLine("Способ 2 (арифметика):" + sum2);
            }
            else
            {
                Console.WriteLine("Введите корректное число");
            }
        }

        private int SumString(int s)
        {
            int sum1 = 0;
            string first = Math.Abs(s).ToString();

            foreach (char prob in first)
            {
                int reg = int.Parse(prob.ToString());
                sum1 += reg;
            }

            return sum1;
        }

        private int SumArithmetic(int s)
        {
            int n = Math.Abs(s);
            int sum2 = 0;
            while (n > 0)
            {
                sum2 += n % 10;
                n /= 10;
            }

            return sum2;
        }
    }
}
