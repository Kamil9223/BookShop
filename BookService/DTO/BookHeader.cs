using System;

namespace BookService.DTO
{
    public class BookHeader
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
    }
}
