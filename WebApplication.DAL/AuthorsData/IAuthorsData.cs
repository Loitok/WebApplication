using System.Collections.Generic;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.AuthorsData
{
    public interface IAuthorsData
    {
        List<Author> GetAuthors();
        Author GetAuthor(int id);
        Author AddAuthor(Author author);
        void DeleteAuthor(Author author);
        Author EditAuthor(int id, Author author);
    }
}
