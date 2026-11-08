using CSharpPracticum.Common;
using CSharpPracticum.Tasks.Level1;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace CSharpPracticum.Menu
{
    public class LevelMenu
    {
        public static void ShowLevel1()
        {
            List<ITask> tasks = new List<ITask>
            {
                new Task01_HelloWorldAndDate(),
                new Task02_PrimitivTypes(),
                new Task03_Calculator(),
                new Task04_Chetnost(),
                new Task05_Table(),
                new Task06_SumNumber(),
                new Task07_Reverse(),
                new Task08_FizzBuzz(),
                new Task09_Arrays(),
                new Task10_Sort(),
                new Task11_Matrix(),
                new Task12_Palindrome(),
                new Task13_WordCount(),
                new Task14_AgeCalc(),

            };

            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== Уровень 1. Базовый ===");
                Console.WriteLine("--------------------------------");

                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i].Title}");
                }

                Console.WriteLine("0. Назад");
                Console.WriteLine("--------------------------------");
                Console.Write("Выберите задание: ");

                string choice = Console.ReadLine();

                if (choice == "0")
                    return;

                if (!int.TryParse(choice, out int number) ||
                    number < 1 || number > tasks.Count)
                {
                    Console.Clear();
                    Console.WriteLine("Некорректный ввод!");
                    Console.WriteLine("Выберите существующее задание.\n");
                    Console.WriteLine("Нажмите Enter для продолжения...");
                    Console.ReadLine();
                    continue;
                }

                Console.Clear();

                ITask selectedTask = tasks[number - 1];

                TaskInfoAttribute.Show(selectedTask);

                selectedTask.Run();

                Console.WriteLine();
                Console.WriteLine("Нажмите Enter для возврата...");
                Console.ReadLine();
            }
        }

        public static void ShowLevel2()
        {
            Console.Clear();
            Console.WriteLine("--- Уровень 2. Средний ---");
            Console.WriteLine("Нажмите Enter для возврата:");
            Console.ReadLine();
        }

        public static void ShowLevel3()
        {
            Console.Clear();
            Console.WriteLine("--- Уровень 3. Продвинутый ---");
            Console.WriteLine("Нажмите Enter для возврата:");
            Console.ReadLine();
        }
    }
}
