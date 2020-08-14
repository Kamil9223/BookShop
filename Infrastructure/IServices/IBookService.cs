using Core.Models;
using Infrastructure.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.IServices
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
