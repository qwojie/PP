using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task23_NullableTypes : ITask
    {
        public string Title => "Задание 23. Nullable-типы, ??, ?.";

        public string Description => "продемонстрировать работу с nullable.";

        public string[] Requirements => new string[]
        {
        "Метод int? FindValue(string key) — возвращает null, если ключа нет.",
        "Использовать ?? для значения по умолчанию.",
        "Использовать ?. для безопасного доступа к свойству объекта, который может быть null.",
        "Использовать HasValue / Value с проверкой.",
        "Продемонстрировать на примере с найденным и ненайденным ключом."
        };

        private Dictionary<string, int> _data = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 }
        };

        public void Run()
        {
            string[] keys = new string[] { "one", "missing" };
            foreach (string key in keys)
            {
                int? val = FindValue(key);
                int withDefault = val ?? -1;
                string? strVal = val?.ToString();

                Console.WriteLine($"Ключ: {key}");
                Console.WriteLine($"  Через ??: {withDefault}");
                Console.WriteLine($"  Через ?.: {strVal ?? "null"}");
                if (val.HasValue)
                {
                    Console.WriteLine($"  Через HasValue/Value: {val.Value}");
                }
                else
                {
                    Console.WriteLine("  Через HasValue/Value: значения нет");
                }
            }
        }

        private int? FindValue(string key)
        {
            if (_data.TryGetValue(key, out int value))
            {
                return value;
            }
            return null;
        }
    }
}
