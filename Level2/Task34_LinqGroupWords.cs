using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task34_LinqGroupWords : ITask
    {
        public string Title => "Задание 34. LINQ: группировка слов по первой букве";

        public string Description => "сгруппировать слова по первой букве.";

        public string[] Requirements => new string[]
        {
        "Взять список слов (10+).",
        "Сгруппировать через LINQ GroupBy(w => w).",
        "Отсортировать группы по первой букве.",
        "Внутри группы — отсортировать слова.",
        "Вывести «Буква: слово1, слово2, ...»."
        };

        public void Run()
        {
            List<string> words = new List<string>
            {
                "банан", "арбуз", "вишня", "абрикос", "груша",
                "береза", "апельсин", "виноград", "горох", "дыня", "дуб"
            };

            var grouped = words.GroupBy(w => char.ToUpper(w[0]))
                               .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                var sortedWords = group.OrderBy(w => w);
                Console.WriteLine($"{group.Key}: {string.Join(", ", sortedWords)}");
            }
        }
    }
}
