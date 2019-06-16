using Ksiegarnia.IRepositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models.Internet_Cart
{
    public class Cart
    {
        private readonly IBookRepository bookRepository;

        public Cart(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public List<CartPosition> GetCart(ISession session)
        {
            List<CartPosition> cart;

            if (session.GetString("key") == null)
                cart = new List<CartPosition>();
            else
            {
                cart = JsonConvert.DeserializeObject<List<CartPosition>>(session.GetString("key"));
            }

            return cart;
        }

        public void AddPositionToCart(ISession session, Guid bookId)
        {
            var cart = GetCart(session);
            var position = cart.Find(x => x.Book.BookId == bookId);

            if (position != null)
            {
                position.NumberOfBooks++;
            }
            else
            {
                var book = bookRepository.GetBook(bookId);
                cart.Add(new CartPosition()
                {
                    Book = book,
                    NumberOfBooks = 1,
                    Price = book.Price
                });
            }
            session.SetString("key", JsonConvert.SerializeObject(cart));
        }

        public string test()
        {
            var t = bookRepository.GetBook("Potop");
            return t.ToString();
        }

        public void RemovePositionFromCart(ISession session, Guid bookId)
        {
            var cart = GetCart(session);
            var position = cart.Find(x => x.Book.BookId == bookId);

            if (position == null)
                return;

            if (position.NumberOfBooks > 1)
                position.NumberOfBooks--;
            else
            {
                cart.Remove(position);
            }
            session.SetString("key", JsonConvert.SerializeObject(cart));
        }
    }
}
