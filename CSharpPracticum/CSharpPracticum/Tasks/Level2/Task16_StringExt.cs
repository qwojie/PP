using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task16_StringExt : ITask
    {
        public string Title => "Задание 16. Методы расширения для string";

        public string Description => "написать extension-методы для string.";

        public string[] Requirements => new string[]
        {
        "Реализовать в статическом классе StringExtensions.",
        "Методы: IsPalindrome(), WordCount(), ReverseString().",
        "Методы должны быть public static с this string первым параметром.",
        "Продемонстрировать вызов на 2–3 строках."
        };

        public void Run()
        {
            string[] tests = new string[] { "abc", "А роза упала на лапу Азора", "Привет мир, как дела?" };
            foreach (string s in tests)
            {
                Console.WriteLine($"Строка: \"{s}\"");
                Console.WriteLine($"  Реверс: \"{s.ReverseString()}\"");
                Console.WriteLine($"  Палиндром: {s.IsPalindrome()}");
                Console.WriteLine($"  Слов: {s.WordCount()}");
            }
        }
    }

    public static class StringExtensions
    {
        public static string ReverseString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            char[] chars = str.ToCharArray();
            int left = 0;
            int right = chars.Length - 1;

            while (left < right)
            {
                char temp = chars[left];
                chars[left] = chars[right];
                chars[right] = temp;
                left++;
                right--;
            }

            return new string(chars);
        }

        public static bool IsPalindrome(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(char.ToLower(c));
                }
            }

            string clean = sb.ToString();
            return clean == clean.ReverseString();
        }

        public static int WordCount(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }

            char[] separators = new char[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?' };
            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
