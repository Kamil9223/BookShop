using System;
using System.Collections.Generic;
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

    }
}
