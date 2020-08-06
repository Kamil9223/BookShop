using Core.IRepositories;
using Core.Models;
using Infrastructure.Exceptions;
using Infrastructure.IServices;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService
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

            foreach(var cartPosition in userCart)
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
    }
}
