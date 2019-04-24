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
        User GetUser(string login);
        User GetUserByEmail(string email);
        IEnumerable<User> GetUsers();
        Address GetAddress(Guid userId);
        void AddUser(User user);
        void AddAddress(Address address);
        void UpdateUser(User user);
        void UpdateAddress(Address address);
        void RemoveUser(Guid userId);
        void SaveChanges();
    }
}
