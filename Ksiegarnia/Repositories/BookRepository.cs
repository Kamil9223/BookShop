using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Ksiegarnia.DB;
using Microsoft.EntityFrameworkCore;

namespace Ksiegarnia.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookShopContext context;

        public BookRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task<Book> GetBook(Guid bookId)
            => await context.Books.SingleOrDefaultAsync(b => b.BookId == bookId);

        public async Task<Book> GetBook(string title)
            => await context.Books.SingleOrDefaultAsync(b => b.Title == title);

        public async Task<IEnumerable<Book>> GetBooks(int page, int pageSize)
            => await context.Books.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        public async Task<IEnumerable<Book>> GetBooksByType(Guid typeId, int page, int pageSize)
        {
            var ids = await context.TypeCategories.Where(x => x.TypeId == typeId).Select(x => x.TypeCategoryId).ToListAsync();
            var books = await context.Books.Where(x => ids.Contains(x.TypeCategoryId)).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Book>> GetBooksByTypeAndCategory(Guid typeId, Guid categoryId, int page, int pageSize)
        {
            var id = (await context.TypeCategories.SingleOrDefaultAsync(x => x.TypeId == typeId && x.CategoryId == categoryId)).TypeCategoryId;
            var books = await context.Books.Where(x => x.TypeCategoryId == id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Book>> GetBooksRandomly(int count)
        {
            Random rand = new Random();
            var books = await context.Books.OrderBy(x => rand.Next()).Take(count).ToListAsync();
            return books;
        }

        public async Task AddBook(Book book)
        {
            await context.Books.AddAsync(book);
        }

        public async Task UpdateBook(Book book)
        {
            context.Books.Update(book);
        }

        public async Task RemoveBook(Guid bookId)
        {
            var book = await GetBook(bookId);
            context.Books.Remove(book);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
