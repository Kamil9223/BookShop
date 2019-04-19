using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    interface IBookRepository
    {
        Book GetBook(Guid bookId);
        Book GetBook(string title);
        IEnumerable<Book> GetBooks();
        void AddBook(Book book);
        void UpdateBook(Book book);
        void RemoveBook(Guid bookId);
    }
}
