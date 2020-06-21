﻿using Core.Models;
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

        public Cart(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public List<CartPosition> GetCart(ISession session, string sessionKey)
        {
            List<CartPosition> cart;

            if (session.GetString(sessionKey) == null)
                cart = new List<CartPosition>();
            else
            {
                cart = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString(sessionKey));
            }

            return cart;
        }

        public async Task AddPositionToCart(ISession session, string sessionKey, Guid bookId)
        {
            var cart = GetCart(session, sessionKey);
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
            session.SetString(sessionKey, JsonConvert.SerializeObject(cart));
        }

        public void RemovePositionFromCart(ISession session, string sessionKey, Guid bookId)
        {
            var cart = GetCart(session, sessionKey);
            var position = cart.Find(x => x.Book.BookId == bookId);

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

        public decimal GetPrice(ISession session, string sessionKey)
        {
            var cart = GetCart(session, sessionKey);
            decimal price = 0;

            foreach(var book in cart)
            {
                price += (book.Price * book.NumberOfBooks);
            }

            return price;
        }
    }
}