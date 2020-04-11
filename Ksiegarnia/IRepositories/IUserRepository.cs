using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(Guid userId);
        Task<User> GetUser(string login);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUsers();
        Task<Address> GetAddress(Guid userId);
        Task AddUser(User user);
        Task AddAddress(Address address);
        Task UpdateUser(User user);
        Task UpdateAddress(Address address);
        Task RemoveUser(Guid userId);
        Task SaveChanges();
    }
}
