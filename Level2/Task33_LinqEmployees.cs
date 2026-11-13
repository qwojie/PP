using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task33_LinqEmployees : ITask
    {
        public string Title => "Задание 33. LINQ: сотрудники (top-3, avg по отделу, фильтр)";

        public string Description => "LINQ-запросы по списку сотрудников.";

        public string[] Requirements => new string[]
        {
        "Класс Employee: Name, Age, Salary, Department.",
        "Список из 10+ сотрудников.",
        "Запросы через LINQ: топ-3 по зарплате (убывание); средняя зарплата по каждому отделу (GroupBy + Average); сотрудники старше 30 (Where).",
        "Вывести результаты."
        };

        public void Run()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee("Алексей", 28, 95000, "IT"),
                new Employee("Мария", 34, 120000, "IT"),
                new Employee("Дмитрий", 41, 150000, "Management"),
                new Employee("Елена", 25, 65000, "HR"),
                new Employee("Сергей", 37, 110000, "IT"),
                new Employee("Ольга", 31, 75000, "HR"),
                new Employee("Игорь", 29, 85000, "Sales"),
                new Employee("Наталья", 45, 140000, "Management"),
                new Employee("Павел", 33, 90000, "Sales"),
                new Employee("Татьяна", 27, 70000, "Sales")
            };

            var top3 = employees.OrderByDescending(e => e.Salary).Take(3);
            Console.WriteLine("Топ-3 по зарплате:");
            foreach (Employee e in top3)
            {
                Console.WriteLine($"  {e.Name} ({e.Department}) — {e.Salary}");
            }

            var avgByDept = employees.GroupBy(e => e.Department)
                                     .Select(g => new { Dept = g.Key, Avg = g.Average(e => e.Salary) });
            Console.WriteLine("\nСредняя зарплата по отделам:");
            foreach (var d in avgByDept)
            {
                Console.WriteLine($"  {d.Dept}: {d.Avg:F2}");
            }

            var olderThan30 = employees.Where(e => e.Age > 30);
            Console.WriteLine("\nСотрудники старше 30 лет:");
            foreach (Employee e in olderThan30)
            {
                Console.WriteLine($"  {e.Name}, {e.Age} лет");
            }
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }

        public Employee(string name, int age, decimal salary, string department)
        {
            Name = name;
            Age = age;
            Salary = salary;
            Department = department;
        }
    }
}
