using Core.Models;
using Infrastructure.Helpers;
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
        private readonly IHttpSessionWrapper sessionWrapper;

        public Cart(IBookService bookService, IHttpSessionWrapper sessionWrapper)
        {
            this.bookService = bookService;
            this.sessionWrapper = sessionWrapper;
        }

        public List<CartPosition> GetCart(string sessionKey)
        {
            List<CartPosition> cart;

            if (sessionWrapper.GetString(sessionKey) == null)
                cart = new List<CartPosition>();
            else
            {
                cart = JsonConvert.DeserializeObject<List<CartPosition>>(sessionWrapper.GetString(sessionKey));
            }

            return cart;
        }

        public async Task AddPositionToCart(string sessionKey, Guid bookId)
        {
            var cart = GetCart(sessionKey);
            var position = cart.Find(x => x.Book.BookId == bookId);

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
            sessionWrapper.SetString(sessionKey, JsonConvert.SerializeObject(cart));
        }

        public void RemovePositionFromCart(string sessionKey, Guid bookId)
        {
            var cart = GetCart(sessionKey);
            var position = cart.Find(x => x.Book.BookId == bookId);

            if (position == null)
                return;

            if (position.NumberOfBooks > 1)
                position.NumberOfBooks--;
            else
            {
                cart.Remove(position);
            }
            sessionWrapper.SetString(sessionKey, JsonConvert.SerializeObject(cart));
        }

        public void ClearCart(string sessionKey)
        {
            var emptyList = JsonConvert.SerializeObject(new List<CartPosition>());
            sessionWrapper.SetString(sessionKey, emptyList);
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
