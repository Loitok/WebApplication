using System.Collections.Generic;
using System.Linq;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.AuthorsData
{
    public class SqlAuthorData : IAuthorsData
    {
        private ApplicationContext _applicationContext;

        public SqlAuthorData(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Author AddAuthor(Author author)
        {
            _applicationContext.Authors.Add(author);
            _applicationContext.SaveChanges();
            return author;
        }

        public void DeleteAuthor(Author author)
        {
            _applicationContext.Authors.Remove(author);
            _applicationContext.SaveChanges();
        }

        public Author EditAuthor(Author author)
        {
            var existingAuthor = _applicationContext.Authors.Find(author.Id);
            if (existingAuthor != null)
            {
                _applicationContext.Authors.Update(author);
                _applicationContext.SaveChanges();
            }

            return author;
        }

        public Author GetAuthor(int id)
        {
            var author = _applicationContext.Authors.Find(id);
            return author;
        }

        public List<Author> GetAuthors()
        {
            return _applicationContext.Authors.ToList();
        }
    }
}
