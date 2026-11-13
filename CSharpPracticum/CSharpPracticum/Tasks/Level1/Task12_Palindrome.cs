using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task12_Palindrome : ITask
    {
        public string Title => "Задание 12. Палиндром (без учёта регистра/пробелов)";

        public string Description => "проверить, является ли строка палиндромом.";

        public string[] Requirements => new string[]
        {
        "Игнорировать регистр (ToLower/ToUpper).",
        "Игнорировать пробелы и знаки пунктуации (или хотя бы пробелы).",
        "Не использовать LINQ Reverse.",
        "Пустая строка и строка из одного символа — палиндром."
        };

        public void Run()
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine() ?? "";

            bool result = IsPalindrome(input);
            Console.WriteLine($"Палиндром: {result}");
        }

        private bool IsPalindrome(string text)
        {
            StringBuilder cleaned = new StringBuilder();
            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    cleaned.Append(char.ToLower(c));
                }
            }

            string s = cleaned.ToString();
            int left = 0;
            int right = s.Length - 1;

            while (left < right)
            {
                if (s[left] != s[right])
                {
                    return false;
                }
                left++;
                right--;
            }

            return true;
        }
    }
}
