using BookService.DTO;
using BookService.Services.Interfaces;
using CommonLib.Exceptions;
using Core.Models;
using Moq;
using Newtonsoft.Json;
using OrderService.Helpers;
using OrderService.OrderServices.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Fact]
        public async Task AddPositionToCart_should_invoke_setString_session_method()
        {
            var cart = new Cart(bookServiceMock.Object, sessionWrapperMock.Object);
            var bookGuid = Guid.NewGuid();

            var bookDetails = new BookDetails
            {
                BookId = bookGuid,
                Title = "sampleTitle"
            };

            bookServiceMock.Setup(x => x.ShowBookDetails(bookGuid)).Returns(Task.FromResult(bookDetails));

            await cart.AddPositionToCart("sampleKey", bookGuid);

            sessionWrapperMock.Verify(x => x.SetString("sampleKey", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void RemovePositionFromCart_should_remove_book_from_session_dictionary()
        {
            var cart = new Cart(bookServiceMock.Object, sessionWrapperMock.Object);
            var bookGuid = Guid.NewGuid();

            var bookDetailsList = new List<CartPosition>
            {
                new CartPosition
                {
                    Book = new Book(bookGuid, "", 0, 0, "", 0, Guid.Empty)
                }
            };

            var serialized = JsonConvert.SerializeObject(bookDetailsList);

            sessionWrapperMock.Setup(x => x.GetString("sampleKey")).Returns(serialized);

            cart.RemovePositionFromCart("sampleKey", bookGuid);

            sessionWrapperMock.Verify(x => x.SetString("sampleKey", It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void RemovePositionFromCart_should_do_nothing_because_session_is_empty()
        {
            var cart = new Cart(bookServiceMock.Object, sessionWrapperMock.Object);

            Action action = () => cart.RemovePositionFromCart("sampleKey", Guid.NewGuid());

            Assert.Throws<NotFoundException>(action);
        }
    }
}
