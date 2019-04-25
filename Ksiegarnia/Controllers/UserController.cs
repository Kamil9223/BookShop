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

namespace Ksiegarnia.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IJwtService jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            this.userService = userService;
            this.jwtService = jwtService;
        }

        [HttpGet("[action]")]
        public IActionResult Token()
        {
            var token = jwtService.CreateToken("Kamil", "user");
            return new JsonResult(token);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Test1()
        {
            return new JsonResult("Success! You got access to authorized test method!");
        }

        [HttpGet("[action]")]
        public IActionResult Test2()
        {
            return new JsonResult("Unauthorized method.");
        }
    }
}