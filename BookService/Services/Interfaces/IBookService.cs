using BookService.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookHeader>> GetBooks(int page, int pageSize);
        Task<IEnumerable<BookHeader>> GetBooksByCategory(int page, int pageSize, Guid categoryId);
        Task<IEnumerable<BookHeader>> GetBooksRandomly(int count);
        Task<BookDetails> ShowBookDetails(Guid bookId);
        Task<IEnumerable<CategoryInformations>> GetCategories();
    }
}
