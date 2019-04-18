using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface IUserRepository
    {
        User GetUser(Guid userId);
        User GetUser(string email);
        IEnumerable<User> GetUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(Guid userId);
    }
}
