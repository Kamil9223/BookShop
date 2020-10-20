using Core.Models;
using Core.Repositories;
using DatabaseAccess.MSSQL_BookShop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookShopContext context;

        public OrderRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task<Order> GetOrder(Guid orderId)
            => await context.Orders.Include(x => x.BooksInOrder)
                .SingleOrDefaultAsync(x => x.OrderId == orderId);

        public async Task<List<Order>> GetUserOrder(Guid userId)
            => await context.Orders.Include(x => x.User)
                .Where(x => x.UserId == userId).ToListAsync();

        public async Task AddOrder(Order order)
        {
            await context.Orders.AddAsync(order);
        }

        public async Task RemoveOrder(Guid orderId)
        {
            var order = await GetOrder(orderId);
            context.Remove(order);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
