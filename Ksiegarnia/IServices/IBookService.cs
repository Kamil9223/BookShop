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
        IEnumerable<BookDTO> GetBooks(int page, int pageSize);
        IEnumerable<BookDTO> GetBooksByType(int page, int pageSize, Guid typeId);
        IEnumerable<BookDTO> GetBooksByTypeAndCategory(int page, int pageSize, Guid typeId, Guid categoryId);
        IEnumerable<BookDTO> GetBooksRandomly(int count);
        IEnumerable<Category> GetCategoriesByType(Guid typeId);
        IEnumerable<Models.Type> GetTypes();
        Models.Type GetType(string typeName);
        Book ShowBookDetails(Guid bookId);
        Guid AddTypeCategoryRelation(Guid categoryId, Guid typeId);
        void AddBook(Book book);
    }
}
