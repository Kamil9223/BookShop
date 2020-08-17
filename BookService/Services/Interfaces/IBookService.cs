using BookService.ApiContracts.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookHeaderResponse>> GetBooks(int page, int pageSize);
        Task<IEnumerable<BookHeaderResponse>> GetBooksByCategory(int page, int pageSize, Guid categoryId);
        Task<IEnumerable<BookHeaderResponse>> GetBooksRandomly(int count);
        Task<BookResponse> ShowBookDetails(Guid bookId);
        Task<IEnumerable<CategoryResponse>> GetCategories();
    }
}
