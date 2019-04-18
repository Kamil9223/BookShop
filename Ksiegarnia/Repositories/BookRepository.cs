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
        {
            return context.Books.SingleOrDefault(b => b.BookId == bookId);
        }

        public Book GetBook(string title)
        {
            return context.Books.SingleOrDefault(b => b.Title == title);
        }

        public IEnumerable<Book> GetBooks()
        {
            return context.Books.ToList();
        }

        public void AddBook(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            context.Books.Update(book);
            context.SaveChanges();
        }

        public void RemoveBook(Guid bookId)
        {
            var book = GetBook(bookId);
            context.Books.Remove(book);
            context.SaveChanges();
        }
    }
}
