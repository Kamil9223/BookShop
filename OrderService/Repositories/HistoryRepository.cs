using Core.Models;
using Core.Repositories;
using DatabaseAccess.MSSQL_BookShop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly BookShopContext bookShopContext;

        public HistoryRepository(BookShopContext bookShopContext)
        {
            this.bookShopContext = bookShopContext;
        }

        public async Task Add(History historyRecord)
        {
            await bookShopContext.History.AddAsync(historyRecord);
        }

        public async Task<History> Get(Guid historyId)
        {
            return await bookShopContext.History.FirstOrDefaultAsync(x => x.HistoryId == historyId);
        }

        public async Task<History> GetByOrderId(Guid orderId)
        {
            return await bookShopContext.History.FirstOrDefaultAsync(x => x.OrderNumber == orderId);
        }

        public async Task SaveChanges()
        {
            await bookShopContext.SaveChangesAsync();
        }
    }
}
