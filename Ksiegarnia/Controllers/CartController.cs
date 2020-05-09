using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Ksiegarnia.Models.Internet_Cart;
using Ksiegarnia.IServices;
using System.Threading.Tasks;
using System.Linq;

namespace Ksiegarnia.Controllers
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

            var session = HttpContext.Session;
            await cart.AddPositionToCart(session, login, bookId);

            var result = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString(login));
            return new JsonResult(result);
        }

        [HttpDelete("{bookId}")]
        public IActionResult RemoveFromCart(Guid bookId)
        {
            var login = jwtHelper.GetClaimsFromToken()
                .Claims.Single(x => x.Type == "login")
                .Value;

            var session = HttpContext.Session;
            cart.RemovePositionFromCart(session, login, bookId);
            try
            {
                List<CartPosition> result = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString(login));
                return new JsonResult(result);
            }
            catch(ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public IActionResult ShowCart()
        {
            var login = jwtHelper.GetClaimsFromToken()
                .Claims.Single(x => x.Type == "login")
                .Value;

            try
            {
                List<CartPosition> result = JsonConvert.DeserializeObject<List<CartPosition>>(HttpContext.Session.GetString(login));
                return new JsonResult(result);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}