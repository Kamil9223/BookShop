﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using AuthService.Services.Interfaces;
using OrderService.OrderServices.Interfaces;
using API.Requests.OrderRequests;
using OrderService.DTO;

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

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var login = jwtHelper.GetClaimsFromToken()
                .Claims.Single(x => x.Type == "login")
                .Value;

            await cart.AddPositionToCart(login, request.BookId);

            return Created("http://localhost:49194/api/Cart", 
                new CreatedResponse { Message = "Resource added propperly." });
        }

        [HttpDelete("{bookId}")]
        public IActionResult RemoveFromCart(Guid bookId)
        {
            var login = jwtHelper.GetClaimsFromToken()
                .Claims.Single(x => x.Type == "login")
                .Value;

            cart.RemovePositionFromCart(login, bookId);

            return NoContent();
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