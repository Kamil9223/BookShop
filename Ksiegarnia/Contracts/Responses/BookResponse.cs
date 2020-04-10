using Ksiegarnia.Models;
using System;

namespace Ksiegarnia.Responses
{
    public class BookResponse
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public Guid TypeCategoryId { get; set; }
    }
}
