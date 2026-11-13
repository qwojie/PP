using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task25_ShapesSort : ITask
    {
        public string Title => "Задание 25. Интерфейс IShape + сортировка фигур";

        public string Description => "реализовать интерфейс фигур и отсортировать их.";

        public string[] Requirements => new string[]
        {
        "IShape с методами Area() и Perimeter().",
        "Классы Circle, Rectangle, Triangle — реализуют IShape.",
        "Создать List<IShape> из 5+ фигур.",
        "Отсортировать по площади (через List.Sort с лямбдой или OrderBy).",
        "Вывести фигуры с площадями до и после сортировки."
        };

        public void Run()
        {
            List<IShape> shapes = new List<IShape>
            {
                new Circle(5),
                new Rectangle(4, 6),
                new Triangle(3, 4, 5),
                new Circle(2),
                new Rectangle(10, 2)
            };

            Console.WriteLine("До сортировки:");
            foreach (IShape s in shapes)
            {
                Console.WriteLine($"{s} -> Площадь: {s.Area():F2}, Периметр: {s.Perimeter():F2}");
            }

            shapes.Sort((a, b) => a.Area().CompareTo(b.Area()));

            Console.WriteLine("\nПосле сортировки по площади:");
            foreach (IShape s in shapes)
            {
                Console.WriteLine($"{s} -> Площадь: {s.Area():F2}, Периметр: {s.Perimeter():F2}");
            }
        }
    }

    public interface IShape
    {
        double Area();
        double Perimeter();
    }

    public class Circle : IShape
    {
        public double Radius { get; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double Area()
        {
            return Math.PI * Radius * Radius;
        }

        public double Perimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string ToString()
        {
            return $"Круг (R={Radius})";
        }
    }

    public class Rectangle : IShape
    {
        public double Width { get; }
        public double Height { get; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Area()
        {
            return Width * Height;
        }

        public double Perimeter()
        {
            return 2 * (Width + Height);
        }

        public override string ToString()
        {
            return $"Прямоугольник ({Width}x{Height})";
        }
    }

    public class Triangle : IShape
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public double Perimeter()
        {
            return A + B + C;
        }

        public double Area()
        {
            double p = Perimeter() / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        public override string ToString()
        {
            return $"Треугольник ({A}, {B}, {C})";
        }
    }
}
