using Ksiegarnia.IRepositories;
using Ksiegarnia.Models;
using Ksiegarnia.Responses;
using Ksiegarnia.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Testy_Jednostkowe.Services
{
    public class BookServiceTest
    {
        [Fact]
        public async Task GetBooksByType_method_should_invoke_GetBooksByType_method_in_Repository()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var typeRepositoryMock = new Mock<ITypeRepository>();
            var typeCategoryRepositoryMock = new Mock<ITypeCategoryRepository>();

            var bookService = new BookService(bookRepositoryMock.Object, categoryRepositoryMock.Object,
                typeRepositoryMock.Object, typeCategoryRepositoryMock.Object);

            await bookService.GetBooksByType(1, 9, new Guid());

            bookRepositoryMock.Verify(x => x.GetBooksByType(It.IsAny<Guid>(), 1, 9), Times.Once);
        }

        [Fact]
        public async Task ShowBookDetails_method_should_throw_exception_when_book_with_provided_id_doesnt_exist()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var typeRepositoryMock = new Mock<ITypeRepository>();
            var typeCategoryRepositoryMock = new Mock<ITypeCategoryRepository>();

            var bookService = new BookService(bookRepositoryMock.Object, categoryRepositoryMock.Object,
                typeRepositoryMock.Object, typeCategoryRepositoryMock.Object);

            bookRepositoryMock.Setup(x => x.GetBook(It.IsAny<Guid>())).Returns(Task.FromResult<Book>(null));

            Func<Task<BookResponse>> showBookDetails = async () => await bookService.ShowBookDetails(It.IsAny<Guid>());

            await Assert.ThrowsAsync<Exception>(showBookDetails);
        }
    }
}
