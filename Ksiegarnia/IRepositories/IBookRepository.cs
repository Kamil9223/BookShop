using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface IBookRepository
    {
        Book GetBook(Guid bookId);
        Book GetBook(string title);
        IEnumerable<Book> GetBooks(int page, int pageSize);
        IEnumerable<Book> GetBooksByType(Guid typeId, int page, int pageSize);
        IEnumerable<Book> GetBooksByTypeAndCategory(Guid typeId, Guid categoryId, int page, int pageSize);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void RemoveBook(Guid bookId);
        void SaveChanges();
    }
}
