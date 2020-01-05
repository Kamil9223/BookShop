using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Ksiegarnia.DB;

namespace Ksiegarnia.Repositories
{
    public class LoggedUserRepository : ILoggedUserRepository
    {
        private readonly BookShopContext context;

        public LoggedUserRepository(BookShopContext context)
        {
            this.context = context;
        }

        public void AddLoggedUser(LoggedUser loggedUser)
        {
            context.LoggedUsers.Add(loggedUser);
        }

        public LoggedUser GetLoggedUser(Guid refreshToken)
            => context.LoggedUsers.SingleOrDefault(x => x.RefreshToken == refreshToken);

        public LoggedUser GetLoggedUser(string jwtId)
            => context.LoggedUsers.SingleOrDefault(x => x.JwtId == jwtId);

        public void RemoveLoggedUser(Guid loggedUserId)
        {
            var loggedUser = GetLoggedUser(loggedUserId);
            context.LoggedUsers.Remove(loggedUser);
        }

        public void RemoveLoggedUser(string jwtId)
        {
            var loggedUser = GetLoggedUser(jwtId);
            context.LoggedUsers.Remove(loggedUser);
        }

        public void UpdateLoggedUser(LoggedUser loggedUser)
        {
            context.LoggedUsers.Update(loggedUser);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
