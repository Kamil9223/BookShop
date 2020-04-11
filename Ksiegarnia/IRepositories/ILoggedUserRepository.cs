using Ksiegarnia.Models;
using System;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface ILoggedUserRepository
    {
        Task AddLoggedUser(LoggedUser loggedUser);
        Task <LoggedUser> GetLoggedUser(Guid loggedUserId);
        Task <LoggedUser> GetLoggedUser(string jwtId);
        Task RemoveLoggedUser(Guid loggedUserId);
        Task RemoveLoggedUser(string jwtId);
        Task UpdateLoggedUser(LoggedUser loggedUser);
        Task SaveChanges();
    }
}
