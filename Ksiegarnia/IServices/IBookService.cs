using Ksiegarnia.DTO;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetBooks(int page, int pageSize);
        Task<IEnumerable<BookDTO>> GetBooksByType(int page, int pageSize, Guid typeId);
        Task<IEnumerable<BookDTO>> GetBooksByTypeAndCategory(int page, int pageSize, Guid typeId, Guid categoryId);
        Task<IEnumerable<BookDTO>> GetBooksRandomly(int count);
        Task<IEnumerable<Category>> GetCategoriesByType(Guid typeId);
        Task<IEnumerable<Models.Type>> GetTypes();
        Task<Models.Type> GetType(Guid typeId);
        Task<Book> ShowBookDetails(Guid bookId);
        Task<Guid> AddTypeCategoryRelation(Guid categoryId, Guid typeId);
        Task AddBook(Book book);
    }
}
