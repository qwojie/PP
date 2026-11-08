using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task08_FizzBuzz : ITask
    {
        public string Title => "Задание 8. FizzBuzz 1–100";

        public string Description => "вывести числа от 1 до 100 с заменами..";

        public string[] Requirements => new string[]
        {
        "Кратно 3 →Fizz.",
        "Кратно 5 →Buzz.",
        "Кратно 15 →FizzBuzz.",
        "Иначе — само число.",
        "Порядок проверок: сначала 15, потом 3 и 5."
        };

        public void Run()
        {
            for(int i = 1; i <= 100; i++)
            {
                if (i % 15 == 0)
                {
                    Console.WriteLine("Fizz-Buzz");
                }

                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }

                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }

                else
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
