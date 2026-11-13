using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task18_TryParseVsTryCatch : ITask
    {
        public string Title => "Задание 18. TryParse vs try/catch";

        public string Description => "сравнить два способа обработки некорректного ввода числа.";

        public string[] Requirements => new string[]
        {
        "Способ 1: int.TryParse — вернуть bool + значение через out.",
        "Способ 2: int.Parse внутри try/catch — ловить FormatException.",
        "Оба оформить отдельными методами.",
        "Продемонстрировать на корректном и некорректном вводе."
        };

        public void Run()
        {
            string[] inputs = new string[] { "123", "abc" };
            foreach (string input in inputs)
            {
                Console.WriteLine($"Ввод: \"{input}\"");

                bool ok1 = ParseWithTryParse(input, out int res1);
                Console.WriteLine($"  TryParse: успех = {ok1}, значение = {res1}");

                bool ok2 = ParseWithTryCatch(input, out int res2);
                Console.WriteLine($"  Try/Catch: успех = {ok2}, значение = {res2}");
            }
        }

        private bool ParseWithTryParse(string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        private bool ParseWithTryCatch(string input, out int result)
        {
            try
            {
                result = int.Parse(input);
                return true;
            }
            catch (FormatException)
            {
                result = 0;
                return false;
            }
        }
    }
}
