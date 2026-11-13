using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task14_AgeCalc : ITask
    {
        public string Title => "Задание 14. Возраст по дате рождения";

        public string Description => "по дате рождения вычислить точный возраст в годах, месяцах и днях.";

        public string[] Requirements => new string[]
        {
        "Ввод даты через DateTime.TryParse (или TryParseExact).",
        "Учесть, что день рождения в этом году мог ещё не наступить.",
        "Вывести «X лет, Y месяцев, Z дней».",
        "Некорректная дата — сообщение об ошибке, не падать."
        };

        public void Run()
        {
            Console.Write("Введите дату рождения (например, 15.05.2005): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            {
                Console.WriteLine("Ошибка! Некорректный формат даты.");
                return;
            }

            DateTime today = DateTime.Today;
            if (birthDate > today)
            {
                Console.WriteLine("Ошибка! Дата рождения не может быть в будущем.");
                return;
            }

            int years = today.Year - birthDate.Year;
            int months = today.Month - birthDate.Month;
            int days = today.Day - birthDate.Day;

            if (days < 0)
            {
                months--;
                DateTime prevMonth = today.AddMonths(-1);
                days += DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            Console.WriteLine($"{years} лет, {months} месяцев, {days} дней");
        }
    }
}
