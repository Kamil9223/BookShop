using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Ksiegarnia.Models.Internet_Cart;

namespace Ksiegarnia.Controllers
{
    [Route("api/Order")]
    public class OrderController : Controller
    { 
        private readonly Cart cart;

        public OrderController(Cart cart)
        {
            this.cart = cart;
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult AddToCart(Guid id)
        {
            //Tylko testowo
            foreach(string token in JwtService.BlackList)
            {
                if (Request.Headers["Authorization"] == token)
                    throw new UnauthorizedAccessException();
            }

            var sessionKey = Request.Headers["login"];
            if ((string)sessionKey == null)
                return BadRequest();

            var session = HttpContext.Session;
            cart.AddPositionToCart(session,sessionKey, id);

            var result = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString(sessionKey));
            return new JsonResult(result);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult RemoveFromCart(Guid id)
        {
            var sessionKey = Request.Headers["login"];
            if ((string)sessionKey == null)
                return BadRequest();

            var session = HttpContext.Session;
            cart.RemovePositionFromCart(session, sessionKey, id);
            List<CartPosition> result = null;
            try
            {
                result = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString(sessionKey));
            }
            catch(ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            return new JsonResult(result);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult ShowCart()
        {
            var sessionKey = Request.Headers["login"];
            if ((string)sessionKey == null)
                return BadRequest();

            List<CartPosition> result = null;
            try
            {
                result = JsonConvert.DeserializeObject<List<CartPosition>>(HttpContext.Session.GetString(sessionKey));
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            return new JsonResult(result);
        }
    }
}