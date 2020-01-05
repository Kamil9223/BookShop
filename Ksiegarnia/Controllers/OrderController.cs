using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Ksiegarnia.Models.Internet_Cart;
using Ksiegarnia.IServices;
using System.Threading.Tasks;

namespace Ksiegarnia.Controllers
{
    [Authorize]
    [Route("api")]
    public class OrderController : Controller
    { 
        private readonly ICart cart;

        public OrderController(ICart cart)
        {
            this.cart = cart;
        }

        [HttpPost("Cart/{id}")]
        public async Task<IActionResult> AddToCart(Guid id)
        {
            var sessionKey = Request.Headers["Login"];
            if ((string)sessionKey == null)
                return BadRequest();

            var session = HttpContext.Session;
            await cart.AddPositionToCart(session, sessionKey, id);

            var result = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString(sessionKey));
            return new JsonResult(result);
        }

        [HttpDelete("Cart/{id}")]
        public IActionResult RemoveFromCart(Guid id)
        {
            var sessionKey = Request.Headers["Login"];
            if ((string)sessionKey == null)
                return BadRequest();

            var session = HttpContext.Session;
            cart.RemovePositionFromCart(session, sessionKey, id);
            try
            {
                List<CartPosition> result = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString(sessionKey));
                return new JsonResult(result);
            }
            catch(ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("Cart")]
        public IActionResult ShowCart()
        {
            var sessionKey = Request.Headers["Login"];
            if ((string)sessionKey == null)
                return BadRequest();

            try
            {
                List<CartPosition> result = JsonConvert.DeserializeObject<List<CartPosition>>(HttpContext.Session.GetString(sessionKey));
                return new JsonResult(result);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}