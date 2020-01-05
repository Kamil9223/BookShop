using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Ksiegarnia.DB;
using Microsoft.EntityFrameworkCore;

namespace Ksiegarnia.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly BookShopContext context;

        public TypeRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task<Models.Type> GetType(Guid typeId)
            => await context.Types.SingleOrDefaultAsync(t => t.TypeId == typeId);

        public async Task<Models.Type> GetType(string typeName)
            => await context.Types.SingleOrDefaultAsync(t => t.TypeName == typeName);

        public async Task<IEnumerable<Models.Type>> GetTypes()
            => await context.Types.ToListAsync();

        public async Task AddType(Models.Type type)
        {
            await context.Types.AddAsync(type);
        }

        public async Task UpdateType(Models.Type type)
        {
            context.Types.Update(type);
        }

        public async Task RemoveType(Guid typeId)
        {
            var type = await GetType(typeId);
            context.Types.Remove(type);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
