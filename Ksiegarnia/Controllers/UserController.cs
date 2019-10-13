using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IServices;
using Ksiegarnia.DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Commands;

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

        [HttpPost("/Register")]
        public IActionResult Register([FromBody] RegisterCommand command)
        {
            if (command.Address.City == null && command.Address.Street == null && command.Address.ZipCode == null)
                command.Address = null;

            userService.Register(command.Login, command.Password, command.Email, command.Address);
            return Ok();
        }

        [HttpPost("/Login")]
        public IActionResult Login([FromBody] LoginCommand command)
        {
            userService.Login(command.Login, command.Password);
            var jwt = memoryCache.Get(command.Login);

            return new JsonResult(jwt);
        }

        [Authorize]
        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"];
            userService.Logout();
            return new JsonResult(token);
        }
    }
}