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
        Book ShowBookDetails(Guid bookId);
        Guid AddTypeCategoryRelation(Guid categoryId, Guid typeId);
        ICategoryRepository CategoryRepository { get; }
        ITypeRepository TypeRepository { get; }
    }
}
