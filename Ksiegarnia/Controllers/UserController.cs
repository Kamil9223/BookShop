using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IServices;
using Ksiegarnia.DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Commands;
using Ksiegarnia.Responses;

namespace Ksiegarnia.Controllers
{
    [Route("api")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;

        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterCommand command)
        {
            if (command.Address.City == null && command.Address.Street == null && command.Address.ZipCode == null)
                command.Address = null;

            userService.Register(command.Login, command.Password, command.Email, command.Address);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCommand command)
        {
            var authResult = userService.Login(command.Login, command.Password);
            var authResponse = new AuthenticationResponse
            {
                JwtToken = authResult.JwtToken,
                RefreshToken = authResult.RefreshToken.ToString()
            };
            return new JsonResult(authResponse);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"];
            userService.Logout();
            return new JsonResult(token);
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshConnectionCommand command)
        {
            var authResult = userService.RefreshConnection(command.JwtToken, command.RefreshToken);
            var authResponse = new AuthenticationResponse
            {
                JwtToken = authResult.JwtToken,
                RefreshToken = authResult.RefreshToken.ToString()
            };
            return new JsonResult(authResponse);
        }
    }
}