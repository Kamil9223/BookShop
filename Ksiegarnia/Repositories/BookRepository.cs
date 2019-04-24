using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Ksiegarnia.DB;

namespace Ksiegarnia.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookShopContext context;

        public BookRepository(BookShopContext context)
        {
            this.context = context;
        }

        public Book GetBook(Guid bookId)
            => context.Books.SingleOrDefault(b => b.BookId == bookId);

        public Book GetBook(string title)
            => context.Books.SingleOrDefault(b => b.Title == title);

        public IEnumerable<Book> GetBooks()
            => context.Books.ToList();

        public void AddBook(Book book)
        {
            context.Books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            context.Books.Update(book);
        }

        public void RemoveBook(Guid bookId)
        {
            var book = GetBook(bookId);
            context.Books.Remove(book);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
