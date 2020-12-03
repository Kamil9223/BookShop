using AuthService.DTO;
using AuthService.Services.Interfaces;
using CommonLib.Exceptions;
using Core.Models;
using Core.Repositories;
using Moq;
using OrderService.OrderServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    public class OrderServiceTest : BookServiceBaseTest
    {
        private readonly Mock<ICart> cartMock;
        private readonly Mock<IOrderRepository> orderRepositoryMock;
        private readonly Mock<IHistoryRepository> historyRepositoryMock;
        private readonly Mock<IUserService> userServiceMock;

        public OrderServiceTest()
        {
            cartMock = new Mock<ICart>();
            orderRepositoryMock = new Mock<IOrderRepository>();
            historyRepositoryMock = new Mock<IHistoryRepository>();
            userServiceMock = new Mock<IUserService>();
        }

        private OrderService.OrderServices.Implementations.OrderService CreateOrderService()
            => new OrderService.OrderServices.Implementations.OrderService(
                cartMock.Object,
                orderRepositoryMock.Object,
                historyRepositoryMock.Object,
                userServiceMock.Object);

        [Fact]
        public async Task CreteOrder_should_throw_exception_when_cart_is_empty()
        {
            var orderService = CreateOrderService();
            cartMock.Setup(x => x.GetCart(It.IsAny<string>())).Returns(new List<CartPosition>());

            Func<Task<Guid>> action = async () => await orderService.CreateOrder("sampleLogin");

            await Assert.ThrowsAsync<NotFoundException>(action);
        }

        [Fact]
        public async Task CreateOrder_should_pass_correctly()
        {
            var orderService = CreateOrderService();

            cartMock.Setup(x => x.GetCart(It.IsAny<string>())).Returns(CorrectCartPositions());
            userServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new UserInformations
            {
                UserId = Guid.NewGuid()
            }));

            await orderService.CreateOrder("sampleLogin");

            orderRepositoryMock.Verify(x => x.AddOrder(It.IsAny<Order>()), Times.Once);
            cartMock.Verify(x => x.ClearCart(It.IsAny<string>()), Times.Once);
            orderRepositoryMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task CreateOrder_should_failed_when_order_contain_too_much_pieces_of_books()
        {
            var orderService = CreateOrderService();

            cartMock.Setup(x => x.GetCart(It.IsAny<string>())).Returns(FailedCartPositions());
            userServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new UserInformations
            {
                UserId = Guid.NewGuid()
            }));

            Func<Task<Guid>> action = async () => await orderService.CreateOrder("sampleLogin");

            await Assert.ThrowsAsync<NotFoundException>(action);
        }

        [Fact]
        public async Task AcceptOrder_should_set_order_status_to_inProgress()
        {
            var orderService = CreateOrderService();
            var order = new Order(Guid.NewGuid());

            orderRepositoryMock.Setup(x => x.GetOrder(It.IsAny<Guid>())).Returns(Task.FromResult(order));

            await orderService.AcceptOrder(Guid.NewGuid());

            Assert.Equal(Status.InProgress, order.Status);
        }
    }
}
