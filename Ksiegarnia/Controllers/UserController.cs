using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.Models;
using Ksiegarnia.IServices;
using Ksiegarnia.DTO;

namespace Ksiegarnia.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("[action]")]
        public IActionResult Test()
        {
            userService.Register("Kamil", "ss", "sds");
            return new JsonResult("OK");
        }
    }
}