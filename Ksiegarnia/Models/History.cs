using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    public class History
    {
        public Guid HistoryId { get; protected set; }
        public DateTime Date { get; protected set; }
        public decimal Price { get; protected set; }
        public string ClientLogin { get; protected set; }
        public Guid OrderNumber { get; protected set; }

        protected History() { }

        public History(DateTime date, decimal price, string clientLogin, Guid orderNumber)
        {
            HistoryId = Guid.NewGuid();
            Date = date;
            Price = price;
            ClientLogin = clientLogin;
            OrderNumber = orderNumber;
        }
    }
}
