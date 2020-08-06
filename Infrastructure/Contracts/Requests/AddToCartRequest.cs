using System;

namespace Infrastructure.Contracts.Requests
{
    public class AddToCartRequest
    {
        public Guid BookId { get; set; }
    }
}
