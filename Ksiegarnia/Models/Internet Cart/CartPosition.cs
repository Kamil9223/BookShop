using Ksiegarnia.Responses;

namespace Ksiegarnia.Models.Internet_Cart
{
    public class CartPosition
    {
        public BookResponse Book { get; set; }
        public int NumberOfBooks { get; set; }
        public decimal Price { get; set; }
    }
}
