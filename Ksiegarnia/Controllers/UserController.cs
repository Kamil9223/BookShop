using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.Models;
using Ksiegarnia.IServices;
using Ksiegarnia.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

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

        [HttpGet("[action]")]
        public IActionResult Test()
        {
            return new JsonResult("Sample text!");
        }
    }
}