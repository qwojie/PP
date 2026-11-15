using System;
using System.Collections.Generic;
using System.Linq;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class Task45_Library : ITask
    {
        public string Title => "Задание 45. Мини-проект «Библиотека» + юнит-тесты";
        public string Description => "Система управления библиотекой с выдачей книг и проверкой бизнес-правил.";
        public string[] Requirements => new string[]
        {
            "Классы: Book (Id, Title, Author, IsAvailable), Reader (Id, Name, BorrowedBooks), Library.",
            "Методы: AddBook, RegisterReader, BorrowBook, ReturnBook, SearchBooks.",
            "Правила: нельзя выдать занятую книгу, один читатель — не более 3 книг.",
            "Отдельный проект тестов (xUnit): минимум 6–8 тестов на основные и граничные случаи."
        };

        public void Run()
        {
            Library library = new Library();

            library.AddBook(new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой" });
            library.AddBook(new Book { Id = 2, Title = "Преступление и наказание", Author = "Фёдор Достоевский" });
            library.AddBook(new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков" });
            library.AddBook(new Book { Id = 4, Title = "Идиот", Author = "Фёдор Достоевский" });

            library.RegisterReader(new Reader { Id = 1, Name = "Алексей" });
            library.RegisterReader(new Reader { Id = 2, Name = "Мария" });

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("--- Меню Библиотеки ---");
                Console.WriteLine("1. Показать все книги");
                Console.WriteLine("2. Поиск книг (по названию или автору)");
                Console.WriteLine("3. Выдать книгу читателю");
                Console.WriteLine("4. Вернуть книгу");
                Console.WriteLine("5. Показать читателей и их книги");
                Console.WriteLine("0. Выйти из библиотеки");
                Console.Write("Выберите действие: ");

                string? choice = Console.ReadLine();

                if (choice == "0")
                {
                    break;
                }

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowBooks(library.GetAllBooks());
                            break;
                        case "2":
                            Console.Write("Введите запрос для поиска: ");
                            string query = Console.ReadLine() ?? string.Empty;
                            ShowBooks(library.SearchBooks(query));
                            break;
                        case "3":
                            Console.Write("Введите Id читателя: ");
                            int readerIdBorrow = int.Parse(Console.ReadLine() ?? "0");
                            Console.Write("Введите Id книги: ");
                            int bookIdBorrow = int.Parse(Console.ReadLine() ?? "0");
                            library.BorrowBook(readerIdBorrow, bookIdBorrow);
                            Console.WriteLine("Книга успешно выдана.");
                            break;
                        case "4":
                            Console.Write("Введите Id читателя: ");
                            int readerIdReturn = int.Parse(Console.ReadLine() ?? "0");
                            Console.Write("Введите Id книги: ");
                            int bookIdReturn = int.Parse(Console.ReadLine() ?? "0");
                            library.ReturnBook(readerIdReturn, bookIdReturn);
                            Console.WriteLine("Книга успешно возвращена.");
                            break;
                        case "5":
                            ShowReaders(library.GetAllReaders());
                            break;
                        default:
                            Console.WriteLine("Некорректный выбор.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private void ShowBooks(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Книги не найдены.");
                return;
            }

            foreach (Book book in books)
            {
                string status = book.IsAvailable ? "Доступна" : "Выдана";
                Console.WriteLine($"[{book.Id}] «{book.Title}» — {book.Author} ({status})");
            }
        }

        private void ShowReaders(List<Reader> readers)
        {
            foreach (Reader reader in readers)
            {
                Console.WriteLine($"Читатель [{reader.Id}] {reader.Name} (книг на руках: {reader.BorrowedBooks.Count}/3):");
                foreach (Book book in reader.BorrowedBooks)
                {
                    Console.WriteLine($"   - [{book.Id}] «{book.Title}»");
                }
            }
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }

    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Book> BorrowedBooks { get; set; } = new List<Book>();
    }

    public class Library
    {
        private readonly List<Book> _books = new List<Book>();
        private readonly List<Reader> _readers = new List<Reader>();
        public const int MaxBooksPerReader = 3;

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (_books.Any(b => b.Id == book.Id))
            {
                throw new InvalidOperationException($"Книга с Id {book.Id} уже существует.");
            }

            _books.Add(book);
        }

        public void RegisterReader(Reader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (_readers.Any(r => r.Id == reader.Id))
            {
                throw new InvalidOperationException($"Читатель с Id {reader.Id} уже зарегистрирован.");
            }

            _readers.Add(reader);
        }

        public void BorrowBook(int readerId, int bookId)
        {
            Reader? reader = _readers.FirstOrDefault(r => r.Id == readerId);
            if (reader == null)
            {
                throw new InvalidOperationException("Читатель не найден.");
            }

            Book? book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                throw new InvalidOperationException("Книга не найдена.");
            }

            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("Книга уже выдана другому читателю.");
            }

            if (reader.BorrowedBooks.Count >= MaxBooksPerReader)
            {
                throw new InvalidOperationException($"Читатель не может взять более {MaxBooksPerReader} книг.");
            }

            book.IsAvailable = false;
            reader.BorrowedBooks.Add(book);
        }

        public void ReturnBook(int readerId, int bookId)
        {
            Reader? reader = _readers.FirstOrDefault(r => r.Id == readerId);
            if (reader == null)
            {
                throw new InvalidOperationException("Читатель не найден.");
            }

            Book? book = reader.BorrowedBooks.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                throw new InvalidOperationException("У данного читателя нет этой книги.");
            }

            book.IsAvailable = true;
            reader.BorrowedBooks.Remove(book);
        }

        public List<Book> SearchBooks(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Book>(_books);
            }

            return _books
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Book> GetAllBooks()
        {
            return new List<Book>(_books);
        }

        public List<Reader> GetAllReaders()
        {
            return new List<Reader>(_readers);
        }
    }
}
