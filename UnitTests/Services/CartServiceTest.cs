using BookService.Services.Interfaces;
using Core.Models;
using Moq;
using Newtonsoft.Json;
using OrderService.Helpers;
using OrderService.OrderServices.Implementations;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Services
{
    public class CartServiceTest
    {
        private Mock<IBookService> bookServiceMock;
        private Mock<IHttpSessionWrapper> sessionWrapperMock;

        public CartServiceTest()
        {
            bookServiceMock = new Mock<IBookService>();
            sessionWrapperMock = new Mock<IHttpSessionWrapper>();
        }

        [Fact]
        public void GetCart_method_should_returns_empty_list_of_cartPositions()
        {
            sessionWrapperMock.Setup(x => x.GetString(It.IsAny<string>())).Returns((string)null);

            var cart = new Cart(bookServiceMock.Object, sessionWrapperMock.Object);

            var result = cart.GetCart("sampleKey");

            Assert.Empty(result);
        }

        [Fact]
        public void GetCart_method_should_returns_appropriate_values()
        {
            sessionWrapperMock.Setup(x => x.GetString(It.IsAny<string>())).Returns( JsonConvert.SerializeObject(
                new List<CartPosition>
                {
                    new CartPosition
                    {
                        Book = null,
                        NumberOfBooks = 10,
                        Price = 199.99M
                    }
                }));

            var cart = new Cart(bookServiceMock.Object, sessionWrapperMock.Object);

            var result = cart.GetCart("sampleKey");

            sessionWrapperMock.Verify(x => x.GetString(It.IsAny<string>()), Times.Exactly(2));

            Assert.NotEmpty(result);
            Assert.Single(result);
            Assert.Null(result.Single().Book);
            Assert.Equal(10, result.Single().NumberOfBooks);
            Assert.Equal(199.99M, result.Single().Price);
        }
    }
}
