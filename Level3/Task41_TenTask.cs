using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task41_TenTask : ITask
    {
        public string Title => "Задание 41. Task.WhenAll — параллельно 10 задач";

        public string Description => "запустить 10 задач параллельно и сравнить с последовательным выполнением.";

        public string[] Requirements => new string[]
        {
        "Метод Task<int> WorkAsync(int id) — Task.Delay(500), возвращает id.",
        "Последовательный запуск 10 задач в цикле с await.",
        "Параллельный запуск через Task.WhenAll.",
        "Замерить время обоих вариантов через Stopwatch.",
        "Вывести оба времени и объяснить разницу."
        };

        public void Run()
        {
            RunDemoAsync().GetAwaiter().GetResult();
        }

        private async Task RunDemoAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<int> seqResults = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                seqResults.Add(await WorkAsync(i));
            }
            sw.Stop();
            long seqTime = sw.ElapsedMilliseconds;

            sw.Restart();
            List<Task<int>> tasks = new List<Task<int>>();
            for (int i = 1; i <= 10; i++)
            {
                tasks.Add(WorkAsync(i));
            }
            int[] parResults = await Task.WhenAll(tasks);
            sw.Stop();
            long parTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"Последовательно: {seqTime} мс (результатов: {seqResults.Count})");
            Console.WriteLine($"Параллельно (WhenAll): {parTime} мс (результатов: {parResults.Length})");
            Console.WriteLine("Объяснение: последовательный запуск ждёт каждую задачу по очереди (10 * 500 мс ≈ 5000 мс), а Task.WhenAll запускает все 10 задач одновременно (≈ 500 мс).");
        }

        private async Task<int> WorkAsync(int id)
        {
            await Task.Delay(500);
            return id;
        }
    }
}
