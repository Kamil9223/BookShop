using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.AdminRepositories
{
    public interface IAdminBookRepository
    {
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task RemoveBook(Guid bookId);
        Task SaveChanges();
    }
}
