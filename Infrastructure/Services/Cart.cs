using Core.Models;
using Infrastructure.IServices;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class Cart : ICart
    {
        private readonly IBookService bookService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public Cart(IBookService bookService, IHttpContextAccessor httpContextAccessor)
        {
            this.bookService = bookService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public List<CartPosition> GetCart(string sessionKey)
        {
            List<CartPosition> cart;
            var session = httpContextAccessor.HttpContext.Session;

            if (session.GetString(sessionKey) == null)
                cart = new List<CartPosition>();
            else
            {
                cart = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString(sessionKey));
            }

            return cart;
        }

        public async Task AddPositionToCart(string sessionKey, Guid bookId)
        {
            var cart = GetCart(sessionKey);
            var position = cart.Find(x => x.Book.BookId == bookId);
            var session = httpContextAccessor.HttpContext.Session;

            if (position != null)
            {
                position.NumberOfBooks++;
            }
            else
            {
                var book = await bookService.ShowBookDetails(bookId);
                cart.Add(new CartPosition()
                {
                    Book = new Book(book.BookId, book.Title, book.Price, book.NumberOfPages,
                        null, book.NumberOfPieces, book.CategoryId),
                    NumberOfBooks = 1,
                    Price = book.Price
                });
            }
            session.SetString(sessionKey, JsonConvert.SerializeObject(cart));
        }

        public void RemovePositionFromCart(string sessionKey, Guid bookId)
        {
            var cart = GetCart(sessionKey);
            var position = cart.Find(x => x.Book.BookId == bookId);
            var session = httpContextAccessor.HttpContext.Session;

            if (position == null)
                return;

            if (position.NumberOfBooks > 1)
                position.NumberOfBooks--;
            else
            {
                cart.Remove(position);
            }
            session.SetString(sessionKey, JsonConvert.SerializeObject(cart));
        }

        public void ClearCart(string sessionKey)
        {
            var session = httpContextAccessor.HttpContext.Session;
            session.SetString(sessionKey, string.Empty);
        }

        public decimal GetPrice(string sessionKey)
        {
            var cart = GetCart(sessionKey);
            decimal price = 0;

            foreach(var book in cart)
            {
                price += (book.Price * book.NumberOfBooks);
            }

            return price;
        }
    }
}
