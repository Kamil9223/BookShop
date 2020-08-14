using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Order
    {
        public Guid OrderId { get; protected set; }
        public DateTime Date { get; protected set; }
        public Status Status { get; protected set; }
        public virtual Guid UserId { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual ICollection<BookInOrder> BooksInOrder { get; protected set; }

        protected Order() { }

        public Order(Guid userId)
        {
            UserId = userId;
            OrderId = Guid.NewGuid();
            Date = DateTime.Now;
            Status = Status.New;
        }
    }

    public enum Status
    {
        New,
        Accepted,
        WaitingForDelivery,
        InProgress,
        Realized
    }
}
