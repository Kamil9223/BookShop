using Ksiegarnia.Models.Internet_Cart;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface ICart
    {
        List<CartPosition> GetCart(ISession session, string sessionKey);
        Task AddPositionToCart(ISession session, string sessionKey, Guid bookId);
        void RemovePositionFromCart(ISession session, string sessionKey, Guid bookId);
        decimal GetPrice(ISession session, string sessionKey);
    }
}
