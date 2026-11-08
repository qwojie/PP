using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task04_Chetnost : ITask
    {
        public string Title => "Задание 4. Чётность числа (два способа)";

        public string Description => "определить, чётное ли введённое число.";

        public string[] Requirements => new string[]
        {
        "Способ 1: через % 2 == 0",
        "Способ 2: через тернарный оператор ? :",
        "Оба способа реализовать отдельными методами.",
        "Вывести результат обоих способов."
        };

        public void Run()
        {
            Console.WriteLine("Введите число: ");
            if (int.TryParse(Console.ReadLine(), out int a))
            {
                if (Method1(a))
                {
                    Console.WriteLine($"Способ 1: Число {a} Четное ");
                }
                else
                {
                    Console.WriteLine($"Способ 1: Число {a} Нечетное ");
                }

                bool sec = Method2(a);
                Console.WriteLine($"Способ 2: четное ли число? : {sec}");
            }
            else
            {
                Console.WriteLine("Введите корректное число");
            }
        }

        private bool Method1(int a)
        {
            if (a % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Method2(int a)
        {
            return a % 2 == 0 ? true : false;
        }
    }
}