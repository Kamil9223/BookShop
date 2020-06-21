﻿using System;
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
        public virtual IEnumerable<BookInOrder> BooksInOrder { get; protected set; }

        protected Order() { }

        public Order(Status status)
        {
            OrderId = Guid.NewGuid();
            Date = DateTime.UtcNow;
            Status = status;
        }
    }

    public enum Status
    {
        New = 0,
        Realized = 1
    }
}