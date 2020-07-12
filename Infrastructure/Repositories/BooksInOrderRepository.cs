using Core.Models;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BooksInOrderRepository
    {
        private readonly BookShopContext context;

        public BooksInOrderRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task<BookInOrder> GetBookFromOrder(Guid orderId, Guid bookId)
            => await context.BooksInOrder.Include(x => x.Order)
                .SingleOrDefaultAsync(x => x.OrderId == orderId && x.BookId == bookId);

        public async Task<List<BookInOrder>> GetBooksFromOrder(Guid orderId)
            => await context.BooksInOrder.Include(x => x.Order)
                .Where(x => x.OrderId == orderId).ToListAsync();

        public async Task AddBookToOrder(BookInOrder bookInOrder)
        {
            await context.AddAsync(bookInOrder);
        }

        public async Task RemoveBookFromOrder(Guid orderId, Guid bookId)
        {
            var bookInOrder = await GetBookFromOrder(orderId, bookId);
            context.Remove(bookInOrder);
        }
    }
}
