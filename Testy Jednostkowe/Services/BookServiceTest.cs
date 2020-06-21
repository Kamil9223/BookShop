using Core.IRepositories;
using Core.Models;
using Infrastructure.Contracts.Responses;
using Infrastructure.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Testy_Jednostkowe.Services
{
    public class BookServiceTest
    {
        [Fact]
        public async Task GetBooksByCategory_method_should_invoke_GetBooksByCategory_method_from_Repository()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var bookService = new BookService(bookRepositoryMock.Object, categoryRepositoryMock.Object);

            await bookService.GetBooksByCategory(1, 9, new Guid());

            bookRepositoryMock.Verify(x => x.GetBooksByCategory(It.IsAny<Guid>(), 1, 9), Times.Once);
        }

        [Fact]
        public async Task ShowBookDetails_method_should_throw_exception_when_book_with_provided_id_doesnt_exist()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var bookService = new BookService(bookRepositoryMock.Object, categoryRepositoryMock.Object);

            bookRepositoryMock.Setup(x => x.GetBook(It.IsAny<Guid>())).Returns(Task.FromResult<Book>(null));

            Func<Task<BookResponse>> showBookDetails = async () => await bookService.ShowBookDetails(It.IsAny<Guid>());

            await Assert.ThrowsAsync<Exception>(showBookDetails);
        }
    }
}
