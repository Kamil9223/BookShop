using System;

namespace OrderService.Contracts.Requests
{
    public class AddToCartRequest
    {
        public Guid BookId { get; set; }
    }
}
