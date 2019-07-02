using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IServices;
using Ksiegarnia.DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace Ksiegarnia.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;

        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        [HttpPost("[action]")]
        public IActionResult Register(string login, string password, string email, AddressDTO address)
        {
            if (address.City == null && address.Street == null && address.ZipCode == null)
                address = null;

            userService.Register(login, password, email, address);
            return new JsonResult(StatusCode(200));
        }

        [HttpPost("[action]")]
        public IActionResult Login(string login, string password)
        {
            userService.Login(login, password);
            var jwt = memoryCache.Get(login);

            return new JsonResult(jwt);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"];
            userService.Logout(token);
            return new JsonResult(token);
        }
    }
}