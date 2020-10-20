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
        private IUserService userService;

        public OrderService(ICart cart, IBooksInOrderRepository booksInOrderRepository,
            IOrderRepository orderRepository, IUserService userService)
        {
            this.cart = cart;
            this.booksInOrderRepository = booksInOrderRepository;
            this.orderRepository = orderRepository;
            this.userService = userService;
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
            }

            await orderRepository.AddOrder(order);
            await orderRepository.SaveChanges();
            cart.ClearCart(login);

            return order.OrderId;
        }

        public async Task RealizeOrder(Guid orderId)
        {

        }

        public async Task TakeOrder(Guid orderId)
        {
            //simple implementation first
            var order = await orderRepository.GetOrder(orderId);

            if (order.Status != Status.New)
                throw new Exception("Wrong status. Can take only new statuses");

            foreach (var bookInOrder in order.BooksInOrder)
            {
                if (bookInOrder.NumberOfBooks > bookInOrder.Book.NumberOfPieces)
                {
                    throw new Exception($"Cannot take order with id = {order.OrderId}. " +
                        $"Not enought books in warehouse.");
                }
                bookInOrder.Book.DecreaseAmount(bookInOrder.NumberOfBooks);
            }

            await orderRepository.SaveChanges();
        }
    }
}
