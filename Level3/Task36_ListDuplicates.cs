using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task36_ListDuplicates : ITask
    {
        public string Title => "Задание 36. Дубликаты в List (HashSet + GroupBy)";

        public string Description => "найти дубликаты в списке чисел двумя способами.";

        public string[] Requirements => new string[]
        {
        "Способ 1: через HashSet<int> — при добавлении проверять Contains.",
        "Способ 2: через LINQ GroupBy(x => x).Where(g => g.Count() > 1).",
        "Оба способа оформить отдельными методами.",
        "Продемонстрировать на списке с повторяющимися значениями."
        };

        public void Run()
        {
            List<int> numbers = new List<int> { 1, 5, 3, 5, 7, 1, 9, 3, 3, 10 };
            Console.WriteLine("Исходный список: " + string.Join(", ", numbers));

            List<int> dup1 = FindDuplicatesWithHashSet(numbers);
            List<int> dup2 = FindDuplicatesWithLinq(numbers);

            Console.WriteLine("Способ 1 (HashSet): " + string.Join(", ", dup1));
            Console.WriteLine("Способ 2 (LINQ GroupBy): " + string.Join(", ", dup2));
        }

        private List<int> FindDuplicatesWithHashSet(List<int> list)
        {
            HashSet<int> seen = new HashSet<int>();
            HashSet<int> duplicates = new HashSet<int>();

            foreach (int item in list)
            {
                if (seen.Contains(item))
                {
                    duplicates.Add(item);
                }
                else
                {
                    seen.Add(item);
                }
            }

            return duplicates.ToList();
        }

        private List<int> FindDuplicatesWithLinq(List<int> list)
        {
            return list.GroupBy(x => x)
                       .Where(g => g.Count() > 1)
                       .Select(g => g.Key)
                       .ToList();
        }
    }
}
