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

        public User GetUser(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email == email.ToLowerInvariant());
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
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
    }
}
