using Core.AdminRepositories;
using Core.Models;
using Core.Repositories;
using DatabaseAccess.MSSQL_BookShop;
using System;
using System.Threading.Tasks;

namespace BookService.Repositories.AdminRepositories
{
    public class AdminBookRepository : IAdminBookRepository
    {
        private readonly BookShopContext context;
        private readonly IBookRepository bookRepository;

        public AdminBookRepository(BookShopContext context, IBookRepository bookRepository)
        {
            this.context = context;
            this.bookRepository = bookRepository;
        }

        public async Task AddBook(Book book)
        {
            await context.Books.AddAsync(book);
        }

        public async Task RemoveBook(Guid bookId)
        {
            var book = await bookRepository.GetBook(bookId);
            context.Books.Remove(book);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            context.Update(book);
        }
    }
}
