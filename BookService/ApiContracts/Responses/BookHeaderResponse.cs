using System;

namespace BookService.ApiContracts.Responses
{
    public class BookHeaderResponse
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
    }
}
