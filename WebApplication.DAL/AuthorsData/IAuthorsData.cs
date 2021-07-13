using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.BLL.ResultModel;
using WebApplication.BLL.ResultModel.Generic;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.AuthorsData
{
    public interface IAuthorsData
    {
        Task<IResult<List<Author>>> GetAuthors();
        Task<IResult<Author>> GetAuthor(int id);
        Task<IResult<Author>> AddAuthor(Author author);
        Task<IResult> DeleteAuthor(int id);
        Task<IResult<Author>> EditAuthor(int id, Author author);
    }
}
