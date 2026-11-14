using CSharpPracticum.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task44_TodoList : ITask
    {
        public string Title => "Задание 44. Мини-проект «Todo List» (классы + JSON)";

        public string Description => "консольное приложение для списка задач с сохранением в JSON.";

        public string[] Requirements => new string[]
        {
        "Классы: TaskItem (Id, Title, IsDone), TaskRepository (CRUD), TaskService (бизнес-логика).",
        "CRUD-операции: добавить, вывести все, отметить выполненной, удалить.",
        "Сохранение/загрузка через System.Text.Json (файл tasks.json).",
        "Меню с выбором действий.",
        "Обработка ошибок (некорректный ввод, отсутствующий файл)."
        };

        public void Run()
        {
            TaskRepository repository = new TaskRepository("tasks.json");
            TaskService service = new TaskService(repository);

            while (true)
            {
                Console.WriteLine("\n--- Todo List ---");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать все задачи");
                Console.WriteLine("3. Отметить выполненной");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("0. Выход из задания");
                Console.Write("Выберите действие: ");

                string? choice = Console.ReadLine();

                if (choice == "0")
                {
                    break;
                }

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите название задачи: ");
                        string? title = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(title))
                        {
                            Console.WriteLine("Ошибка: название не может быть пустым.");
                        }
                        else
                        {
                            service.CreateTask(title);
                            Console.WriteLine("Задача добавлена.");
                        }
                        break;

                    case "2":
                        List<TaskItem> items = service.GetAllTasks();
                        if (items.Count == 0)
                        {
                            Console.WriteLine("Список задач пуст.");
                        }
                        else
                        {
                            foreach (TaskItem item in items)
                            {
                                string status = item.IsDone ? "[Выполнено]" : "[В работе]";
                                Console.WriteLine($"{item.Id}. {status} {item.Title}");
                            }
                        }
                        break;

                    case "3":
                        Console.Write("Введите Id задачи: ");
                        if (int.TryParse(Console.ReadLine(), out int doneId))
                        {
                            if (service.MarkAsDone(doneId))
                            {
                                Console.WriteLine("Статус обновлён.");
                            }
                            else
                            {
                                Console.WriteLine("Задача с таким Id не найдена.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: введите корректное число.");
                        }
                        break;

                    case "4":
                        Console.Write("Введите Id задачи для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int delId))
                        {
                            if (service.RemoveTask(delId))
                            {
                                Console.WriteLine("Задача удалена.");
                            }
                            else
                            {
                                Console.WriteLine("Задача с таким Id не найдена.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: введите корректное число.");
                        }
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }

    public class TaskRepository
    {
        private readonly string _filePath;
        private readonly List<TaskItem> _tasks;

        public TaskRepository(string filePath)
        {
            _filePath = filePath;
            _tasks = LoadFromFile();
        }

        public List<TaskItem> GetAll()
        {
            return _tasks.ToList();
        }

        public void Add(TaskItem item)
        {
            int nextId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
            item.Id = nextId;
            _tasks.Add(item);
            SaveToFile();
        }

        public bool Update(TaskItem updatedItem)
        {
            TaskItem? existing = _tasks.FirstOrDefault(t => t.Id == updatedItem.Id);
            if (existing == null)
            {
                return false;
            }

            existing.Title = updatedItem.Title;
            existing.IsDone = updatedItem.IsDone;
            SaveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            TaskItem? existing = _tasks.FirstOrDefault(t => t.Id == id);
            if (existing == null)
            {
                return false;
            }

            _tasks.Remove(existing);
            SaveToFile();
            return true;
        }

        private List<TaskItem> LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<TaskItem>();
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch
            {
                return new List<TaskItem>();
            }
        }

        private void SaveToFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_tasks, options);
            File.WriteAllText(_filePath, json);
        }
    }

    public class TaskService
    {
        private readonly TaskRepository _repository;

        public TaskService(TaskRepository repository)
        {
            _repository = repository;
        }

        public void CreateTask(string title)
        {
            TaskItem item = new TaskItem
            {
                Title = title,
                IsDone = false
            };
            _repository.Add(item);
        }

        public List<TaskItem> GetAllTasks()
        {
            return _repository.GetAll();
        }

        public bool MarkAsDone(int id)
        {
            TaskItem? item = _repository.GetAll().FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return false;
            }

            item.IsDone = true;
            return _repository.Update(item);
        }

        public bool RemoveTask(int id)
        {
            return _repository.Delete(id);
        }
    }
}
