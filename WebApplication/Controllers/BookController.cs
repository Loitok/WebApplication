using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DAL.BooksData;
using WebApplication.DAL.Models;
using WebApplication.Extensions;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooks() =>
            (await _booksData.GetBooks()).ToActionResult();


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBook(int id) =>
            (await _booksData.GetBook(id)).ToActionResult();


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookByName([FromQuery] string? name) =>
            (await _booksData.GetBookByName(name)).ToActionResult();


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookByAuthor([FromQuery] string? name) =>
            (await _booksData.GetBookByAuthor(name)).ToActionResult();


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBook([FromBody] Book book) =>
            (await _booksData.AddBook(book)).ToActionResult();


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBook(int id) =>
            (await _booksData.DeleteBook(id)).ToActionResult();


        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditBook([FromBody] int id, Book book)
            => (await _booksData.EditBook(id, book)).ToActionResult();
    }
}
