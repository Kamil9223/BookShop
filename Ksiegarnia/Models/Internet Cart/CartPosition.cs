using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models.Internet_Cart
{
    public class CartPosition
    {
        public Book Book { get; set; }
        public int NumberOfBooks { get; set; }
        public decimal Price { get; set; }
    }
}
