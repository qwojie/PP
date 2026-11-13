using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task20_Generics : ITask
    {
        public string Title => "Задание 20. Обобщения: Swap<T>, Max<T> с IComparable<T>";

        public string Description => "написать обобщённые методы.";

        public string[] Requirements => new string[]
        {
        "Swap<T>(ref T a, ref T b) — меняет значения местами.",
        "Max<T>(T a, T b) where T : IComparable<T> — возвращает большее.",
        "Продемонстрировать на int, string, double.",
        "Swap — без ограничений, Max — с ограничением."
        };

        public void Run()
        {
            int a1 = 5;
            int b1 = 10;
            Console.WriteLine($"int до: a={a1}, b={b1}, Max={Max(a1, b1)}");
            Swap(ref a1, ref b1);
            Console.WriteLine($"int после: a={a1}, b={b1}");

            double a2 = 3.14;
            double b2 = 2.71;
            Console.WriteLine($"double до: a={a2}, b={b2}, Max={Max(a2, b2)}");
            Swap(ref a2, ref b2);
            Console.WriteLine($"double после: a={a2}, b={b2}");

            string a3 = "Apple";
            string b3 = "Banana";
            Console.WriteLine($"string до: a={a3}, b={b3}, Max={Max(a3, b3)}");
            Swap(ref a3, ref b3);
            Console.WriteLine($"string после: a={a3}, b={b3}");
        }

        private void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        private T Max<T>(T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) > 0)
            {
                return a;
            }
            return b;
        }
    }
}
