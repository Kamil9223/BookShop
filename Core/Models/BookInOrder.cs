using System;

namespace Core.Models
{
    public class BookInOrder
    {
        public virtual Guid OrderId { get; protected set; }
        public virtual Guid BookId { get; protected set; }
        public virtual Order Order { get; protected set; }
        public virtual Book Book { get; protected set; }
        public int NumberOfBooks { get; protected set; }

        protected BookInOrder() { }

        public BookInOrder(Guid orderId, Guid bookId, int numberOfBooks, Book book, Order order)
        {
            OrderId = orderId;
            BookId = bookId;
            NumberOfBooks = numberOfBooks;
            Book = book;
            Order = order;
        }
    }
}
