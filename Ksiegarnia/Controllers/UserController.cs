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
            AddressDTO address = new AddressDTO()
            {
                City = "Pliskowola",
                Street = "Długa",
                HouseNumber = "114b",
            };
            userService.Register("Junker2", "xd", "asda", address);
            var user = userService.Get("Testowy");
            return new JsonResult(user);
        }
    }
}