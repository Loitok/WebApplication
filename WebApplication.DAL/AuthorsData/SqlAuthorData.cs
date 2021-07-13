using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.BLL.ResultConstants;
using WebApplication.BLL.ResultModel;
using WebApplication.BLL.ResultModel.Generic;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.AuthorsData
{
    public class SqlAuthorData : IAuthorsData
    {
        private readonly ApplicationContext _applicationContext;

        public SqlAuthorData(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IResult<List<Author>>> GetAuthors()
        {
            try
            {
                return Result<List<Author>>.CreateSuccess(_applicationContext.Authors.ToList());
            }
            catch (Exception e)
            {
                return Result<List<Author>>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult<Author>> GetAuthor(int id)
        {
            try
            {
                var author = _applicationContext.Authors.Find(id);
                return Result<Author>.CreateSuccess(author);
            }
            catch (Exception e)
            {
                return Result<Author>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
        public async Task<IResult<Author>> AddAuthor(Author author)
        {
            try
            {
                _applicationContext.Authors.Add(author);
                _applicationContext.SaveChanges();
                return Result<Author>.CreateSuccess(author);
            }
            catch (Exception e)
            {
                return Result<Author>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult> DeleteAuthor(int id)
        {
            try
            {
                var author = _applicationContext.Authors.Find(id);
                _applicationContext.Authors.Remove(author);
                _applicationContext.SaveChanges();
                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.DeleteFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult<Author>> EditAuthor(int id, Author author)
        {
            try
            {
                var existingAuthor = _applicationContext.Authors.Find(id);

                existingAuthor.Name = author.Name;
                existingAuthor.BirthDate = author.BirthDate;
                existingAuthor.Books = author.Books;

                _applicationContext.Authors.Update(existingAuthor);
                _applicationContext.SaveChanges();

                return Result<Author>.CreateSuccess(author);
            }
            catch (Exception e)
            {
                return Result<Author>.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }
    }
}
