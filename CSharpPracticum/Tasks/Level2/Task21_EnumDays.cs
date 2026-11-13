using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task21_EnumDays : ITask
    {
        public string Title => "Задание 21. enum дни недели";

        public string Description => "описать дни недели через enum и определить тип дня.";

        public string[] Requirements => new string[]
        {
        "enum DayOfWeek { Monday, ..., Sunday }.",
        "Метод IsWeekend(DayOfWeek day) — true для субботы и воскресенья.",
        "Метод GetRussianName(DayOfWeek day) — возвращает название на русском.",
        "Продемонстрировать на всех 7 днях."
        };

        public void Run()
        {
            foreach (CustomDayOfWeek day in Enum.GetValues(typeof(CustomDayOfWeek)))
            {
                Console.WriteLine($"{GetRussianName(day)} ({day}) — Выходной: {IsWeekend(day)}");
            }
        }

        private bool IsWeekend(CustomDayOfWeek day)
        {
            return day == CustomDayOfWeek.Saturday || day == CustomDayOfWeek.Sunday;
        }

        private string GetRussianName(CustomDayOfWeek day)
        {
            switch (day)
            {
                case CustomDayOfWeek.Monday:
                    return "Понедельник";
                case CustomDayOfWeek.Tuesday:
                    return "Вторник";
                case CustomDayOfWeek.Wednesday:
                    return "Среда";
                case CustomDayOfWeek.Thursday:
                    return "Четверг";
                case CustomDayOfWeek.Friday:
                    return "Пятница";
                case CustomDayOfWeek.Saturday:
                    return "Суббота";
                case CustomDayOfWeek.Sunday:
                    return "Воскресенье";
                default:
                    return "";
            }
        }
    }

    public enum CustomDayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
