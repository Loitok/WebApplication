using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication.BLL.Authentication;
using WebApplication.BLL.Services;
using WebApplication.Extensions;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticateController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginModel model) =>
            (await _authService.Login(model)).ToActionResult();


        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model) =>
            (await _authService.Register(model)).ToActionResult();


        [HttpPost("register-admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model) =>
            (await _authService.RegisterAdmin(model)).ToActionResult();
    }
}