using System;

namespace BookService.ApiContracts.Responses
{
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
