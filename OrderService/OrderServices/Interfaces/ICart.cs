using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.OrderServices.Interfaces
{
    public interface ICart
    {
        List<CartPosition> GetCart(string sessionKey);
        Task AddPositionToCart(string sessionKey, Guid bookId);
        void RemovePositionFromCart(string sessionKey, Guid bookId);
        decimal GetPrice(string sessionKey);
        void ClearCart(string sessionKey);
    }
}
