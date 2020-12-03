namespace Core.Models
{
    public class CartPosition
    {
        public Book Book { get; set; }
        public int NumberOfBooks { get; set; }
        public decimal Price { get; set; }
    }
}
