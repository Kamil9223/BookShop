using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IHistoryRepository
    {
        Task Add(History historyRecord);
        Task<History> Get(Guid historyId);
        Task<History> GetByOrderId(Guid orderId);
        Task SaveChanges();
    }
}
