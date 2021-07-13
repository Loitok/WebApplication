using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.BLL.ResultConstants;
using WebApplication.BLL.ResultModel;
using WebApplication.BLL.ResultModel.Generic;
using WebApplication.DAL;
using WebApplication.DAL.BooksData;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.BooksData
{
    public class SqlBookData : IBooksData
    {
        private readonly ApplicationContext _applicationContext;
        private List<Book> books;

        public SqlBookData(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IResult<List<Book>>> GetBooks()
        {
            try
            {
                return Result<List<Book>>.CreateSuccess(_applicationContext.Books.ToList());
            }
            catch (Exception e)
            {
                return Result<List<Book>>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult<Book>> GetBook(int id)
        {
            try
            {
                var book = _applicationContext.Books.Find(id);
                return Result<Book>.CreateSuccess(book);
            }
            catch (Exception e)
            {
                return Result<Book>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult<List<Book>>> GetBookByName(string name)
        {
            try
            {
                var books = _applicationContext
                    .Books
                    .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
                    .ToList();
                return Result<List<Book>>.CreateSuccess(books);
            }
            catch (Exception e)
            {
                return Result<List<Book>>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult<List<Book>>> GetBookByAuthor(string name)
        {
            try
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

                return Result<List<Book>>.CreateSuccess(books);
            }
            catch (Exception e)
            {
                return Result<List<Book>>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult<Book>> AddBook(Book book)
        {
            try
            {
                _applicationContext.Books.Add(book);
                _applicationContext.SaveChanges();
                return Result<Book>.CreateSuccess(book);
            }
            catch (Exception e)
            {
                return Result<Book>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult> DeleteBook(int id)
        {
            try
            {
                var book = _applicationContext.Books.Find(id);
                _applicationContext.Books.Remove(book);
                _applicationContext.SaveChanges();
                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.DeleteFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult<Book>> EditBook(int id, Book book)
        {
            try
            {
                var existingBook = _applicationContext.Books.Find(id);

                existingBook.Name = book.Name;
                existingBook.Description = book.Description;
                existingBook.IssueYear = book.IssueYear;
                existingBook.PageNumber = book.PageNumber;
                existingBook.Authors = book.Authors;

                _applicationContext.Books.Update(book);
                _applicationContext.SaveChanges();

                return Result<Book>.CreateSuccess(book);
            }
            catch (Exception e)
            {
                return Result<Book>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
    }
}
