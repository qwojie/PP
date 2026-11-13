using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task27_StudentCompare : ITask
    {
        public string Title => "Задание 27. IComparable / IComparer<T> для Student";

        public string Description => "научить Student сортироваться.";

        public string[] Requirements => new string[]
        {
        "Класс Student: Name, AverageGrade.",
        "Реализовать IComparable<Student> — сортировка по среднему баллу (по убыванию).",
        "Реализовать IComparer<Student> ByNameComparer — сортировка по имени.",
        "Продемонстрировать обе сортировки на List<Student>."
        };

        public void Run()
        {
            List<Student> students = new List<Student>
            {
                new Student("Иван", 4.2),
                new Student("Анна", 4.9),
                new Student("Пётр", 3.8)
            };

            students.Sort();
            Console.WriteLine("По убыванию среднего балла:");
            foreach (Student s in students)
            {
                Console.WriteLine("  " + s);
            }

            students.Sort(new ByNameComparer());
            Console.WriteLine("\nПо имени:");
            foreach (Student s in students)
            {
                Console.WriteLine("  " + s);
            }
        }
    }

    public class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public double AverageGrade { get; set; }

        public Student(string name, double averageGrade)
        {
            Name = name;
            AverageGrade = averageGrade;
        }

        public int CompareTo(Student? other)
        {
            if (other == null)
            {
                return 1;
            }
            return other.AverageGrade.CompareTo(AverageGrade);
        }

        public override string ToString()
        {
            return $"{Name}: {AverageGrade}";
        }
    }

    public class ByNameComparer : IComparer<Student>
    {
        public int Compare(Student? x, Student? y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
