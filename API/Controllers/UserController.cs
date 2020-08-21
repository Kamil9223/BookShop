using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using AuthService.Services.Interfaces;
using API.Requests.AuthRequests;
using AuthService.DTO;

namespace API.Controllers
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
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request.Address?.City == null && request.Address?.Street == null && request.Address?.ZipCode == null)
                request.Address = null;

            var addressInfo = request.Address == null ? null : new AddressInformations
            {
                City = request.Address.City,
                FlatNumber = request.Address.FlatNumber,
                HouseNumber = request.Address.HouseNumber,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode
            };

            await userService.Register(request.Login, request.Password, request.Email, addressInfo);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var authResult = await userService.Login(request.Login, request.Password);
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
        public async Task<IActionResult> RefreshToken([FromBody] RefreshConnectionRequest request)
        {
            try
            {
                var authResult = await userService.RefreshConnection(request.JwtToken, request.RefreshToken);
                var authResponse = new AuthenticationResponse
                {
                    JwtToken = authResult.JwtToken,
                    RefreshToken = authResult.RefreshToken.ToString()
                };
                return new JsonResult(authResponse);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpGet("user/{login}/get")]
        public async Task<IActionResult> GetUser(string login)
        {
            var userResponse = await userService.Get(login);
            return new JsonResult(userResponse);
        }
    }
}