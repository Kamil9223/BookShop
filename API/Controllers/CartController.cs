using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using Infrastructure.IServices;
using Core.Models;

namespace API.Controllers
{
    [Authorize]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICart cart;
        private readonly IJwtHelper jwtHelper;

        public CartController(ICart cart, IJwtHelper jwtHelper)
        {
            this.cart = cart;
            this.jwtHelper = jwtHelper;
        }

        [HttpPost("{bookId}")]
        public async Task<IActionResult> AddToCart(Guid bookId)
        {
            var login = jwtHelper.GetClaimsFromToken()
                .Claims.Single(x => x.Type == "login")
                .Value;

            await cart.AddPositionToCart(login, bookId);

            var result = cart.GetCart(login);
            return new JsonResult(result);
        }

        [HttpDelete("{bookId}")]
        public IActionResult RemoveFromCart(Guid bookId)
        {
            var login = jwtHelper.GetClaimsFromToken()
                .Claims.Single(x => x.Type == "login")
                .Value;

            cart.RemovePositionFromCart(login, bookId);
            var response = cart.GetCart(login);

            return new JsonResult(response);
        }

        [HttpDelete]
        public IActionResult ClearCart()
        {
            var login = jwtHelper.GetClaimsFromToken()
                .Claims.Single(x => x.Type == "login")
                .Value;

            cart.ClearCart(login);
            var response = cart.GetCart(login);

            return new JsonResult(response);
        }

        [HttpGet]
        public IActionResult ShowCart()
        {
            var login = jwtHelper.GetClaimsFromToken()
                .Claims.Single(x => x.Type == "login")
                .Value;

            var response = cart.GetCart(login);

            return new JsonResult(response);
        }
    }
}