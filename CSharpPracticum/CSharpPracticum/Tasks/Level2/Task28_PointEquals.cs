using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task28_PointEquals : ITask
    {
        public string Title => "Задание 28. Equals / GetHashCode для Point";

        public string Description => "переопределить Equals и GetHashCode для класса Point(x, y).";

        public string[] Requirements => new string[]
        {
        "Переопределить Equals(object) и GetHashCode().",
        "Реализовать IEquatable<Point>.",
        "Проверить: две точки с одинаковыми координатами равны.",
        "Проверить в HashSet<Point>: добавление дубликата не увеличивает Count."
        };

        public void Run()
        {
            Point p1 = new Point(2, 3);
            Point p2 = new Point(2, 3);
            Point p3 = new Point(5, 7);

            Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");
            Console.WriteLine($"p1.Equals(p3): {p1.Equals(p3)}");

            HashSet<Point> set = new HashSet<Point>();
            set.Add(p1);
            set.Add(p2);
            set.Add(p3);

            Console.WriteLine($"Количество уникальных точек в HashSet: {set.Count}");
        }
    }

    public class Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point? other)
        {
            if (other == null)
            {
                return false;
            }
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Point);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
