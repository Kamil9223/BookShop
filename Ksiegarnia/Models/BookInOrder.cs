using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    [Table("BooksInOrder")]
    public class BookInOrder
    {
        public Guid OrderId { get; protected set; }
        public Guid BookId { get; protected set; }
        public Order Order { get; protected set; }
        public Book Book { get; protected set; }
        public int NumberOfBooks { get; protected set; }

        protected BookInOrder() { }

        public BookInOrder(Guid orderId, Guid bookId, int numberOfBooks)
        {
            OrderId = orderId;
            BookId = bookId;
            NumberOfBooks = numberOfBooks;
        }
    }
}
