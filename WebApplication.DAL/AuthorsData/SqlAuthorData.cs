using System.Collections.Generic;
using System.Linq;
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

        public Author EditAuthor(int id, Author author)
        {
            var existingAuthor = _applicationContext.Authors.Find(id);

            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                existingAuthor.BirthDate = author.BirthDate;
                existingAuthor.Books = author.Books;

                _applicationContext.Authors.Update(existingAuthor);
                _applicationContext.SaveChanges();
            }

            return AddAuthor(author);
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
