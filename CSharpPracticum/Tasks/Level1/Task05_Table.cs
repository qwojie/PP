using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    internal class Task05_Table : ITask
    {
        public string Title => "Задание 5. Таблица умножения 10×10";

        public string Description => "вывести таблицу умножения 10×10.";

        public string[] Requirements => new string[]
        {
        "Использовать вложенные циклы for.",
        "Выровнять столбцы.",
        "Заголовки строк/столбцов — не обязательны, но приветствуются."
        };

        public void Run()
        {
            string table = "";

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    table += $"{i * j, 5}";
                }
                table += "\n";
            }

            Console.WriteLine(table);
        }
    }
}
