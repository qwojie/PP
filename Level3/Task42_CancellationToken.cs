using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task42_CancellationToken : ITask
    {
        public string Title => "Задание 42. CancellationToken — отмена через 2 сек";

        public string Description => "реализовать долгую операцию с поддержкой отмены.";

        public string[] Requirements => new string[]
        {
        "async Task LongOperationAsync(CancellationToken token) — цикл с Task.Delay(500, token) 10 раз.",
        "В Main создать CancellationTokenSource, запустить операцию, отменить через 2 секунды.",
        "Обработать OperationCanceledException.",
        "Вывести «операция отменена»."
        };

        public void Run()
        {
            RunDemoAsync().GetAwaiter().GetResult();
        }

        private async Task RunDemoAsync()
        {
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                cts.CancelAfter(2000);

                try
                {
                    await LongOperationAsync(cts.Token);
                    Console.WriteLine("Операция успешно завершена.");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Операция отменена.");
                }
            }
        }

        private async Task LongOperationAsync(CancellationToken token)
        {
            for (int i = 1; i <= 10; i++)
            {
                token.ThrowIfCancellationRequested();
                Console.WriteLine($"Шаг {i} из 10...");
                await Task.Delay(500, token);
            }
        }
    }
}
