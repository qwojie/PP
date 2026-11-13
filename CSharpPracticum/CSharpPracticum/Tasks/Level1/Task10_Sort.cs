using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CSharpPracticum.Tasks.Level1
{
    public class Task10_Sort : ITask
    {
        public string Title => "Задание 10. Сортировка пузырьком + вставками";

        public string Description => "отсортировать массив двумя алгоритмами.";

        public string[] Requirements => new string[]
        {
        "Реализовать BubbleSort и InsertionSort отдельными методами.",
        "Оба — без LINQ и Array.Sort.",
        "Замерить время каждого через Stopwatch на массиве 10 000 элементов.",
        "Вывести оба времени."
        };

        public void Run()
        {
            int[] original = new int[10000];
            Random rand = new Random();
            for (int i = 0; i < original.Length; i++)
            {
                original[i] = rand.Next(1, 10001);
            }

            int[] arr1 = (int[])original.Clone();
            int[] arr2 = (int[])original.Clone();

            Stopwatch sw = Stopwatch.StartNew();
            BubbleSort(arr1);
            sw.Stop();
            Console.WriteLine($"BubbleSort: {sw.ElapsedMilliseconds} мс");

            sw.Restart();
            InsertionSort(arr2);
            sw.Stop();
            Console.WriteLine($"InsertionSort: {sw.ElapsedMilliseconds} мс");
        }

        private void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        private void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }
    }
}
