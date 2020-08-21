using System;

namespace BookService.DTO
{
    public class BookDetails
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int NumberOfPieces { get; set; }
        public int NumberOfPages { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
