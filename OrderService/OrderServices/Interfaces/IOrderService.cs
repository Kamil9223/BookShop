using System;
using System.Threading.Tasks;

namespace OrderService.OrderServices.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrder(string login);
        Task AcceptOrder(Guid orderId);
        Task RealizeOrder(Guid orderId);
    }
}
