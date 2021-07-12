using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DAL.BooksData;
using WebApplication.DAL.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBooksData _booksData;

        public BookController(IBooksData booksData)
        {
            _booksData = booksData;
        }

        [HttpGet]
        public IActionResult GetBooksByName([FromQuery] string? name)
        {
            var book = name is null ? _booksData.GetBooks() : _booksData.GetBookByName(name);
            
            if (book != null)
            {
                return Ok(book);
            }

            return NotFound($"Book not found! Name : {name}");
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook(int id)
        {
            var book = _booksData.GetBook(id);

            if (book != null)
            {
                return Ok(book);
            }

            return NotFound($"Book not found! Id : {id}");
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _booksData.AddBook(book);
            return Created(
                HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" +
                book.Id,
                book);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _booksData.GetBook(id);

            if (book != null)
            {
                _booksData.DeleteBook(book);
                return Ok();
            }

            return NotFound("Book was not found");
        }

        [HttpPatch("{id:int}")]
        public IActionResult EditBook(int id, Book book) 
            => (Ok(_booksData.EditBook(id, book)));
        
    }
}
