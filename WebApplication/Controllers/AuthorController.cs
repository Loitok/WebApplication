using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WebApplication.DAL.AuthorsData;
using WebApplication.DAL.Models;
using WebApplication.Extensions;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthors() =>
            (await _authorsData.GetAuthors()).ToActionResult();


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthor(int id) =>
            (await _authorsData.GetAuthor(id)).ToActionResult();


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAuthor([FromBody] Author author) =>
            (await _authorsData.AddAuthor(author)).ToActionResult();


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAuthor(int id) =>
            (await _authorsData.DeleteAuthor(id)).ToActionResult();
        

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EditAuthor([FromBody] int id, Author author) 
        => (await _authorsData.EditAuthor(id, author)).ToActionResult();
    }
}
