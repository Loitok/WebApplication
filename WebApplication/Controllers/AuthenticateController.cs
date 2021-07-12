using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication.BLL.Authentication;
using WebApplication.BLL.Services;
using WebApplication.BLL.ResultModel;

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
        public async Task<IResult> Login([FromBody] LoginModel model) =>
            (await _authService.Login(model));


        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> Register([FromBody] RegisterModel model) =>
            (await _authService.Register(model));


        [HttpPost("register-admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IResult> RegisterAdmin([FromBody] RegisterModel model) =>
            (await _authService.RegisterAdmin(model));
    }
}