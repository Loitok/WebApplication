using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.BLL.ResultModel;
using WebApplication.BLL.ResultModel.Generic;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.BooksData
{
    public interface IBooksData
    {
        Task<IResult<List<Book>>> GetBooks();
        Task<IResult<Book>> GetBook(int id);
        Task<IResult<List<Book>>> GetBookByName(string name);
        Task<IResult<List<Book>>> GetBookByAuthor(string name);
        Task<IResult<Book>> AddBook(Book book);
        Task<IResult> DeleteBook(int id);
        Task<IResult<Book>> EditBook(int id, Book book);
    }
}
