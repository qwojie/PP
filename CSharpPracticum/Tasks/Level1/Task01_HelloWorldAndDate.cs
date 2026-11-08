using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task01_HelloWorldAndDate : ITask
    {
        public string Title => "Задание 1. Hello World и дата";

        public string Description => "вывести приветствие и текущую дату";

        public string[] Requirements => new string[]
        {
        "ИспользоватьConsole.WriteLine",
        "Дату получить черезDateTime.Now, отформатировать какdd.MM.yyyy.",
        "Вывести две строки: приветствие и дату."
        };

        public void Run()
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(DateTime.Now);
        }
    }
}
