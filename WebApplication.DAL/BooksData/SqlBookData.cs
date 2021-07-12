using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication.DAL;
using WebApplication.DAL.BooksData;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.BooksData
{
    public class SqlBookData : IBooksData
    {
        private ApplicationContext _applicationContext;
        private List<Book> books;

        public SqlBookData(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        
        public Book AddBook(Book book)
        {
            _applicationContext.Books.Add(book);
            _applicationContext.SaveChanges();
            return book; 
        }

        public void DeleteBook(Book book)
        {
            _applicationContext.Books.Remove(book);
            _applicationContext.SaveChanges();
        }

        public Book EditBook(Book book)
        {
            var existingBook = _applicationContext.Books.Find(book.Id);
            if (existingBook != null)
            {
                _applicationContext.Books.Update(book);
                _applicationContext.SaveChanges();
            }

            return book;
        }

        public Book GetBook(int id)
        {
            var book = _applicationContext.Books.Find(id);
            return book;
        }

        public List<Book> GetBooks()
        {
            return _applicationContext.Books.ToList();
        }

        public List<Book> GetBookByName(string name)
        {
            var books = _applicationContext
                .Books
                .Where(p=> EF.Functions.Like(p.Name, $"%{name}%"))
                .ToList();
            return books;
        }

        public List<Book> GetBookByAuthor(string name)
        {
            books = new List<Book>();
            var authors = _applicationContext
                .Authors
                .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
                .ToList();

            foreach (var author in authors)
            {
                foreach (var book in author.Books)
                {
                    books.Add(book);
                }
            }

            return this.books;
        }
    }
}
