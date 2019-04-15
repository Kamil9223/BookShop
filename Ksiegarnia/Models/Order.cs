﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public Guid OrderId { get; protected set; }
        public DateTime Date { get; protected set; }
        public Status Status { get; protected set; }
        [ForeignKey("User")]
        public Guid UserId { get; protected set; }
        public User User { get; protected set; }
        public IEnumerable<BookInOrder> BooksInOrder { get; protected set; }

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
