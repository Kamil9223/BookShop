using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Ksiegarnia.Contracts.Responses;
using Ksiegarnia.Contracts.Requests;

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
        public async Task<IActionResult> Register([FromBody] RegisterRequest command)
        {
            if (command.Address.City == null && command.Address.Street == null && command.Address.ZipCode == null)
                command.Address = null;

            await userService.Register(command.Login, command.Password, command.Email, command.Address);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest command)
        {
            var authResult = await userService.Login(command.Login, command.Password);
            var authResponse = new AuthenticationResponse
            {
                JwtToken = authResult.JwtToken,
                RefreshToken = authResult.RefreshToken.ToString()
            };
            return new JsonResult(authResponse);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"];
            await userService.Logout();
            return new JsonResult(token);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshConnectionRequest command)
        {
            var authResult = await userService.RefreshConnection(command.JwtToken, command.RefreshToken);
            var authResponse = new AuthenticationResponse
            {
                JwtToken = authResult.JwtToken,
                RefreshToken = authResult.RefreshToken.ToString()
            };
            return new JsonResult(authResponse);
        }
    }
}