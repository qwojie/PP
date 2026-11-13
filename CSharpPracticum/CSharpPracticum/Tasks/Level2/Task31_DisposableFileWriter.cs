using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    public class Task31_DisposableFileWriter : ITask
    {
        public string Title => "Задание 31. IDisposable + using";

        public string Description => "реализовать IDisposable для класса, работающего с файлом.";

        public string[] Requirements => new string[]
        {
        "Класс FileWriter с полем StreamWriter.",
        "Реализовать Dispose() (закрыть StreamWriter).",
        "Продемонстрировать использование через using.",
        "Дополнительно: продемонстрировать using-декларацию (C# 8+, using var)."
        };

        public void Run()
        {
            string filePath = "task31_demo.txt";

            using (FileWriter fw = new FileWriter(filePath, false))
            {
                fw.WriteLine("Первая запись через блок using.");
            }

            WriteWithUsingVar(filePath);

            Console.WriteLine("Содержимое файла после повторной записи:");
            Console.WriteLine(File.ReadAllText(filePath));
        }

        private void WriteWithUsingVar(string path)
        {
            using var fw = new FileWriter(path, true);
            fw.WriteLine("Вторая запись через using var.");
        }
    }

    public class FileWriter : IDisposable
    {
        private StreamWriter _writer;
        private bool _disposed = false;

        public FileWriter(string path, bool append = false)
        {
            _writer = new StreamWriter(path, append, Encoding.UTF8);
        }

        public void WriteLine(string text)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(FileWriter));
            }
            _writer.WriteLine(text);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_writer != null)
                {
                    _writer.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
