using AuthService.Services.Interfaces;
using CommonLib.Exceptions;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore.Internal;
using OrderService.OrderServices.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderService.OrderServices.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ICart cart;
        private readonly IBooksInOrderRepository booksInOrderRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IHistoryRepository historyRepository;
        private IUserService userService;

        public OrderService(ICart cart, IBooksInOrderRepository booksInOrderRepository,
            IOrderRepository orderRepository, IHistoryRepository historyRepository, IUserService userService)
        {
            this.cart = cart;
            this.booksInOrderRepository = booksInOrderRepository;
            this.orderRepository = orderRepository;
            this.userService = userService;
            this.historyRepository = historyRepository;
        }

        public async Task<Guid> CreateOrder(string login)
        {
            var userCart = cart.GetCart(login);
            if (!userCart.Any())
                throw new NotFoundException(AppErrorCodes.EmptyCart, "Cart in empty");

            var userId = (await userService.Get(login)).UserId;
            var order = new Order(userId);

            foreach (var cartPosition in userCart)
            {
                if (cartPosition.Book.NumberOfPieces <= 0)
                    throw new NotFoundException($"Book '{cartPosition.Book.Title}' is not available");

                order.BooksInOrder.Add(new BookInOrder(
                    order.OrderId,
                    cartPosition.Book.BookId,
                    cartPosition.NumberOfBooks,
                    cartPosition.Book,
                    order));

                cartPosition.Book.DecreaseAmount(1);
            }

            await orderRepository.AddOrder(order);
            cart.ClearCart(login);
            await orderRepository.SaveChanges();

            return order.OrderId;
        }

        public async Task AcceptOrder(Guid orderId)
        {
            var order = await orderRepository.GetOrder(orderId);
            if (order.Status == Status.New)
                order.ChangeStatus(Status.InProgress);

            await orderRepository.SaveChanges();
        }

        public async Task RealizeOrder(Guid orderId)
        {
            var order = await orderRepository.GetOrder(orderId);
            if (order.Status != Status.InProgress)
                return;

            order.ChangeStatus(Status.Realized);

            await historyRepository.Add(new History(
                DateTime.Now,
                0, //TODO
                order.User.Login,
                order.OrderId));

            await orderRepository.SaveChanges();
        }
    }
}
