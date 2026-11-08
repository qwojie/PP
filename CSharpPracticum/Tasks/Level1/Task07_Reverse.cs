using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task07_Reverse : ITask
    {
        public string Title => "Задание 7. Реверс строки без LINQ";

        public string Description => "перевернуть введённую строку.";

        public string[] Requirements => new string[]
        {
        "Не использовать Array.Reverse и LINQ.",
        "Реализовать через цикл for от конца к началу (или через char[]и swap).",
        "Пустая строка и строка из одного символа — обрабатываются корректно."
        };

        public void Run()
        {
            Console.WriteLine("Введите строку:");
            string vvod = Console.ReadLine();
            string res = "";

            for (int i = vvod.Length - 1; i >= 0; i--)
            {
                res+= vvod[i];
            }
            Console.WriteLine("Развернутая строка:" + res);
        }
    }
}
