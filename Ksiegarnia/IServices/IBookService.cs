using Ksiegarnia.Contracts.Responses;
using Ksiegarnia.Models;
using Ksiegarnia.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<BookHeaderResponse>> GetBooks(int page, int pageSize);
        Task<IEnumerable<BookHeaderResponse>> GetBooksByCategory(int page, int pageSize, Guid categoryId);
        Task<IEnumerable<BookHeaderResponse>> GetBooksRandomly(int count);
        Task<BookResponse> ShowBookDetails(Guid bookId);
        Task AddBook(Book book);
        Task<IEnumerable<CategoryResponse>> GetCategories();
    }
}
