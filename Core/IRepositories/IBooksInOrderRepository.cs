using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IBooksInOrderRepository
    {
        Task<BookInOrder> GetBookFromOrder(Guid orderId, Guid bookId);
        Task<List<BookInOrder>> GetBooksFromOrder(Guid orderId);
        Task AddBookToOrder(BookInOrder bookInOrder);
        Task RemoveBookFromOrder(Guid orderId, Guid bookId);
    }
}
