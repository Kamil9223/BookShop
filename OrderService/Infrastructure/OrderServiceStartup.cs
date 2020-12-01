using Core.Repositories;
using OrderService.OrderServices.Implementations;
using OrderService.OrderServices.Interfaces;
using OrderService.Repositories;
using Unity;

namespace OrderService.Infrastructure
{
    public class OrderServiceStartup
    {
        public static void RegisterServices(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IHistoryRepository, HistoryRepository>();
            unityContainer.RegisterType<IBooksInOrderRepository, BooksInOrderRepository>();
            unityContainer.RegisterType<IOrderRepository, OrderRepository>();
            unityContainer.RegisterType<ICart, Cart>();
            unityContainer.RegisterType<IOrderService, OrderServices.Implementations.OrderService>();
        }
    }
}
