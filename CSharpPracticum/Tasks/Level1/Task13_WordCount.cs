using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task13_WordCount : ITask
    {
        public string Title => "Задание 13. Счётчик вхождений слов в тексте (Dictionary)";

        public string Description => "посчитать, сколько раз каждое слово встречается в тексте.";

        public string[] Requirements => new string[]
        {
        "Использовать Dictionary<string, int>.",
        "Разбить текст на слова (по пробелам и знакам пунктуации).",
        "Привести слова к нижнему регистру.",
        "Вывести пары «слово → количество», отсортированные по убыванию количества."
        };

        public void Run()
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine() ?? "";

            char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r', '\t', '"', '(', ')' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (string word in words)
            {
                string lower = word.ToLower();
                if (counts.ContainsKey(lower))
                {
                    counts[lower]++;
                }
                else
                {
                    counts[lower] = 1;
                }
            }

            foreach (var pair in counts.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{pair.Key} → {pair.Value}");
            }
        }
    }
}
