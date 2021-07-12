using System.Collections.Generic;
using System.Linq;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.BooksData
{
    public class BookData : IBooksData

    {
        private List<Author> authors = new List<Author>();
        private List<Book> books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Description = "_",
                Name = "sd;l,s",
                IssueYear = 2012,
                PageNumber = 121,
                Authors = null,
            }
        };
        public List<Book> GetBooks()
        {
            return books;
        }

        public Book GetBook(int id)
        {
            return books.SingleOrDefault(x => x.Id == id);
        }

        public List<Book> GetBookByName(string name)
        {
            return (List<Book>) books.Where(p => p.Name.Contains(name));
        }

        public List<Book> GetBookByAuthor(string name)
        {
            var books = new List<Book>();
            var existingAuthors = authors.Where(p => p.Name.Contains(name));

            foreach (var author in existingAuthors)
            {
                foreach (var book in author.Books)
                {
                    books.Add(book);
                }
            }

            return this.books;
        }

        public Book AddBook(Book book)
        {
            books.Add(book);
            return book;
        }

        public void DeleteBook(Book book)
        {
            books.Remove(book);
        }

        public Book EditBook(Book book)
        {
            var existingBook = GetBook(book.Id);
            existingBook.Name = book.Name;
            existingBook.Authors = book.Authors;
            existingBook.Description = book.Description;
            existingBook.IssueYear = book.IssueYear;
            existingBook.PageNumber = book.PageNumber;

            return existingBook;
        }
    }
}
