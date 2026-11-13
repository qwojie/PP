using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task11_Matrix : ITask
    {
        public string Title => "Задание 11. Диагональ матрицы N×N";

        public string Description => "найти сумму элементов главной диагонали матрицы N×N.";

        public string[] Requirements => new string[]
        {
        "Размер N вводится пользователем (минимум 2).",
        "Матрица заполняется случайными числами.",
        "Сумма считается без LINQ, в одном цикле for.",
        "Вывести саму матрицу и сумму."
        };

        public void Run()
        {
            Console.Write("Введите размер матрицы N (минимум 2): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 2)
            {
                Console.WriteLine("Ошибка! Введите целое число >= 2.");
                return;
            }

            int[,] matrix = new int[n, n];
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = rand.Next(1, 10);
                    Console.Write($"{matrix[i, j],3}");
                }
                Console.WriteLine();
            }

            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += matrix[i, i];
            }

            Console.WriteLine($"Сумма главной диагонали: {sum}");
        }
    }
}
