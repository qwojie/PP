using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task09_Arrays : ITask
    {
        public string Title => "Задание 9. Массивы: min/max/sum/avg";

        public string Description => "для массива чисел найти минимум, максимум, сумму и среднее.";

        public string[] Requirements => new string[]
        {
        "Заполнить массив (например, случайными числами 1–100, размер 20).",
        "Реализовать без LINQ (Min/Max/Sum/Average запрещены).",
        "Оформить 4 отдельных метода.",
        "Пустой массив — обработать (вернуть ошибку или 0, но не падать)."
        };

        public void Run()
        {
            int[] mass = new int[20];
            Random rand = new Random();

            for (int i = 0; i < mass.Length; i++)
            {
                mass[i] = rand.Next(1, 101);
            }

            Console.WriteLine("Массив: " + string.Join(", ", mass));
            Console.WriteLine($"Минимум: {GetMin(mass)}");
            Console.WriteLine($"Максимум: {GetMax(mass)}");
            Console.WriteLine($"Сумма: {GetSum(mass)}");
            Console.WriteLine($"Среднее: {GetAverage(mass)}");

            int[] emptyMass = new int[0];
            Console.WriteLine("\nПроверка на пустом массиве:");
            Console.WriteLine($"Минимум: {GetMin(emptyMass)}, Максимум: {GetMax(emptyMass)}, Сумма: {GetSum(emptyMass)}, Среднее: {GetAverage(emptyMass)}");
        }

        private int GetMin(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return 0;
            }

            int min = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }

            return min;
        }

        private int GetMax(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return 0;
            }

            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }

            return max;
        }

        private int GetSum(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return 0;
            }

            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }

            return sum;
        }

        private double GetAverage(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return 0;
            }

            return (double)GetSum(arr) / arr.Length;
        }
    }
}
