using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.IRepositories;
using DatabaseAccess.MSSQL_BookShop;

namespace BookService.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookShopContext context;

        public BookRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task<Book> GetBook(Guid bookId)
            => await context.Books.Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.BookId == bookId);

        public async Task<Book> GetBook(string title)
            => await context.Books.Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.Title == title);

        public async Task<IEnumerable<Book>> GetBooks(int page, int pageSize)
            => await context.Books.Include(x => x.Category)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        public async Task<IEnumerable<Book>> GetBooksByCategory(Guid categoryId, int page, int pageSize)
            => await context.Books.Include(x => x.Category)
                .Where(x => x.CategoryId == categoryId)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        public async Task<IEnumerable<Book>> GetBooksRandomly(int count)
        {//NIEOPTYMALNE !!!
            Random rand = new Random();
            var books = await context.Books.Include(x => x.Category)
                .OrderBy(x => rand.Next()).Take(count).ToListAsync();
            return books;
        }
    }
}
