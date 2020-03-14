using Ksiegarnia.Models;
using Ksiegarnia.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponse>> GetBooks(int page, int pageSize);
        Task<IEnumerable<BookResponse>> GetBooksByType(int page, int pageSize, Guid typeId);
        Task<IEnumerable<BookResponse>> GetBooksByTypeAndCategory(int page, int pageSize, Guid typeId, Guid categoryId);
        Task<IEnumerable<BookResponse>> GetBooksRandomly(int count);
        Task<IEnumerable<Category>> GetCategoriesByType(Guid typeId);
        Task<IEnumerable<Models.Type>> GetTypes();
        Task<Models.Type> GetType(Guid typeId);
        Task<BookResponse> ShowBookDetails(Guid bookId);
        Task<Guid> AddTypeCategoryRelation(Guid categoryId, Guid typeId);
        Task AddBook(Book book);
    }
}
