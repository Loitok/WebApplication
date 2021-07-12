using System.Collections.Generic;
using System.Linq;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.AuthorsData
{
    public class AuthorData : IAuthorsData
    {
        private List<Author> authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "sd;l,s",
            }
        };
        public List<Author> GetAuthors()
        {
            return authors;
        }

        public Author GetAuthor(int id)
        {
            return authors.SingleOrDefault(x => x.Id == id);
        }

        public Author AddAuthor(Author author)
        {
            authors.Add(author);
            return author;
        }

        public void DeleteAuthor(Author author)
        {
            authors.Remove(author);
        }

        public Author EditAuthor(Author author)
        {
            var existingAuthor = GetAuthor(author.Id);
            existingAuthor.Name = author.Name;
            existingAuthor.BirthDate = author.BirthDate;
            existingAuthor.Books = author.Books;

            return existingAuthor;
        }
    }
}
