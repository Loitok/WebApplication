using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApplication.DAL.AuthorsData;
using WebApplication.DAL.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorsData _authorsData;
        public AuthorController(IAuthorsData authorsData)
        {
            _authorsData = authorsData;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            return Ok(_authorsData.GetAuthors());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAuthor(int id)
        {
            var author = _authorsData.GetAuthor(id);

            if (author != null)
            {
                return Ok(author);
            }

            return NotFound($"Author not found! Id : {id}");
        }

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            _authorsData.AddAuthor(author);
            return Created(
                HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" +
                author.Id,
                author);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _authorsData.GetAuthor(id);

            if (author != null)
            {
                _authorsData.DeleteAuthor(author);
                return Ok();
            }

            return NotFound("Author was not found");
        }

        [HttpPatch("{id:int}")]
        public IActionResult EditAuthor(int id, Author author)
        {
            var existingAuthor = _authorsData.GetAuthor(id);
            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                existingAuthor.BirthDate = author.BirthDate;
                existingAuthor.Books = author.Books;
                _authorsData.EditAuthor(author);
            }
            return Ok(author);
        }
    }
}
