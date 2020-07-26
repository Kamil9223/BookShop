using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(Guid orderId);
        Task<List<Order>> GetUserOrder(Guid userId);
        Task AddOrder(Order order);
        Task RemoveOrder(Guid orderId);
        Task SaveChanges();
    }
}
