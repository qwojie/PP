using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task03_Calculator : ITask 
    {
        public string Title => "Задание 3. Калькулятор с обработкой деления на 0";

        public string Description => "принимать два числа и оператор (+ - * /), выводить результат.";

        public string[] Requirements => new string[]
        {
        "Оператор обрабатывать через switch.",
        "При делении на ноль — вывести сообщение об ошибке, не ронять приложение.",
        "Некорректный оператор — вывести «Неизвестная операция».",
        "Ввод чисел — через double.Parse или TryParse."
        };

        public void Run()
        {
            Console.WriteLine("Введите два числа");
            double a = double.Parse(Console.ReadLine());
            double b = double.Parse(Console.ReadLine());

            Console.WriteLine("Выберите оператор (+; -; *; /;)");
            string z = Console.ReadLine();

            switch (z)
            {
                case "+":
                    var OpPlus = a + b;
                    Console.WriteLine($"{a} + {b} =" + OpPlus);
                    break;
                case "-":
                    var OpMinus = a - b;
                    Console.WriteLine($"{a} - {b} =" + OpMinus);
                    break;
                case "*":
                    var OpUMNOJIT = a * b;
                    Console.WriteLine($"{a} * {b} =" + OpUMNOJIT);
                    break;
                case "/":
                    if (b == 0)
                    {
                        Console.WriteLine("На ноль нельзя делить");
                        return;
                    }
                    var OpDELIT = a / b;
                    Console.WriteLine($"{a} / {b} =" + OpDELIT);
                    break;
                    default: Console.WriteLine("такого оператор нет");
                        break;
            }
        }

    }
}
