using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using DatabaseAccess.MSSQL_BookShop;
using Core.Repositories;

namespace AuthService.Repositories
{
    public class LoggedUserRepository : ILoggedUserRepository
    {
        private readonly BookShopContext context;

        public LoggedUserRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task AddLoggedUser(LoggedUser loggedUser)
        {
            await context.LoggedUsers.AddAsync(loggedUser);
        }

        public async Task<LoggedUser> GetLoggedUser(Guid refreshToken)
            => await context.LoggedUsers.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);

        public async Task<LoggedUser> GetLoggedUser(string jwtId)
            => await context.LoggedUsers.SingleOrDefaultAsync(x => x.JwtId == jwtId);

        public async Task RemoveLoggedUser(Guid loggedUserId)
        {
            var loggedUser = await GetLoggedUser(loggedUserId);
            context.LoggedUsers.Remove(loggedUser);
        }

        public async Task RemoveLoggedUser(string jwtId)
        {
            var loggedUser = await GetLoggedUser(jwtId);
            context.LoggedUsers.Remove(loggedUser);
        }

        public async Task UpdateLoggedUser(LoggedUser loggedUser)
        {
            context.LoggedUsers.Update(loggedUser);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
