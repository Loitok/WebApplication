using System.Collections.Generic;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.BooksData
{
    public interface IBooksData
    {
        List<Book> GetBooks();
        Book GetBook(int id);
        List<Book> GetBookByName(string name);
        List<Book> GetBookByAuthor(string name);
        Book AddBook(Book book);
        void DeleteBook(Book book);
        Book EditBook(Book book);
    }
}
