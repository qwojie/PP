using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task40_AsyncDownload : ITask
    {
        public string Title => "Задание 40. async/await + обработка исключений";

        public string Description => "асинхронный метод с эмуляцией «скачивания».";

        public string[] Requirements => new string[]
        {
        "async Task<string> DownloadAsync(string url) — внутри Task.Delay(1000).",
        "Имитировать ошибку: если url пустой — бросить ArgumentException.",
        "В Main вызвать через await внутри try/catch.",
        "Продемонстрировать успешный и ошибочный вызов."
        };

        public void Run()
        {
            RunDemoAsync().GetAwaiter().GetResult();
        }

        private async Task RunDemoAsync()
        {
            string[] urls = new string[] { "https://example.com", "" };

            foreach (string url in urls)
            {
                try
                {
                    Console.WriteLine($"Запрос по адресу: \"{url}\"");
                    string data = await DownloadAsync(url);
                    Console.WriteLine($"Успех: {data}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Перехвачена ошибка: {ex.Message}");
                }
            }
        }

        private async Task<string> DownloadAsync(string url)
        {
            await Task.Delay(1000);

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL не может быть пустым.");
            }

            return $"Содержимое страницы {url} (200 OK)";
        }
    }
}
