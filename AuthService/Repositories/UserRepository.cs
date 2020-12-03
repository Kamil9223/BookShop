using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using DatabaseAccess.MSSQL_BookShop;
using Core.Repositories;

namespace AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookShopContext context;

        public UserRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUser(Guid userId)
            => await context.Users.SingleOrDefaultAsync(u => u.UserId == userId);

        public async Task<User> GetUser(string login)
            => await context.Users.SingleOrDefaultAsync(u => u.Login == login);

        public async Task<User> GetUserByEmail(string email)
            => await context.Users.SingleOrDefaultAsync(u => u.Email == email.ToLowerInvariant());

        public async Task<IEnumerable<User>> GetUsers()
            => await context.Users.ToListAsync();

        public async Task<Address> GetAddress(Guid userId)
            => await context.Addresses.SingleOrDefaultAsync(a => a.UserId == userId);

        public async Task AddUser(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            context.Users.Update(user);
        }

        public async Task RemoveUser(Guid userId)
        {
            var user = await GetUser(userId);
            context.Users.Remove(user);
        }

        public async Task AddAddress(Address address)
        {
            await context.Addresses.AddAsync(address);
        }

        public async Task UpdateAddress(Address address)
        {
            context.Addresses.Update(address);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
