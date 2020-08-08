using System;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    public interface IOrderService
    {
        Task<Guid> CreateOrder(string login);
        Task RealizeOrder(Guid orderId);
        Task TakeOrder(Guid orderId);
    }
}
