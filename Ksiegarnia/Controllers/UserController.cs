using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Models;

namespace Ksiegarnia.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("[action]")]
        public Address Test()
        {
            var user = userRepository.GetUser("kamil9223@vp.pl");
            var address = userRepository.GetAddress(user.UserId);
            return address;
        }
    }
}