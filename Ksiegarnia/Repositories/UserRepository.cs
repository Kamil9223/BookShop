using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Ksiegarnia.DB;

namespace Ksiegarnia.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookShopContext context;

        public UserRepository(BookShopContext context)
        {
            this.context = context;
        }

        public User GetUser(Guid userId)
        {
            return context.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public User GetUser(string login)
        {
            return context.Users.SingleOrDefault(u => u.Login == login);
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email == email.ToLowerInvariant());
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public Address GetAddress(Guid userId)
        {
            return context.Addresses.SingleOrDefault(a => a.UserId == userId);
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public void RemoveUser(Guid userId)
        {
            var user = GetUser(userId);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public void AddAddress(Address address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();
        }

        public void UpdateAddress(Address address)
        {
            context.Addresses.Update(address);
            context.SaveChanges();
        }
    }
}
