using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.Models.Internet_Cart;
using Ksiegarnia.IRepositories;
using Newtonsoft.Json;

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

        [HttpGet("[action]")]
        public IActionResult test(Guid id)
        {
            var session = HttpContext.Session;
            cart.AddPositionToCart(session, id);

            var ret = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString("key"));
            return new JsonResult(ret);
        }

        [HttpGet("[action]")]
        public IActionResult Remove(Guid id)
        {
            var session = HttpContext.Session;
            cart.RemovePositionFromCart(session, id);

            var ret = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString("key"));
            return new JsonResult(ret);
        }

        [HttpGet("[action]")]
        public IActionResult test2()
        {
            return new JsonResult(HttpContext.Session.GetString("key"));
        }

        [HttpGet("[action]")]
        public IActionResult test3()
        {
            return new JsonResult(JsonConvert.DeserializeObject<List<CartPosition>>(HttpContext.Session.GetString("key")));
        }
    }
}