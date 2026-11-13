using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task17_StringBuilderVsConcat : ITask
    {
        public string Title => "Задание 17. StringBuilder vs конкатенация (замер)";

        public string Description => "сравнить производительность конкатенации через + и StringBuilder.";

        public string[] Requirements => new string[]
        {
        "Собрать строку из 10 000 (или 100 000) чисел двумя способами.",
        "Замерить время каждого через Stopwatch.",
        "Вывести оба времени и разницу.",
        "Объяснить в комментарии, почему StringBuilder быстрее."
        };

        public void Run()
        {
            int count = 30000;

            Stopwatch sw = Stopwatch.StartNew();
            string s1 = "";
            for (int i = 0; i < count; i++)
            {
                s1 += i.ToString();
            }
            sw.Stop();
            long timeConcat = sw.ElapsedMilliseconds;

            sw.Restart();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(i);
            }
            string s2 = sb.ToString();
            sw.Stop();
            long timeSb = sw.ElapsedMilliseconds;

            Console.WriteLine($"Конкатенация (+): {timeConcat} мс");
            Console.WriteLine($"StringBuilder: {timeSb} мс");
            Console.WriteLine($"Разница: {timeConcat - timeSb} мс");
            Console.WriteLine($"Результаты идентичны: {s1 == s2}");
        }
    }
}
