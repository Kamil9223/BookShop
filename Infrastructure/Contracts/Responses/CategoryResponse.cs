using System;

namespace Infrastructure.Contracts.Responses
{
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
