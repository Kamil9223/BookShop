using System;
using System.Threading.Tasks;

namespace OrderService.OrderServices.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrder(string login);
        Task RealizeOrder(Guid orderId);
        Task TakeOrder(Guid orderId);
    }
}
