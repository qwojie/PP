using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task43_MultipleExceptions : ITask
    {
        public string Title => "Задание 43. Множественные типы исключений, порядок catch";

        public string Description => "продемонстрировать порядок обработки исключений.";

        public string[] Requirements => new string[]
        {
        "Метод, который может бросить FormatException, DivideByZeroException или свой CustomException.",
        "Несколько catch-блоков в правильном порядке (от частного к общему).",
        "Продемонстрировать все три сценария.",
        "В конце — finally с выводом «завершено»."
        };

        public void Run()
        {
            for (int scenario = 1; scenario <= 3; scenario++)
            {
                Console.WriteLine($"Сценарий {scenario}:");
                try
                {
                    ThrowByScenario(scenario);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"  Поймано FormatException: {ex.Message}");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"  Поймано DivideByZeroException: {ex.Message}");
                }
                catch (CustomTaskException ex)
                {
                    Console.WriteLine($"  Поймано CustomTaskException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Поймано общее Exception: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("  завершено");
                }
            }

            Console.WriteLine("Объяснение: catch-блоки проверяются сверху вниз. Если поставить общий Exception первым, он перехватит всё, и до частных блоков управление не дойдёт.");
        }

        private void ThrowByScenario(int scenario)
        {
            if (scenario == 1)
            {
                int.Parse("не_число");
            }
            else if (scenario == 2)
            {
                int zero = 0;
                int res = 10 / zero;
            }
            else if (scenario == 3)
            {
                throw new CustomTaskException("Сработала пользовательская ошибка бизнес-логики.");
            }
        }
    }

    public class CustomTaskException : Exception
    {
        public CustomTaskException(string message) : base(message)
        {
        }
    }
}
