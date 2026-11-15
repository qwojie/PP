using CSharpPracticum.Tasks.Level3;

using System;
using System.Collections.Generic;

using Xunit;

namespace CSharpPracticum.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void AddBook_ShouldAddBookToLibrary()
        {
            Library library = new Library();
            Book book = new Book { Id = 1, Title = "1984", Author = "Джордж Оруэлл" };

            library.AddBook(book);
            List<Book> books = library.GetAllBooks();

            Assert.Single(books);
            Assert.Equal("1984", books[0].Title);
        }

        [Fact]
        public void BorrowBook_WhenAvailable_ShouldMarkBookAsUnavailableAndAddToReader()
        {
            Library library = new Library();
            Book book = new Book { Id = 1, Title = "Дюна", Author = "Фрэнк Герберт" };
            Reader reader = new Reader { Id = 1, Name = "Иван" };
            library.AddBook(book);
            library.RegisterReader(reader);

            library.BorrowBook(1, 1);

            Assert.False(book.IsAvailable);
            Assert.Single(reader.BorrowedBooks);
        }

        [Fact]
        public void BorrowBook_WhenBookAlreadyBorrowed_ShouldThrowException()
        {
            Library library = new Library();
            library.AddBook(new Book { Id = 1, Title = "Дюна", Author = "Фрэнк Герберт" });
            library.RegisterReader(new Reader { Id = 1, Name = "Иван" });
            library.RegisterReader(new Reader { Id = 2, Name = "Анна" });
            library.BorrowBook(1, 1);

            Assert.Throws<InvalidOperationException>(() => library.BorrowBook(2, 1));
        }

        [Fact]
        public void BorrowBook_WhenReaderReachedMaxLimit_ShouldThrowException()
        {
            Library library = new Library();
            library.RegisterReader(new Reader { Id = 1, Name = "Иван" });
            library.AddBook(new Book { Id = 1, Title = "Книга 1", Author = "Автор" });
            library.AddBook(new Book { Id = 2, Title = "Книга 2", Author = "Автор" });
            library.AddBook(new Book { Id = 3, Title = "Книга 3", Author = "Автор" });
            library.AddBook(new Book { Id = 4, Title = "Книга 4", Author = "Автор" });

            library.BorrowBook(1, 1);
            library.BorrowBook(1, 2);
            library.BorrowBook(1, 3);

            Assert.Throws<InvalidOperationException>(() => library.BorrowBook(1, 4));
        }

        [Fact]
        public void ReturnBook_WhenValid_ShouldMakeBookAvailableAgain()
        {
            Library library = new Library();
            Book book = new Book { Id = 1, Title = "Солярис", Author = "Станислав Лем" };
            Reader reader = new Reader { Id = 1, Name = "Иван" };
            library.AddBook(book);
            library.RegisterReader(reader);
            library.BorrowBook(1, 1);

            library.ReturnBook(1, 1);

            Assert.True(book.IsAvailable);
            Assert.Empty(reader.BorrowedBooks);
        }

        [Fact]
        public void ReturnBook_WhenReaderDoesNotHaveBook_ShouldThrowException()
        {
            Library library = new Library();
            library.AddBook(new Book { Id = 1, Title = "Солярис", Author = "Станислав Лем" });
            library.RegisterReader(new Reader { Id = 1, Name = "Иван" });

            Assert.Throws<InvalidOperationException>(() => library.ReturnBook(1, 1));
        }

        [Fact]
        public void SearchBooks_ByTitleOrAuthor_ShouldReturnMatchingBooksCaseInsensitive()
        {
            Library library = new Library();
            library.AddBook(new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой" });
            library.AddBook(new Book { Id = 2, Title = "Анна Каренина", Author = "Лев Толстой" });
            library.AddBook(new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков" });

            List<Book> result = library.SearchBooks("толстой");

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void BorrowBook_WhenReaderOrBookDoesNotExist_ShouldThrowException()
        {
            Library library = new Library();
            library.AddBook(new Book { Id = 1, Title = "Книга 1", Author = "Автор" });
            library.RegisterReader(new Reader { Id = 1, Name = "Иван" });

            Assert.Throws<InvalidOperationException>(() => library.BorrowBook(99, 1));
            Assert.Throws<InvalidOperationException>(() => library.BorrowBook(1, 99));
        }
    }
}
