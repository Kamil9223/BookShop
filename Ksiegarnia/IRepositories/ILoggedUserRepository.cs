using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface ILoggedUserRepository
    {
        void AddLoggedUser(LoggedUser loggedUser);
        LoggedUser GetLoggedUser(Guid loggedUserId);
        LoggedUser GetLoggedUser(string jwtId);
        void RemoveLoggedUser(Guid loggedUserId);
        void RemoveLoggedUser(string jwtId);
        void UpdateLoggedUser(LoggedUser loggedUser);
        void SaveChanges();
    }
}
